using System.Reflection;

namespace Stock.Helpers;

public static class Mapper
{
    public static TDestination ToEntity<TSource, TDestination>(TSource source)
        where TDestination : new()
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var destination = new TDestination();
        var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name && x.CanWrite);
            if (destinationProperty is null)
            {
                continue;
            }

            var value = sourceProperty.GetValue(source);
            destinationProperty.SetValue(destination, value);
        }

        return destination;
    }
}
