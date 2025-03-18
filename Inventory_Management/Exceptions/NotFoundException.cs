using System.Security.Cryptography.X509Certificates;

namespace DigitalBookstoreManagement.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
       
    }
}
