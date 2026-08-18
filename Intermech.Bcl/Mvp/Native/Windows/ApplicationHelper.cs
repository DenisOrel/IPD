
// Type: Intermech.Mvp.Native.Windows.ApplicationHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Windows.Forms;


namespace Intermech.Mvp.Native.Windows
{
    public static class ApplicationHelper
    {
      /// <summary>
      /// Включает поддержку системных стилей оформления для приложений на основе Winforms.
      /// </summary>
      public static void EnableVisualStyles()
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
      }
    }
}
