namespace MyUniversity.Contracts.Services
{
    public class ModificationServiceResult
    {
        public ModificationServiceResult(object value)
        {
            Type = ResultType.Success;
            Value = value;
        }

        public ModificationServiceResult()
        {
            Type = ResultType.Success;
        }

        public ResultType Type { get; set; }
        public object Value { get; set; }
    }

    public enum ResultType
    {
        Success,
        DataException,
        DbUpdateConcurrencyException
    }
}
