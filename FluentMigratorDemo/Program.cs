using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using Cocona;

namespace FluentMigratorDemo
{
    #region online Example of configuration 
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var serviceProvider = CreateServices();

    //        // Put the database update into a scope to ensure
    //        // that all resources will be disposed.
    //        using (var scope = serviceProvider.CreateScope())
    //        {
    //            UpdateDatabase(scope.ServiceProvider);
    //        }
    //    }

    //    /// <summary>
    //    /// Configure the dependency injection services
    //    /// </summary>
    //    private static IServiceProvider CreateServices()
    //    {
    //        return new ServiceCollection()
    //            // Add common FluentMigrator services
    //            .AddFluentMigratorCore()
    //            .ConfigureRunner(rb => rb
    //                // Add SQLite support to FluentMigrator
    //                .AddSqlServer()
    //                // Set the connection string
    //                .WithGlobalConnectionString("Server=.\\SQLEXPRESS;Database=FluentMigratorDemo;Trusted_Connection=True;")
    //                // Define the assembly containing the migrations
    //                .ScanIn(typeof(Tables).Assembly).For.Migrations())
    //            // Enable logging to console in the FluentMigrator way
    //            .AddLogging(lb => lb.AddFluentMigratorConsole())
    //            // Build the service provider
    //            .BuildServiceProvider(false);
    //    }

    //    /// <summary>
    //    /// Update the database
    //    /// </summary>
    //    private static void UpdateDatabase(IServiceProvider serviceProvider)
    //    {
    //        // Instantiate the runner
    //        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    //        // Execute the migrations
    //        runner.MigrateUp();
    //    }
    //}
    #endregion

    #region interact with cmd for migration
    //class Program
    //{
    //    private static async Task Main(string[] args)
    //    {
    //        var upOption = new Option("--up", "Migrate Up");
    //        upOption.Argument = new Argument<bool>(() => false);
    //        var downOption = new Option("--down", "Rollback database to a version");
    //        downOption.Argument = new Argument<long>(() => -1);

    //        var rootCommand = new RootCommand("NewValley Fluent Migrator Runner");
    //        rootCommand.AddOption(upOption);
    //        rootCommand.AddOption(downOption);
    //        rootCommand.Handler = CommandHandler.Create<bool, long>((up, down) =>
    //        {
    //            var serviceProvider = CreateServices();

    //            using (var scope = serviceProvider.CreateScope())
    //            {
    //                if (up)
    //                    UpdateDatabase(scope.ServiceProvider);

    //                if (down > -1)
    //                    RollbackDatabase(scope.ServiceProvider, down);
    //            }
    //        });

    //        await rootCommand.InvokeAsync(args);
    //    }

    //    public static IConfigurationRoot Configuration { get; set; }

    //    /// <summary>
    //    /// Configure the dependency injection services
    //    /// </sumamry>
    //    private static IServiceProvider CreateServices()
    //    {
    //        var builder = new ConfigurationBuilder()
    //           .SetBasePath(Directory.GetCurrentDirectory())
    //           .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

    //        //configuration has an hashtable with key, value pairs
    //        Configuration = builder.Build();
    //        var connectionString = Configuration["connectionString"];

    //        return new ServiceCollection()
    //            // Add common FluentMigrator services
    //            .AddFluentMigratorCore()
    //            .ConfigureRunner(rb => rb
    //                // Add Postgres support to FluentMigrator
    //                .AddSqlServer()
    //                // Set the connection string
    //                .WithGlobalConnectionString(connectionString)
    //                // Define the assembly containing the migrations
    //                .ScanIn(typeof(Tables).Assembly).For.Migrations())
    //            // Enable logging to console in the FluentMigrator way
    //            .AddLogging(lb => lb.AddFluentMigratorConsole())
    //            // Build the service provider
    //            .BuildServiceProvider(false);
    //    }

    //    private static void UpdateDatabase(IServiceProvider serviceProvider)
    //    {
    //        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    //        runner.MigrateUp();
    //    }

    //    private static void RollbackDatabase(IServiceProvider serviceProvider, long rollbackVersion)
    //    {
    //        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    //        runner.MigrateDown(rollbackVersion);
    //    }
    //}
    #endregion

    #region interact with cmd Using Cocona
    class Program
    {
        private static async Task Main(string[] args)
        {
            var app = CoconaLiteApp.Create();

            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                app.AddCommand("up", () => UpdateDatabase(scope.ServiceProvider))
                    .WithDescription("Migrate Database Up!");

                app.AddCommand("down", ([Argument] int value) =>
                {
                    if (value > -1) RollbackDatabase(scope.ServiceProvider, value);
                }).WithDescription("Migrate Database down to specific value");
            }
            app.Run();
        }
        public static IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Configure the dependency injection services
        /// </sumamry>
        private static IServiceProvider CreateServices()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            //configuration has an hashtable with key, value pairs
            Configuration = builder.Build();
            var connectionString = Configuration["connectionString"];

            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add Postgres support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Tables).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = ServiceProviderServiceExtensions.GetRequiredService<IMigrationRunner>(serviceProvider);
            runner.MigrateUp();
        }

        private static void RollbackDatabase(IServiceProvider serviceProvider, long rollbackVersion)
        {
            var runner = ServiceProviderServiceExtensions.GetRequiredService<IMigrationRunner>(serviceProvider);
            runner.MigrateDown(rollbackVersion);
        }
    }
    #endregion
}