using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ClassVsStructRecordBenchmark>();

[MemoryDiagnoser]
[Config(typeof(StyleConfig))]
public class ClassVsStructRecordBenchmark
{
    private const string title = "New Blog Post";
    private const string content = "New Blog Post";

    [Params(1, 100, 1000, 10000)]
    public int MAX_CREATION { get; set; }

    [Benchmark(Baseline = true)]
    public void ClassObject()
    {
        for (int i = 0; i < MAX_CREATION; i++)
        {
            _ = new ClassBlog()
            {
                Id = 1,
                Title = title,
                Content = content,
                Category = BlogCategory.Automotive
            };
        }
    }

    [Benchmark]
    public void Struct()
    {
        for (int i = 0; i < MAX_CREATION; i++)
        {
            _ = new StructBlog()
            {
                Id = 1,
                Title = title,
                Content = content,
                Category = BlogCategory.Automotive
            };
        }
    }

    [Benchmark]
    public void Record()
    {
        for (int i = 0; i < MAX_CREATION; i++)
        {
            _ = new RecordBlog
            {
                Id = 1,
                Title = title,
                Content = content,
                Category = BlogCategory.Automotive
            };
        }
    }
}

public class ClassBlog
{
    public int Id { get; init; }

    public string Title { get; init; }

    public string Content { get; init; }

    public BlogCategory Category { get; init; }
}

public struct StructBlog
{
    public int Id { get; init; }

    public string Title { get; init; }

    public string Content { get; init; }

    public BlogCategory Category { get; init; }
}

public record RecordBlog
{
    public int Id { get; init; }

    public string Title { get; init; }

    public string Content { get; init; }

    public BlogCategory Category { get; init; }
}

public enum BlogCategory
{
    Automotive,
    Fishing,
    Reading,
    Cooking
}


/// <summary>
/// Style config for baseline summary style
/// </summary>
public class StyleConfig : ManualConfig
{
    public StyleConfig()
    {
        SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
    }
}
