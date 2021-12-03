namespace Aoc2021.Library
{
    public static class Extensions
    {
        public static int ToInt(this string i)
        {
            return int.Parse(i);
        }

        public static T PopAt<T>(this List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }

        public static T? PopLast<T>(this List<T> list)
        {
            T? r = list.LastOrDefault();
            list.RemoveAt(list.Count - 1);
            return r;
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> enumerable) where T : class
        {
            return enumerable.Where(x => x != null).Select(s => s!);
        }
    }
}
