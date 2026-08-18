
// Type: Intermech.Util.XPThemeManager
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Util;

internal class XPThemeManager
{
  public static bool a() => Path.GetFileName(XPThemeManager.b()).ToLower() == "luna.msstyles";

  public static string b()
  {
    StringBuilder A_0 = new StringBuilder(512 /*0x0200*/);
    XPThemeManager.GetCurrentThemeName(A_0, A_0.Capacity, (StringBuilder) null, 0, (StringBuilder) null, 0);
    return A_0.ToString();
  }

  public static string c()
  {
    StringBuilder A_2 = new StringBuilder(512 /*0x0200*/);
    XPThemeManager.GetCurrentThemeName((StringBuilder) null, 0, A_2, A_2.Capacity, (StringBuilder) null, 0);
    return A_2.ToString();
  }

  [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
  private static extern int GetCurrentThemeName(
    StringBuilder A_0,
    int A_1,
    StringBuilder A_2,
    int A_3,
    StringBuilder A_4,
    int A_5);
}
