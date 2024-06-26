using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Domain.Entities
{
    public class Place
    {  
       
        public int Id { get; set; }

        public int Number { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
}
