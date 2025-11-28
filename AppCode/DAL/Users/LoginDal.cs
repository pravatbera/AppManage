using AppManage.AppCode.DAL.System;
using AppManage.Model.Users;
using System.Data;

namespace AppManage.AppCode.DAL.Users
{
    public class LoginDal:DalBase
    {
        public LoginDal(IConfiguration configuration) : base(configuration)
        {
        }
        internal User UserAuthentication(User userInfo)
        {
            var cmd = NewCommand("get_UserDeatile");
            cmd.Parameters.AddWithValue("@UserName", userInfo.UserName);
            return GetResult(cmd).Convert<User>().FirstOrDefault(); ;
        }
        internal DBMessage UserRegistration(User userInfo)
        {
            var cmd = NewCommand("sp_InsertUserRegistration");
            cmd.Parameters.AddWithValue("@UserID", userInfo.UserID==null?0: userInfo.UserID);
            cmd.Parameters.AddWithValue("@FirstName", userInfo.FirstName);
            cmd.Parameters.AddWithValue("@LastName", userInfo.LastName);
            cmd.Parameters.AddWithValue("@Email", userInfo.Email);
            cmd.Parameters.AddWithValue("@DOB",
            userInfo.DOB.HasValue ? userInfo.DOB.Value.ToString("yyyy-MM-dd") : DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", userInfo.Phone);
            cmd.Parameters.AddWithValue("@Address", userInfo.Address);
            cmd.Parameters.AddWithValue("@LandMark", userInfo.LandMark);
            cmd.Parameters.AddWithValue("@UserName", userInfo.UserName);
            cmd.Parameters.AddWithValue("@Password", userInfo.Password);
            cmd.Parameters.AddWithValue("@ProfileImage", userInfo.ProfileImage);
            cmd.Parameters.AddWithValue("@IsActive", userInfo.IsActive);
            cmd.Parameters.AddWithValue("@InsertedBy", userInfo.UserID == null ? DBNull.Value : userInfo.UserID);
            cmd.Parameters.AddWithValue("@RoleID", userInfo.RoleID == null ? DBNull.Value : userInfo.RoleID);
            return GetResult(cmd).Convert<DBMessage>().FirstOrDefault(); ;
        }
    }
}
