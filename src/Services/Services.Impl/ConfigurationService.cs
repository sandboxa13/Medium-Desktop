﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DryIocAttributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Interfaces.Interfaces;

namespace Services.Impl
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(IConfigurationService))]   
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// Base path to directory with config file
        /// </summary>
        private string _basePath;

        /// <summary>
        /// Contains all values from config file
        /// </summary>
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        /// <summary>
        /// Add new JSON config file
        /// </summary>
        /// <param name="flieName">name of file</param>
        /// <returns></returns>
        public async Task AddJsonFile(string flieName)
        {
            await Task.Run(() =>
            {
                var pathToFile = _basePath + "\\" + flieName;

                using (var stream = new StreamReader(pathToFile))
                {
                    var objects = (JObject)JsonConvert.DeserializeObject(stream.ReadToEnd());

                    GetAllObjects(objects);
                }
            });
        }

        /// <summary>
        /// Getting value by specific key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            var token = (JToken)_values[key];

            return token.ToObject<T>();
        }

        /// <summary>
        /// Set base path to config file
        /// </summary>
        /// <param name="basePath"></param>
        public void SetBasePath(string basePath) => _basePath = basePath;


        /// <summary>
        /// Recursively get and save all values from config file
        /// </summary>
        /// <param name="jObject"></param>
        private void GetAllObjects(JObject jObject)
        {
            foreach (var o in jObject)
            {
                if (o.Value is JObject child)
                {
                    GetAllObjects(child);
                    continue;
                }

                _values.Add(o.Key, jObject[o.Key]);
            }
        }
    }
}