
// Type: Intermech.UI.Winforms.HighDPIServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.InteropServices;


namespace Intermech.UI.Winforms
{
    /// <summary>Утилиты поддержки High DPI мониторов</summary>
    public static class HighDPIServices
    {
      /// <summary>
      /// Включает поддержку High DPI мониторов для текущего процесса.
      /// </summary>
      public static void EnableHighDPIMode()
      {
        if (Environment.OSVersion.Version.Major < 6)
          return;
        HighDPIServices.NativeMethods.SetProcessDPIAware();
      }

      private static class NativeMethods
      {
        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
      }
    }
}
