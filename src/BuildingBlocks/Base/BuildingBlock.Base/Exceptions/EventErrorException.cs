using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{
    [Serializable]
    public class EventErrorException : Exception
    {
        public EventErrorException()
            : base($"Event got error !")
        {
        }

        public EventErrorException(string message)
            : base($"Event error message : {message}")
        {
        }
        public EventErrorException(string message,string eventType)
            : base($"Event error message : {message} and event : {eventType}")
        {
        }

        public EventErrorException(string message, Exception inner)
            : base($"Event error message: {message}", inner)
        {

        }

        protected EventErrorException(
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
