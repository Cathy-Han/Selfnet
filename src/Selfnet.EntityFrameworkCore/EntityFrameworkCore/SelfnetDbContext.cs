using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Selfnet.EntityFrameworkCore
{
    public class SelfnetDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public SelfnetDbContext(DbContextOptions<SelfnetDbContext> options) 
            : base(options)
        {

        }

        /// <summary>
        /// struct func
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //this.RegisterModel(modelBuilder);
            //base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 实体创建处理函数
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void RegisterModel(ModelBuilder modelBuilder)
        {
            //IEnumerable<Type> typesToRegister = this.GetTypesToRegister();
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}
        }
        /// <summary>
        /// 获取当前database下实体mapping,注册模型
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetTypesToRegister(string mappingAssembly)
        {
            var assembly = Assembly.Load(mappingAssembly);
            var typesToRegister = from t in assembly.GetTypes()
                                  where !t.IsAbstract
                                  && !t.IsInterface
                                  && t.IsClass
                                  select t;
            return typesToRegister.ToList();
        }
    }
}
