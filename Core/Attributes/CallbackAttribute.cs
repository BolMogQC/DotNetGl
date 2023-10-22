namespace Renderer.Interpreters.Common;

[AttributeUsage(AttributeTargets.Method)]
public class CallbackAttribute : Attribute
{
    public TokenType Token { get; init; }
    public CallbackAttribute(TokenType token)
    {
        Token = token;
    }
}