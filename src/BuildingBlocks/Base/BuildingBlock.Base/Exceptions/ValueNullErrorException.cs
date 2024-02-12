using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{
    [Serializable]
    public class ValueNullErrorException : Exception
    {
        public string VeriableName { get; }

        public ValueNullErrorException()
            : base($"Veriable not null error !")
        {
        }

        public ValueNullErrorException(string veriableName)
            : base($"Veriable : '{veriableName}' null error !")
        {
            VeriableName = veriableName;
        }

        public ValueNullErrorException(string veriableName, string message)
            : base($"Veriable : {veriableName} - Error : {message}")
        {
            VeriableName = veriableName;
        }

        public ValueNullErrorException(string veriableName, string message, Exception inner)
            : base(message, inner)
        {
            VeriableName = veriableName;
        }

        protected ValueNullErrorException(
          SerializationInfo info,
          StreamingContext context) : base(info, context)
        {
            VeriableName = info.GetString("veriableName")!;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("veriableName", VeriableName);
        }
    }
}
