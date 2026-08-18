// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IDraftDocumentsIdCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс контейнера метаданных, относящихся к черновикам документов.
/// </summary>
public interface IDraftDocumentsIdCache
{
  /// <summary>Тип объектов "Черновики документов".</summary>
  IMetadataResolver<int> DraftDocuments { get; }

  /// <summary>
  /// Атрибут "Внешний файл черновика документа". Значение атрибута содержит путь к файлу, который необходимо импортировать в базу данных IPS,
  /// чтобы преобразовать черновик в полноценный документ.
  /// </summary>
  IMetadataResolver<int> ExternalFilePath { get; }
}
