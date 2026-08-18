
// Type: Intermech.Search.Metadata.TypeMetadataParser
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;
using System.Reflection;


namespace Intermech.Search.Metadata
{
    /// <summary>Стандартный парсер метаданных</summary>
    public sealed class TypeMetadataParser : ITypeMetadataParser
    {
      /// <summary>Парсить метаданные на типе</summary>
      /// <param name="type">Тип объекта</param>
      /// <returns>Метаданные типа</returns>
      /// <exception cref="T:System.ArgumentNullException">type</exception>
      public TypeMetadata Parse(Type type)
      {
        TypeMetadata typeMetadata = !(type == (Type) null) ? new TypeMetadata(type) : throw new ArgumentNullException(nameof (type));
        DisplayNameAttribute customAttribute = this.GetCustomAttribute<DisplayNameAttribute>(type);
        if (customAttribute != null)
          typeMetadata.DisplayName = customAttribute.DisplayName;
        this.ParseProperties(type, typeMetadata);
        return typeMetadata;
      }

      private void ParseProperties(Type type, TypeMetadata typeMetadata)
      {
        foreach (PropertyInfo property in type.GetProperties())
        {
          PropertyMetadata propertyMetadata = new PropertyMetadata(property);
          this.ParseCategory(property, propertyMetadata);
          this.ParseDefaultValue(property, propertyMetadata);
          this.ParseDescription(property, propertyMetadata);
          this.ParseDisplayName(property, propertyMetadata);
          this.ParseEditor(property, propertyMetadata);
          this.ParseIsAdmin(property, propertyMetadata);
          this.ParseTypeConverter(property, propertyMetadata);
          typeMetadata.Properties.Add(propertyMetadata);
        }
      }

      private void ParseDefaultValue(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        DefaultValueAttribute customAttribute = this.GetCustomAttribute<DefaultValueAttribute>(property);
        if (customAttribute != null)
        {
          propertyMetadata.DefaultValue = customAttribute.Value;
        }
        else
        {
          if (!property.PropertyType.IsValueType)
            return;
          propertyMetadata.DefaultValue = Activator.CreateInstance(property.PropertyType);
        }
      }

      private void ParseTypeConverter(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        TypeConverterAttribute customAttribute = this.GetCustomAttribute<TypeConverterAttribute>(property);
        if (customAttribute == null)
          return;
        propertyMetadata.TypeConverterName = customAttribute.ConverterTypeName;
      }

      private void ParseIsAdmin(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        if (this.GetCustomAttribute<IsAdminAttribute>(property) == null)
          return;
        propertyMetadata.IsAdmin = true;
      }

      private void ParseEditor(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        EditorAttribute customAttribute = this.GetCustomAttribute<EditorAttribute>(property);
        if (customAttribute == null)
          return;
        propertyMetadata.EditorTypeName = customAttribute.EditorTypeName;
      }

      private void ParseDisplayName(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        DisplayNameAttribute customAttribute = this.GetCustomAttribute<DisplayNameAttribute>(property);
        if (customAttribute == null)
          return;
        propertyMetadata.DisplayName = customAttribute.DisplayName;
      }

      private void ParseDescription(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        DescriptionAttribute customAttribute = this.GetCustomAttribute<DescriptionAttribute>(property);
        if (customAttribute == null)
          return;
        propertyMetadata.Description = customAttribute.Description;
      }

      private void ParseCategory(PropertyInfo property, PropertyMetadata propertyMetadata)
      {
        CategoryAttribute customAttribute = this.GetCustomAttribute<CategoryAttribute>(property);
        if (customAttribute == null)
          return;
        propertyMetadata.Category = customAttribute.Category;
      }

      private T GetCustomAttribute<T>(PropertyInfo property) where T : class
      {
        return Attribute.GetCustomAttribute((MemberInfo) property, typeof (T)) as T;
      }

      private T GetCustomAttribute<T>(Type type) where T : class
      {
        return Attribute.GetCustomAttribute((MemberInfo) type, typeof (T)) as T;
      }
    }
}
