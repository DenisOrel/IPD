// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.CehRoutesElemsListDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.TechCard.Client.Tools.Controls;
using Intermech.TechCard.Client.UI.Controls;
using Intermech.TechCard.Client.UI.Forms;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>Диалог выбора элемнтов РМ</summary>
public class CehRoutesElemsListDlg
{
  /// <summary>Вызов диалога</summary>
  /// <param name="artObjectId">Ид. версии изделия</param>
  /// <param name="productionObjectId">Ид. версии вида производства</param>
  /// <param name="routeElemNodes">Список выбранных РЗ</param>
  /// <returns></returns>
  public static bool ShowDialog(
    long artObjectId,
    long productionObjectId,
    ref RouteElemClassList routeElemNodes)
  {
    Form form = new Form();
    CehRoutesElemsListView viewCrel = new CehRoutesElemsListView();
    viewCrel.ArticleObjectId = artObjectId;
    viewCrel.ProductionObjectId = productionObjectId;
    viewCrel.pnlButtons.Visible = true;
    viewCrel.tlElemRoutes.CheckBoxes = CheckBoxesStyle.TwoState;
    form.CancelButton = (IButtonControl) viewCrel.btnCancel;
    form.AcceptButton = (IButtonControl) viewCrel.btnApply;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(420, 520);
    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    viewCrel.Parent = (Control) form;
    viewCrel.Dock = DockStyle.Fill;
    viewCrel.BringToFront();
    CehRoutesElemsListDlg.LoadSettings(form);
    CehRoutesElemsListDlg.LoadSettings(viewCrel);
    viewCrel.LoadData();
    viewCrel.MultiRoute = true;
    viewCrel.RouteElemNodes = routeElemNodes;
    int num = (int) form.ShowDialog();
    CehRoutesElemsListDlg.SaveSettings(form);
    CehRoutesElemsListDlg.SaveSettings(viewCrel);
    if (form.DialogResult == DialogResult.OK)
    {
      routeElemNodes = viewCrel.RouteElemNodes;
      return true;
    }
    routeElemNodes = (RouteElemClassList) null;
    return false;
  }

  /// <summary>Вызов диалога</summary>
  /// <param name="artObjID">  Ид. версии изделия</param>
  /// <param name="productionObjectId"> Ид. версии вида производства</param>
  /// <param name="procRouteId">Ид. версии маршрута</param>
  /// <param name="routeObjectId">Ид. версии цеха</param>
  /// <param name="routeElemNodes">Ид. версии элемента расцеховки</param>
  /// <returns></returns>
  public static bool ShowDialog(
    long articleObjectId,
    long productionObjectId,
    ref long procRouteId,
    ref long routeObjectId,
    ref RouteElemClassList routeElemNodes)
  {
    Form form = new Form();
    CehRoutesElemsListView viewCrel = new CehRoutesElemsListView();
    viewCrel.ArticleObjectId = articleObjectId;
    viewCrel.ProductionObjectId = productionObjectId;
    viewCrel.pnlButtons.Visible = true;
    viewCrel.tlElemRoutes.CheckBoxes = CheckBoxesStyle.TwoState;
    form.CancelButton = (IButtonControl) viewCrel.btnCancel;
    form.AcceptButton = (IButtonControl) viewCrel.btnApply;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(sc_19464.ssp_techcard_19465(745448014), 520);
    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    viewCrel.Parent = (Control) form;
    viewCrel.Dock = DockStyle.Fill;
    viewCrel.BringToFront();
    viewCrel.Show();
    CehRoutesElemsListDlg.LoadSettings(form);
    CehRoutesElemsListDlg.LoadSettings(viewCrel);
    viewCrel.LoadData();
    viewCrel.MultiRoute = false;
    viewCrel.MoObjectId = procRouteId;
    viewCrel.RouteObjectId = routeObjectId;
    viewCrel.RouteElemNodes = routeElemNodes;
    int num = (int) form.ShowDialog();
    CehRoutesElemsListDlg.SaveSettings(form);
    CehRoutesElemsListDlg.SaveSettings(viewCrel);
    if (form.DialogResult == DialogResult.OK)
    {
      procRouteId = viewCrel.MoObjectId;
      routeObjectId = viewCrel.RouteObjectId;
      routeElemNodes = viewCrel.RouteElemNodes;
      return true;
    }
    procRouteId = 0L;
    routeObjectId = 0L;
    routeElemNodes = (RouteElemClassList) null;
    return false;
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="form">форма, для которой надо загрузить параметры</param>
  private static void LoadSettings(Form form)
  {
    TechCardFormUtils.LoadSettings((Control) form, TechCardFormUtils.Mode.All);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="viewCrel"></param>
  internal static void LoadSettings(CehRoutesElemsListView viewCrel)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open("CehRoutesElemListDlg");
    TechCardClientIGridUtils.LoadSettings(config, viewCrel.igrdProcRoutes);
    TechCardClientIGridUtils.LoadSettings(config, viewCrel.igrdCehRoutes);
    TechcardClientTreeListUtils.LoadSettings(config, viewCrel.tlElemRoutes);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  /// <param name="form">форма, для которой надо сохранить параметры</param>
  private static void SaveSettings(Form form)
  {
    TechCardFormUtils.SaveSettings((Control) form, TechCardFormUtils.Mode.All);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="viewCrel"></param>
  internal static void SaveSettings(CehRoutesElemsListView viewCrel)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open("CehRoutesElemListDlg") ?? service.Create("CehRoutesElemListDlg");
    if (config == null)
      return;
    TechCardClientIGridUtils.SaveSettings(config, viewCrel.igrdProcRoutes);
    TechCardClientIGridUtils.SaveSettings(config, viewCrel.igrdCehRoutes);
    TechcardClientTreeListUtils.SaveSettings(config, viewCrel.tlElemRoutes);
  }
}
