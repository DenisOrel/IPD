// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node.NumNodeEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node;

/// <summary>NumNodeEditor</summary>
public class NumNodeEditor
{
  /// <summary>ShowDialog</summary>
  /// <param name="objID"></param>
  /// <returns></returns>
  public static bool ShowDialog(long objID)
  {
    Form currentControl = new Form();
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1450);
    NumNodeView numNodeView = new NumNodeView();
    numNodeView._objectID = objID;
    numNodeView.DataLoad();
    numNodeView.btnApply.Enabled = false;
    currentControl.CancelButton = (IButtonControl) numNodeView.btnCancel;
    currentControl.AcceptButton = (IButtonControl) numNodeView.btnApply;
    currentControl.StartPosition = FormStartPosition.CenterScreen;
    currentControl.AutoSize = true;
    currentControl.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    currentControl.Text = LocalizationHolder.rm.GetString("TechCard.Client_228");
    numNodeView.Parent = (Control) currentControl;
    numNodeView.Dock = DockStyle.Fill;
    numNodeView.BringToFront();
    numNodeView.Show();
    int num = (int) currentControl.ShowDialog();
    return currentControl.DialogResult == DialogResult.OK;
  }
}
