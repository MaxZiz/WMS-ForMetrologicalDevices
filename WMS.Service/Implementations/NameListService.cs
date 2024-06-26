using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.DAL.Repositories;
using WMS.Domain.Entities;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
    public class NameListService : INameListService
    {
        IBaseRepository<NameList> _nameRepository;

        public NameListService(IBaseRepository<NameList> nameRepository) 
        { 
            _nameRepository = nameRepository;
        }

        public NameList CreateName(NameList Model)
        {
            _nameRepository.Create(Model);
            return Model;
        }

        public bool DeleteName(int id)
        {
            var element = _nameRepository.GetAll().FirstOrDefault(x => x.Id == id);
            _nameRepository.Delete(element);
            return true;
        }

        public NameList Edit(int id, NameList Model)
        {
            var element = _nameRepository.GetAll().FirstOrDefault(x => x.Id == id);
            element = Model;
            _nameRepository.Update(element);
            return element;
        }

        public List<NameList> GetListNames()
        {
           return _nameRepository.GetAll().ToList();
        }

        public NameList GetName(int id)
        {
            var element = _nameRepository.GetAll().FirstOrDefault(x => x.Id == id);
            return element;
        }
    }
}
