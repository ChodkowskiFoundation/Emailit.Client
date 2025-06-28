namespace Emailit.Client.Responses
{
    public interface IEmailitResult
    {
        public string Message { get; set; }

        public bool Notify { get; set; }

        public bool Success { get; set; }
    }
}
