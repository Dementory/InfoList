using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoList.Domain
{
    public interface IDataItemsRepository
    {
        public Task<int> GetAvaliableDataAmount();

        public Task<List<DataItem>> GetData(int index, int count);
    }
}
