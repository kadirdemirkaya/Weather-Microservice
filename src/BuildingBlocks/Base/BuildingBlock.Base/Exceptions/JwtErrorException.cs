using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{
    [Serializable]
    public class JwtErrorException : Exception
    {

        public JwtErrorException()
            : base($"Jwt error !")
        {
        }

        public JwtErrorException(string message)
            : base($"Jwt error : {message}")
        {

        }

        public JwtErrorException(string message, Exception inner)
            : base($"Jwt error : {message}", inner)
        {

        }

        protected JwtErrorException(
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
