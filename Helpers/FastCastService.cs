using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCast
{
    public interface IFastCastService
    {
        void AddData(string key, string data);
        string GetData(string key);
    }
    public class FastCastService: IFastCastService
    {
        private Dictionary<String, String> fastCastService = new Dictionary<string, string>();

        public void AddData(string key, string data)
        {
            fastCastService.Add(key, data);
        }

        public string GetData(string key)
        {
            if (fastCastService.ContainsKey(key)) {
                return fastCastService.GetValueOrDefault(key);
            }
            else
            {
                return "-1";
            }
        }
    }
}
