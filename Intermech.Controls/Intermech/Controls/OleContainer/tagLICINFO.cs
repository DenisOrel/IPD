
// Type: Intermech.Controls.OleContainer.tagLICINFO
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Controls.OleContainer;

[StructLayout(LayoutKind.Sequential)]
public sealed class tagLICINFO
{
  [MarshalAs(UnmanagedType.U4)]
  public int cbLicInfo;
  public int fRuntimeAvailable;
  public int fLicVerified;

  public tagLICINFO() => this.cbLicInfo = Marshal.SizeOf(typeof (tagLICINFO));
}
