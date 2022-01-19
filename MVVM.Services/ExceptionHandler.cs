namespace MVVM.Services
{
    using System;

    using MVVM.Models;

    public static class ExceptionHandler
    {
        public static IContact HandleException(Exception exception)
        {
            throw exception;
            return new Contact(exception.ToString());
        }
    }
}