// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Ceh_Route.CehRouteSettingsEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard.Ceh_Route.Settings;
using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Ceh_Route;

/// <summary>Редактор настроек расцеховки</summary>
internal class CehRouteSettingsEditor : IPropertyPage
{
  /// <summary>Контрол с настройками</summary>
  private CehRoutesSettingsControl _control;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControl()
  {
    if (this._control != null)
      return;
    this._control = new CehRoutesSettingsControl();
    this._control.Changed += new EventHandler(this.OnChanged);
    this._control.ReadOnly = !ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, true).IsAdmin;
    this.Cancel();
  }

  private void LoadData()
  {
    if (this._control == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAppSettingsService<ICehRouteSettings> service = ServiceUtils.GetService<IAppSettingsService<ICehRouteSettings>>((object) sessionKeeper.Session, true);
      ICehRouteSettings cehRouteSettings = (ICehRouteSettings) new CehRouteSettings();
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      ref ICehRouteSettings local = ref cehRouteSettings;
      if (!service.LoadSettings(sessionGuid, ref local))
        return;
      this._control.CehRouteSettings = cehRouteSettings;
    }
  }

  private void SaveData()
  {
    if (this._control == null || this._control.ReadOnly)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IAppSettingsService<ICehRouteSettings>>((object) sessionKeeper.Session, true).SaveSettings(sessionKeeper.Session.SessionGUID, this._control.CehRouteSettings);
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
      if (this._control == null)
        this.InitializeControl();
      return (object) this._control;
    }
  }

  /// <summary>Принять изменения</summary>
  public void Apply() => this.SaveData();

  /// <summary>Тип возвращаемого объекта</summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>Заголовок</summary>
  public string PageName => LocalizationHolder.rm.GetString("TechCard.Client_102");

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
  public string HelpTopicID => "1446";

  /// <summary>
  /// 
  /// </summary>
  internal static void RegisterSettingsPage(IServiceProvider serviceProvider)
  {
    ServiceUtils.GetService<IPropertyPagesService>((object) serviceProvider, false)?.AddPage(LocalizationHolder.rm.GetString(sc_19752.ssp_techcard_19753()), (IPropertyPage) new CehRouteSettingsEditor());
  }
}
