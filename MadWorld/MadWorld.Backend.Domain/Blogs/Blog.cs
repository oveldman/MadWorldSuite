using LanguageExt.Common;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Domain.System;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Backend.Domain.Blogs;

public class Blog : IValueObject
{
    public readonly GuidId Id;
    public readonly DateTimeUtc Created;
    
    public Text Title { get; private set; }
    public Text Writer  { get; private set; }
    public DateTimeUtc Updated { get; private set; }
    public bool IsDeleted { get; private set; }

    [RepositoryPublicOnly]
    public Blog(GuidId id, Text title, Text writer, DateTimeUtc created, DateTimeUtc updated, bool isDeleted)
    {
        Id = id;
        Title = title;
        Writer = writer;
        Created = created;
        Updated = updated;
        IsDeleted = isDeleted;
    }
    
    private Blog(GuidId id, Text title, Text writer)
    {
        Id = id;
        Title = title;
        Writer = writer;
        Created = DateTimeUtc.Now();
        Updated = Created;
        IsDeleted = false;
    }

    public static Result<Blog> Parse(string title, string writer)
    {
        var id = Guid.NewGuid().ToString();
        return Parse(id, title, writer);
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

    public bool IsThirtyDaysOld()
    {
        var now = (DateTime)DateTimeUtc.Now();
        var thirtyDaysAgo = now.AddDays(-30);
        
        var updated = (DateTime)Updated;

        return updated > thirtyDaysAgo;
    }

    public void Update(Text title, Text writer)
    {
        Title = title;
        Writer = writer;
        Updated = DateTimeUtc.Now();
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        Updated = DateTimeUtc.Now();
    }
    
    public BlogContract ToContract()
    {
        return new BlogContract()
        {
            Id = Id,
            Title = Title,
            Writer = Writer,
            Created = Created,
            Updated = Updated
        };
    }
    
    public BlogDetailContract ToDetailContract()
    {
        return new BlogDetailContract()
        {
            Id = Id,
            Title = Title,
            Writer = Writer,
            Created = Created,
            Updated = Updated,
        };
    }
}