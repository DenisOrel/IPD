// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcElemRouteDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.Controls;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Forms;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>Диалог выбора РЭ расцеховки для ТП</summary>
public static class TechProcElemRouteDlg
{
  /// <summary>Вызов диалога</summary>
  /// <param name="techProcessId">Ид. версии техпроцесса </param>
  /// <param name="techProcessObj">Данные по техпроцессу + цехозаходам</param>
  /// <param name="cehRoutesObj">Данные по расцеховке</param>
  /// <param name="routeElemList">Выбранные РЭ</param>
  /// <returns></returns>
  public static bool ShowDialog(
    long techProcessId,
    out TechProcClass techProcessObj,
    out CehRouteClass cehRoutesObj,
    ref CehRouteElementList routeElemList)
  {
    techProcessObj = (TechProcClass) null;
    cehRoutesObj = (CehRouteClass) null;
    routeElemList.Clear();
    Form form = new Form();
    TechProcElemRouteView cehRouteView = new TechProcElemRouteView();
    cehRouteView.pnlButtons.Visible = true;
    form.CancelButton = (IButtonControl) cehRouteView.btnCancel;
    form.AcceptButton = (IButtonControl) cehRouteView.btnApply;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(420, 520);
    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    cehRouteView.Parent = (Control) form;
    cehRouteView.Dock = DockStyle.Fill;
    cehRouteView.BringToFront();
    cehRouteView.Show();
    TechProcElemRouteDlg.LoadSettings(form);
    TechProcElemRouteDlg.LoadSettings(cehRouteView);
    cehRouteView.TechProcId = techProcessId;
    cehRouteView.LoadData();
    int num = (int) form.ShowDialog();
    TechProcElemRouteDlg.SaveSettings(cehRouteView);
    TechProcElemRouteDlg.SaveSettings(form);
    if (form.DialogResult != DialogResult.OK)
      return false;
    techProcessObj = cehRouteView.TechProcObj;
    cehRoutesObj = cehRouteView.CehRoutesObj;
    cehRouteView.GetCehRouteElems(ref routeElemList);
    return true;
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="form">форма, для которой надо загрузить параметры</param>
  private static void LoadSettings(Form form)
  {
    TechCardFormUtils.LoadSettings((Control) form, TechCardFormUtils.Mode.All);
  }

  /// <summary>Загрузка настроек формы</summary>
  /// <param name="cehRouteView"></param>
  private static void LoadSettings(TechProcElemRouteView cehRouteView)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open("TechProcElemRouteViewDlg");
    if (config == null || cehRouteView.tolcElemRouteList == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) cehRouteView.tolcElemRouteList);
  }

  /// <summary>Сохранение расположения и размеров, свойств формы</summary>
  /// <param name="form">форма, для которой надо сохранить параметры</param>
  private static void SaveSettings(Form form)
  {
    TechCardFormUtils.SaveSettings((Control) form, TechCardFormUtils.Mode.All);
  }

  /// <summary>Сохранение настроек формы</summary>
  /// <param name="cehRouteView"></param>
  private static void SaveSettings(TechProcElemRouteView cehRouteView)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open("TechProcElemRouteViewDlg") ?? service.Create("TechProcElemRouteViewDlg");
    if (config == null || cehRouteView.tolcElemRouteList == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) cehRouteView.tolcElemRouteList);
  }
}
