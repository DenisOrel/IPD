
// Type: Intermech.Services.DraftDocumentsIdCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.Metadata;
using System;


namespace Intermech.Services;

/// <summary>
/// Контейнер метаданных, относящихся к черновикам документов. Реализация является thread safe.
/// </summary>
internal sealed class DraftDocumentsIdCache : IDraftDocumentsIdCache
{
  /// <summary>Создает объект.</summary>
  /// <param name="metadataResolvers">Фабрика метаданных IPS</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="metadataResolvers" /> не должен быть равен null</exception>
  public DraftDocumentsIdCache(MetadataResolverFactory metadataResolvers)
  {
    this.DraftDocuments = metadataResolvers != null ? (IMetadataResolver<int>) metadataResolvers.ObjectTypeResolver(new Guid("CADD9712-306C-11D8-B4E9-00304F19F545")) : throw new ArgumentNullException(nameof (metadataResolvers));
    this.ExternalFilePath = (IMetadataResolver<int>) metadataResolvers.AttributeTypeResolver(new Guid("CADD9714-306C-11D8-B4E9-00304F19F545"));
  }

  /// <summary>Тип объектов "Черновики документов".</summary>
  public IMetadataResolver<int> DraftDocuments { get; private set; }

  /// <summary>
  /// Атрибут "Внешний файл черновика документа". Значение атрибута содержит путь к файлу, который необходимо импортировать в базу данных IPS,
  /// чтобы преобразовать черновик в полноценный документ.
  /// </summary>
  public IMetadataResolver<int> ExternalFilePath { get; private set; }
}
