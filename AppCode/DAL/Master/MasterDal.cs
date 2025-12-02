using AppManage.AppCode.DAL.System;
using AppManage.Model.Users;
using AppManage.Models.Master;

namespace AppManage.AppCode.DAL.Master
{
    public class MasterDal : DalBase
    {
        public MasterDal(IConfiguration configuration) : base(configuration)
        {
        }
        internal List<Master_Md> get_Role()
        {
            var cmd = NewCommand("get_Role");
            return GetResult(cmd).Convert<Master_Md>(); ;
        }
        internal List<Master_Md> get_unit()
        {
            var cmd = NewCommand("get_unit");
            return GetResult(cmd).Convert<Master_Md>(); ;
        }
    }
}
