
// Type: Intermech.UI.PropertyPages.PropertyPageMvpAdapter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Mvp;
using System;
using System.Collections.Generic;


namespace Intermech.UI.PropertyPages;

public sealed class PropertyPageMvpAdapter : 
  IPropertyPage,
  IPropertyPageActivationEvents,
  IPropertyPageSearchOptionEvents
{
  private readonly string pageName;
  private readonly IPropertyPageMvpModel model;
  private readonly IView view;
  private readonly IPropertyPageMvpPresenter presenter;
  private bool isChanged;

  public PropertyPageMvpAdapter(
    string pageName,
    IPropertyPageMvpModel model,
    IView view,
    IPropertyPageMvpPresenter presenter)
  {
    if (pageName == null)
      throw new ArgumentNullException(nameof (pageName));
    if (model == null)
      throw new ArgumentNullException(nameof (model));
    if (view == null)
      throw new ArgumentNullException(nameof (view));
    if (presenter == null)
      throw new ArgumentNullException(nameof (presenter));
    this.pageName = pageName;
    this.model = model;
    this.view = view;
    this.presenter = presenter;
  }

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this.view;

  public string PageName => this.pageName;

  public string HeaderText => this.pageName;

  public string HelpTopicID => string.Empty;

  public event EventHandler Changed;

  public void Apply()
  {
    if (!this.isChanged)
      return;
    if (this.view.DisplayState.IsViewShown)
      this.presenter.AcceptChanges();
    this.model.SaveChanges();
    this.isChanged = false;
  }

  public void Cancel()
  {
    if (!this.isChanged)
      return;
    this.isChanged = false;
  }

  public void InitializePage()
  {
    this.model.Reset();
    this.isChanged = false;
  }

  public void BeforeActivatePage()
  {
  }

  public void AfterActivatePage()
  {
    this.presenter.SettingsChanged += new EventHandler(this.OnSettingsChanged);
  }

  public void BeforeDeactivatePage()
  {
    this.presenter.SettingsChanged -= new EventHandler(this.OnSettingsChanged);
    this.presenter.AcceptChanges();
  }

  public void AfterDeactivatePage()
  {
  }

  private void OnSettingsChanged(object sender, EventArgs e)
  {
    if (this.isChanged)
      return;
    this.isChanged = true;
    if (this.Changed == null)
      return;
    this.Changed((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }
}
