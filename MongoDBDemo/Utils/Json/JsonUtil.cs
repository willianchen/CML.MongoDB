using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.Utils.Json
{
    /// <summary>
    /// Json序列化工具类
    /// by:阿礼 date:2017-3-30
    /// </summary>
    public class JsonUtil
    {
       
        public static string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static List<T> Deserialize<T>(string obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(obj);
        }

        public static T DeserializeModel<T>(string obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
