
// Type: Intermech.Extensions.ProgressbarExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Extensions
{
    public static class ProgressbarExtensions
    {
      /// <summary>
      /// Sets the progress bar value, without using 'Windows Aero' animation.
      /// This is to work around a known WinForms issue where the progress bar
      /// is slow to update.
      /// http://stackoverflow.com/questions/6071626/progressbar-is-slow-in-windows-forms
      /// </summary>
      public static void SetProgressNoAnimation([NotNull] this ProgressBar pb, int value)
      {
        if (value == pb.Maximum)
        {
          pb.Maximum = value + 1;
          pb.Value = value + 1;
          pb.Maximum = value;
        }
        else
          pb.Value = value + 1;
        pb.Value = value;
      }
    }
}
