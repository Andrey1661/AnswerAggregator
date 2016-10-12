namespace BL.Enviroment
{
    public class MessageSendResult : OperationResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string AdditionalInfo { get; set; }

        public MessageSendResult(bool success) : base(success)
        {

        }
    }
}
