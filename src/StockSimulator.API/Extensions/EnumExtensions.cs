namespace StockSimulator.API.Extensions;

public static class EnumExtensions
{
    public static IEnumerable<T> GetFlags<T>(this Enum value) where T : Enum
    {
        foreach (Enum flag in Enum.GetValues(typeof(T)))
        {
            if (value.HasFlag(flag))
            {
                yield return (T)flag;
            }
        }
    }
}