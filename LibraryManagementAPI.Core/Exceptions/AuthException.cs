namespace LibraryManagementAPI.Core.Exceptions
{
    public class AuthException : ApiException
    {
        public AuthException() : base("Invalid credentials!", 400 ) { }
    }
}
