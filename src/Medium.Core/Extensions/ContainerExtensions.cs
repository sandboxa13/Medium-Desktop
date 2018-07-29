using DryIoc;
using DryIoc.MefAttributedModel;

namespace Medium.Core.Extensions
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
