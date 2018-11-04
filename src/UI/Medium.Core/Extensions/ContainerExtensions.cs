using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.Managers;
using Medium.Services.Configuration;
using Medium.Services.Utils;

namespace Medium.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterShared(this IContainer container)    
        {
            container.WithMefAttributedModel();
            container.RegisterExports(new[] { typeof(AuthenticationManager).GetAssembly() });    
            container.RegisterExports(new[] { typeof(Factory<>).GetAssembly() });
            container.RegisterExports(new[] { typeof(ConfigurationService).GetAssembly() });
        }
    }
}
