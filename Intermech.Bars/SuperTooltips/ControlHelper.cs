
// Type: SuperTooltips.ControlHelper
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace SuperTooltips
{
    internal class ControlHelper
    {
      private static ArrayList _screens = new ArrayList();

      public static ScreenInfo GetScreenInfo(Point pos)
      {
        if (ControlHelper._screens.Count == 0)
          ControlHelper.ScanScreens();
        foreach (ScreenInfo screen in ControlHelper._screens)
        {
          if (screen._bounds.Contains(pos))
            return screen;
        }
        Screen screen1 = Screen.FromPoint(pos);
        return screen1 != null ? new ScreenInfo(screen1.Bounds, screen1.WorkingArea) : (ScreenInfo) null;
      }

      private static void ScanScreens()
      {
        foreach (Screen allScreen in Screen.AllScreens)
          ControlHelper._screens.Add((object) new ScreenInfo(allScreen.Bounds, allScreen.WorkingArea));
      }

      public static bool IsControlValid(Control control)
      {
        return control != null && !control.Disposing && !control.IsDisposed && control.IsHandleCreated;
      }

      internal static Image LoadResourceImage(string p) => (Image) null;
    }
}
