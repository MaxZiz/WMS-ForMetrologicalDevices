using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Domain.Entities
{
    public class Paragraph
    {
        public int Id { get; set; }
        public int NumberOfText { get; set; }
        public Guide Guide { get; set; }
        public string Text { get; set; }

        public string LinkOfImage { get; set; }
    }
}
