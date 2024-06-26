using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Service.Interfaces
{
   public interface IHistoryService
    {
        public IBaseResponse<IEnumerable<History>> GetHistory();
    }
}
