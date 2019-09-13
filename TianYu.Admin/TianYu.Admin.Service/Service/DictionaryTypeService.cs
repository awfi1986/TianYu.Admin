using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using System.Linq;

namespace TianYu.Admin.Service.Service
{
    public class DictionaryTypeService : BaseService, IDictionaryTypeService
    {
        private readonly IDictionaryTypeRepository dictionaryTypeRepository;
        public DictionaryTypeService(IDictionaryTypeRepository dictionaryTypeRepository)
        {
            this.dictionaryTypeRepository = dictionaryTypeRepository;
        }
    }
}
