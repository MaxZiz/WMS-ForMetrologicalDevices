using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Enums;
using WMS.Domain.Extensions;
using WMS.Domain.Interfaces;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
    public class DeviceService : IDeviceService
    {
        IBaseRepository<Device> _deviceRepository;
        IBaseRepository<History> _historyRepository;
        IPlaceService _placeService;
        IBaseRepository<TypeOfDevice> _typeOfDeviceRepository;

        public DeviceService(IBaseRepository<Device> deviceRepository, IBaseRepository<History> historyRepository, IPlaceService placeService, IBaseRepository<TypeOfDevice> typeOfDeviceRepository)
        {
            _deviceRepository = deviceRepository;
            _historyRepository = historyRepository;
            _placeService = placeService;
            _typeOfDeviceRepository = typeOfDeviceRepository;
        }

        public IBaseResponse<Device> CreateDevice(DeviceViewModel deviceViewModel, string path)
        {
            var baseResponse = new BaseResponse<Device>();
            try
            {
                var device = new Device()
                {
                    TypeDevice = deviceViewModel.TypeDevice,
                    Name = deviceViewModel.Name,
                    Description = deviceViewModel.Description,
                    Histories = deviceViewModel.Histories,
                    PlaceId = deviceViewModel.PlaceNumber,
                    LinkForImage = path,
                };

                _deviceRepository.Create(device);

                return new BaseResponse<Device>()
                {
                    StatusCode = StatusCode.OK,
                    Data = device
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Device>
                {
                    Description = $"[CreateElement] : {ex.Message}"
                };
            }
        }

        public IBaseResponse<bool> DeleteDevice(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };

            try
            {
                var element = _deviceRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if (element == null)
                {
                    baseResponse.Description = "Объект не найден";
                    baseResponse.StatusCode = StatusCode.ElementNotFound;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                _deviceRepository.Delete(element);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[GetElements] : {ex.Message}"
                };
            }
        }

        public IBaseResponse<Device> Edit(DeviceViewModel deviceViewModel, string userName)
        {
            var baseResponse = new BaseResponse<Device>();
            try
            {
                var device = _deviceRepository.GetAll().FirstOrDefault(x => x.Id == deviceViewModel.Id);
                if (device == null)
                {
                    baseResponse.Description = "Объект не найден";
                    baseResponse.StatusCode = StatusCode.ElementNotFound;
                    return baseResponse;
                }

                device.Description = deviceViewModel.Description;
                device.Name = deviceViewModel.Name;
                device.TypeDevice = deviceViewModel.TypeDevice;

                if (deviceViewModel.PlaceNumber != deviceViewModel.ToPlaceNumber && deviceViewModel.ToPlaceNumber != 0)
                {
                    device.Histories.Add(new History
                    {
                        DateTime = DateTime.Now,
                        FromPlace = device.PlaceId,
                        ToPlace = deviceViewModel.ToPlaceNumber,
                        Device = device,
                        _Guid = Guid.NewGuid(),
                        UserId = userName
                    });
                    device.PlaceId = deviceViewModel.ToPlaceNumber;
                }
                if (device.Histories.Count > 10)
                {
                    device.Histories.RemoveAt(0);
                }
                _deviceRepository.Update(device);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Device>()
                {
                    Description = $"[Get Element] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<DeviceViewModel> GetDevice(int id)
        {
            try
            {
                var device = _deviceRepository.GetAll().FirstOrDefault(x => x.Id == id);
                //device.TypeDevice.GetDisplayName();
                if (device == null)
                {
                    return new BaseResponse<DeviceViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new DeviceViewModel()
                {
                    DateCreate = device.DateCreate,
                    Description = device.Description,
                    Name = device.Name,
                    Id = device.Id,
                    Histories = device.Histories,
                    PlaceNumber = device.PlaceId,
                    LinkForImage = device.LinkForImage,
                };

                return new BaseResponse<DeviceViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<DeviceViewModel>()
                {
                    Description = $"[GetDevice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<Device> GetDeviceByName(string name)
        {
            var baseResponse = new BaseResponse<Device>();
            try
            {
                var element = _deviceRepository.GetAll().FirstOrDefault(x => x.Name == name);
                if (element == null)
                {
                    baseResponse.Description = "Объект не найден";
                    baseResponse.StatusCode = StatusCode.ElementNotFound;
                    return baseResponse;
                }
                baseResponse.Data = element;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Device>()
                {
                    Description = $"[Get Element] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<Device>> GetDevices()
        {
            var baseResponse = new BaseResponse<IEnumerable<Device>>();
            try
            {
                var elements = _deviceRepository.GetAll().ToList();

                if (elements.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Description = "Элементы получены";
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = elements;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Device>>
                {
                    Description = $"[GetElements] : {ex.Message}"
                };

            }
        }

        /// <summary>
        /// Получение прибора через строку
        /// </summary>
        /// <returns></returns>
        public BaseResponse<Dictionary<int, string>> GetDevice(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var devices = _deviceRepository.GetAll()
                    .Select(x => new DeviceViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        DateCreate = x.DateCreate,
                        TypeDevice = x.TypeDevice
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionary(x => x.Id, t => t.Name);

                baseResponse.Data = devices;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((TypeDevice[])Enum.GetValues(typeof(TypeDevice)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseResponse<Dictionary<int, int>> GetPlacesDictionary()
        {
            try
            {
                var places = _placeService.GetPlaces().Data
                    .ToDictionary(k => k.Id, t => t.Number);
                return new BaseResponse<Dictionary<int, int>>()
                {
                    Data = places,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, int>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetTypesDictionary()
        {
            var places = _typeOfDeviceRepository.GetAll()
                    .ToDictionary(k => k.Id, t => t.Name);
            return places;
        }

    }
}
