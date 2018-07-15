using System.Threading.Tasks;
using DryIocAttributes;
using LiteDB;
using MediumDesktop.Core.Domain;
using MediumDesktop.Core.Managers.Interfaces;

namespace MediumDesktop.Core.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IStoreManager))]
    public class StoreManager : IStoreManager
    {
        private readonly LiteDatabase _liteDatabase;

        public StoreManager(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public async Task<ApplicationData> GetApplicationData()
        {
            return await Task.Run(() =>
            {
                var applicationDataCollection = _liteDatabase.GetCollection<ApplicationData>("application");

                var result = applicationDataCollection.FindOne(x => x.Id == 1);

                return result;
            });
        }
    }
}
