using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Service.Interfaces
{
    public interface IGuideService
    {
        public IEnumerable<Guide> GetGuides();

        public Guide GetGuide(int id);

        public Guide CreateGuide(Guide Model);

        public bool DeleteGuide(int id);

        public Guide Edit(int id, Guide Model);
    }
}
