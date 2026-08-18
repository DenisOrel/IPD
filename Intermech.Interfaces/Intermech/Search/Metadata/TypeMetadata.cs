
// Type: Intermech.Search.Metadata.TypeMetadata
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search.Metadata
{
    /// <summary>Метаданные типа</summary>
    public class TypeMetadata
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Intermech.Search.Metadata.TypeMetadata" /> class.
      /// </summary>
      /// <param name="type">The type.</param>
      public TypeMetadata(Type type)
      {
        this.Type = !(type == (Type) null) ? type : throw new ArgumentNullException(nameof (type));
        this.Properties = new List<PropertyMetadata>();
      }

      /// <summary>Gets the type.</summary>
      /// <value>The type.</value>
      public Type Type { get; private set; }

      /// <summary>Gets or sets the display name.</summary>
      /// <value>The display name.</value>
      public string DisplayName { get; set; }

      /// <summary>Gets the properties.</summary>
      /// <value>The properties.</value>
      public List<PropertyMetadata> Properties { get; private set; }
    }
}
