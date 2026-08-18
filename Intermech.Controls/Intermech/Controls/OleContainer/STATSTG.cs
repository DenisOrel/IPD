
// Type: Intermech.Controls.OleContainer.STATSTG
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public class STATSTG
{
  [MarshalAs(UnmanagedType.LPWStr)]
  public string pwcsName;
  public int type;
  [MarshalAs(UnmanagedType.I8)]
  public long cbSize;
  [MarshalAs(UnmanagedType.I8)]
  public long mtime;
  [MarshalAs(UnmanagedType.I8)]
  public long ctime;
  [MarshalAs(UnmanagedType.I8)]
  public long atime;
  [MarshalAs(UnmanagedType.I4)]
  public int grfMode;
  [MarshalAs(UnmanagedType.I4)]
  public int grfLocksSupported;
  public int clsid_data1;
  [MarshalAs(UnmanagedType.I2)]
  public short clsid_data2;
  [MarshalAs(UnmanagedType.I2)]
  public short clsid_data3;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b0;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b1;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b2;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b3;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b4;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b5;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b6;
  [MarshalAs(UnmanagedType.U1)]
  public byte clsid_b7;
  [MarshalAs(UnmanagedType.I4)]
  public int grfStateBits;
  [MarshalAs(UnmanagedType.I4)]
  public int reserved;
}
