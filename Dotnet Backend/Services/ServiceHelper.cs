using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetBackend.Models;

namespace DotnetBackend.Services
{
    public abstract class ServiceHelper<T>
    {
        #region Helpers
        public static async Task<ServiceResponse<T>> ActionHandler(Func<object, Task<T>> fn, object arg)
        {
            var response = new ServiceResponse<T>();
            try
            {
                response.Body = await fn(arg);
                response.Successfull = true;
            }
            catch (Exception err)
            {
                response.Successfull = false;
                string relevant =
                    $"The request couldn't be completed because of the following error:{err.Message}";
                response.Message = relevant;
            }
            return response;
        }

        public static T NoNullsAccepted(T? toCheck)
        {
            if (toCheck is null)
            {
                throw new NullReferenceException($"No object could be found with that information");
            }
            return toCheck;
        }
        
        #endregion
    }
}