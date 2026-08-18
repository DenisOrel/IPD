// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.UI.EditFileNameRulePresenter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Tools.UI;

internal sealed class EditFileNameRulePresenter : Presenter<IEditFileNameRuleView>
{
  private FileNameRule rule;
  private bool success;

  public FileNameRule Rule
  {
    get => this.rule;
    set
    {
      this.CheckAllowPropertyChange();
      this.rule = value;
    }
  }

  public bool Success => this.success;

  /// <summary>
  /// Позволяет проверить корректность инициализации представления. Метод вызывается перед подключением к виду.
  /// </summary>
  /// <exception cref="T:Intermech.Mvp.PresenterPropertyException">Указанное свойство преставления некорректно</exception>
  /// <exception cref="T:Intermech.Mvp.MvpException">Представление не было корректно инициализировано</exception>
  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.Rule == null)
      throw new PresenterPropertyException("Rule");
  }

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.success = false;
    this.View.Description = string.Format(LocalizationHolder.rm.GetString("SR_544"), (object) this.rule.ObjectType);
    this.View.Extension = this.rule.Extension;
    this.View.NamePattern = this.rule.NamePattern;
    this.View.Directory = this.rule.Directory;
    this.View.OperationConfirmed += new EventHandler(this.OnApplyChanges);
  }

  protected override void OnDetachView()
  {
    base.OnDetachView();
    this.View.OperationConfirmed -= new EventHandler(this.OnApplyChanges);
  }

  private void OnApplyChanges(object sender, EventArgs e)
  {
    string str1 = EditFileNameRulePresenter.TrimText(this.View.Extension);
    string str2 = EditFileNameRulePresenter.TrimText(this.View.NamePattern);
    string str3 = EditFileNameRulePresenter.TrimText(this.View.Directory);
    if (string.IsNullOrEmpty(str1) || !Regex.IsMatch(str1, "\\.\\w+"))
    {
      MvpContext.ViewService.ShowModal((IPresenter) new SimpleMessagePresenter(LocalizationHolder.rm.GetString("SR_545"), LocalizationHolder.rm.GetString("Tools.Components_347"), MessageIcon.Error));
      this.View.ResetSuccess();
    }
    if (EditFileNameRulePresenter.IsTextChanged(this.rule.Extension, str1) || EditFileNameRulePresenter.IsTextChanged(this.rule.NamePattern, str2) || EditFileNameRulePresenter.IsTextChanged(this.rule.Directory, str3))
      this.rule = new FileNameRule(str1, str2, str3, this.rule.ObjectType);
    this.success = true;
  }

  private static string TrimText(string text) => text?.Trim();

  private static bool IsTextChanged(string origValue, string newValue)
  {
    return string.Compare(origValue, newValue, StringComparison.CurrentCultureIgnoreCase) == 0;
  }
}
