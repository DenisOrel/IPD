// Decompiled with JetBrains decompiler
// Type: Intermech.Project.AddToCompositionInfo
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Project;

public class AddToCompositionInfo
{
  public readonly int RelationTypeID;
  public readonly long ObjectID;
  private int _objectTypeID = -1;

  public AddToCompositionInfo(int relationID, long objectID)
  {
    this.RelationTypeID = relationID;
    this.ObjectID = objectID;
  }

  public int ObjectTypeID
  {
    get
    {
      if (this._objectTypeID == -1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID, false);
          this._objectTypeID = dbObject != null ? dbObject.TypeID : 0;
        }
      }
      return this._objectTypeID;
    }
  }
}
