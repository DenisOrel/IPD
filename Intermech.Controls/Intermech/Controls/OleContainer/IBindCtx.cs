
// Type: Intermech.Controls.OleContainer.IBindCtx
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[Guid("0000000e-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
public interface IBindCtx
{
  void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

  void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

  void ReleaseBoundObjects();

  void SetBindOptions([In] ref System.Runtime.InteropServices.ComTypes.BIND_OPTS pbindopts);

  void GetBindOptions(ref System.Runtime.InteropServices.ComTypes.BIND_OPTS pbindopts);

  void GetRunningObjectTable(out IRunningObjectTable pprot);

  void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

  void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

  void EnumObjectParam(out IEnumString ppenum);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
}
