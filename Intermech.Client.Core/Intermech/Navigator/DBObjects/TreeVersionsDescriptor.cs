
// Type: Intermech.Navigator.DBObjects.TreeVersionsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Persistence;
using System;


namespace Intermech.Navigator.DBObjects;

internal sealed class TreeVersionsDescriptor : VersionsDescriptor
{
  private string _path;

  public TreeVersionsDescriptor(PersistentState state)
    : base(state)
  {
  }

  public TreeVersionsDescriptor(
    long objectID,
    long id,
    int objectTypeID,
    string objectCaption,
    DateTime onDate)
    : base(objectID, id, objectTypeID, objectCaption, VersionsWindowVisualModes.TREE, onDate)
  {
  }

  public override string Path
  {
    get
    {
      if (this._path == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long objectID = this.ObjectID;
          do
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID, true);
            objectID = objectActualCopy.ParentVersionID;
            this._path = $"{objectActualCopy.ObjectID}{System.IO.Path.DirectorySeparatorChar}{this._path}";
          }
          while (objectID > 0L);
          this._path = $"{LocalizationHolder.rm.GetString("Client.Core_324")}{System.IO.Path.DirectorySeparatorChar}{this._path}";
        }
      }
      return this._path;
    }
  }

  protected override void Initialize()
  {
    this._caption = LocalizationHolder.rm.GetString("Client.Core_324");
  }

  public override bool Equals(object obj)
  {
    return obj != null && obj.GetType() == typeof (TreeVersionsDescriptor) ? this.Equals((VersionsDescriptor) obj) : base.Equals(obj);
  }

  public override int GetHashCode() => base.GetHashCode();
}
