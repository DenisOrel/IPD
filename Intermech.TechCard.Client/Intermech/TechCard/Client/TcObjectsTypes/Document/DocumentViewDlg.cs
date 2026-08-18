// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Document.DocumentViewDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.Controls;
using Intermech.TechCard.Client.Tools.Controls.Navigator;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Document;

/// <summary>
/// 
/// </summary>
public static class DocumentViewDlg
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="documentView"></param>
  internal static void LoadSettings(DocumentView documentView)
  {
    IConfiguration config = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false)?.Open(nameof (DocumentViewDlg));
    if (config == null || documentView.tolcDocList == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) documentView.tolcDocList);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="documentView"></param>
  internal static void SaveSettings(DocumentView documentView)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(nameof (DocumentViewDlg)) ?? service.Create(nameof (DocumentViewDlg));
    if (config == null || documentView.tolcDocList == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) documentView.tolcDocList);
  }
}
