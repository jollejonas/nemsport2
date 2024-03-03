using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using nemsport.Models.ProductModels;
using nemsport.Models.UserModels;

namespace nemsport.Data
{
    public class nemsportContext : DbContext
    {
        public nemsportContext (DbContextOptions<nemsportContext> options)
            : base(options)
        {
        }
        public DbSet<Material> Material { get; set; } = default!;
        public DbSet<Brand> Brand { get; set; } = default!;
        public DbSet<Collection> Collection { get; set; } = default!;
        public DbSet<BaseProduct> BaseProduct { get; set; } = default!;
        public DbSet<Club> Club { get; set; } = default!;
        public DbSet<ParentCategory> ParentCategory { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Sport> Sport { get; set; } = default!;
        public DbSet<Option> Option { get; set; } = default!;
        public DbSet<ProductOption> ProductOption { get; set; } = default!;
        public DbSet<ProductVariation> ProductVariation { get; set; } = default!;
        public DbSet<ClubUser> ClubUser { get; set; } = default!;
        public DbSet<ClubProduct> ClubProduct { get; set; } = default!;
        public DbSet<ProductSport> ProductSport { get; set; } = default!;
        public DbSet<ProductCategory> ProductCategory { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ProductMaterial> ProductMaterial { get; set; } = default!;
        public DbSet<nemsport.Models.ProductModels.ProductAttribute> ProductAttribute { get; set; } = default!;
        public DbSet<nemsport.Models.ProductModels.ProductAttributeValue> ProductAttributeValue { get; set; } = default!;
    }
}
