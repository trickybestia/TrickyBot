using System;

namespace TrickyBot
{
    internal static class ObjectExtensions
    {
        public static void CopyPropertiesFrom(this object target, object source)
        {
            Type type = target.GetType();

            if (type != source.GetType())
            {
                throw new Exception("Target and source type mismatch!");
            }

            foreach (var sourceProperty in type.GetProperties())
            {
                type.GetProperty(sourceProperty.Name)?.SetValue(target, sourceProperty.GetValue(source, null), null);
            }
        }
    }
}
