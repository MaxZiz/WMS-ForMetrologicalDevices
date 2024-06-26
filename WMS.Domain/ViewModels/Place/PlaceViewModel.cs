using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Domain.ViewModels.Place
{
    public class PlaceViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
}
