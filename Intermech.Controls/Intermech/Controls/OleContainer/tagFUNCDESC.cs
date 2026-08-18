
// Type: Intermech.Controls.OleContainer.tagFUNCDESC
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public sealed class tagFUNCDESC
{
  public int memid;
  [MarshalAs(UnmanagedType.U2)]
  public short lprgscode;
  public IntPtr lprgelemdescParam;
  public int funckind;
  public int invkind;
  public int callconv;
  [MarshalAs(UnmanagedType.I2)]
  public short cParams;
  [MarshalAs(UnmanagedType.I2)]
  public short cParamsOpt;
  [MarshalAs(UnmanagedType.I2)]
  public short oVft;
  [MarshalAs(UnmanagedType.I2)]
  public short cScodes;
  public value_tagELEMDESC elemdescFunc;
  [MarshalAs(UnmanagedType.U2)]
  public short wFuncFlags;
}
