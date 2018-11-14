using DryIoc;
using DryIoc.MefAttributedModel;
using Medium.Core.ViewModels;
using Medium.Services.MediumApi.Managers;
using Medium.Services.Utils;
using MediumSDK.Net.Domain;

namespace Medium.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterShared(this IContainer container)    
        {
            container.WithMefAttributedModel();
            container.RegisterExports(new[] { typeof(AuthenticationManager).GetAssembly() });    
            container.RegisterExports(new[] { typeof(MediumClient).GetAssembly() });    
            container.RegisterExports(new[] { typeof(Factory<>).GetAssembly() });
            container.RegisterExports(new[] { typeof(MainWindowViewModel).GetAssembly() });
        }
    }
}
