using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Domain.Responses;

namespace WMS.Service.Interfaces
{
    public interface IPlaceService
    {
        public IBaseResponse<IEnumerable<Place>> GetPlaces();

        public IBaseResponse<Place> GetPlace(int id);

        public IBaseResponse<Place> CreatePlace(Place Model);

        public IBaseResponse<bool> DeletePlace(int id);

        public IBaseResponse<Place> Edit(int id, Place Model);
    }
}
