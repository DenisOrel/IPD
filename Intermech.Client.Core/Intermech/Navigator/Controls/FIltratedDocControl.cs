
// Type: Intermech.Navigator.Controls.FIltratedDocControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Базовый класс для форм, поддержифающих фильтрацию содержимого
/// </summary>
public class FIltratedDocControl : 
  DockControl,
  IFiltrationClass,
  IFiltrationRuleClass,
  IEditingContextNavWindow
{
  /// <summary>
  /// Сервис для управления тулбаром "Фильтрация состава" в главной форме
  /// </summary>
  protected IFiltrationService filtrationService;
  /// <summary>
  /// Уникальный идентификатор формы для хранения её настроек фильтрации состава в сервисе IVersionRulesCacheService
  /// </summary>
  protected string filtrationOwnerID = string.Empty;
  /// <summary>
  /// Были ли установлены настройки фильтрации после восстановления
  /// </summary>
  protected bool filtrationsApplyed = true;
  /// <summary>
  /// Назначив этому свойству значение, можно уведомить реализующий данный интерфейс класс о том,
  /// что при его активации следует использовать именно это правило подбора версий
  /// </summary>
  private VersionsRule _newRule;
  /// <summary>Первая активация окна выполнена</summary>
  protected bool firstActivation;
  /// <summary>Выполнена ли активация окна</summary>
  private bool _activated;

  /// <summary>
  /// Фактически - ссылка на саму же форму, на её интерфейс IFiltrationClass
  /// </summary>
  protected IFiltrationClass filtrationClass => (IFiltrationClass) this;

  public FIltratedDocControl() => this.InitializeFiltrationService();

  public override void Activated()
  {
    base.Activated();
    if (this._activated)
      return;
    try
    {
      this.FiltrationInitToolbar();
      IMainFormUpdate service1 = ServicesManager.GetService(typeof (IMainFormUpdate)) as IMainFormUpdate;
      ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (service1 != null && service2.CachedEditingContextSource == EditingContextSource.WindowContext)
        service1.RefreshEditingContextToolbar();
      this.filtrationService.OnFiltrationChanged += new Intermech.Interfaces.Client.FiltrationChanged(this.FiltrationChanged);
      if (this._newRule != null)
      {
        this.FiltrationService.RuleClass = this._newRule;
        this._newRule = (VersionsRule) null;
      }
      if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service3) || !service3.BlockedToolbars || this.firstActivation || service1 == null)
        return;
      service1.CollectCurrentContextsHistory();
    }
    finally
    {
      this._activated = true;
      this.firstActivation = true;
    }
  }

  public override void Deactivated()
  {
    base.Deactivated();
    if (!this._activated)
      return;
    try
    {
      if (this.filtrationService != null)
        this.filtrationService.OnFiltrationChanged -= new Intermech.Interfaces.Client.FiltrationChanged(this.FiltrationChanged);
      this.FiltrationClearToolbar();
    }
    finally
    {
      this._activated = false;
    }
  }

  /// <summary>Удалить из базы данных свои настройки фильтрации</summary>
  protected virtual void Do_DeleteFiltrationSettings()
  {
    if (this.filtrationOwnerID.Length <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).DeleteRuleTuning((object) sessionKeeper.Session.SessionGUID, this.filtrationOwnerID);
      this.filtrationOwnerID = string.Empty;
    }
  }

  /// <summary>
  /// Получить ссылку на интерфейс сервиса, управляющего тулбаром "Фильтрация состава"
  /// </summary>
  /// <returns>Cсылка на интерфейс сервиса, управляющего тулбаром "Фильтрация состава"</returns>
  protected virtual void InitializeFiltrationService()
  {
    this.filtrationService = (IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService));
    if (this.filtrationService == null)
      return;
    this.filtrationService.FiltrationServiceOwnerID = this.FiltrationOwnerID;
  }

  /// <summary>
  /// Освободить ресурсы в сервисе, управляющем тулбаром "Фильтрация состава"
  /// </summary>
  /// <param name="filtrationService">Cсылка на интерфейс сервиса, управляющего тулбаром "Фильтрация состава"</param>
  protected virtual void DisposeFiltrationService(IFiltrationService filtrationService)
  {
  }

  /// <summary>
  /// Вернуть значение переменной, хранящей ссылку на интерфейс сервиса, управляющего тулбаром "Фильтрация состава"
  /// </summary>
  public IFiltrationService FiltrationService => this.filtrationService;

  /// <summary>
  /// Заполнить тулбар нашими настройками фильтрации состава
  /// </summary>
  private void FiltrationInitToolbar()
  {
    if (this.FiltrationService == null)
      return;
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.FiltrationService.FiltrationServiceOwnerID = this.Get_FiltrationOwnerID();
    if (!this.filtrationsApplyed)
    {
      IFiltrationSettings filtration = this.FiltrationService.Filtration;
      if (filtration != null && filtration.Tags != null && filtration.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] != null && filtration.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"].Equals((object) true))
        filtration.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
      this.FiltrationService.FiltrationApplyUpdates(true);
      this.filtrationsApplyed = true;
    }
    else if (service.CachedEditingContextSource == EditingContextSource.WindowContext)
      this.FiltrationService.FiltrationApplyUpdates(true);
    this.FiltrationService.Enabled = true;
    if (this.FiltrationService.FiltrationToolbarHidden)
      return;
    this.FiltrationService.FiltrationToolbarVisible = true;
  }

  /// <summary>Убрать из тулбара наши настройки фильтрации состава</summary>
  private void FiltrationClearToolbar()
  {
    if (this.FiltrationService == null)
      return;
    this.FiltrationService.FiltrationServiceOwnerID = string.Empty;
  }

  /// <summary>
  /// Вернуть значение _FiltrationOwnerID. Если оно пустое - сначала проинициализировать его.
  /// </summary>
  protected string Get_FiltrationOwnerID()
  {
    if (this.filtrationOwnerID.Length <= 0)
      this.filtrationOwnerID = Convert.ToString((object) Guid.NewGuid());
    return this.filtrationOwnerID;
  }

  /// <summary>Вернуть уникальный ID</summary>
  public string FiltrationOwnerID => this.Get_FiltrationOwnerID();

  /// <summary>
  /// Виртуальный метод, который надо перекрывать. Вызывается сервисом тулбара "Фильтрация состава" тогда,
  /// когда происходит смена настроек фильтрации состава.
  /// </summary>
  /// <param name="NewFiltration">Новые настройки фильтрации состава</param>
  /// <param name="FiltrationValid">Являются ли эти настройки валидными</param>
  public virtual void FiltrationChanged(IFiltrationSettings NewFiltration, bool FiltrationValid)
  {
  }

  /// <summary>
  /// Назначив этому свойству значение, можно уведомить реализующий данный интерфейс класс о том,
  /// что при его активации следует использовать именно это правило подбора версий
  /// </summary>
  public virtual VersionsRule NewRule
  {
    set => this._newRule = value;
  }

  /// <summary>Идентификатор текущего контекста редактирования</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public long EditingContextID
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.EditingContextID;
    }
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.EditingContextID = value;
    }
  }

  /// <summary>Режим работы текущего контекста редактирования</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public EditingContextMode EditingContextMode
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.EditingContextMode;
    }
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.EditingContextMode = value;
    }
  }

  /// <summary>
  /// Список контекстов редактирования, которые будут отображаться в комбо-боксе (история выбранных контекстов)
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public List<long> History
  {
    get
    {
      return !(ServicesManager.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service) ? new List<long>() : service.EditingContextHistory;
    }
    set
    {
      if (!(ServicesManager.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service))
        return;
      service.EditingContextHistory = value;
    }
  }
}
