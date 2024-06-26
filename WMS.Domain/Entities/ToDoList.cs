using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Domain.Entities
{
    public class ToDoList
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? DateCreation { get; set; }

        public DateTime? TimeToDo { get; set; }

        public string Description { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();

        public bool isDone { get; set; }

        public string TypeOfOperation { get; set; }

        public long UserId { get; set; }

        public string UserNameWhoDid {  get; set; }

        public int? FromPlace {  get; set; }

        public int? ToPlace { get; set; }
    }
}
