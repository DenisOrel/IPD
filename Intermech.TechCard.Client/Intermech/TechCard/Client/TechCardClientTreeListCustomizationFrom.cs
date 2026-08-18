// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechCardClientTreeListCustomizationFrom
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>
/// Форма для настройки видимости столбцов в DevExpess.TreeList (obsoleted)
/// </summary>
public class TechCardClientTreeListCustomizationFrom : Form
{
  /// <summary>Вызов формы</summary>
  /// <param name="objTypeId"></param>
  /// <param name="customTreeList"></param>
  /// <returns></returns>
  public static bool ShowModal(int objTypeId, TreeList customTreeList)
  {
    return TechCardClientTreeListCustomizationFrom.ShowModal(objTypeId, customTreeList, string.Empty);
  }

  /// <summary>Вызов формы</summary>
  /// <param name="objTypeId"></param>
  /// <param name="customTreeList"></param>
  /// <param name="dlgCaption"></param>
  /// <returns></returns>
  public static bool ShowModal(int objTypeId, TreeList customTreeList, string dlgCaption)
  {
    Form form = new Form();
    TechCardClientTreeListCustomizationView customizationView = new TechCardClientTreeListCustomizationView();
    customizationView.ObjTypeID = objTypeId;
    customizationView.CustomTreeList = customTreeList;
    customizationView.LoadData();
    customizationView.pnlButtons.Visible = true;
    form.Text = dlgCaption;
    form.CancelButton = (IButtonControl) customizationView.btnCancel;
    form.AcceptButton = (IButtonControl) customizationView.btnOk;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(320, 420);
    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    customizationView.Parent = (Control) form;
    customizationView.Dock = DockStyle.Fill;
    customizationView.BringToFront();
    customizationView.Show();
    int num = (int) form.ShowDialog();
    return form.DialogResult == DialogResult.OK;
  }
}
