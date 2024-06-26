using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;

namespace WMS.Service.Interfaces
{
    public interface IProfileService
    {
      BaseResponse<ProfileViewModel> GetProfile(string userName);

       BaseResponse<Profile> Save(ProfileViewModel model);
    }
}
