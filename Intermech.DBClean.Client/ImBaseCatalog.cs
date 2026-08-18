// Decompiled with JetBrains decompiler
// Type: Intermech.DBClean.Client.ImBaseCatalog
// Assembly: Intermech.DBClean.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 973F13FD-72F3-4555-9BF9-74AC5C606885
// Assembly location: D:\IPS\Client\Intermech.DBClean.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.DBClean.Client.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.DBClean.Client;

[XmlType(TypeName = "cat")]
[Serializable]
public class ImBaseCatalog
{
  private long id;
  private Guid objectId;
  private string name;
  private string type;
  private CleanEnum cleanMode;

  public long Id
  {
    get => this.id;
    set => this.id = value;
  }

  [XmlAttribute(AttributeName = "id")]
  public Guid ObjectId
  {
    get => this.objectId;
    set => this.objectId = value;
  }

  [XmlIgnore]
  public string Name
  {
    get => this.name;
    set => this.name = value;
  }

  [XmlIgnore]
  public string Type
  {
    get => this.type;
    set => this.type = value;
  }

  [XmlAttribute(AttributeName = "m")]
  public CleanEnum CleanMode
  {
    get => this.cleanMode;
    set => this.cleanMode = value;
  }
}
