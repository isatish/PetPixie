using System;

namespace Merial.PetPixie.Core.Common.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException() : base("Username already exists. Please chose another one")
        {
        }
       
        public UsernameAlreadyExistsException(Exception innerException) : base("Username already exists. Please chose another one", innerException)
        {
        }
    }
}
