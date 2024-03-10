using Antlr4.Runtime;
using TypeCheckerProject;

//ctrl+Z -> enter -> start parser
Main();
//TestTypeChecker();

void Main()
{
    var stellaCode = "";
    while (true)
    {
        var line = Console.ReadLine();
        if (line != null)
        {
            stellaCode += line + "\n";
        }
        else
        {
            break;
        }
    }

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
        Console.Error.WriteLine(ex.Message);
        Environment.Exit(1);
    }
}

void TestTypeChecker()
{
    var stellaCodeTest = @"language core;
extend with #let-bindings;


fn main(n : Nat) -> Nat {
  return let y = let x = 0 in x in x
}";


    var streamTest = CharStreams.fromString(stellaCodeTest);
    var lexerTest = new stellaLexer(streamTest);
    var tokensTest = new CommonTokenStream(lexerTest);
    var parserTest = new stellaParser(tokensTest);

    try
    {
        var visitorTest = new TypeChecker(parserTest);
        var tree = parserTest.program().Accept(visitorTest);
        Console.WriteLine(tree);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        Environment.Exit(1);
    }
}