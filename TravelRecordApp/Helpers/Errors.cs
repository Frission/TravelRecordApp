using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Helpers
{
    public enum ErrorCode
    {
        EmailPasswordEmpty,
        EmailPasswordIncorrect,
        PasswordMismatch,
        UserExists,
        ServiceUnavailable,
        NoError,
    }

    public struct ApplicationError
    {
        public ErrorCode Code;
        public string Message;
    }

    public static class ApplicationErrors
    {
        public static ApplicationError EmailPasswordEmpty = new ApplicationError()
        {
            Code = ErrorCode.EmailPasswordEmpty,
            Message = "Please enter your e-mail and password."
        };

        public static ApplicationError EmailPasswordIncorrect = new ApplicationError()
        {
            Code = ErrorCode.EmailPasswordIncorrect,
            Message = "Email or password are incorrect."
        };

        public static ApplicationError ServiceUnavailable = new ApplicationError()
        {
            Code = ErrorCode.ServiceUnavailable,
            Message = "Could not reach the service."
        };

        public static ApplicationError PasswordMismatch = new ApplicationError()
        {
            Code = ErrorCode.PasswordMismatch,
            Message = "Passwords do not match!"
        };

        public static ApplicationError UserExists = new ApplicationError()
        {
            Code = ErrorCode.UserExists,
            Message = "A user with this e-mail already exists."
        };

        public static ApplicationError NoError = new ApplicationError()
        {
            Code = ErrorCode.NoError,
            Message = "Success!"
        };

        public static ApplicationError GetError(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.EmailPasswordEmpty:
                    return EmailPasswordEmpty;
                case ErrorCode.EmailPasswordIncorrect:
                    return EmailPasswordIncorrect;
                case ErrorCode.ServiceUnavailable:
                    return ServiceUnavailable;
                case ErrorCode.UserExists:
                    return UserExists;
                case ErrorCode.NoError:
                    return NoError;
                default:
                    return NoError;
            }
        }
    }

    public class CloudOperationResult
    {
        public bool Success;
        public ErrorCode Error;
    }
}
