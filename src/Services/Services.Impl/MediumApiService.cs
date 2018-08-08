using System;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Domain.Domain;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IMediumApiService))]
    public class MediumApiService : IMediumApiService
    {
        public Task<User> GetUserProfile()
        {
            throw new NotImplementedException();
        }
    }
}
