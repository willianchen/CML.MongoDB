using MongoDB.Driver;
using MongoDBDemo.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.Utils.MongoDB
{
    /// <summary>
    /// MongoDB连接工具类
    /// by:阿礼 date:2017-3-29
    /// </summary>
    public class MongoDB
    {
        private static string connStr = ConfigUtil.GetValue(SysConstant._MongoDBConnection);

        private static string dbName = ConfigUtil.GetValue(SysConstant._MongoDB);

        private static IMongoDatabase db = null;

        private static readonly object lockHelper = new object();

        private MongoDB()
        {
        }

        /// <summary>
        /// 创建DB
        /// </summary>
        /// <returns></returns>
        public static IMongoDatabase CreateDB()
        {
            if (db == null)
            {
                lock (lockHelper)
                {
                    if (db == null)
                    {
                        var client = new MongoClient(connStr);
                        db = client.GetDatabase(dbName);
                    }
                }
            }
            return db;
        }
    }
}
