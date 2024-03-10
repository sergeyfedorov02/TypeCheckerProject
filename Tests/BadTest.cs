namespace Tests;

public class BadTest
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

    private static void TestTypeChecker(string? currentFolder, bool writeAll)
    {
        var resourceFolder = currentFolder is null ? "Tests.Resources.Bad" : $"Tests.Resources.Bad.{currentFolder}";
        var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames()
            .Where(name => name.StartsWith(resourceFolder) && !string.Equals(name, resourceFolder));

        foreach (var resourceName in resourceNames)
        {
            var stellaCode = GetStResource(resourceName);

            var fileFullName = resourceName.Split("Bad.")[1];
            var fileName = fileFullName.Split(".")[1] + ".st";
            var errorName = fileFullName.Split(".")[0];

            var stream = CharStreams.fromString(stellaCode);
            var lexer = new stellaLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new stellaParser(tokens);

            try
            {
                var visitor = new TypeChecker(parser);
                var message = parser.program().Accept(visitor);

                if (message.ToString()!.Length < 6 || !message.ToString()![..5].Equals("ERROR"))
                {
                    Console.Error.WriteLine($"В файле: {fileName}");
                    Console.Error.WriteLine($"Ожидалась ошибка: {errorName}");
                    Console.Error.WriteLine("Но была получена:");
                    Console.Error.WriteLine(message);
                    Console.Error.WriteLine();
                }
                else if (writeAll)
                {
                    Console.Error.WriteLine($"В файле: {fileFullName} - проверка прошла");
                    Console.Error.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message;
                var actualError = exMessage.Split(":")[0];
                if (!actualError.Equals(errorName))
                {
                    Console.Error.WriteLine($"В файле: {fileName}");
                    Console.Error.WriteLine($"Ожидалась ошибка: {errorName}");
                    Console.Error.WriteLine("Но была получена:");
                    Console.Error.WriteLine(exMessage);
                    Console.Error.WriteLine();
                }
                else if (writeAll)
                {
                    Console.Error.WriteLine($"В файле: {fileFullName} - проверка прошла");
                    Console.Error.WriteLine(exMessage);
                }
            }
        }
    }

    [Test]
    public void TestAll()
    {
        TestTypeChecker(null, true);
    }
    
    [Test]
    public void TestAllWithoutPrintingPassed()
    {
        TestTypeChecker(null, false);
    }

    [Test]
    public void TestCurrentFolderByName()
    {
        //write the folder name
        var currentFolderName = "ERROR_UNEXPECTED_PATTERN_FOR_TYPE";
        
        TestTypeChecker(currentFolderName, true);
    }

    [Test]
    public void TestCurrentFolderByIndex()
    {
        var folderNames = new List<string>
        {
            "ERROR_AMBIGUOUS_LIST",
            "ERROR_ILLEGAL_EMPTY_MATCHING",
            "ERROR_MISSING_MAIN",
            "ERROR_MISSING_RECORD_FIELDS",
            "ERROR_NONEXHAUSTIVE_MATCH_PATTERNS",
            "ERROR_NOT_A_FUNCTION",
            "ERROR_NOT_A_LIST",
            "ERROR_NOT_A_RECORD",
            "ERROR_NOT_A_TUPLE",
            "ERROR_TUPLE_INDEX_OUT_OF_BOUNDS",
            "ERROR_UNDEFINED_VARIABLE",
            "ERROR_UNEXPECTED_FIELD_ACCESS",
            "ERROR_UNEXPECTED_INJECTION",
            "ERROR_UNEXPECTED_LAMBDA",
            "ERROR_UNEXPECTED_LIST",
            "ERROR_UNEXPECTED_PATTERN_FOR_TYPE",
            "ERROR_UNEXPECTED_RECORD",
            "ERROR_UNEXPECTED_RECORD_FIELDS",
            "ERROR_UNEXPECTED_TUPLE",
            "ERROR_UNEXPECTED_TUPLE_LENGTH",
            "ERROR_UNEXPECTED_TYPE_FOR_EXPRESSION",
            "ERROR_UNEXPECTED_TYPE_FOR_PARAMETER",
            "ERROR_UNEXPECTED_VARIANT",
            "ERROR_UNEXPECTED_VARIANT_LABEL"
        };
        //choose the folder name by index
        var currentFolder = folderNames[2];
        
        TestTypeChecker(currentFolder, true);
    }
}