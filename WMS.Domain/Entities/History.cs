using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Domain.Entities
{
    public class History
    {       
        public Guid _Guid { get; set; }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }
        
        public Device Device { get; set; } 

        public int FromPlace { get; set; } 

        public int ToPlace { get; set; } 

    }
}
