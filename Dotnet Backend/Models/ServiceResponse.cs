using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Backend.Models
{
    public class ServiceResponse<T>
    {
        public bool Successfull { get; set; }
        public T? Body { get; set; }
        public String Message { get; set; } = "Success";
    }
}