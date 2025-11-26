using AppManage.AppCode.DAL.System;
using AppManage.Model.Users;

namespace AppManage.AppCode.DAL.Users
{
    public class UserDal : DalBase
    {
        public UserDal(IConfiguration configuration) : base(configuration)
        {
        }
        internal List<User> UsersDeatiles()
        {
            var cmd = NewCommand("get_UsersDeatiles");
            return GetResult(cmd).Convert<User>(); ;
        }
    }
}
