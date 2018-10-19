using DryIoc;
using DryIocAttributes;
using Services.Interfaces.Interfaces;

namespace Services.Impl
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
