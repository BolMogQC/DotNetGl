namespace Renderer.Exceptions;

public class UnhandledFileExtension : Exception
{
    public UnhandledFileExtension(string fileExtension) : base($"{fileExtension} are not handled yet") { }
}