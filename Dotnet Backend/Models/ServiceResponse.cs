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
        /// <summary>
        /// True if the Response executed as expected, False if an exception ocurred
        /// </summary>
        public bool Successfull { get; set; } = true;
        /// <summary>
        /// The object expected by the user in the response. Null if an exception ocurrs
        /// </summary>
        public T? Body { get; set; }
        /// <summary>
        /// Aditional Information relevant to the state of the response. If an exception occurrs, the exception message can be copied here
        /// </summary>
        public String Message { get; set; } = "Success";
    }
}