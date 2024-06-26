using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Service.Interfaces
{
    public interface INameListService
    {
        public List<NameList> GetListNames();

        public NameList GetName(int id);

        public NameList CreateName(NameList Model);

        public bool DeleteName(int id);

        public NameList Edit(int id, NameList Model);
    }
}
