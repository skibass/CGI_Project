using CGI_Project_WebApp_DAL.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class Result<T>
    {
        public Result() { 
        
        }
        public Result(T data)
        {
            Data = data;
        }
        public Result(ErrorTransferObject errors)
        {
            CopyOverMessage(errors);
        }
        public Result(string BackendMessage, string FrontendMessageNL, string FrontendMessageENG)
        {
            
        }
        public Result(string FrontendMessageNL, string FrontendMessageENG)
        {

        }
        public Result(string BackendMessage)
        {

        }

        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public string ErrorMessageUserDisplay { get; set; } = string.Empty;

        public string ErrorMessageUserDisplayEnglish { get; set; } = string.Empty;

        public ErrorTransferObject ErrorTransferObject 
        {
            get { return new ErrorTransferObject { ErrorMessage = ErrorMessage, ErrorMessageUserDisplay = ErrorMessageUserDisplay, ErrorMessageUserDisplayEnglish= ErrorMessageUserDisplayEnglish }; }
        }


        public bool IsFailed => !string.IsNullOrEmpty(ErrorMessage);
        
        //bijvoorbeeld bestaat een gebruiker niet bij het inloggen? ja is gefaald maar is geen error perse. dus dat kan je zien in IsFailedNonError
        public bool IsFailedNonError => !string.IsNullOrEmpty(ErrorMessageUserDisplay);

        public bool IsFailedAny => (IsFailed || IsFailedNonError);

        public void CopyOverMessage(ErrorTransferObject result)
        {
            ErrorMessage = result.ErrorMessage;
            ErrorMessageUserDisplay = result.ErrorMessageUserDisplay;
            ErrorMessageUserDisplayEnglish = result.ErrorMessageUserDisplayEnglish;
        }
    }

    //voor als je een die maakt en eigelijk niets terug hoeft maar wel de errors wil kunnen gebruiken
    public class EmptyResult
    {

    }

    public class ErrorTransferObject
    {
        public string ErrorMessage { get; set; } = string.Empty;

        public string ErrorMessageUserDisplay { get; set; } = string.Empty;

        public string ErrorMessageUserDisplayEnglish { get; set; } = string.Empty;
    }

}
