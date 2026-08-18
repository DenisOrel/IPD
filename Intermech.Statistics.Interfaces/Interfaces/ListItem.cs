// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.ListItem
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public class ListItem : IEquatable<ListItem>
{
  private string _caption;
  private long _ID;
  private long _objID;
  private string _guid;

  /// <summary>Заголовок для отображения в списке</summary>
  [XmlElement(ElementName = "Caption")]
  public string Caption
  {
    get => this._caption;
    set => this._caption = value;
  }

  /// <summary>Идентификатор объекта, типа и т.п.</summary>
  [XmlElement(ElementName = "ID")]
  public long ID
  {
    get => this._ID;
    set => this._ID = value;
  }

  /// <summary>
  /// Идентификатор объекта, типа и т.п. objectID (F_OBJECT_ID)
  /// Хранятся значения со знаком. Следует использовать GetObjectActualCopy
  /// </summary>
  [XmlElement(ElementName = "ObjID")]
  public long ObjID
  {
    get => this._objID;
    set => this._objID = value;
  }

  /// <summary>GUID объекта</summary>
  [XmlElement(ElementName = "Guid")]
  public string GUID
  {
    get => this._guid;
    set => this._guid = value;
  }

  public ListItem()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption">Заголовок объекта</param>
  /// <param name="ID">Идентификатор объекта</param>
  public ListItem(string caption, long ID)
  {
    this._caption = caption;
    this._ID = ID;
  }

  public ListItem(string caption, long ID, long objID)
  {
    this._caption = caption;
    this._ID = ID;
    this._objID = objID;
  }

  public bool Equals(ListItem other)
  {
    return this.ObjID == other.ObjID && this.Caption == other.Caption && this.ID == other.ID;
  }

  public override string ToString() => this.Caption;
}
