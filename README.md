# CML.MongoDB
C#封装MongoDB工具类库

### 什么是MongoDB
> MongoDB 是由C++语言编写的，是一个基于分布式文件存储的开源数据库系统。
> 在高负载的情况下，添加更多的节点，可以保证服务器性能。
> MongoDB 旨在为WEB应用提供可扩展的高性能数据存储解决方案。
> MongoDB 将数据存储为一个文档，数据结构由键值(key=>value)对组成。MongoDB 文档类似于 JSON 对象。字段值可以包含其他文档，数组及文档数组。
### MongoDB安装
  这里是详细的安装说明(http://www.cnblogs.com/lzrabbit/p/3682510.html) 
### 用例   
-----------------------------------  
    //实例化对象 类名保存为MongoDB表名称
    MongoDBUtil<PlayerEntity> mgdbUtil = new MongoDBUtil<PlayerEntity>();
    //清空数据
    mgdbUtil.DeleteAll();
    //插入一条数据
    PlayerEntity t = new PlayerEntity() { NId = 1, Name = "曾城", Birthday = DateTime.Parse("1900-1-1"), CountryName = "中国", Club = "广州恒大", Position = "GK" };
    mgdbUtil.Insert(t);
    //更新一条数据
    PlayerEntity t2 = new PlayerEntity() { NId = 1, Name = "曾城update", Birthday = DateTime.Now, CountryName = "中国", Club = "广州恒大", Position = "GK" };
    mgdbUtil.Update(t2, s => s.NId == 1);
    //查询一条数据
    var m = mgdbUtil.GetEntity(s => s.NId == 1);
    Console.WriteLine(JsonUtil.Serialize(m));
    //删除一条数据
    mgdbUtil.Delete(s => s.NId == 1);
    
### 引用 
  MongoDB.Driver<br >
  MongoDB.Bson<br >
  MongoDB.Driver.Core
  
### 备注

> 目前数据Id自增采用MongoDB默认ObjectId，暂未实现自定义自增解决方案

> MongoDB 日期类型保存为UTC格式，<br/>
> 例：本地时间（8时区）2017-03-30 15:00：00 MongoDB保存为（格林尼治时间）：ISODate("2017-03-30T07:00:00.000Z")<br/>
> 相差8小时

### 测试结果
![github](https://raw.githubusercontent.com/willianchen/CML.MongoDB/master/testmgdb.png "github")  
