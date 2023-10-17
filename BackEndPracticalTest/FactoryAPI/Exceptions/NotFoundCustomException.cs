
namespace FactoryAPI.Exceptions
{
    public class NotFoundCustomException : Exception
    {
        public NotFoundCustomException(string internalMessage) : base(internalMessage)
        {
         
        }
        //public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public string ResponseTitle => "Invalid arguments to the api";
        public string InternalMessage { get; }

        
    }
}
