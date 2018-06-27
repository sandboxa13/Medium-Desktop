using DryIoc;
using DryIoc.MefAttributedModel;

namespace MediumDesktop.Core.Extensions
{   
    public static class ContainerExtensions
    {
        public static void RegisterShared(this IContainer registrator)
        {
            registrator.WithMefAttributedModel();
            registrator.RegisterExports(new[] { typeof(ContainerExtensions).GetAssembly() });
        }
    }
}
