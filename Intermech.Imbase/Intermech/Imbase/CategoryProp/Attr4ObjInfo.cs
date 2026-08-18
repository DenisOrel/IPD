// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CategoryProp.Attr4ObjInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.CategoryProp;

internal class Attr4ObjInfo
{
  public int AttrTypeID;
  public int ObjTypeID;
  public InheritModes InheritMode;
  public FieldTypes FieldType;
  public int[] ValueType;

  public Attr4ObjInfo(
    int attrTypeId,
    int objTypeId,
    InheritModes inheritMode,
    FieldTypes fieldType,
    int[] valueType)
  {
    this.AttrTypeID = attrTypeId;
    this.ObjTypeID = objTypeId;
    this.InheritMode = inheritMode;
    this.FieldType = fieldType;
    this.ValueType = valueType;
  }
}
