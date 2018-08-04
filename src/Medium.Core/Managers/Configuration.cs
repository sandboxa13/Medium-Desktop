using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Domain;
using Medium.Core.Managers.Interfaces;
using Newtonsoft.Json;

namespace Medium.Core.Managers
{
    [Reuse(ReuseType.Singleton)]    
    [ExportEx(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {   
        private string _basePath;   
        public AppSettingsItem SettingsItem { get; set; }


        public void SetBasePath(string basePath) => _basePath = basePath;

        public void AddJsonFile(string path)
        {
            var pathToFile = _basePath + "\\" + path;

            using (var stream = new StreamReader(pathToFile))
            {   
                var json = stream.ReadToEnd();
                SettingsItem = JsonConvert.DeserializeObject<AppSettingsItem>(json);
            }
        }   

        public AppSettingsItem GetAppSettings() => SettingsItem;
    }
}
