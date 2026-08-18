// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Process_Route.ProcRouteViewDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Localization;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Process_Route;

/// <summary>Диалог создания нового маршрута обработки</summary>
[Obsolete("Will be removed in IPS 8.0")]
public class ProcRouteViewDlg
{
  /// <summary>Вызов диалога</summary>
  /// <param name="procRouteObj"></param>
  /// <param name="creationMode"></param>
  /// <returns></returns>
  public static bool ShowDialog(ref long procRouteObj, bool creationMode)
  {
    Form form = new Form();
    ProcRouteView procRouteView = new ProcRouteView();
    procRouteView.ProcRoute = procRouteObj;
    procRouteView.btnApply.Enabled = creationMode;
    procRouteView.btnCancel.Enabled = true;
    form.CancelButton = (IButtonControl) procRouteView.btnCancel;
    form.AcceptButton = (IButtonControl) procRouteView.btnApply;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(350, 240 /*0xF0*/);
    form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    form.Text = LocalizationHolder.rm.GetString("TechCard.Client_217");
    procRouteView.Parent = (Control) form;
    procRouteView.Dock = DockStyle.Fill;
    procRouteView.BringToFront();
    procRouteView.Show();
    int num = (int) form.ShowDialog();
    return form.DialogResult == DialogResult.OK;
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="form">форма, для которой надо загрузить параметры</param>
  private static void LoadSettings(Form form)
  {
    TechCardFormUtils.LoadSettings((Control) form, TechCardFormUtils.Mode.LocationOnly);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  /// <param name="form">форма, для которой надо сохранить параметры</param>
  private static void SaveSettings(Form form)
  {
    TechCardFormUtils.SaveSettings((Control) form, TechCardFormUtils.Mode.LocationOnly);
  }
}
