using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace YourMail.Filters
{
    public class MyMailAddress : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var userEmail = value as string;
            if (userEmail != null && userEmail.Length < 11)
            {
                ErrorMessage = "The email field cannot be empty or less than 11 characters";
                return false;
            }
            else if (userEmail.Length > 25)
            {
                ErrorMessage = "The email field cannot be more than 25 characters";
                return false;
            }
            else if (!(new Regex(@"(\w+)@your.com$").Matches(userEmail).Count > 0))
            {
                ErrorMessage = "Email should end on @uour.com and contain only letters or numbers";
                return false;
            }
            return true;
        }
    }
}
