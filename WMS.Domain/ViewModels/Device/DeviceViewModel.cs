using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Enums;

namespace WMS.Domain.ViewModels
{
    public class DeviceViewModel
    {    
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateCreate { get; set; } = DateTime.Now;

        public TypeDevice TypeDevice { get; set; } = TypeDevice.Other;

        public List<History> Histories { get; set; }

        public int PlaceNumber { get; set; }

        public int ToPlaceNumber { get; set; }

        public string LinkForImage { get; set; }

        public IFormFile Picture {  get; set; }

        public byte[] Image { get; set; }

    }
}
