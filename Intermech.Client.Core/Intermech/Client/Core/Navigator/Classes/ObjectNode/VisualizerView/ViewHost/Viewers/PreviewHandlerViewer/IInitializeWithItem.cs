
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.IInitializeWithItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("7F73BE3F-FB79-493C-A6C7-7EE14E245841")]
[ComImport]
internal interface IInitializeWithItem
{
  void Initialize(IShellItem psi, uint grfMode);
}
