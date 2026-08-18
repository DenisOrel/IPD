// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcNumerationRules.NumRuleEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.TechCard.Client.UI.Forms;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcNumerationRules;

/// <summary>
/// Класс для создания окна диалога редактирования правил нумерации
/// </summary>
public class NumRuleEditor
{
  /// <summary>Вызов диалога редактирования условия правил нумерации</summary>
  /// <param name="numRule">Правило нумерации</param>
  /// <param name="numNode">Элемент правила нумерации</param>
  /// <returns>true - если нажата кнопка "ОК", иначе false</returns>
  public static bool ShowDialog(ref TechNumerationRule numRule, ref TechNumerationNode numNode)
  {
    Form currentControl = new Form();
    NumRuleControl numRuleControl = new NumRuleControl();
    currentControl.CancelButton = (IButtonControl) numRuleControl.btnCancel;
    currentControl.AcceptButton = (IButtonControl) numRuleControl.btnApply;
    currentControl.StartPosition = FormStartPosition.CenterScreen;
    currentControl.Size = new Size(420, 520);
    currentControl.FormBorderStyle = FormBorderStyle.Sizable;
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1451);
    currentControl.Text = LocalizationHolder.rm.GetString("TechCard.Client_234");
    numRuleControl.Parent = (Control) currentControl;
    numRuleControl.Dock = DockStyle.Fill;
    numRuleControl.BringToFront();
    numRuleControl.Show();
    numRuleControl.NumRule = numRule;
    numRuleControl.NumNode = numNode;
    numRuleControl.btnApply.Enabled = false;
    int num = (int) currentControl.ShowDialog();
    return currentControl.DialogResult == DialogResult.OK;
  }

  /// <summary>Вызов диалога редактирования условия правил нумерации</summary>
  /// <param name="objectId">Ид. версии правила нумерации</param>
  /// <param name="creatingMode"></param>
  /// <returns>true - если нажата кнопка "ОК", иначе false</returns>
  public static bool ShowDialog(long objectId, bool creatingMode)
  {
    Form currentControl = new Form();
    NumRuleObjControl numRuleObjControl = new NumRuleObjControl();
    currentControl.CancelButton = (IButtonControl) numRuleObjControl.btnCancel;
    currentControl.AcceptButton = (IButtonControl) numRuleObjControl.btnApply;
    currentControl.StartPosition = FormStartPosition.CenterScreen;
    currentControl.Size = new Size(420, 480);
    currentControl.FormBorderStyle = FormBorderStyle.Sizable;
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1451);
    currentControl.Text = LocalizationHolder.rm.GetString("TechCard.Client_235");
    numRuleObjControl.Parent = (Control) currentControl;
    numRuleObjControl.Dock = DockStyle.Fill;
    numRuleObjControl.BringToFront();
    numRuleObjControl.Show();
    numRuleObjControl.ObjectID = objectId;
    numRuleObjControl.btnApply.Enabled = false;
    if (creatingMode)
      numRuleObjControl.btnApply.Enabled = numRuleObjControl.btnCancel.Enabled = true;
    int num = (int) currentControl.ShowDialog();
    return currentControl.DialogResult == DialogResult.OK;
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
