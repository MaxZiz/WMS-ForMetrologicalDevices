using System.Collections.Generic;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;

namespace WMS.Service.Interfaces
{
    public interface IDeviceService
    {
        public IBaseResponse<IEnumerable<Device>> GetDevices();
        public IBaseResponse<DeviceViewModel> GetDevice(int id);
        BaseResponse<Dictionary<int, string>> GetTypes();
        BaseResponse<Dictionary<int, string>> GetDevice(string term);
        public IBaseResponse<Device> GetDeviceByName(string name);
        public IBaseResponse<Device> CreateDevice(DeviceViewModel deviceViewModel, string path);
        public IBaseResponse<bool> DeleteDevice(int id);
        public IBaseResponse<Device> Edit(DeviceViewModel deviceViewModel, string userName);
        BaseResponse<Dictionary<int,int>> GetPlacesDictionary();
        public Dictionary<int, string> GetTypesDictionary();
    }
}
