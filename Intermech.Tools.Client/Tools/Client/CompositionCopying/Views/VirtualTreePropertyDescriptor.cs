// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.VirtualTreePropertyDescriptor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Tools.Client.CompositionCopying.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal class VirtualTreePropertyDescriptor : PropertyDescriptor
{
  private IMSAttributeType _property;

  public VirtualTreePropertyDescriptor(string name, Attribute[] attrs)
    : base(name, attrs)
  {
  }

  public VirtualTreePropertyDescriptor(MemberDescriptor descr)
    : base(descr)
  {
  }

  public VirtualTreePropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
    : base(descr, attrs)
  {
  }

  public VirtualTreePropertyDescriptor(IMSAttributeType attributeType)
    : base(attributeType.Name, (Attribute[]) null)
  {
    this._property = attributeType;
  }

  public override bool CanResetValue(object component) => false;

  public override object GetValue(object component)
  {
    if (this._property == null || !(component is DBObjectGraphVertex objectGraphVertex))
      return (object) null;
    int index = CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) objectGraphVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == this._property.AttributeID));
    if (index == -1)
      return (object) null;
    DBObjectAttributeEntry attribute = objectGraphVertex.Attributes[index];
    if (new Guid("cad0002e-306c-11d8-b4e9-00304f19f545") == this._property.AttributeGuid)
    {
      int result;
      if (int.TryParse(attribute.OriginalValues[0].ToString(), out result))
        return (object) MetaDataHelper.GetObjectTypeName(result);
      return !(attribute.NewValues[0] is DBNull) ? attribute.NewValues[0] : attribute.OriginalValues[0];
    }
    return !(attribute.NewValues[0] is DBNull) ? attribute.NewValues[0] : attribute.OriginalValues[0];
  }

  public bool IsUnique(object component)
  {
    if (this._property == null || !(component is DBObjectGraphVertex objectGraphVertex))
      return false;
    int index = CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) objectGraphVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == this._property.AttributeID));
    return index != -1 && objectGraphVertex.Attributes[index].IsUniqueValuesRequired;
  }

  public bool IsEditable(object component)
  {
    if (this._property == null || !(component is DBObjectGraphVertex objectGraphVertex))
      return false;
    int index = CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) objectGraphVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == this._property.AttributeID));
    return index != -1 && objectGraphVertex.Attributes[index].IsEditableAttribute;
  }

  public int GetAttributeID() => this._property != null ? this._property.AttributeID : -1;

  public override void ResetValue(object component)
  {
  }

  public override void SetValue(object component, object value)
  {
    if (this._property == null || !(component is DBObjectGraphVertex objectGraphVertex))
      return;
    int index = CollectionUtils.IndexOf<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) objectGraphVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == this._property.AttributeID));
    if (index == -1)
      return;
    objectGraphVertex.Attributes[index].SetNewValue(0, value);
  }

  public override bool ShouldSerializeValue(object component) => false;

  public override Type ComponentType => typeof (DBObjectGraphVertex);

  public override bool IsReadOnly => false;

  public override Type PropertyType
  {
    get
    {
      return this._property != null ? this.GetTypeFromAttributeFieldType(this._property.FieldType, this._property.RealFieldType) : typeof (object);
    }
  }

  private Type GetTypeFromAttributeFieldType(FieldTypes type, FieldTypes realType)
  {
    switch (type)
    {
      case FieldTypes.ftUnknown:
        return typeof (object);
      case FieldTypes.ftString:
      case FieldTypes.ftPassword:
      case FieldTypes.ftMemo:
        return typeof (string);
      case FieldTypes.ftInteger:
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftAutoInc:
        return typeof (long);
      case FieldTypes.ftDouble:
        return typeof (double);
      case FieldTypes.ftDateTime:
        return typeof (DateTime);
      case FieldTypes.ftShortBlob:
      case FieldTypes.ftFile:
      case FieldTypes.ftExternalLink:
      case FieldTypes.ftBlob:
      case FieldTypes.ftMeasured:
        return typeof (object);
      case FieldTypes.ftBoolean:
        return typeof (bool);
      case FieldTypes.ftSystem:
        return this.GetTypeFromAttributeFieldType(realType, FieldTypes.ftUnknown);
      case FieldTypes.ftGuid:
        return typeof (Guid);
      default:
        return typeof (object);
    }
  }

  public ItemPropertyInfo ToItemProperty()
  {
    return new ItemPropertyInfo(this.Name, this.PropertyType, (object) this);
  }
}
