using Renderer.Exceptions;
using Renderer.Interfaces;
using Renderer.Interpreters.XML;

namespace Renderer.Interpreters;

public static class InterpreterFactory
{
    /// <summary>
    /// Create an interpreter for the provided file.
    ///
    /// Handled files:
    /// - XML
    /// </summary>
    /// <param name="filePath">The file to read and interpret</param>
    /// <returns>An <see cref="IInterpreter"/> corresponding to the provided file</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist</exception>
    /// <exception cref="UnhandledFileExtension">Thrown if the file does not have a corresponding <see cref="IInterpreter"/></exception>
    public static IInterpreter Create(string filePath)
    {
        if (!Path.Exists(filePath)) throw new FileNotFoundException("File not fount", filePath);
        
        string extension = Path.GetExtension(filePath);

        return extension switch
        {
            ".xml" => new XmlInterpreter(filePath),
            _ => throw new UnhandledFileExtension(extension)
        };
    }
}