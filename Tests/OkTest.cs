namespace Tests;

public class OkTest
{
    private static string GetStResource(string resourceName)
    {
        return System.Text.Encoding.UTF8.GetString(GetResource(resourceName));
    }

    private static byte[] GetResource(string resourcePath)
    {
        using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);

        if (resourceStream == null)
        {
            return null;
        }

        using var memStream = new MemoryStream();

        resourceStream.CopyTo(memStream);
        var key = memStream.ToArray();
        return key;
    }

    [Test]
    public void Test()
    {
        const string resourceFolder = "Tests.Resources.Ok";
        var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames()
            .Where(name => name.StartsWith(resourceFolder) && !string.Equals(name, resourceFolder));

        foreach (var resourceName in resourceNames)
        {
            var stellaCode = GetStResource(resourceName);
            var fileName = resourceName.Split("Ok.")[1];
            
            var stream = CharStreams.fromString(stellaCode);
            var lexer = new stellaLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new stellaParser(tokens);

            try
            {
                var visitor = new TypeChecker(parser);
                parser.program().Accept(visitor);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"В файле: {fileName}");
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine();
            }
        }
    }
}