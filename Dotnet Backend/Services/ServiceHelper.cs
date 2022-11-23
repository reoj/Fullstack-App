using DotnetBackend.Models;
using System.Security.Cryptography.X509Certificates;

namespace DotnetBackend.Services
{
    public abstract class ServiceHelper<T>
    {
        #region Helpers
        /// <summary>
        /// Ensures that the ServiceResponse will contain information relevant to the result of the executed function
        /// </summary>
        /// <param name="fn">The Function to execute and get data from</param>
        /// <param name="arg">The expected argument of that function</param>
        /// <returns>An async Service response that reflects the state of the request</returns>
        public static async Task<ServiceResponse<T>> ActionHandler(
            Func<object, Task<T>> fn, params object[] args)
        {
            var response = new ServiceResponse<T>();
            try
            {
                //Atempt to create a successfull response with the data given
                response.Body = await fn(args[0]);
                response.Successfull = true;
            }
            catch (Exception err)
            {
                //The Request couldn't be completed as given
                response.Successfull = false;
                string relevant =
                    $"The request couldn't be completed because of the following error: {err.Message}";
                response.Message = relevant;
            }
            return response;
        }

        /// <summary>
        /// Ensures that a given variable is not null
        /// </summary>
        /// <param name="toCheck">the variable to check for nulls</param>
        /// <returns>The recieved object if it's not null</returns>
        /// <exception cref="ServiceException"></exception>
        public static T NoNullsAccepted(T? toCheck)
        {
            try
            {
                if (toCheck is null)
                {
                    throw new (
                        $"No object could be found with that information");
                }
                return toCheck;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion
        #region Custom Exception
        public class ServiceException : Exception
        {
            public ServiceException(string Message) : base(Message)
            {
            }
        } 
        #endregion
    }
}