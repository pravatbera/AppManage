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
        internal DataTable UserAuthentication(UserCredential_MD userInfo)
        {
            var cmd = NewCommand("get_UserDeatile");
            cmd.Parameters.AddWithValue("@UserName", userInfo.username);
            return GetResult(cmd);
        }
    }
}
