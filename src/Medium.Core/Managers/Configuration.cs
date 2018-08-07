using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DryIocAttributes;
using Medium.Core.Managers.Interfaces;
using Newtonsoft.Json;

namespace Medium.Core.Managers
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {
        private string _basePath;   
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();


        public async Task AddJsonFile(string flieName)
        {
            await Task.Run(() =>    
            {
                var pathToFile = _basePath + "\\" + flieName;

                using (var stream = new StreamReader(pathToFile))
                {
                    var tmp = JsonConvert.DeserializeObject<Dictionary<string, string>>(stream.ReadToEnd());

                    foreach (var tmpKey in tmp.Keys)
                    {
                        _values.Add(tmpKey, tmp[tmpKey]);
                    }
                }
            });
        }

        public string GetValue(string key) => _values[key];

        public void SetBasePath(string basePath) => _basePath = basePath;
    }
}
