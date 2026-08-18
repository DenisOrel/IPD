
// Type: Intermech.Navigator.DBObjects.VersionsHiveNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Navigator.DBObjects;

internal sealed class VersionsHiveNodeID : HiveNodeID
{
  public VersionsWindowVisualModes Mode { get; private set; }

  public VersionsHiveNodeID(int categoryID, int objectTypeID, VersionsWindowVisualModes mode)
    : base(categoryID, objectTypeID)
  {
    this.Mode = mode;
  }
}
