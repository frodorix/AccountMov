using System.Runtime.Serialization;

namespace CORE.Account.Exception
{
    [Serializable]
    public class CuentaException : IOException
    {
        public CuentaException()
        {
        }

        public CuentaException(string? message) : base(message)
        {
        }

   
    }
}