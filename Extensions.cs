public static class TaskExtensions
{
    public static Task WaitAll(this IEnumerable<Task> tasks)
        => Task.WhenAll(tasks.ToArray());
}