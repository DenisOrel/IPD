
// Type: Intermech.Navigator.DBObjects.UserToRoles
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.DBObjects;

public class UserToRoles
{
  private readonly long _parentID = -1;
  private string _parentCaption = string.Empty;
  private long _relationID = -1;
  private int _status;
  private int _oldstatus;

  public UserToRoles(long ParentID, string ParentCaption, long RelationID)
  {
    this._parentID = ParentID;
    this._parentCaption = ParentCaption;
    this._relationID = RelationID;
    this._status = 0;
  }

  public UserToRoles(long ParentID, string ParentCaption, long RelationID, int Status)
  {
    this._parentID = ParentID;
    this._parentCaption = ParentCaption;
    this._relationID = RelationID;
    this._status = Status;
  }

  public long RelationID
  {
    get => this._relationID;
    set
    {
      if (value == this._relationID)
        return;
      this._relationID = value;
    }
  }

  public int Status
  {
    get => this._status;
    set
    {
      if (this._status == value)
        return;
      this._oldstatus = this._status;
      this._status = value;
    }
  }

  public long ParentID => this._parentID;

  public string ParentCaption => this._parentCaption;

  public int OldStatus => this._oldstatus;
}
