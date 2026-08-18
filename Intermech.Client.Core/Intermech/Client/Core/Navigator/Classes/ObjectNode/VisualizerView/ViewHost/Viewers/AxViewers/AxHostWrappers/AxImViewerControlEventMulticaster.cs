
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.AxImViewerControlEventMulticaster
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Interop.IMViewer.Controls;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

[ClassInterface(ClassInterfaceType.None)]
public class AxImViewerControlEventMulticaster : _DIMViewerOCXEvents
{
  private AxImViewerControl parent;

  public AxImViewerControlEventMulticaster(AxImViewerControl parent) => this.parent = parent;
}
