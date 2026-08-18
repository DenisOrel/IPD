
// Type: Intermech.Controls.PrintPreviewController
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.Drawing.Printing;


namespace Intermech.Controls;

/// <summary>
/// 
/// </summary>
public class PrintPreviewController : PrintController
{
  /// <summary>
  /// 
  /// </summary>
  public override bool IsPreview => base.IsPreview;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="document"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
  {
    return base.OnStartPage(document, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="document"></param>
  /// <param name="e"></param>
  public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
  {
    base.OnStartPrint(document, e);
  }
}
