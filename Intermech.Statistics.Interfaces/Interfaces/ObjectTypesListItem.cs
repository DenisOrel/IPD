// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.ObjectTypesListItem
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>Класс, описывающий тип объекта.</summary>
[Serializable]
public class ObjectTypesListItem : IEquatable<ObjectTypesListItem>
{
  [XmlElement(ElementName = "ObjectTypeID")]
  public int ObjectTypeID { get; set; }

  [XmlElement(ElementName = "ObjectTypeName")]
  public string ObjectTypeName { get; set; }

  [XmlElement(ElementName = "ObjectTypeGuid")]
  public string ObjectTypeGuid { get; set; }

  public ObjectTypesListItem()
  {
  }

  public ObjectTypesListItem(int objectTypeID, string objectTypeGuid, string objectTypeName)
  {
    this.ObjectTypeID = objectTypeID;
    this.ObjectTypeName = objectTypeName;
    this.ObjectTypeGuid = objectTypeGuid;
  }

  public bool Equals(ObjectTypesListItem other) => this.ObjectTypeGuid == other.ObjectTypeGuid;

  public override string ToString() => this.ObjectTypeName;
}
