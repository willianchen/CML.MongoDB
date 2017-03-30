using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDBDemo.Utils.Common;
using MongoDBDemo.Utils.Json;
using MongoDBDemo.Utils.MongoDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化对象 类名保存为MongoDB数据库名称
            MongoDBUtil<PlayerEntity> mgdbUtil = new MongoDBUtil<PlayerEntity>();
            ////清空数据
            mgdbUtil.DeleteAll();
            ////插入一条数据
            //PlayerEntity t = new PlayerEntity() { NId = 1, Name = "曾城", Birthday = DateTime.Parse("1900-1-1"), CountryName = "中国", Club = "广州恒大", Position = "GK" };
            //mgdbUtil.Insert(t);
            ////更新一条数据
            //PlayerEntity t2 = new PlayerEntity() { NId = 1, Name = "曾城update", Birthday = DateTime.Now, CountryName = "中国", Club = "广州恒大", Position = "GK" };
            //mgdbUtil.Update(t2, s => s.NId == 1);
            ////查询一条数据
            //var m = mgdbUtil.GetEntity(s => s.NId == 1);
            //Console.WriteLine(JsonUtil.Serialize(m));
            ////删除一条数据
            //mgdbUtil.Delete(s => s.NId == 1);

            #region
            int cnt = 10000;
            double s1 = StopWatchUtil.CalMilSeconds(() =>
            {
                for (int i = 0; i < cnt; i++)
                {
                    PlayerEntity t = new PlayerEntity() { NId = i, Name = "曾城" + i.ToString(), Birthday = DateTime.Parse("1900-1-1"), CountryName = "中国", Club = "广州恒大", Position = "GK" };
                    mgdbUtil.Insert(t);
                }
            });
            Console.WriteLine("插入数据总共花费{0}ms.", s1);

            double s3 = StopWatchUtil.CalMilSeconds(() =>
            {
                for (int i = 0; i < cnt; i++)
                {
                    PlayerEntity t = new PlayerEntity() { NId = i, Name = "曾城update" + i.ToString(), Birthday = DateTime.Now, CountryName = "中国", Club = "广州恒大", Position = "GK" };
                    mgdbUtil.Update(t, s => s.NId == i);
                }
            });
            Console.WriteLine("更新数据总共花费{0}ms.", s3);


            double s4 = StopWatchUtil.CalMilSeconds(() =>
            {
                var list = mgdbUtil.ListByCondition(s => 1 == 1, cnt);

                var res = JsonUtil.Serialize(list);
                //Console.WriteLine(res);
            });
            Console.WriteLine("获取数据总共花费{0}ms.", s4);

            //double s2 = StopWatchUtil.CalMilSeconds(() =>
            //{
            //    mgdbUtil.DeleteAll();
            //});
            //Console.WriteLine("清空数据总共花费{0}ms.", s2);
            #endregion
            Console.WriteLine("输入任何键结束……");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 测试实体类型
    /// </summary>
    public class PlayerEntity : BaseEntity
    {
        public int NId { get; set; }
        public string Name { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Birthday { get; set; }

        public string CountryName { get; set; }

        public string Club { get; set; }

        public string Position { get; set; }

    }
}
