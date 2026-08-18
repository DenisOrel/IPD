
// Type: Intermech.Controls.OleContainer.IDataObject
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("0000010E-0000-0000-C000-000000000046")]
[ComImport]
public interface IDataObject
{
  void GetData([In] ref FORMATETC format, ref STGMEDIUM medium);

  void GetDataHere([In] ref FORMATETC format, ref STGMEDIUM medium);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int QueryGetData([In] ref FORMATETC format);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int GetCanonicalFormatEtc([In] ref FORMATETC formatIn, out FORMATETC formatOut);

  void SetData([In] ref FORMATETC formatIn, [In] ref STGMEDIUM medium, [MarshalAs(UnmanagedType.Bool)] bool release);

  IEnumFORMATETC EnumFormatEtc(DATADIR direction);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int DAdvise([In] ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection);

  void DUnadvise(int connection);

  [MethodImpl(MethodImplOptions.PreserveSig)]
  int EnumDAdvise(out IEnumSTATDATA enumAdvise);
}
