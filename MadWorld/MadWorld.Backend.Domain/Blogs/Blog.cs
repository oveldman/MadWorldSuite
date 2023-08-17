using System.Net.Mime;
using LanguageExt.Common;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Domain.System;

namespace MadWorld.Backend.Domain.Blogs;

public class Blog
{
    public readonly GuidId Id;
    public readonly DateTime Created;
    
    public Text Title { get; private set; } = null!;
    public Text Writer  { get; private set; } = null!;
    public DateTime Updated { get; private set; }

    [RepositoryPublicOnly]
    public Blog(GuidId id, Text title, Text writer, DateTime created, DateTime updated)
    {
        Id = id;
        Title = title;
        Writer = writer;
        Created = created;
        Updated = updated;
    }
    
    private Blog(GuidId id, Text title, Text writer)
    {
        Id = id;
        Title = title;
        Writer = writer;
        Created = SystemTime.Now();
        Updated = Created;
    }
    
    public static Result<Blog> Parse(string id, string title, string writer)
    {
        var guidIdResult = GuidId.Parse(id);
        var titleResult = Text.Parse(title);
        var writerResult = Text.Parse(writer);

        if (Result.HasFaultyState(
            out var exception,
            guidIdResult.GetValueObjectResult(),
            titleResult.GetValueObjectResult(),
            writerResult.GetValueObjectResult()
        ))
        {
            return new Result<Blog>(exception);
        }

        return new Blog(
            guidIdResult.GetValue(),
            titleResult.GetValue(),
            writerResult.GetValue()
        );
    }

    public void Update(Text title, Text writer)
    {
        Title = title;
        Writer = writer;
        Updated = SystemTime.Now();
    }
}