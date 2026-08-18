// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.IDocumentTypeWeight
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Интерфейс позволяет получить описание и "вес" для типов объектов-документов</summary>
public interface IDocumentTypeWeight
{
  /// <summary>Отыскать корневой тип объекта-документа для текущего узла</summary>
  DocumentTypeWeight RootDocumentType { get; }

  /// <summary>Отыскать корневую коллекцию</summary>
  DocumentTypeWeightCollection RootCollection { get; }

  /// <summary>Получить значение "веса" указанного типа объекта-документа</summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Значение "веса" или DocumentTypeWeight.UndefinedWeight,
  /// если тип объекта не найден, либо значение "веса" неопределено</returns>
  long GetWeight(int docTypeID);

  /// <summary>Получить значение "веса" указанного типа объекта-документа</summary>
  /// <param name="docTypeGuid">Guid типа объекта-документа</param>
  /// <returns>Значение "веса" или DocumentTypeWeight.UndefinedWeight,
  /// если тип объекта не найден, либо значение "веса" неопределено</returns>
  long GetWeight(Guid docTypeGuid);

  /// <summary>Отыскать описание указанного типа объекта-документа</summary>
  /// <param name="docTypeID">Идентификатор типа объекта-документа</param>
  /// <returns>Описание указанного типа объекта-документа или null</returns>
  DocumentTypeWeight FindDocumentType(int docTypeID);

  /// <summary>Отыскать описание указанного типа объекта-документа</summary>
  /// <param name="docTypeGuid">Guid типа объекта-документа</param>
  /// <returns>Описание указанного типа объекта-документа или null</returns>
  DocumentTypeWeight FindDocumentType(Guid docTypeGuid);

  /// <summary>Выполнить автоматический пересчёт "весов"</summary>
  /// <param name="startWeight">Стартовое значение "веса"</param>
  /// <param name="delta">Приращение "веса" для каждого элемента</param>
  /// <returns>Следующее значение "веса" (с учётом того, что "веса" были назначены всей дочерней иерархии
  /// типов объектов-документов)
  /// </returns>
  long UpdateWeights(long startWeight, int delta);
}
