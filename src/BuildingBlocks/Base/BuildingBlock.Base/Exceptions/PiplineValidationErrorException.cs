using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{

    [Serializable]
    public class PiplineValidationErrorException : Exception
    {

        public PiplineValidationErrorException()
            : base($"Validation pipline error !")
        {
        }

        public PiplineValidationErrorException(string message)
            : base($"Pipline validation error : {message}")
        {

        }

        public PiplineValidationErrorException(string message, Exception inner)
            : base($"Pipline validation error : {message}", inner)
        {

        }

        protected PiplineValidationErrorException(
          SerializationInfo info,
          StreamingContext context) : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
