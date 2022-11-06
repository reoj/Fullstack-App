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
        public static ServiceResponse<T> ActionHandler(Func<Object, T> fn, Object arg)
        {
            var response = new ServiceResponse<T>();
            try
            {
                response.Body = fn(arg);
            }
            catch (Exception err)
            {
                response.Successfull = false;
                string relevant =
                    $"The request couldn't be completed because of the following error:{err.Message.ToString()}";
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