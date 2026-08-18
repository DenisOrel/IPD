
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.IPreviewHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("8895b1c6-b41f-4c1c-a562-0d564250836f")]
[ComImport]
internal interface IPreviewHandler
{
  void SetWindow(IntPtr hwnd, ref Rectangle rect);

  void SetRect(ref Rectangle rect);

  void DoPreview();

  void Unload();

  void SetFocus();

  void QueryFocus(out IntPtr phwnd);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  uint TranslateAccelerator(ref Message pmsg);
}
