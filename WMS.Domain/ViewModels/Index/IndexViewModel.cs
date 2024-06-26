using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Domain.ViewModels.Index
{
    public class IndexViewModel
    {
        public Guid _Guid { get; set; }

        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }

        public Device Device { get; set; } = new Device();

        public int FromPlace { get; set; }

        public int ToPlace { get; set; }

        public DateTime? DateCreation { get; set; }

        public DateTime? TimeToDo { get; set; }

        public string Description { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();

        public bool isDone { get; set; }

        public string TypeOfOperation { get; set; }

    }
}
