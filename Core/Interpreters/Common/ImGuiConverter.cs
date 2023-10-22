using System.Reflection;
using ImGuiNET;

namespace Renderer.Interpreters.Common;

public static class ImGuiConverter
{
    public static Action<Token> GetCallback(TokenType tokenType)
    {
        TypeInfo infos = typeof(ImGuiConverter).GetTypeInfo();
        MethodInfo[] methods = infos.GetMethods();

        foreach (MethodInfo method in methods)
        {
            CallbackAttribute? attr = method.GetCustomAttributes<CallbackAttribute>().FirstOrDefault();
            if (attr != null && attr.Token.Equals(tokenType))
            {
                return (Action<Token>)Delegate.CreateDelegate(typeof(Action<Token>), method);
            }
        }

        throw new ArgumentException($"Token {tokenType} does not have a corresponding callback.");
    }

    [Callback(TokenType.Frame)]
    public static Action<Token> Frame() => token =>
    {
        if (token.Args?[0] is null) throw new ArgumentException("Argument 0 (Frame name) is required");
        ImGui.Begin(token.Args[0], ImGuiWindowFlags.MenuBar);
        
        token.GetNext();
        
        ImGui.End();
    };
}