using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Service.Interfaces
{
    public interface ITypeOfDeviceService
    {
        public List<TypeOfDevice> GetAll();

        public TypeOfDevice GetTypeOfDevice(int id);

        public TypeOfDevice CreateType(TypeOfDevice Model);

        public bool DeleteType(int id);

        public TypeOfDevice Edit(int id, TypeOfDevice Model);
    }
}
