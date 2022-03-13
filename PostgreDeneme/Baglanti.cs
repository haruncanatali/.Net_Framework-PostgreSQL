using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreDeneme
{
    public class Baglanti:DbContext
    {
        public DbSet<Tbl_Ogrenci> Tbl_Ogrenci { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           //modelBuilder.Entity<Tbl_Ogrenci>().ToTable("tbl_ogrenci", "public");
            modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
