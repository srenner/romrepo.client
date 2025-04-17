using Microsoft.EntityFrameworkCore;
using romrepo.lib.Models;
using Microsoft.EntityFrameworkCore.Sqlite;
using SQLitePCL;

namespace romrepo.lib
{
    public class RomRepoContext : DbContext
    {
        public DbSet<Core> Core { get; set; }
        public DbSet<Rom> Rom { get; set; }
        public DbSet<SystemSetting> SystemSetting { get; set; }
        public DbSet<JobQueue> JobQueue { get; set; }

        public string DbPath { get; }

        //Using experimental .NET code for AOT compilation. How exciting!
        #pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
        #pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
        public RomRepoContext()
        {
            Batteries.Init();

            string dbName = "romrepo.client.db";
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                string subfolder = "\\RomRepo";
                if (!Directory.Exists(path + subfolder))
                {
                    Directory.CreateDirectory(path + subfolder);
                }
                DbPath = System.IO.Path.Join(path + subfolder + "\\", dbName);
                this.Database.EnsureCreated();
            }
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(" *** CRITICAL ERROR *** ");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
        #pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
        #pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
