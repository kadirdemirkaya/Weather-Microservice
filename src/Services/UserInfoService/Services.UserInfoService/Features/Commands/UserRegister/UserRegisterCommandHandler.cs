using AutoMapper;
using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Extensions;
using Elastic.CommonSchema;
using MediatR;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.Enums;
using Services.UserInfoService.Aggregates.ValueObjects;
using Services.UserInfoService.Configurations.Configs;

namespace Services.UserInfoService.Features.Commands.UserRegister
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, UserRegisterCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserRegisterCommandResponse> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            request.userRegisterDto.Password = PasswordHashExtension.StringHashingEncrypt(request.userRegisterDto.Password, GetConfigs.GetEncryptionKey());
            bool response = await _unitOfWork.GetReadRepository<Aggregates.User, UserId>().AnyAsync(u => u.Email == request.userRegisterDto.Email);
            if (response is false)
            {
                try
                {
                    var role = await _unitOfWork.GetReadRepository<Role, RoleId>().GetAsync(r => r.RoleEnum == Aggregates.Enums.RoleEnum.User);
                    var user = _mapper.Map<Aggregates.User>(request.userRegisterDto);
                    user.Id = UserId.CreateUnique();
                    user.UpdatedDate = DateTime.UtcNow;
                    user.AddUserRole(role.Id, user.Id, true, UserRoleStatus.Active);
                    response = await _unitOfWork.GetWriteRepository<Aggregates.User, UserId>().CreateAsync(user);
                    return response is true ? new(await _unitOfWork.SaveChangesAsync() > 0) : new(false);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error($"{nameof(UserRegisterCommandHandler)} class error : " + ex.Message);
                    throw new ServiceErrorException(nameof(UserRegisterCommandHandler), ex.Message);
                }
            }
            return new(false);
        }

        static string GetEnumName(Enum enumValue)
            => enumValue.ToString();

        static int GetEnumIndex(Enum enumValue)
            => Convert.ToInt32(enumValue);
    }
}
