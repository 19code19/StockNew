namespace Stock.Helpers;

/// <summary>
/// Generic mapper helper for converting between model and entity types
/// </summary>
public static class Mapper
{
    /// <summary>
    /// Maps an object from TSource to TDestination using reflection and property copying
    /// </summary>
    public static TDestination ToEntity<TSource, TDestination>(TSource source)
        where TSource : class
        where TDestination : class, new()
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var destination = new TDestination();
        var sourceProperties = typeof(TSource).GetProperties();
        var destProperties = typeof(TDestination).GetProperties();

        foreach (var sourceProp in sourceProperties)
        {
            if (!sourceProp.CanRead)
                continue;

            var destProp = destProperties.FirstOrDefault(p =>
                p.Name == sourceProp.Name && p.CanWrite);

            if (destProp == null)
                continue;

            try
            {
                var value = sourceProp.GetValue(source);
                if (value != null && destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                {
                    destProp.SetValue(destination, value);
                }
            }
            catch
            {
                // Skip properties that can't be copied
            }
        }

        return destination;
    }
}
