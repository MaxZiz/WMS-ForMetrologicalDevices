using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;

namespace WMS.Service.Interfaces
{
    public interface IAccountService
    {
        
           BaseResponse<ClaimsIdentity> Register(RegisterViewModel model);

           BaseResponse<ClaimsIdentity> Login(LoginViewModel model);

            BaseResponse<bool> ChangePassword(ChangePasswordViewModel model);
        
    }
}
