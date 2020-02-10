using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server.Models
{
    public class DataUpLoadException : Exception
    {
        public DataUpLoadException()
        {
        }

        public DataUpLoadException(string message)
            : base(message)
        {
        }

        public DataUpLoadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
