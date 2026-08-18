
// Type: Intermech.Search.Diff.AttributeDiffPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;


namespace Intermech.Search.Diff;

public sealed class AttributeDiffPropertyDescriptor : DiffPropertyDescriptorBase<AttributeDiff>
{
  private AttributeHolderConverter.AttributeHolderPropertyDescriptor _attributeHolderPropertyDescriptor;

  public AttributeDiffPropertyDescriptor(Type componentType, IMSAttributeType attributeType)
    : base(componentType, AttributeDiffPropertyDescriptor.GetName(attributeType), AttributeDiffPropertyDescriptor.GetPropertyType(attributeType))
  {
    this.AttributeType = attributeType != null ? attributeType : throw new ArgumentNullException(nameof (attributeType));
    this._attributeHolderPropertyDescriptor = new AttributeHolderConverter.AttributeHolderPropertyDescriptor(componentType, attributeType);
  }

  public IMSAttributeType AttributeType { get; private set; }

  public override string Category => this._attributeHolderPropertyDescriptor.Category;

  public override TypeConverter Converter
  {
    get
    {
      if (this._attributeHolderPropertyDescriptor.AttributeType.MultiValueMode == MultiValueModes.MultiValues || this._attributeHolderPropertyDescriptor.AttributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
        return (TypeConverter) new ListItemDiffCollectionConverter();
      return this._attributeHolderPropertyDescriptor.AttributeType.FieldType == FieldTypes.ftBlob || this._attributeHolderPropertyDescriptor.AttributeType.FieldType == FieldTypes.ftFile || this._attributeHolderPropertyDescriptor.AttributeType.FieldType == FieldTypes.ftShortBlob ? (TypeConverter) new PropertyDiffCollectionConverter() : this._attributeHolderPropertyDescriptor.Converter;
    }
  }

  public override string Description => this._attributeHolderPropertyDescriptor.Description;

  public override string DisplayName => this._attributeHolderPropertyDescriptor.DisplayName;

  public override AttributeDiff GetDiff(IDiffCollection<AttributeDiff> diffCollection)
  {
    if (diffCollection == null)
      throw new ArgumentNullException();
    if (!(diffCollection is AttributeDiffCollection))
      throw new ArgumentException();
    return ((AttributeDiffCollection) diffCollection)[this._attributeHolderPropertyDescriptor.AttributeType.AttributeID];
  }

  public override Type PropertyType => this._attributeHolderPropertyDescriptor.PropertyType;

  private static string GetName(IMSAttributeType attributeType) => attributeType.Name;

  private static Type GetPropertyType(IMSAttributeType attributeType) => typeof (object);
}
