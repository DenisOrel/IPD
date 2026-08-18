// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRoute2TpObjDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.TechCard.Client.UI.Controls;
using Intermech.TechCard.Client.UI.Forms;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Диалог привязки элементов к расцеховке</summary>
public class CehRoute2TpObjDlg
{
  /// <summary>Вызов диалога</summary>
  /// <param name="moObjId">Ид. версии маршрута </param>
  /// <returns></returns>
  public static bool ShowDialog(long moObjId)
  {
    Form form = new Form();
    CehRoute2TpObjView cehRouteView = new CehRoute2TpObjView();
    cehRouteView.pnlButtons.Visible = true;
    form.CancelButton = (IButtonControl) cehRouteView.btnCancel;
    form.AcceptButton = (IButtonControl) cehRouteView.btnApply;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(sc_19436.ssp_techcard_19437(1036753469), 520);
    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    cehRouteView.Parent = (Control) form;
    cehRouteView.Dock = DockStyle.Fill;
    cehRouteView.BringToFront();
    cehRouteView.Show();
    CehRoute2TpObjDlg.LoadSettings(form);
    CehRoute2TpObjDlg.LoadSettings(cehRouteView);
    cehRouteView.MoObjId = moObjId;
    cehRouteView.LoadData();
    int num = (int) form.ShowDialog();
    CehRoute2TpObjDlg.SaveSettings(cehRouteView);
    CehRoute2TpObjDlg.SaveSettings(form);
    return form.DialogResult == DialogResult.OK;
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="form">форма, для которой надо загрузить параметры</param>
  private static void LoadSettings(Form form)
  {
    TechCardFormUtils.LoadSettings((Control) form, TechCardFormUtils.Mode.All);
  }

  /// <summary>Загрузка параметров контролов</summary>
  /// <param name="cehRouteView"></param>
  internal static void LoadSettings(CehRoute2TpObjView cehRouteView)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open("CehRoute2TpObjViewDlg");
    TechcardClientTreeListUtils.LoadSettings(config, cehRouteView.tlCehRoutes);
    TechcardClientTreeListUtils.LoadSettings(config, cehRouteView.tlElemRoutes);
    TechcardClientTreeListUtils.LoadSettings(config, cehRouteView.tlTpAll);
  }

  /// <summary>Сохранение расположения и размеров, свойств формы</summary>
  /// <param name="form">форма, для которой надо сохранить параметры</param>
  private static void SaveSettings(Form form)
  {
    TechCardFormUtils.SaveSettings((Control) form, TechCardFormUtils.Mode.All);
  }

  /// <summary>Сохранение параметров контролов</summary>
  /// <param name="cehRouteView"></param>
  internal static void SaveSettings(CehRoute2TpObjView cehRouteView)
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open("CehRoute2TpObjViewDlg") ?? service.Create("CehRoute2TpObjViewDlg");
    if (config == null)
      return;
    TechcardClientTreeListUtils.SaveSettings(config, cehRouteView.tlCehRoutes);
    TechcardClientTreeListUtils.SaveSettings(config, cehRouteView.tlElemRoutes);
    TechcardClientTreeListUtils.SaveSettings(config, cehRouteView.tlTpAll);
  }
}
