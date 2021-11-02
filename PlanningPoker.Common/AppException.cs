using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace PlanningPoker.Common
{
    [Serializable]
    public class AppException : Exception
    {
        protected AppException(SerializationInfo info, StreamingContext context) : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

