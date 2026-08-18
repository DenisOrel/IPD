
// Type: Intermech.Search.Metadata.PropertyMetadata
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Reflection;


namespace Intermech.Search.Metadata
{
    /// <summary>Метаданные свойства</summary>
    public class PropertyMetadata
    {
      /// <summary>Конструктор</summary>
      /// <param name="property">Свойство</param>
      public PropertyMetadata(PropertyInfo property)
      {
        this.Property = !(property == (PropertyInfo) null) ? property : throw new ArgumentNullException(nameof (property));
      }

      /// <summary>Gets or sets the category.</summary>
      /// <value>The category.</value>
      public string Category { get; set; }

      /// <summary>Gets or sets the default value.</summary>
      /// <value>The default value.</value>
      public object DefaultValue { get; set; }

      /// <summary>Gets or sets the description.</summary>
      /// <value>The description.</value>
      public string Description { get; set; }

      /// <summary>Gets or sets the display name.</summary>
      /// <value>The display name.</value>
      public string DisplayName { get; set; }

      /// <summary>Gets or sets the name of the editor type.</summary>
      /// <value>The name of the editor type.</value>
      public string EditorTypeName { get; set; }

      /// <summary>Gets or sets the name of the type converter.</summary>
      /// <value>The name of the type converter.</value>
      public string TypeConverterName { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is admin.
      /// </summary>
      /// <value>
      ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
      /// </value>
      public bool IsAdmin { get; set; }

      /// <summary>Gets the property.</summary>
      /// <value>The property.</value>
      public PropertyInfo Property { get; private set; }
    }
}
