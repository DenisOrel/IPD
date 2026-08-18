// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.IDBAVSDocumentObject
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Интерфейс обработчика объектов типа "Спецификация"</summary>
public interface IDBAVSDocumentObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>Установить значения атрибутов</summary>
  /// <param name="valuesList">Значения атрибутов</param>
  /// <param name="deleteNotExistingAttributes">Удалять несуществующие атрибуты</param>
  /// <param name="dontDeleteBlobs">Не удалять Blob-атрибуты</param>
  /// <param name="returnDelta">Вернуть разницу значений атрибутов</param>
  /// <param name="modes">Режимы</param>
  /// <param name="calledFromAVS">Метод вызван из AVS</param>
  /// <returns>Разница значений атрибутов</returns>
  AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    bool calledFromAVS);

  /// <summary>Установить значения атрибутов</summary>
  /// <param name="valuesList">Значения атрибутов</param>
  /// <param name="calledFromAVS">Метод вызван из AVS</param>
  /// <returns>Разница значений атрибутов</returns>
  AttributeValues[] SetAttributesValues(AttributeValues[] valuesList, bool calledFromAVS);
}
