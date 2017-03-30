using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.Utils.MongoDB
{

    /// <summary>
    /// MongoDB数据操作工具类
    /// by:阿礼 date:2017-3-29
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDBUtil<T> where T : BaseEntity
    {
        private IMongoDatabase db = null;
        private IMongoCollection<T> collection = null;

        public MongoDBUtil()
        {
            this.db = MongoDB.CreateDB();
            collection = db.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// 添加一条对象记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Insert(T entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// 根据ID更新一条记录  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void Update(string id, string field, string value)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var updated = Builders<T>.Update.Set(field, value);
            UpdateResult result = collection.UpdateOneAsync(filter, updated).Result;
        }

        /// <summary>
        /// 根据ID更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public void Update(T entity, string id)
        {
            Update(entity, a => a.Id == ObjectId.Parse(id));
        }

        /// <summary>
        /// 根据条件更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="func"></param>
        public void Update(T entity, Expression<Func<T, bool>> func)
        {
            var old = GetEntity(func);
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name.Equals("Id"))
                    continue;
                var newValue = prop.GetValue(entity);
                var oldValue = old.GetType().GetProperty(prop.Name).GetValue(old);
                if (newValue != null)
                {
                    if (!newValue.ToString().Equals(oldValue.ToString()))
                    {
                        old.GetType().GetProperty(prop.Name).SetValue(old, newValue);
                    }
                }
            }
            collection.ReplaceOne(func, old);
        }

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntity(string id)
        {
            return collection.Find(a => a.Id == ObjectId.Parse(id)).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Lambar 表达式选择一个模型
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public T GetEntity(Expression<Func<T, bool>> func)
        {
            return collection.Find(func).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 获取全部信息
        /// </summary>
        /// <returns></returns>
        public List<T> ListAll()
        {
            return ListByCondition(s => 1 == 1);
        }

        /// <summary>
        /// 根据条件筛选列表
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public List<T> ListByCondition(Expression<Func<T, bool>> func)
        {
            return collection.Find(func).ToList<T>();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public List<T> ListByCondition(Expression<Func<T, bool>> func, int count)
        {
            return collection.Find(func).Limit(count).ToList();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="func"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="record"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> ListPage(Expression<Func<T, bool>> func, int page, int pageSize, ref long record, SortDefinition<T> sort = null)
        {
            record = collection.Count(func);
            return collection.Find(func).Sort(sort).Skip((page - 1) * pageSize).Limit(pageSize).ToList();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public long Delete(Expression<Func<T, bool>> func)
        {
            return collection.DeleteMany(func).DeletedCount;
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        /// <returns></returns>
        public long DeleteAll()
        {
            return Delete(s => 1 == 1);
        }
    }
}
