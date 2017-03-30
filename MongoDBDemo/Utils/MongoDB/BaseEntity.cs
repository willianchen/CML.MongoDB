using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.Utils.MongoDB
{
    /// <summary>
    /// MongoDB基类
    /// </summary>
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
