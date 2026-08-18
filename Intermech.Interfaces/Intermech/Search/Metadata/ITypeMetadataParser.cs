
// Type: Intermech.Search.Metadata.ITypeMetadataParser
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Metadata
{
    /// <summary>Парсер метаданных</summary>
    public interface ITypeMetadataParser
    {
      /// <summary>Парсить метаданные на типе</summary>
      /// <param name="type">Тип объекта</param>
      /// <returns>Метаданные типа</returns>
      TypeMetadata Parse(Type type);
    }
}
