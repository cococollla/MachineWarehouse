using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace MachineWarehouse.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
