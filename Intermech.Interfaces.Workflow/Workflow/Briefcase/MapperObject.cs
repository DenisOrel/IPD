// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Briefcase.MapperObject
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Briefcase;

[XmlInclude(typeof (MapperObject))]
[XmlInclude(typeof (MapperVariable))]
[Serializable]
public class MapperObject
{
  public Guid Guid;
  public string Caption;
  private static MapperObject _empty;

  public MapperObject()
  {
  }

  public MapperObject(Guid guid, string caption)
  {
    this.Guid = guid;
    this.Caption = caption;
  }

  public static MapperObject Empty
  {
    get
    {
      if (MapperObject._empty == null)
        MapperObject._empty = new MapperObject(Guid.Empty, "");
      return MapperObject._empty;
    }
  }
}
