// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcNumerationRules.NumRuleFormDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcNumerationRules;

/// <summary>
/// Класс для создания окна диалога редактирования правил нумерация
/// </summary>
public static class NumRuleFormDialog
{
  /// <summary>Вызов диалога редактирования условия правил нумерации</summary>
  /// <param name="canSelectObjMode"></param>
  /// <param name="numRule">Правило нумерации</param>
  /// <param name="numNode">Элемент правила нумерации</param>
  /// <param name="objectMode">Область нумерации</param>
  /// <returns>true - если нажата кнопка "ОК", иначе false</returns>
  public static bool ShowDialog(
    bool canSelectObjMode,
    ref TechNumerationRule numRule,
    ref TechNumerationNode numNode,
    ref TechNumerationObjectModes objectMode)
  {
    Form currentControl = new Form();
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1451);
    NumRuleForm numRuleForm = new NumRuleForm();
    numRuleForm.NumRule = numRule;
    numRuleForm.NumNode = numNode;
    numRuleForm.ObjectMode = objectMode;
    numRuleForm.cbNumObjectMode.Enabled = canSelectObjMode;
    currentControl.CancelButton = (IButtonControl) numRuleForm.btnCancel;
    currentControl.AcceptButton = (IButtonControl) numRuleForm.btnApply;
    currentControl.StartPosition = FormStartPosition.CenterScreen;
    currentControl.Size = new Size(440, 460);
    currentControl.FormBorderStyle = FormBorderStyle.FixedDialog;
    currentControl.Text = LocalizationHolder.rm.GetString("TechCard.Client_234");
    numRuleForm.Parent = (Control) currentControl;
    numRuleForm.Dock = DockStyle.Fill;
    numRuleForm.BringToFront();
    numRuleForm.Show();
    int num = (int) currentControl.ShowDialog();
    if (currentControl.DialogResult != DialogResult.OK)
      return false;
    objectMode = numRuleForm.ObjectMode;
    return true;
  }
}
