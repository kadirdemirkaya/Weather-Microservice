using System.Runtime.Serialization;

namespace BuildingBlock.Base.Exceptions
{
    [Serializable]
    public class DatabaseErrorException : Exception
    {
        public string DbName { get; } = string.Empty;

        public DatabaseErrorException()
            : base($"Database got error !")
        {
        }

        public DatabaseErrorException(string message, string dbName)
            : base($"Database type: {dbName} - Error : {message}")
        {
            DbName = DbName;
        }

        public DatabaseErrorException(string message, string dbName, Exception inner)
            : base($"Database type: {dbName} - Error : {message}", inner)
        {

        }

        protected DatabaseErrorException(
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
