namespace Sharp48.UserInterfaces
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<List<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
        {
            var enumerators = source.Select(e => e.GetEnumerator()).ToArray();
            try
            {
                while (enumerators.All(e => e.MoveNext()))
                {
                    yield return enumerators.Select(e => e.Current).ToList();
                }
            }
            finally
            {
                foreach (var enumerator in enumerators)
                    enumerator.Dispose();
            }
        }
    }
}