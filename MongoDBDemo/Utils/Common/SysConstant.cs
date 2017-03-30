using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.Utils.Common
{
    /// <summary>
    /// 系统常量配置
    /// by:阿礼 date:2017-3-29
    /// </summary>
    public class SysConstant
    {
        /// <summary>
        /// MongoDB连接字符串配置名
        /// </summary>
        public const string _MongoDBConnection = "MONGODB_CONNECTION";

        /// <summary>
        /// MongoDB 数据库名称配置名
        /// </summary>
        public const string _MongoDB = "MONGODB_DB";

        /// <summary>
        /// config配置文件路径
        /// </summary>
        public const string _Path_AppConfig = "app.config";
    }
}
