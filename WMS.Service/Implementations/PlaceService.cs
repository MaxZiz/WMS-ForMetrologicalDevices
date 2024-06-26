using System;
using System.Collections.Generic;
using System.Linq;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Enums;
using WMS.Domain.Interfaces;
using WMS.Domain.Responses;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
    public class PlaceService : IPlaceService
    {
        IBaseRepository<Place> _placeRepository;
        public PlaceService(IBaseRepository<Place> placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public IBaseResponse<Place> CreatePlace(Place Model)
        {
            var baseResponse = new BaseResponse<Place>();
            try
            {
                var place = new Place()
                {
                    Id = Model.Id,
                    Number = Model.Number,

                };
                _placeRepository.Create(place);

                return new BaseResponse<Place>()
                {
                    StatusCode = StatusCode.OK,
                    Data = place
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Place>
                {
                    Description = $"[CreateElement] : {ex.Message}"
                };
            }
        }

        public IBaseResponse<bool> DeletePlace(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };

            try
            {
                var element = _placeRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if (element == null)
                {
                    baseResponse.Description = "Объект не найден";
                    baseResponse.StatusCode = StatusCode.ElementNotFound;
                    baseResponse.Data = false;
                    return baseResponse;
                }
                _placeRepository.Delete(element);
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

        public IBaseResponse<Place> Edit(int id, Place Model)
        {
            var baseResponse = new BaseResponse<Place>();
            try
            {
                var place = _placeRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if (place == null)
                {
                    baseResponse.Description = "Объект не найден";
                    baseResponse.StatusCode = StatusCode.ElementNotFound;
                    return baseResponse;
                }
                place.Id = Model.Id;
                place.Number = Model.Number;


                _placeRepository.Update(place);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Place>()
                {
                    Description = $"[Get Element] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<Place> GetPlace(int id)
        {
            try
            {
                var place = _placeRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if (place == null)
                {
                    return new BaseResponse<Place>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new Place()
                {
                    Number = place.Number,
                    Id = place.Id,
                    Devices = place.Devices
                };

                return new BaseResponse<Place>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Place>()
                {
                    Description = $"[GetPlace] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<Place>> GetPlaces()
        {
            var baseResponse = new BaseResponse<IEnumerable<Place>>();
            try
            {
                var elements = _placeRepository.GetAll().ToList();

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
                return new BaseResponse<IEnumerable<Place>>
                {
                    Description = $"[GetElements] : {ex.Message}"
                };

            }
        }
    }
}
