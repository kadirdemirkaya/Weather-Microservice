using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{
    [Serializable]
    public class EmailErrorException : Exception
    {
        public EmailErrorException()
            : base($"Email got error !")
        {
        }

        public EmailErrorException(string message)
            : base($"Email error message : {message}")
        {
        }

        public EmailErrorException(string message, Exception inner)
            : base($"Email error message: {message}", inner)
        {

        }

        protected EmailErrorException(
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
