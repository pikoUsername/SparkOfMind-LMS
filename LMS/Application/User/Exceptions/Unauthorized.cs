namespace LMS.Application.User.Exceptions
{
    public class Unauthorized : Exception
    {
        public Unauthorized()
            : base($"User is not authorized")
        {
        }
    }
}
