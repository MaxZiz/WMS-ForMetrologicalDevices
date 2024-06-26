using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Domain.Enums;

namespace WMS.Domain.Entities
{
    [Table("Device")]
    public class Device
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = "Прибор";

        public string Description { get; set; } = "Описание";

        public DateTime DateCreate { get; set; } = DateTime.Now;

        public TypeDevice TypeDevice { get; set; } = TypeDevice.Device;

        public List<History> Histories { get; set; } = new();

        public int PlaceId { get; set; }
     
        public Place Place { get; set; }
       
        public string LinkForImage { get; set; } 

        public byte[] Image { get; set; }
    }
}
