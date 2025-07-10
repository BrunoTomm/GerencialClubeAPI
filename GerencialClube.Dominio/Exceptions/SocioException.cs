using System;

namespace GerencialClube.Dominio.Exceptions
{
    public class SocioException : Exception
    {
        public SocioException() { }

        public SocioException(string message) : base(message) { }

        public SocioException(string message, Exception innerException) : base(message, innerException) { }
    }
}
