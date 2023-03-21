using System;
using System.Linq;
using System.Reflection;
using DbUp;

public class Program
{
    static Program()
    {
        
    }

    public static int Main(string[] args)
    {
        var connectionString =
            args.FirstOrDefault() 
            ?? "Server=localhost,1443;Database=falcata-local;User Id=sa;Password=MyLocalDatabase-Falcata;TrustServerCertificate=true";

        EnsureDatabase.For.SqlDatabase(connectionString);
        
        var upgrader =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            Console.ReadLine();
#endif                
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }
}