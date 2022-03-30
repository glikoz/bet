namespace Oz.Bet
{
    public class ServiceResult<TResult>
    {
        public TResult Result { get; set; }
        public string Exception { get; set; }

        public bool Success => Exception == null;
    }
}
