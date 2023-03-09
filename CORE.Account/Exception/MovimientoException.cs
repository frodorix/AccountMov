using System.Runtime.Serialization;

namespace CORE.Account.Exception
{
    [Serializable]
    public class MovimientoException : IOException
    {
        public MovimientoException()
        {
        }

        public MovimientoException(string? message) : base(message)
        {
        }

        
    }
}