using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Domain.ViewModels.Basket
{
    public class BasketViewModel
    {
        public  long Id { get; set; }

        public User User { get; set; }

        public long UserId { get; set; }

        public List<Device> Devices { get; set; }
    }
}
