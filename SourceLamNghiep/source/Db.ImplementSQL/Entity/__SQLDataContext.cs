using System.Data.Entity;

namespace Db.ImplementSQL.Entity
{
    internal class SqlDataContext : DbContext
    {
        public SqlDataContext() : base("DbConnectionString") { }

        public DbSet<DataLog> DataLogs { get; set; }

        public DbSet<CustomerVisit> CustomerVisits { get; set; }

        public DbSet<CustomerImage> CustomerImages { get; set; }

        public DbSet<Artice> Artices { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<CategoryImage> CategoryImages { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<BeUser> BeUsers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<AppSetting> AppSettings { get; set; }

        public DbSet<SystemEmailTemplate> SystemEmailTemplates { get; set; }

        public DbSet<RequestPassword> RequestPasswords { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<HtmlContent> HtmlContents { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}
