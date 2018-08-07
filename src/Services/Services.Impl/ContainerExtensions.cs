using DryIoc;
using DryIoc.MefAttributedModel;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    public static class ContainerExtensions 
    {
        public static void RegisterServices(this IContainer container)
        {
            container.WithMefAttributedModel();
            container.RegisterExports(new[] { typeof(MediumApiService).GetAssembly() });
        }
    }
}
