using System.Data;
using System.Security.Authentication;

namespace AppManage.Model.Users
{
    public class UserCredential_MD
    {
        
    }
    public class User: UserMain
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public void validate()
        {
            if (string.IsNullOrEmpty(UserName))
                throw new AuthenticationException("Username can not be empty");
            if (string.IsNullOrEmpty(Password))
                throw new AuthenticationException("Password can not be empty");
        }
    }
    public class UserMain
    {
        public string? Token {  get; set; }
        public long? UserID { get; set; }
        public int? RoleID { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? LandMark { get; set; }
        public string? ProfileImage { get; set; }
        public IFormFile? file { get; set; }
        public bool? IsActive { get; set; }
    }
}
