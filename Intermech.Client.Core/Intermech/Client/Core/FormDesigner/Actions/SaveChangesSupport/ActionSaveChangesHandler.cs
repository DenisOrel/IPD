
// Type: Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport.ActionSaveChangesHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System.Windows;


namespace Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport;

/// <summary>Обработка действия c поддержкой сохранения изменений</summary>
public abstract class ActionSaveChangesHandler : IFormDesignerActionHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  private void DoAcceptChanges(object button, object form)
  {
    DesForm desForm = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    if (desForm == null || attrButton == null || !(attrButton.FormDesignerActionParams is ActionSaveChangesParams designerActionParams) || !desForm.Modified)
      return;
    switch (designerActionParams.SaveChangesMode)
    {
      case ActionSaveChangesMode.Discard:
        this.Cancel(form);
        break;
      case ActionSaveChangesMode.Apply:
        this.Apply(form);
        break;
      case ActionSaveChangesMode.Confirm:
        if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_178"), LocalizationHolder.rm.GetString("Client.Core_135"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
          break;
        this.Apply(form);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  protected abstract bool DoButtonEnabled(object button, object form);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  protected abstract void DoButtonPressed(object button, object form);

  /// <summary>Check button's state</summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form) => this.DoButtonEnabled(button, form);

  /// <summary>Implementation of button's press events</summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
    this.DoAcceptChanges(button, form);
    this.DoButtonPressed(button, form);
  }
}
