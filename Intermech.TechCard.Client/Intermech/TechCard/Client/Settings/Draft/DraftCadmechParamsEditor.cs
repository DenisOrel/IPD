// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Draft.DraftCadmechParamsEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechAcad;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Draft;

internal class DraftCadmechParamsEditor : IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>
  /// 
  /// </summary>
  private bool _paramsLoaded;
  /// <summary>
  /// 
  /// </summary>
  private DraftCadmechParamsControl _control;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControl()
  {
    if (this._control != null)
      return;
    this._control = new DraftCadmechParamsControl();
    this._control.Changed += new EventHandler(this.OnChanged);
    this._control.LoadParams(false);
    this._paramsLoaded = true;
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

  /// <summary>
  /// 
  /// </summary>
  public DraftCadmechParamsEditor()
  {
    this.PageName = LocalizationHolder.rm.GetString("TechCard.Client_108");
  }

  /// <summary>Отменить изменения</summary>
  public void Cancel()
  {
    if (this._control == null)
      return;
    this._control.LoadParams(false);
    this._paramsLoaded = false;
  }

  /// <summary>Контрол для вставки</summary>
  public object Control
  {
    get
    {
      if (this._control != null)
      {
        if (!this._paramsLoaded)
        {
          this._control.LoadParams(false);
          this._paramsLoaded = true;
        }
      }
      else
        this.InitializeControl();
      return (object) this._control;
    }
  }

  /// <summary>Принять изменения</summary>
  public void Apply()
  {
    if (this._control == null || !this._paramsLoaded)
      return;
    this._control.SaveParams();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechAcadParamsService customService = sessionKeeper.Session.GetCustomService(typeof (ITechAcadParamsService)) as ITechAcadParamsService;
      TechAcadParamsHelper.SaveData(this._control.DraftParamsItem, sessionKeeper.Session, customService);
    }
    this._paramsLoaded = false;
  }

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

  /// <summary>вернуть id раздела в справке для данной страницы</summary>
  public string HelpTopicID => "1447";

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>Регистрации закладки в сервисах</summary>
  internal static void RegisterSettingsPage()
  {
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("TechCard.Client_107"), (IPropertyPage) new DraftCadmechParamsEditor());
  }
}
