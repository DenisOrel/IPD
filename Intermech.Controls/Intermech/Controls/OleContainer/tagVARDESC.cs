
// Type: Intermech.Controls.OleContainer.tagVARDESC
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public sealed class tagVARDESC
{
  public int memid;
  public IntPtr lpstrSchema;
  public IntPtr unionMember;
  public value_tagELEMDESC elemdescVar;
  [MarshalAs(UnmanagedType.U2)]
  public short wVarFlags;
  public int varkind;
}
