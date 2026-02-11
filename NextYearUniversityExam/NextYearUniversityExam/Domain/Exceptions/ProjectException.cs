using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
 
    public class ProjectException : Exception
    {

        public static event Action<string, string>? OnLimitReached; 
        public ProjectException(string value, string message) : base(message) {
            OnLimitReached?.Invoke(value, message);
        }

       
    }




}
