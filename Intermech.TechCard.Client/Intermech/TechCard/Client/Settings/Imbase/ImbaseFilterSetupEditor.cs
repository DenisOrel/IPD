// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Imbase.ImbaseFilterSetupEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Imbase;

/// <summary>Imbase filter setup editor</summary>
public class ImbaseFilterSetupEditor : IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Наименование закладки</summary>
  private string _pageName;
  /// <summary>Время последнего обновления данных</summary>
  private DateTime _lastDataUpdated = DateTime.MinValue;
  /// <summary>User control</summary>
  private ImbaseFolderFilterSetupCtrl _control;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._pageName = LocalizationHolder.rm.GetString("TechCard.Client_355");
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControls()
  {
    if (this._control != null)
      return;
    this._control = new ImbaseFolderFilterSetupCtrl(-1L, -1L);
    this._control.FilterTune.DirtyChanged += new EventHandler(this.OnChanged);
  }

  /// <summary>Загрузка данных</summary>
  private void LoadData() => this._control?.FilterTune.LoadFilter();

  /// <summary>Constructor</summary>
  public ImbaseFilterSetupEditor() => this.InitializeData();

  /// <summary>Событие на изменения</summary>
  /// <param name="sender">вызвавший объект</param>
  /// <param name="e">параметры</param>
  private void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  /// <summary>Отменить изменения</summary>
  public void Cancel() => this.LoadData();

  /// <summary>
  /// 
  /// </summary>
  public object Control
  {
    get
    {
      this.InitializeControls();
      if (!this._control.FilterTune.Dirty)
      {
        if (this._lastDataUpdated + TimeSpan.FromSeconds(5.0) < DateTime.Now)
        {
          this._control.LoadData(true);
          this._lastDataUpdated = DateTime.Now;
        }
        else
          this._control.LoadData();
      }
      return (object) this._control;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Apply() => this._control?.FilterTune.SaveFilter();

  /// <summary>
  /// 
  /// </summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>Caption</summary>
  public string PageName
  {
    [DebuggerStepThrough] get => this._pageName;
  }

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Событие на изменение</summary>
  public event EventHandler Changed;

  /// <summary>вернуть id раздела в справке для данной страницы</summary>
  public string HelpTopicID => "1448";

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>
  /// 
  /// </summary>
  internal static void RegisterSettingsPage()
  {
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("TechCard.Client_356"), (IPropertyPage) new ImbaseFilterSetupEditor());
  }
}
