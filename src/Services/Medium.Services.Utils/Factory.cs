using DryIoc;
using DryIocAttributes;

namespace Medium.Services.Utils
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IFactory<>))]
    public class Factory<T> : IFactory<T> where T :class
    {
        private readonly IContainer _container;

        public Factory(IContainer container)
        {
            _container = container;
        }

        public T Create()
        {
            return _container.Resolve<T>();
        }
    }
}
