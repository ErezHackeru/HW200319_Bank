using System;
using System.Runtime.Serialization;

namespace HW2003_Bank
{
    [Serializable]
    public class BalanceEception : Exception
    {
        public BalanceEception()
        {
        }

        public BalanceEception(string message) : base(message)
        {
        }

        public BalanceEception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BalanceEception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}