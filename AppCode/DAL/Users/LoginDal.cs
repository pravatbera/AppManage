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
    }
}
