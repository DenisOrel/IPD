
// Type: Intermech.Tools.Data.Sync.IAttributeSyncHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Позволяет реализовать сервисный объект, обслуживающий перенос атрибутов из одной системы в другую.
/// Используется как на передающей, так и на принимающей стороне.
/// </summary>
public interface IAttributeSyncHelper
{
  /// <summary>
  /// Возвращает имя системы, участвующей в переносе атрибутов. Значение этого свойства используется при формировании журнала.
  /// </summary>
  string ParticipantName { get; }

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
  ValueRecord GetOmittedValue(StringKey key);

  /// <summary>
  /// Этот метод вызывается, когда обслуживаемая система является принимающей стороной для
  /// переносимого атрибута. Он позволяет преобразовать переносимое значение к поддерживаемому типу данных.
  /// </summary>
  /// <param name="nativeItem">Значение атрибута на принимающей стороне</param>
  /// <param name="foreignItem">Значение атрибута на передающей стороне. Это значение подлежит проверке и преобразованию</param>
  /// <returns>Преобразованное значение атрибута</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на значения атрибутов в аргументах метода не должны быть null</exception>
  /// <exception cref="T:System.InvalidCastException">Невозможно преобразовать значение атрибута к поддерживаемому типу данных. Текст исключения содержит подробное описание возникшей проблемы</exception>
  object GetCompatibleValue(ValueRecord nativeItem, ValueRecord foreignItem);
}
