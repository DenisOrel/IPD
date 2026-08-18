// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.ItemProxy
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.Params;

internal class ItemProxy
{
  public ItemProxy()
  {
  }

  public ItemProxy(
    AttributeForObjectTypeInfo attributeForObjectTypeInfo)
  {
    this.AttributeTypeId = attributeForObjectTypeInfo.AttrTypeId;
    this.ObjectTypeId = attributeForObjectTypeInfo.ObjectTypeId;
  }

  [DisplayName("Атрибут")]
  [Category("Описание")]
  [Editor(typeof (AttributeTypeUITypeEditor), typeof (UITypeEditor))]
  [TypeConverter(typeof (AttributeTypeTypeConverter))]
  [DefaultValue(0)]
  public int AttributeTypeId { get; set; }

  [DisplayName("Тип объекта")]
  [Category("Описание")]
  [Editor(typeof (ObjectTypeUITypeEditor), typeof (UITypeEditor))]
  [TypeConverter(typeof (ObjectTypeTypeConverter))]
  [DefaultValue(-1)]
  public int ObjectTypeId { get; set; }

  [Browsable(false)]
  public string Name
  {
    get
    {
      return this.ObjectTypeId != -1 ? (this.AttributeTypeId != 0 ? $"{MetaDataHelper.GetObjectTypeName(this.ObjectTypeId)} => {MetaDataHelper.GetAttributeTypeName(this.AttributeTypeId)}" : "Атрибут не указан") : (this.AttributeTypeId != 0 ? MetaDataHelper.GetAttributeTypeName(this.AttributeTypeId) : "Атрибут не указан");
    }
  }
}
