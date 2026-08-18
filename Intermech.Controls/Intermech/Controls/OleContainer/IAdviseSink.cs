
// Type: Intermech.Controls.OleContainer.IAdviseSink
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("0000010F-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IAdviseSink
{
  [MethodImpl(MethodImplOptions.PreserveSig)]
  void OnDataChange([In] ref FORMATETC format, [In] ref STGMEDIUM stgmedium);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  void OnViewChange(int aspect, int index);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  void OnRename(IMoniker moniker);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  void OnSave();

  [MethodImpl(MethodImplOptions.PreserveSig)]
  void OnClose();
}
