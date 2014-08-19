using Core.Ef.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Core.Tests.Core.Repository
{
    public class FakeDbContext : IDbContext //where T : class , new()
    {
        public FakeDbContext()
        {
            Users = new TypesWithIdDbSet<User>();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IDbSet<T> Set<T>() where T : class , new() 
        {
            //throw new NotImplementedException();
            IDbSet<T> rtVal = null;
            switch (typeof(T).Name)
            {
                case "User":
                    //var loc = new DbSet<User>
                    rtVal = this.Users as IDbSet<T>;//as DbSet<typeof(User)>> ;
                    break;
                case "Role":
                    rtVal = this.Roles as IDbSet<T>;
                    break;
                case "UserProfile":
                    rtVal = this.UserProfiles as IDbSet<T>;
                    break;
            }
            return rtVal;
        }

        public DbSet Set(Type entityType)
        {
            throw new NotImplementedException();
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Role> Roles { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

    }
}
