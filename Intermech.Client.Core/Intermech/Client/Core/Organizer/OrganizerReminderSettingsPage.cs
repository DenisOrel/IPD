
// Type: Intermech.Client.Core.Organizer.OrganizerReminderSettingsPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Страница с настройками оповещения пользователя о запланированых задачах.
/// </summary>
public class OrganizerReminderSettingsPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _provider;
  /// <summary>Враппер</summary>
  private OrganizerReminderSettingsWrapper _wrapper;
  /// <summary>Проперти-грид</summary>
  private ClassWrapperForPropertyGrid _object;

  /// <summary>Конструктор.</summary>
  /// <param name="provider">Провайдер сервисов</param>
  public OrganizerReminderSettingsPage(IServiceProvider provider)
  {
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("Organizer_Reminder_PathToSettings"), (IPropertyPage) this);
    this._wrapper = new OrganizerReminderSettingsWrapper(this._provider);
    this._object = new ClassWrapperForPropertyGrid((object) this._wrapper);
  }

  /// <summary>Событие об изменении свойств на странице.</summary>
  public event EventHandler Changed;

  /// <summary>Тип страницы.</summary>
  public PropertyPageType Type => PropertyPageType.Object;

  /// <summary>Объект для отображения свойств.</summary>
  public object Control => (object) this._object;

  /// <summary>Наименование страницы.</summary>
  public string PageName => LocalizationHolder.rm.GetString("Organizer_Reminder_SettingsViewsName");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Сохранение изменений.</summary>
  public void Apply()
  {
    if (this._wrapper == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._wrapper.Save(sessionKeeper.Session);
    if (this._object == null)
      return;
    this._object.ResetOldValues();
  }

  /// <summary>Отмена изменений.</summary>
  public void Cancel()
  {
    if (this._wrapper == null)
      return;
    this._wrapper.ResetValues();
  }

  /// <summary>id раздела справки для данного элемента управления.</summary>
  public string HelpTopicID => "-1";

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }
}
