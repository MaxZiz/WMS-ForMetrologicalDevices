using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;

namespace WMS.Service.Interfaces
{
    public interface IUserService
    {
       public BaseResponse<IEnumerable<UserViewModel>> GetUsers();

        IBaseResponse<User> CreateUser(UserViewModel model);

        BaseResponse<Dictionary<int, string>> GetRoles();   

        IBaseResponse<bool> DeleteUser(long id);
    }
}
