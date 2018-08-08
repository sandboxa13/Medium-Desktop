using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.Managers;
using Services.Impl;

namespace Medium.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterShared(this IContainer container)    
        {
            container.WithMefAttributedModel();
            container.RegisterExports(new[] { typeof(LoginManager).GetAssembly() });
            container.RegisterServices();
        }
    }
}
