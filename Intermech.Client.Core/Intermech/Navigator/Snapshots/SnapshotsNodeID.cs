
// Type: Intermech.Navigator.Snapshots.SnapshotsNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Snapshots;

/// <summary>узел</summary>
public class SnapshotsNodeID : INodeID
{
  protected long snapshotID;
  protected long objectID;
  protected int objectType;
  protected long id;
  public string name;
  public long userID;
  public DateTime snapDate;
  protected object cookie;

  public int TypeID => this.objectType;

  public long ObjectID
  {
    get => this.objectID;
    set => this.objectID = value;
  }

  public long ID
  {
    get => this.id;
    set => this.id = value;
  }

  public long SnapshotID
  {
    get => this.snapshotID;
    set => this.snapshotID = value;
  }

  public object Cookie
  {
    get => this.cookie;
    set => this.cookie = value;
  }

  public SnapshotsNodeID(
    long snapshotID,
    long objectID,
    long id,
    int objType,
    string name,
    long userID,
    DateTime snapDate)
  {
    this.snapshotID = snapshotID;
    this.objectID = objectID;
    this.objectType = objType;
    this.id = id;
    this.name = name;
    this.userID = userID;
    this.snapDate = snapDate;
  }

  /// <summary>
  /// Идентификатор категории элемента пространства навигации
  /// </summary>
  public int CategoryID => 23;

  public override bool Equals(object obj)
  {
    return obj is SnapshotsNodeID snapshotsNodeId ? this.snapshotID.Equals(snapshotsNodeId.SnapshotID) : base.Equals(obj);
  }

  public override int GetHashCode() => this.SnapshotID.GetHashCode();
}
