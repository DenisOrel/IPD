
// Type: Intermech.Tools.Data.Sync.DBAttributeSyncHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует сервисный объект, обслуживающий IPS в процессах переноса атрибутов в интегрируемое приложение и обратно.
/// </summary>
public sealed class DBAttributeSyncHelper : IAttributeSyncHelper
{
  private readonly IDBAttributableTypeRef attrTypeRef;

  /// <summary>Создает объект.</summary>
  /// <param name="attributableType">Ссылка на метаданные элемента данных</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на метаданные элемента данных IPS не может быть null</exception>
  public DBAttributeSyncHelper(IDBAttributableTypeRef attributableType)
  {
    this.attrTypeRef = attributableType != null ? attributableType : throw new ArgumentNullException(nameof (attributableType));
  }

  /// <summary>Возвращает ссылку на объект для работы с атрибутами.</summary>
  public IDBAttributableTypeRef AttributableType => this.attrTypeRef;

  /// <summary>
  /// Возвращает имя системы, участвующей в переносе атрибутов. Значение этого свойства используется при формировании журнала.
  /// </summary>
  string IAttributeSyncHelper.ParticipantName => "IPS";

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
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((string) key, false);
      if (attributeType != null && attributeType.AttributeType != FieldTypes.ftSystem)
      {
        IDBAttributeType4 attributeById = this.attrTypeRef.GetAttributableType(sessionKeeper.Session).GetAttributeByID(attributeType.AttributeID, false);
        if (attributeById != null && !DBAttributeHelper.IsReadOnly(this.attrTypeRef, (IDBAttributeType) attributeById) && attributeById.Required != RequiredModes.AutoRequired && DBAttributeHelper.IsSimpleAttributeType((IDBAttributeType) attributeById))
          return DBAttributeHelper.GetBlankValue((IDBAttributeType) attributeById, false);
      }
      return (ValueRecord) null;
    }
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
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType((string) foreignItem.Key, true);
      return DBAttributeHelper.ConvertValue(attributeType, DBAttributeHelper.GetDataType(attributeType), foreignItem.Value);
    }
  }
}
