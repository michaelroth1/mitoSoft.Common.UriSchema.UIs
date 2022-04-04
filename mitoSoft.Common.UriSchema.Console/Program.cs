using mitoSoft.Common.UriSchema;
using System.CommandLine;

// Create some options:
var pathArg = new Option<string>(new string[] { "-path", "-p" }, "Enter the executable file.");
pathArg.IsRequired = true;
var schemaArg = new Option<string>(new string[] { "-schema", "-s" }, "Enter the Uri schema.");
schemaArg.IsRequired = true;
var updateArg = new Option<bool>(new string[] { "--override", "--o" }, () => false, "Enter the Uri schema.");

// Add the options to a root command:
var rootCommand = new RootCommand
{
    pathArg,
    schemaArg,
    updateArg,
};

rootCommand.Description = "Register an Uri-schema to allow the start of an executable via a url.";

rootCommand.SetHandler((string path, string schema, bool update) =>
{
    try
    {
        var registered = RegistryHelper.IsRegistered(schema);

        if (!update && !string.IsNullOrEmpty(registered))
        {
            throw new Exception($"Url-schema '{schema}' already existing. Please use '--override' argument to allow a replacement.");
        }

        RegistryHelper.RegisterScheme(schema, path);

        Console.WriteLine($"Registered: schema='{schema}'; path='{path}';");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}, pathArg, schemaArg, updateArg);

// Parse the incoming args and invoke the handler
rootCommand.Invoke(args);