namespace CORE.Account.Exception
{
    [Serializable]
    public class NotFoundException : IOException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

    }
}