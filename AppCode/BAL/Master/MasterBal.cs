using AppManage.AppCode.DAL.Master;
using AppManage.AppCode.DAL.Users;
using AppManage.Model.Users;
using AppManage.Models.Master;

namespace AppManage.AppCode.BAL.Master
{
    public class MasterBal
    {
        private readonly MasterDal r;
        private readonly IConfiguration _config;

        public MasterBal(IConfiguration configuration)
        {
            _config = configuration;
            // Pass IConfiguration to MasterDataDal's constructor
            r = new MasterDal(configuration);
        }
        internal List<Master_Md> get_Role()
        {
            return r.get_Role();
        }
        internal List<Master_Md> get_unit()
        {
            return r.get_unit();
        }
    }
}

