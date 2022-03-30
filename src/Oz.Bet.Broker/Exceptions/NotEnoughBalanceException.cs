using Oz.Bet.Exceptions;
using System.Runtime.Serialization;

namespace Oz.Bet.Broker.Exceptions
{
    [Serializable]
    public class NotEnoughBalanceException : DomainException
    {
        public NotEnoughBalanceException()
        {
        }

        public NotEnoughBalanceException(string? message) : base(message)
        {
        }

        public NotEnoughBalanceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotEnoughBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
