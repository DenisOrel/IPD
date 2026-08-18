// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Imbase.ImObjFilterSetupEditor
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
public class ImObjFilterSetupEditor : IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Наименование закладки</summary>
  private string _pageName;
  /// <summary>Контрол с настройками</summary>
  private ImbaseObjectFilterSetupCtrl _control;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._pageName = LocalizationHolder.rm.GetString("TechCard.Client_457");
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControls()
  {
    if (this._control != null)
      return;
    this._control = new ImbaseObjectFilterSetupCtrl();
    this._control.FilterTune.DirtyChanged += new EventHandler(this.OnChanged);
    this._control.LoadData();
  }

  /// <summary>Загрузка данных</summary>
  private void LoadData()
  {
    if (this._control == null)
      return;
    this._control.FilterTune.LoadData();
  }

  /// <summary>Cохранение данных</summary>
  private void SaveData()
  {
    if (this._control == null)
      return;
    this._control.FilterTune.SaveData();
  }

  /// <summary>Contructor</summary>
  public ImObjFilterSetupEditor() => this.InitializeData();

  /// <summary>Событие на изменения</summary>
  /// <param name="sender">вызвавший объект</param>
  /// <param name="e">параметры</param>
  protected virtual void OnChanged(object sender, EventArgs e)
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
      if (this._control != null)
      {
        if (!this._control.FilterTune.Dirty)
          this._control.LoadData();
      }
      else
        this.InitializeControls();
      return (object) this._control;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Apply() => this.SaveData();

  /// <summary>
  /// 
  /// </summary>
  public PropertyPageType Type
  {
    [DebuggerStepThrough] get => PropertyPageType.Control;
  }

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

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => "";

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
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("TechCard.Client_456"), (IPropertyPage) new ImObjFilterSetupEditor());
  }
}
