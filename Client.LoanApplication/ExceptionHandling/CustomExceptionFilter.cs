using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Client.LoanApplication.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomExceptionFilter :  ExceptionFilterAttribute,IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
      
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            var absolutePath = filterContext.ActionContext.RequestContext.Url.Request.RequestUri.AbsolutePath;
            var exceptionMessage = filterContext.Exception.Message;
            var stackTrace = filterContext.Exception.StackTrace;
           
            string Message = Environment.NewLine + "======================================================================================================================" +
                             Environment.NewLine + "Date :" + DateTime.Now.ToString() + Environment.NewLine +
                             Environment.NewLine + "AbsolutePath: " + absolutePath +
                             Environment.NewLine + "Error Message : " + exceptionMessage +
                             Environment.NewLine + "Stack Trace : " + stackTrace;

            //saving the data in a text file called Log.txt
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/ExceptionHandling/ErrorLogs.txt"), Message);
        }
    }
}