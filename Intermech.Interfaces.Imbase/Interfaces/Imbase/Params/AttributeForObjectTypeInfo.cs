// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.AttributeForObjectTypeInfo
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>Класс для хранения атрибута в контексте типа объекта</summary>
[Serializable]
public class AttributeForObjectTypeInfo
{
  public AttributeForObjectTypeInfo()
    : this(-1, 0)
  {
  }

  public AttributeForObjectTypeInfo(int objectTypeId, int attrTypeId)
  {
    this.ObjectTypeId = objectTypeId;
    this.AttrTypeId = attrTypeId;
  }

  public int ObjectTypeId { get; }

  public int AttrTypeId { get; }

  public override bool Equals(object obj)
  {
    return obj is AttributeForObjectTypeInfo forObjectTypeInfo && this.AttrTypeId.Equals(forObjectTypeInfo.AttrTypeId) && this.ObjectTypeId.Equals(forObjectTypeInfo.ObjectTypeId);
  }

  public override int GetHashCode()
  {
    int num = this.AttrTypeId;
    int hashCode1 = num.GetHashCode();
    num = this.ObjectTypeId;
    int hashCode2 = num.GetHashCode();
    return hashCode1 ^ hashCode2;
  }
}
