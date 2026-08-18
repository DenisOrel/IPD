
// Type: Intermech.UI.ActionConfirmations.YesNoActionConfirmation
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.UI.Winforms;
using System;
using System.Windows.Forms;


namespace Intermech.UI.ActionConfirmations;

public abstract class YesNoActionConfirmation
{
  private static readonly object currentSessionSyncRoot = new object();
  private static Tuple<int, ActionConfirmationMode> currentSessionValue;
  private string actionKey;
  private bool defaultValue;

  protected YesNoActionConfirmation(string actionKey, bool defaultValue)
  {
    this.actionKey = actionKey != null ? actionKey : throw new ArgumentNullException(nameof (actionKey));
    this.defaultValue = defaultValue;
  }

  protected abstract string GetActionCaption();

  protected abstract string GetActionText();

  public bool ConfirmAction()
  {
    Tuple<int, ActionConfirmationMode> valueAndMode = this.TryGetValue() ?? new Tuple<int, ActionConfirmationMode>(this.defaultValue ? 1 : 0, ActionConfirmationMode.AskUser);
    if (valueAndMode.Item2 == ActionConfirmationMode.AskUser)
    {
      valueAndMode = this.ConfirmActionInDialogMode(this.GetActionCaption(), this.GetActionText(), valueAndMode.Item1);
      if (valueAndMode.Item2 != ActionConfirmationMode.AskUser)
        this.PutValue(valueAndMode);
    }
    return valueAndMode.Item1 == 1;
  }

  private Tuple<int, ActionConfirmationMode> TryGetValue()
  {
    lock (YesNoActionConfirmation.currentSessionSyncRoot)
      return YesNoActionConfirmation.currentSessionValue;
  }

  private void PutValue(Tuple<int, ActionConfirmationMode> valueAndMode)
  {
    lock (YesNoActionConfirmation.currentSessionSyncRoot)
      YesNoActionConfirmation.currentSessionValue = valueAndMode;
  }

  private Tuple<int, ActionConfirmationMode> ConfirmActionInDialogMode(
    string caption,
    string text,
    int value)
  {
    CustomMessageBox customMessageBox = new CustomMessageBox();
    customMessageBox.Caption = caption;
    customMessageBox.Text = text;
    customMessageBox.Icon = MessageBoxIcon.Question;
    customMessageBox.Buttons.Add(new CustomMessageBoxButton()
    {
      Text = "Да, для всех",
      CustomDialogResult = (object) YesNoActionConfirmation.YesNoDialogResult.YesToAll
    });
    customMessageBox.Buttons.Add(new CustomMessageBoxButton()
    {
      Text = "Да",
      CustomDialogResult = (object) YesNoActionConfirmation.YesNoDialogResult.Yes
    });
    customMessageBox.Buttons.Add(new CustomMessageBoxButton()
    {
      Text = "Нет",
      CustomDialogResult = (object) YesNoActionConfirmation.YesNoDialogResult.No
    });
    customMessageBox.Buttons.Add(new CustomMessageBoxButton()
    {
      Text = "Нет, для всех",
      CustomDialogResult = (object) YesNoActionConfirmation.YesNoDialogResult.NoToAll
    });
    if (value == 1)
      customMessageBox.Buttons[1].IsDefaultButton = true;
    else
      customMessageBox.Buttons[2].IsDefaultButton = true;
    switch ((YesNoActionConfirmation.YesNoDialogResult) customMessageBox.ShowDialog())
    {
      case YesNoActionConfirmation.YesNoDialogResult.Yes:
        return new Tuple<int, ActionConfirmationMode>(1, ActionConfirmationMode.AskUser);
      case YesNoActionConfirmation.YesNoDialogResult.YesToAll:
        return new Tuple<int, ActionConfirmationMode>(1, ActionConfirmationMode.UseInSession);
      case YesNoActionConfirmation.YesNoDialogResult.NoToAll:
        return new Tuple<int, ActionConfirmationMode>(0, ActionConfirmationMode.UseInSession);
      default:
        return new Tuple<int, ActionConfirmationMode>(0, ActionConfirmationMode.AskUser);
    }
  }

  private enum YesNoDialogResult
  {
    Yes,
    YesToAll,
    No,
    NoToAll,
  }
}
