
// Type: Intermech.Navigator.DBObjects.ListVersionsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Persistence;
using System;


namespace Intermech.Navigator.DBObjects;

internal sealed class ListVersionsDescriptor : VersionsDescriptor
{
  public ListVersionsDescriptor(PersistentState state)
    : base(state)
  {
  }

  public ListVersionsDescriptor(long objectID, int objectTypeID)
    : base(objectID, objectTypeID, VersionsWindowVisualModes.LIST, DateTime.MaxValue)
  {
  }

  public ListVersionsDescriptor(
    long objectID,
    long id,
    int objectTypeID,
    string objectCaption,
    DateTime onDate)
    : base(objectID, id, objectTypeID, objectCaption, VersionsWindowVisualModes.LIST, onDate)
  {
  }

  public override string Path
  {
    get
    {
      return $"{LocalizationHolder.rm.GetString("Client.Core_325")}{System.IO.Path.DirectorySeparatorChar}{this.ObjectID}";
    }
  }

  protected override bool IsList => true;

  protected override void Initialize()
  {
    this._caption = LocalizationHolder.rm.GetString("Client.Core_325");
  }

  public override bool Equals(object obj)
  {
    return obj != null && obj.GetType() == typeof (ListVersionsDescriptor) ? this.Equals((VersionsDescriptor) obj) : base.Equals(obj);
  }

  public override int GetHashCode() => base.GetHashCode();
}
