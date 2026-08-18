// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.UI.ApplyFileNameRulePresenter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Mvp;
using System;

#nullable disable
namespace Intermech.Tools.UI;

internal sealed class ApplyFileNameRulePresenter : Presenter<IApplyFileNameRuleView>
{
  private FileNameRule rule;
  private bool success;
  private FileNameRuleAction userAnswer;

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

  public FileNameRuleAction UserAnswer => this.userAnswer;

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
    this.userAnswer = FileNameRuleAction.AllowForAll;
    this.View.Description = string.Format(LocalizationHolder.rm.GetString("SR_543"), (object) this.rule.ObjectType, (object) this.rule);
    this.View.OperationConfirmed += new EventHandler(this.OnApplyChanges);
  }

  protected override void OnDetachView()
  {
    base.OnDetachView();
    this.View.OperationConfirmed -= new EventHandler(this.OnApplyChanges);
  }

  private void OnApplyChanges(object sender, EventArgs e)
  {
    this.userAnswer = this.View.UserAnswer;
    this.success = true;
  }
}
