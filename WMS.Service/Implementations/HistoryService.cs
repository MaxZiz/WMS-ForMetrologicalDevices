using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.DAL.Repositories;
using WMS.Domain.Entities;
using WMS.Domain.Enums;
using WMS.Domain.Interfaces;
using WMS.Domain.Responses;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
   public class HistoryService : IHistoryService
    {
        private readonly IBaseRepository<History> _historyRepository;
        public HistoryService(IBaseRepository<History> historyRepository) 
        {
            _historyRepository = historyRepository;
        }


        public IBaseResponse<IEnumerable<History>> GetHistory()
        {
            var baseResponse = new BaseResponse<IEnumerable<History>>();
            try
            {
                var elements = _historyRepository.GetAll().ToList();

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
                return new BaseResponse<IEnumerable<History>>
                {
                    Description = $"[GetElements] : {ex.Message}"
                };

            }
        }
    }
}
