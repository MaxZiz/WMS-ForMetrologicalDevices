using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.DAL.Repositories;
using WMS.Domain.Entities;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
    public class TypeOfDeviceService : ITypeOfDeviceService
    {
        IBaseRepository<TypeOfDevice> _typeOfDeviceRepository;
        public TypeOfDeviceService(IBaseRepository<TypeOfDevice> typeOfDeviceRepository) 
        {
            _typeOfDeviceRepository = typeOfDeviceRepository;
        }
        public TypeOfDevice CreateType(TypeOfDevice Model)
        {
           _typeOfDeviceRepository.Create(Model);
            return Model;
        }

        public bool DeleteType(int id)
        {
            var type = _typeOfDeviceRepository.GetAll().FirstOrDefault(x => x.Id == id);
            _typeOfDeviceRepository.Delete(type);
            return true;
        }

        public TypeOfDevice Edit(int id, TypeOfDevice Model)
        {
            var type = _typeOfDeviceRepository.GetAll().FirstOrDefault(b => b.Id == id);
            type = Model;
            _typeOfDeviceRepository.Update(type);
            return type;
        }

        public List<TypeOfDevice> GetAll()
        {
            return _typeOfDeviceRepository.GetAll().ToList();
        }

        public TypeOfDevice GetTypeOfDevice(int id)
        {
            var type = _typeOfDeviceRepository.GetAll().FirstOrDefault(x => x.Id == id);
            return type;
        }
    }
}
