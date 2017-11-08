namespace TogglesWebApi.Results
{
    public class ErrorResult
    {
        public string Message { get; set; }

        public ErrorResult(string message)
        {
            this.Message = message;
        }
    }
}
