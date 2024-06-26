using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Enums;

namespace WMS.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        StatusCode StatusCode { get; }
        T Data { get; }

        public string Description { get; set; }
    }
}
