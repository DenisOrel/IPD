// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DSettingsPage
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>Закладка настроек интеграции c Сadmech 3D</summary>
internal class Cadmech3DSettingsPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Наименование закладки</summary>
  private string _pageName = string.Empty;
  /// <summary>Контрол с настройками</summary>
  private Cadmech3DSettingsControl _control;
  /// <summary>
  /// 
  /// </summary>
  private static Cadmech3DSettingsPage _instance;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._pageName = LocalizationHolder.rm.GetString("TechCard.Client_487");
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControl()
  {
    if (this._control != null)
      return;
    this._control = new Cadmech3DSettingsControl();
  }

  /// <summary>Загрузка настроек</summary>
  private void LoadSettings()
  {
    if (this._control == null)
      return;
    this._control.LoadSettings();
  }

  /// <summary>Cохранение настроек</summary>
  private void SaveSettings()
  {
    if (this._control == null)
      return;
    this._control.SaveSettings();
  }

  /// <summary>
  /// 
  /// </summary>
  public Cadmech3DSettingsPage() => this.InitializeData();

  /// <summary>
  /// 
  /// </summary>
  internal static void RegisterSettingsPage()
  {
  }

  /// <summary>Отменить изменения</summary>
  public void Cancel() => this.LoadSettings();

  /// <summary>
  /// 
  /// </summary>
  public object Control
  {
    get
    {
      if (this._control == null)
        this.InitializeControl();
      return (object) this._control;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void Apply() => this.SaveSettings();

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
}
