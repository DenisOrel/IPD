// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Ceh_Route.CehRoutesStringEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard.Ceh_Route;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Ceh_Route;

/// <summary>Редактор правила сбора строки расцеховки</summary>
internal class CehRoutesStringEditor : IPropertyPageSearchOptionEvents, IPropertyPage
{
  /// <summary>
  /// 
  /// </summary>
  private CehRoutesStringControl _control;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControl()
  {
    if (this._control != null)
      return;
    this._control = new CehRoutesStringControl();
    this._control.Changed += new EventHandler(this.OnChanged);
    this._control.ReadOnly = !ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, true).IsAdmin;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadData()
  {
    if (this._control == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICehRouteStringItem cehRouteStringItem;
      ServiceUtils.GetService<ICehRouteStringService>((object) sessionKeeper.Session, true).LoadSettings(sessionKeeper.Session.SessionGUID, out cehRouteStringItem);
      this._control.CehRouteStrItem = cehRouteStringItem;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveData()
  {
    if (this._control == null || this._control.ReadOnly)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<ICehRouteStringService>((object) sessionKeeper.Session, true).SaveSettings(sessionKeeper.Session.SessionGUID, this._control.CehRouteStrItem);
  }

  /// <summary>Конструктор</summary>
  private CehRoutesStringEditor()
  {
    this.PageName = LocalizationHolder.rm.GetString("TechCard.Client_106");
  }

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

  /// <summary>Контрол для вставки</summary>
  public object Control
  {
    get
    {
      if (this._control != null)
      {
        if (!this._control.Modified)
          this.LoadData();
      }
      else
        this.InitializeControl();
      return (object) this._control;
    }
  }

  /// <summary>Принять изменения</summary>
  public void Apply() => this.SaveData();

  /// <summary>Тип возвращаемого объекта</summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>Заголовок</summary>
  public string PageName { get; }

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
  public string HelpTopicID => "1445";

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
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("TechCard.Client_105"), (IPropertyPage) new CehRoutesStringEditor());
  }
}
