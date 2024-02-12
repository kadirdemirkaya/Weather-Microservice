using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{
    [Serializable]
    public class ServiceErrorException : Exception
    {
        public string Servicename { get; }
        public string Methodname { get; set; }
        public ServiceErrorException()
            : base($"Service got error !")
        {
        }

        public ServiceErrorException(string servicename)
            : base($"Service Name : '{servicename}' got error !")
        {
            Servicename = servicename;
        }

        public ServiceErrorException(string servicename, string message)
            : base($"Service Name : {servicename} - Error : {message}")
        {
            Servicename = servicename;
        }

        public ServiceErrorException(string servicename, string message, string methodname)
            : base($"Service Name : {servicename} - Method : {methodname} - Error : {message}")
        {
            Servicename = servicename;
            Methodname = methodname;
        }

        public ServiceErrorException(string servicename, string methodname, string message, Exception inner)
            : base(message, inner)
        {
            Servicename = servicename;
            Methodname = methodname;
        }

        protected ServiceErrorException(
          SerializationInfo info,
          StreamingContext context) : base(info, context)
        {
            Servicename = info.GetString("Servicename")!;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Servicename", Servicename);
        }
    }
}
