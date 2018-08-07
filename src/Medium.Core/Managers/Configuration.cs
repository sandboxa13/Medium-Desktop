using System;
using System.Collections.Generic;
using System.IO;
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

        public void SetBasePath(string basePath) => _basePath = basePath;

        public void AddJsonFile(string path)
        {
            var pathToFile = _basePath + "\\" + path;   

            using (var stream = new StreamReader(pathToFile))
            {   
                var tmp  = JsonConvert.DeserializeObject<Dictionary<string, string>>(stream.ReadToEnd());

                foreach (var tmpKey in tmp.Keys)
                {
                    _values.Add(tmpKey, tmp[tmpKey]);
                }
            }
        }

        public string GetValue(string key)
        {
            try
            {
                return _values[key];

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
