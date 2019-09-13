using TianYu.Admin.Repository.IModelRepository;
using TianYu.Admin.Service.IService;
using System.Linq;

namespace TianYu.Admin.Service.Service
{
    public class DictionaryItemService : BaseService, IDictionaryItemService
    {
        private readonly IDictionaryItemRepository dictionaryItemRepository;
        public DictionaryItemService(IDictionaryItemRepository dictionaryItemRepository)
        {
            this.dictionaryItemRepository = dictionaryItemRepository;
        }
    }
}
