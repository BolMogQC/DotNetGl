using System.Xml;
using Renderer.Interfaces;
using Renderer.Interpreters.Common;

namespace Renderer.Interpreters.XML;

public class XmlInterpreter : IInterpreter, IDisposable, IAsyncDisposable
{
    private readonly FileStream stream;
    private readonly List<Token> tokens = new();
    
    public XmlInterpreter(string filePath)
    {
        stream = File.OpenRead(filePath);;
    }

    public void Parse()
    {
        Token? tokenBuffer = null;
        using (var reader = new XmlTextReader(stream))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    tokenBuffer = ParseXmlElement(reader, tokenBuffer);
                }
            }
        }
        
        foreach (Token token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Args: {string.Join(", ", token.Args ?? Array.Empty<string>())}");
        }
    }

    public void Render()
    {
        tokens.First().GetNext();
    }

    private Token ParseXmlElement(XmlReader reader, Token? parentToken)
    {
        var type = Enum.Parse<TokenType>(reader.Name);
        string[]? args = null;

        if (reader.HasAttributes)
        {
            args = new string[reader.AttributeCount];
            for (int i = 0; i < reader.AttributeCount; i++)
            {
                reader.MoveToAttribute(i);
                args[i] = reader.Value;
            }
        }

        var token = new Token(type, parentToken, args);
        tokens.Add(token);
        return token;
    }

    public void Dispose() => stream.Dispose();
    public async ValueTask DisposeAsync() => await stream.DisposeAsync();
}