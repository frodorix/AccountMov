using System.Runtime.Serialization;

namespace CORE.Account.Exception
{
    [Serializable]
    internal class CuentaException : IOException
    {
        public CuentaException()
        {
        }

        public CuentaException(string? message) : base(message)
        {
        }

   
    }
}