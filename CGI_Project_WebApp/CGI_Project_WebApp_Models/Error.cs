using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Models
{
    public class Error
    {
        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public bool MessageBool { get; set; }

        public void HandleError(string errorMessage)
        {
            MessageBool = true;
            ErrorMessage = errorMessage;
        }

        public void HandleSuccess(string successMessage)
        {
            MessageBool = true;
            SuccessMessage = successMessage;
        }

        public void ResetErrorHandling()
        {
            MessageBool = false;
            SuccessMessage = "";
            ErrorMessage = "";
        }
    }
}
