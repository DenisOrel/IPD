
// Type: Intermech.Navigator.Snapshots.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;


namespace Intermech.Navigator.Snapshots;

/// <summary>меню и вьюшки для итераций</summary>
public sealed class Services
{
  internal static void Start()
  {
    Holder.Factory.AddNodeType(23, typeof (SnapshotsNode));
    Holder.Factory.AddCommandsProvider(23, (ICommandsProvider) new SnapshotsCommandsProvider());
    Holder.Factory.AddViewsProvider(23, (IViewsProvider) new SnapshotViewsProvider());
    (ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes).Register(SnapshotConsts.SNAPSHOT_SCHEME_GUID, (INodeColumnScheme) new SnapshotColumnScheme());
  }

  internal static void Stop()
  {
  }
}
