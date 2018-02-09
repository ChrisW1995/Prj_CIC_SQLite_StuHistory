using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_CIC_RegisteStudent.Model
{
    public class StudentContext : DbContext
    {
        public StudentContext()
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<StudentContext>(null);
        }

        public DbSet<student> Students { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
