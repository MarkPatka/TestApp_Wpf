using System.IO;
using TestApp_Wpf.Models.DomainModels;
using TestApp_Wpf.Services.Parsing.Interfaces;

namespace TestApp_Wpf.Services.Parsing.ParsedModels;

public class TestObjectsParsedResult : IParsedFile
{
    private readonly List<TestObject> _parsedObjects;

    public TestObjectsParsedResult(IEnumerable<TestObject> parsedObjects)
    {
        _parsedObjects = [.. parsedObjects];
    }

    public IReadOnlyList<TestObject> TestObjects => _parsedObjects.AsReadOnly();

    public string ContentType => throw new NotImplementedException();

    public string FileName => throw new NotImplementedException();

    public string Path => throw new NotImplementedException();

    public long Length => throw new NotImplementedException();

    public void CopyTo(Stream target)
    {
        throw new NotImplementedException();
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Stream OpenReadStream()
    {
        throw new NotImplementedException();
    }
}

