// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.TechCardGridCustomizeForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.TechCard.Client.UI.Controls;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls;

/// <summary>Customization form</summary>
public class TechCardGridCustomizeForm : Form
{
  /// <summary>Show dialog</summary>
  /// <param name="objTypeID"></param>
  /// <param name="techGrid"></param>
  /// <returns></returns>
  public static bool ShowModal(int objTypeID, TechCardGrid techGrid)
  {
    Form form = new Form();
    TechCardGridCustomizeView gridCustomizeView = new TechCardGridCustomizeView();
    gridCustomizeView.ObjTypeID = objTypeID;
    gridCustomizeView.TechGrid = techGrid;
    gridCustomizeView.LoadData();
    gridCustomizeView.pnlButtons.Visible = true;
    form.CancelButton = (IButtonControl) gridCustomizeView.btnCancel;
    form.AcceptButton = (IButtonControl) gridCustomizeView.btnOk;
    form.StartPosition = FormStartPosition.CenterScreen;
    form.Size = new Size(320, 420);
    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    gridCustomizeView.Parent = (Control) form;
    gridCustomizeView.Dock = DockStyle.Fill;
    gridCustomizeView.BringToFront();
    gridCustomizeView.Show();
    int num = (int) form.ShowDialog();
    return form.DialogResult == DialogResult.OK;
  }
}
