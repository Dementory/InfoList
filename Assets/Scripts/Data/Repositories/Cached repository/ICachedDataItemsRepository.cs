using InfoList.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoList.Data
{
    public interface ICachedDataItemsRepository : IDataItemsRepository
    {
        void AddDataToCache(List<DataItem> data);

        Task<bool> ContainsData(int index, int count);
    }
}
