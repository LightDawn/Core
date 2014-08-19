using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Core.Model;
using Core.Model.Interface;

namespace Core.Ef
{
    public class DbContextBase : DbContext, IDbContextBase
    {
        public DbContextBase(string connectionString)
            : base(connectionString)
        {



        }

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserProfile> UserProfiles { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Url> Urls { get; set; }
        //public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var ff = 0;
            //modelBuilder.Entity<User>().HasOptional(x => x.UserProfile)
            //  .WithRequired(x => x.User)
            //  .WillCascadeOnDelete();

            // Database.ExecuteSqlCommand("ALTER TABLE RoleUsers ADD CONSTRAINT uc_Billing UNIQUE(Role_Id)");
            //   // Database.ExecuteSqlCommand("ALTER TABLE RoleUsers ADD CONSTRAINT uc_Delivery UNIQUE(User_Id)");
          
        }


        public new IDbSetBase<T> Set<T>() where T : EntityBase<T>, new()
        {
            return new DBSetBase<T>((this as DbContext).Set<T>());
        }

      

        public void SetContextState<T>(EntityBase<T> entity, EntityState entityState) where T : EntityBase<T> , new()
        {
            var entry = this.Entry(entity);
            switch (entityState)
            {
                case EntityState.Added:
                    {
                        entry.State = System.Data.EntityState.Added;

                        break;
                    }
                case EntityState.Deleted:
                    {
                        entry.State = System.Data.EntityState.Deleted;

                        break;
                    }
                case EntityState.Detached:
                    {
                        entry.State = System.Data.EntityState.Detached;

                        break;
                    }
                case EntityState.Modified:
                    {
                        entry.State = System.Data.EntityState.Modified;

                        break;
                    }
                case EntityState.Unchanged:
                    {
                        entry.State = System.Data.EntityState.Unchanged;

                        break;
                    }

            }
        }

        //public DbEntityEntry<T> Entry<T>(T entity) where T : EntityBase<T>, new()
        //{
        //    return this.Entry(entity);
        //}
    }
}
