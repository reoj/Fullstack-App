using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetBackend.Models
{
    /// <summary>
    /// Simplifies the exchange of data of a particular type of entity
    /// </summary>
    /// <typeparam name="T">The type of Entity expected in the response body</typeparam>
    public class ServiceResponse<T>
    {
        public bool Successfull { get; set; } = true;
        public T? Body { get; set; }
        public String Message { get; set; } = "Success";
    }
}