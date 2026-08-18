
// Type: Intermech.Runtime.PropertyExceptions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Reflection;


namespace Intermech.Runtime
{
    /// <summary>
    /// Содержит методы для создания исключений о некорретно заполненных свойствах объектов.
    /// </summary>
    public static class PropertyExceptions
    {
      public static InvalidOperationException PropertyNotSetException(
        object propertyOwner,
        string propertyName)
      {
        if (propertyOwner == null)
          throw new ArgumentNullException(nameof (propertyOwner));
        return propertyName != null ? new InvalidOperationException(string.Format("Не заполнено свойство '{1}' у объекта '{0}'. Заполните это свойство перед обращением к объекту.", propertyOwner, (object) propertyName)) : throw new ArgumentNullException(nameof (propertyName));
      }

      public static InvalidOperationException PropertyBadValueException(
        object propertyOwner,
        string propertyName,
        string message)
      {
        if (propertyOwner == null)
          throw new ArgumentNullException(nameof (propertyOwner));
        if (propertyName == null)
          throw new ArgumentNullException(nameof (propertyName));
        if (message == null)
          throw new ArgumentNullException(nameof (message));
        return new InvalidOperationException(string.Format("Свойство '{1}' у объекта '{0}' содержит недопустимое значение. {2}", propertyOwner, (object) propertyName, (object) message));
      }

      public static InvalidOperationException PropertyBadValueException(
        object propertyOwner,
        string propertyName,
        object propertyBadValue)
      {
        return PropertyExceptions.PropertyBadValueException(propertyOwner, propertyName, $"Значение свойства не должно быть равно {propertyBadValue}.");
      }

      [Conditional("DEBUG")]
      private static void CheckPropertyExists(object propertyOwner, string propertyName)
      {
        if (propertyOwner.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty) == (PropertyInfo) null)
          throw new InvalidOperationException(string.Format("Не найдено проверяемое свойство '{1}' у объекта '{0}'.", propertyOwner, (object) propertyName));
      }
    }
}
