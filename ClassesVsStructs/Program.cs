
Console.WriteLine("Hello, World of Structs!!");

/**
 * Reference variables
 */ 
Post postA = new()
{
    Id = 1,
    Title = "What are classes?",
    Content = "Something about classes",
    Category = BlogCategory.Automotive
};

Post postB = postA;

postA.Title = "Let's talk about classes!";

Console.WriteLine($"PostA: {postA.Title}");
Console.WriteLine($"PostA Reference: {postB.Title}");

Post postC = new()
{
    Id = 2,
    Title = "Where are classes stored?",
    Content = "Classes are stored...",
    Category = BlogCategory.Automotive
};


/**
 * structs and value variables
 */
StructPost postD = new()
{
    Id = 3,
    Title = "What are structs?",
    Content = "Something about structs",
    Category = BlogCategory.Automotive
};

StructPost postE = postD;

PostHelper.CompareStructPostTitles(ref postD, ref postE);

postE.Title = "Updated Title";

PostHelper.CompareStructPostTitles(ref postD, ref postE);


/**
 * C# 10 syntax for copying with modification on structs
 */ 
StructPost postF = postE with { Title = "The title of post F" };

PostHelper.CompareStructPostTitles(ref postE, ref postF);

/**
 * Cannot use 
 * 
 */
// cannot do this  because they are not records
// Post postG = postA with { Id = 4, Title = "AnotherTest" };

/**
 * Record struct equality overrides and comparisons
 */ 

ReadonlyStructPost postG = new()
{
    Id = 4,
    Title = "What are readonly structs?",
    Content = "Something about structs",
    Category = BlogCategory.Automotive
};

ReadonlyStructPost postH = postG with { Id = 4, Title = "See, I changed the title but overrode the equality operator." };
PostHelper.CompareRecordStructs(ref postG, ref postH);

ReadonlyStructPost postI = postG with { Id = 5 };
PostHelper.CompareRecordStructs(ref postG, ref postI);

public class Post
{
    public required int Id { get; init; }

    public required string Title { get; set; }

    public required string Content { get; init; }

    public required BlogCategory Category { get; init; }

    public void PrintTitle()
    {
        Console.WriteLine($"Post {Id}: Title: {Title}");
    }
}

/// <summary>
/// Struct
/// 
/// 1. Can't inherit from other classes or structures
/// 2. Can't be the base of a class
/// 3. Can implement interfaces
/// 4. Primarily kept on the stack
/// 5. Can't declare finalizer within a struct
/// 6. Prior to C# 11 must initialize all instance fields of a type
/// 7. Prior to C# 10 yo ucan't declare a parameterless constructor
/// 8. Structures are passed by value, but you can use ref to indicate it should be passed by reference
/// 9. Starting with 7.2 you can declare structures as reference type with the ref keyword
/// 
/// </summary>
public struct StructPost
{
    public required int Id { get; init; }

    public required string Title { get; set; }

    public required string Content { get; init; }

    public required BlogCategory Category { get; init; }

    public void PrintTitle()
    {
        Console.WriteLine($"Post {Id}: Title: {Title}");
    }
}

/// <summary>
/// Adding readonly here to make the struct full immutable out side it's constructor.
/// Notice the : IEquatable<ReadonlyStructPost> as it gives an interface to the ReadonlyStructPost
/// </summary>
public readonly record struct ReadonlyStructPost : IEquatable<ReadonlyStructPost>
{
    public required int Id { get; init; }

    public required string Title { get; init; } // {get; set;} will not work because we are readonly.

    public required string Content { get; init; }

    public required BlogCategory Category { get; init; }

    public void PrintTitle()
    {
        Console.WriteLine($"Post {Id}: Title: {Title}");
    }

    /// <summary>
    /// How to override the equality operator for record structs.
    /// Note: you cannot declare public override bool Equals(), that will result in an error
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ReadonlyStructPost other)
    {
        return Id == other.Id;
    }
}

public enum BlogCategory
{
    Automotive,
    Fishing,
    Reading,
    Cooking
}

/// <summary>
/// Display and modify helpers
/// </summary>
public class PostHelper
{
    /// <summary>
    /// Modify the reference struct post
    /// </summary>
    /// <param name="postA"></param>
    /// <param name="updatedTitle"></param>
    public static void ModifyStructPostReference(ref StructPost postA, string updatedTitle)
    {
        postA.Title = updatedTitle;
    }

    /// <summary>
    /// Do not modify the reference
    /// </summary>
    /// <param name="postA"></param>
    /// <param name="updatedTitle"></param>
    public static void ModifyStructNoReference(StructPost postA, string updatedTitle)
    {
        postA.Title = updatedTitle;
    }

    /// <summary>
    /// Print structs for title comparisons
    /// </summary>
    /// <param name="post1"></param>
    /// <param name="post2"></param>
    public static void CompareStructPostTitles(ref StructPost post1, ref StructPost post2)
    {
        Console.WriteLine($"Post1: {post1.Title}");
        Console.WriteLine($"Post2: {post2.Title}");
    }

    /// <summary>
    /// Compare post titles
    /// </summary>
    /// <param name="post1"></param>
    /// <param name="post2"></param>
    public static void ComparePostClassTitles(Post post1, Post post2)
    {
        Console.WriteLine($"Post1: {post1.Title}");
        Console.WriteLine($"Post2: {post2.Title}");
    }

    public static void CompareRecordStructs(ref ReadonlyStructPost post1,ref ReadonlyStructPost post2)
    {
        bool areEqual = post1 == post2;

        if (areEqual)
        {
            Console.WriteLine($"Post \"{post1.Title}\" is equal to Post \"{post2.Title}\"");
        }
        else
        {
            Console.WriteLine($"Post \"{post1.Title}\" is not equal to Post \"{post2.Title}\"");
        }
    }
}