
// Type: Intermech.Interfaces.Data.SidecarObjects.SidecarObjectsIDCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Data.Metadata;
using System;


namespace Intermech.Interfaces.Data.SidecarObjects
{
    /// <summary>
    /// Базовый класс для кэша метаданных ассоциированных объектов IPS.
    /// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
    /// косвенной связью (например, через содержимое файла исходного объекта).
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    public class SidecarObjectsIDCache
    {
      private readonly ObjectTypeResolver sidecarObjectType;
      private readonly string sidecarInstanceName;
      private readonly AttributeTypeResolver caption;
      private readonly AttributeTypeResolver sourceDocumentReference;
      private readonly AttributeTypeResolver contentStatus;

      /// <summary>Создает объект.</summary>
      /// <param name="metadataResolvers">Фабрика определителей метаданных</param>
      /// <param name="sidecarObjectTypeGuid">Глобальный идентификатор типа ассоциированных объектов</param>
      /// <param name="sidecarInstanceName">Имя экземпляра ассоциированного объекта</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="metadataResolvers" /> содержит null; параметр <paramref name="sidecarInstanceName" /> содержит null</exception>
      public SidecarObjectsIDCache(
        MetadataResolverFactory metadataResolvers,
        Guid sidecarObjectTypeGuid,
        string sidecarInstanceName)
      {
        if (metadataResolvers == null)
          throw new ArgumentNullException(nameof (metadataResolvers));
        if (sidecarInstanceName == null)
          throw new ArgumentNullException(nameof (sidecarInstanceName));
        this.sidecarObjectType = metadataResolvers.ObjectTypeResolver(sidecarObjectTypeGuid);
        this.sidecarInstanceName = sidecarInstanceName;
        this.caption = metadataResolvers.AttributeTypeResolver(new Guid("CAD00047-306C-11D8-B4E9-00304F19F545"));
        this.sourceDocumentReference = metadataResolvers.AttributeTypeResolver(new Guid("CADD94EB-306C-11D8-B4E9-00304F19F545"));
        this.contentStatus = metadataResolvers.AttributeTypeResolver(new Guid("CADD9AAC-306C-11D8-B4E9-00304F19F545"));
      }

      /// <summary>Возвращает тип ассоциированных объектов.</summary>
      public ObjectTypeResolver SidecarObjectType => this.sidecarObjectType;

      /// <summary>Возвращает имя экземпляра ассоциированного объекта.</summary>
      public string SidecarInstanceName => this.sidecarInstanceName;

      /// <summary>Возвращает атрибут "Заголовок объекта"</summary>
      public AttributeTypeResolver Caption => this.caption;

      /// <summary>Возвращает атрибут "Ссылка на исходный документ"</summary>
      public AttributeTypeResolver SourceDocumentReference => this.sourceDocumentReference;

      /// <summary>Возвращает атрибут "Статус содержимого объекта"</summary>
      public AttributeTypeResolver ContentStatus => this.contentStatus;
    }
}
