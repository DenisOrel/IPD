
// Type: Intermech.Tools.Data.Sync.AppAttributeSyncHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует сервисный объект, обслуживающий интегрируемое приложение в процессах переноса атрибутов в IPS и обратно.
/// </summary>
public sealed class AppAttributeSyncHelper : IAttributeSyncHelper
{
  private readonly bool isOpenMetadata;

  /// <summary>Создает объект.</summary>
  /// <param name="isOpenMetadata">Признак, поддерживает ли документ приложения добавление новых произвольных значений атрибутов</param>
  public AppAttributeSyncHelper(bool isOpenMetadata) => this.isOpenMetadata = isOpenMetadata;

  /// <summary>
  /// Возвращает имя системы, участвующей в переносе атрибутов. Значение этого свойства используется при формировании журнала.
  /// </summary>
  string IAttributeSyncHelper.ParticipantName => "Приложение";

  /// <summary>
  /// Этот метод вызывается при отсутствии переносимого атрибута. Если атрибут отсутствует
  /// по причине невозможности его существования, то метод должен вернуть null. Иначе
  /// атрибут расценивается как необязательный, в этом случае метод должен вернуть
  /// начальное значение атрибута (значение, которым необязательный атрибут инициализируется
  /// при создании.
  /// </summary>
  /// <param name="key">Ключ переносимого атрибута</param>
  /// <returns>Начальное значение атрибута, если это необязательный атрибут, или null</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на ключ переносимого атрибута не может быть null</exception>
  ValueRecord IAttributeSyncHelper.GetOmittedValue(StringKey key)
  {
    if (key == (StringKey) null)
      throw new ArgumentNullException(nameof (key));
    return !this.isOpenMetadata ? (ValueRecord) null : new ValueRecord(key, (object) string.Empty);
  }

  /// <summary>
  /// Этот метод вызывается, когда обслуживаемая система является принимающей стороной для
  /// переносимого атрибута. Он позволяет преобразовать переносимое значение к поддерживаемому типу данных.
  /// </summary>
  /// <param name="nativeItem">Значение атрибута на принимающей стороне</param>
  /// <param name="foreignItem">Значение атрибута на передающей стороне. Это значение подлежит проверке и преобразованию</param>
  /// <returns>Преобразованное значение атрибута</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на значения атрибутов в аргументах метода не должны быть null</exception>
  /// <exception cref="T:System.InvalidCastException">Невозможно преобразовать значение атрибута к поддерживаемому типу данных. Текст исключения содержит подробное описание возникшей проблемы</exception>
  object IAttributeSyncHelper.GetCompatibleValue(ValueRecord nativeItem, ValueRecord foreignItem)
  {
    if (nativeItem == null)
      throw new ArgumentNullException(nameof (nativeItem));
    if (foreignItem == null)
      throw new ArgumentNullException(nameof (foreignItem));
    if (foreignItem.DataType == nativeItem.DataType)
      return foreignItem.Value;
    if (foreignItem.IsNull)
    {
      if (!nativeItem.DataType.IsClass && !nativeItem.DataType.IsInterface)
        throw new InvalidCastException($"Невозможно преобразовать значение атрибута '{foreignItem.Key}' = '{foreignItem.Value}' к типу '{nativeItem.DataType}'.");
      return (object) null;
    }
    if (!(nativeItem.DataType == typeof (string)))
      return Convert.ChangeType(foreignItem.Value, nativeItem.DataType);
    if (foreignItem.DataType == typeof (bool))
      return this.ConvertBoolToString(foreignItem);
    return foreignItem.DataType == typeof (DateTime) ? this.ConvertDateTimeToString(foreignItem) : (object) foreignItem.Value.ToString();
  }

  private object ConvertBoolToString(ValueRecord foreignItem)
  {
    return !(bool) foreignItem.Value ? (object) Consts.NoValue : (object) Consts.YesValue;
  }

  private object ConvertDateTimeToString(ValueRecord foreignItem)
  {
    DateTime dateTime = (DateTime) foreignItem.Value;
    return !(dateTime.TimeOfDay == TimeSpan.Zero) ? (object) dateTime.ToString() : (object) dateTime.ToString("d");
  }
}
