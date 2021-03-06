﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MvcTemplate.Data.Core;
using MvcTemplate.Objects;
using MvcTemplate.Tests.Objects;
using System.Linq;

namespace MvcTemplate.Tests.Data
{
    public class TestingContext : Context
    {
        #region Tests

        protected DbSet<TestModel> TestModel { get; set; }

        #endregion

        static TestingContext()
        {
            using (TestingContext context = new TestingContext())
                context.Database.Migrate();
        }
        public TestingContext() : base(ConfigurationFactory.Create())
        {
        }

        public void DropState()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries().ToArray())
                entry.State = EntityState.Detached;
        }
        public void DropData()
        {
            RemoveRange(Set<RolePermission>());
            RemoveRange(Set<Permission>());
            RemoveRange(Set<Account>());
            RemoveRange(Set<Role>());

            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Config["Data:TestConnection"]);
        }
    }
}
