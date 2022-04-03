//https://www.meziantou.net/registering-an-application-to-a-uri-scheme-using-net.htm
using mitoSoft.Common.UriSchema;
using mitoSoft.Common.UriSchema.Console;

#pragma warning disable CA1416 // Validate platform compatibility

try
{
    var help = (new ArgHelper(args)).IsAvailable("-help", "/help", "--help", "-h", "--h");
    if (help || args?.Count() == 0)
    {
        Console.WriteLine("Options:");
        Console.WriteLine("  -help|-h\t\tDisplay help.");
        Console.WriteLine("  -path|-p\t\t(required) Enter the executable file.");
        Console.WriteLine("  -schema|-s\t\t(required) Enter the Uri schema.");
        Console.WriteLine("  --override|--o\t(optional) Allow updating the Uri Schema.");
        Console.ReadLine();
        return;
    }

    var path = (new ArgHelper(args)).GetValue("-p", "-path");
    var schema = (new ArgHelper(args)).GetValue("-s", "-schema");
    var @override = (new ArgHelper(args)).IsAvailable("--o", "--override");

    var registered = RegistryHelper.IsRegistered(schema);

    if (!@override && !string.IsNullOrEmpty(registered))
    {
        throw new Exception($"Url-schema '{schema}' already existing. Please use '/override' argument to allow a replacement.");
    }

    RegistryHelper.RegisterScheme(schema, path);

    Console.WriteLine($"Registered: schema={schema}; path={path};");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}