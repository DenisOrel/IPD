
// Type: Intermech.Search.Metadata.TypeMetadataRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search.Metadata
{
    /// <summary>Стандарный репозиторий метаданных</summary>
    public sealed class TypeMetadataRepository : ITypeMetadataRepository
    {
      private Dictionary<Type, TypeMetadata> _typeMetadataDictionary = new Dictionary<Type, TypeMetadata>();

      /// <summary>Найти метаданные для типа</summary>
      /// <param name="type">Тип объекта</param>
      /// <returns>Метаданные</returns>
      /// <exception cref="T:System.ArgumentNullException">type</exception>
      public TypeMetadata Find(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException(nameof (type));
        TypeMetadata typeMetadata = (TypeMetadata) null;
        this._typeMetadataDictionary.TryGetValue(type, out typeMetadata);
        return typeMetadata;
      }

      /// <summary>Добавить метаданные</summary>
      /// <param name="typeMetadata">Метаданыне типа</param>
      /// <exception cref="T:System.ArgumentNullException">typeMetadata</exception>
      public void Add(TypeMetadata typeMetadata)
      {
        if (typeMetadata == null)
          throw new ArgumentNullException(nameof (typeMetadata));
        this._typeMetadataDictionary.Add(typeMetadata.Type, typeMetadata);
      }
    }
}
