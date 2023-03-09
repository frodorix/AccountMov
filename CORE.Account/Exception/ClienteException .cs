
namespace CORE.Account.Exception
{
    [Serializable]
    public class ClienteException : IOException
    {
        public ClienteException()
        {
        }

        public ClienteException(string? message) : base(message)
        {
        }


    }
}