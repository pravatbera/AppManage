using AppManage.AppCode.DAL.Users;
using AppManage.Model.Users;

namespace AppManage.AppCode.BAL.Users
{
    public class UserBal
    {
        private readonly UserDal r;
        private readonly IConfiguration _config;

        public UserBal(IConfiguration configuration)
        {
            _config = configuration;
            // Pass IConfiguration to MasterDataDal's constructor
            r = new UserDal(configuration);
        }
        internal List<User> UsersDeatiles()
        {
            return r.UsersDeatiles();
        }
    }
}
