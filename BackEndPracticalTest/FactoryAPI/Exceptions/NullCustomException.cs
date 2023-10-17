namespace FactoryAPI.Exceptions
{
    public class NullCustomException : Exception
    {
        public NullCustomException(string internalMessage) : base(internalMessage){ }

        //public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public string ResponseTitle => "Invalid arguments to the api";
        public string InternalMessage { get; }


    }
}
