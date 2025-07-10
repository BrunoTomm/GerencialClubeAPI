using System;

namespace GerencialClube.Dominio.Exceptions
{
    public class PlanoException : Exception
    {
        public PlanoException() { }

        public PlanoException(string message)
            : base(message) { }

        public PlanoException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
