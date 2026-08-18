
// Type: Intermech.Interfaces.Data.SidecarObjects.IMViewerObjectsIDCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Data.Metadata;
using System;


namespace Intermech.Interfaces.Data.SidecarObjects
{
    /// <summary>Класс кэша метаданных для типа "Объекты IMViewer".</summary>
    /// <remarks>Реализация является thread safe.</remarks>
    /// <summary>Создает объект.</summary>
    /// <param name="metadataResolvers">Фабрика определителей метаданных</param>
    /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="metadataResolvers" /> содержит null</exception>
    public class IMViewerObjectsIDCache(MetadataResolverFactory metadataResolvers) : 
      SidecarObjectsIDCache(metadataResolvers, new Guid("CADD9AAE-306C-11D8-B4E9-00304F19F545"), "Объект IMViewer")
    {
    }
}
