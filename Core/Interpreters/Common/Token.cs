namespace Renderer.Interpreters.Common;

public class Token
{
    public TokenType Type { get; init; }
    public Token? Next { get; init; }
    public string[]? Args { get; init; }
    
    public Token(TokenType type, Token? next, string[]? args)
    {
        Type = type;
        Next = next;
        Args = args;
    }

    public void GetNext() => ImGuiConverter.GetCallback(Type)(this);
}