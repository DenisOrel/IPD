
// Type: Intermech.Navigator.DBObjects.CheckInOutCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Предназначен для облегчения процесса создания провайдеров команд контекстного меню,
/// которым надо проверять выполнение некоторого условия для каждого элемента навигации
/// прежде чем определить, какие команды для них возможны.
/// </summary>
public class CheckInOutCommandsProvider : IStepwiseCommandsProvider
{
  /// <summary>Идентификатор текущего пользователя</summary>
  private static long _currentUserID = -1;
  /// <summary>Можно ли показать команду "Взять на изменение"</summary>
  private bool _allowCheckOut;
  /// <summary>Можно ли показать  команду "Завершить изменение"</summary>
  private bool _allowCheckIn;
  /// <summary>Можно ли показать команду "Отменить изменения"</summary>
  private bool _allowCancel;
  /// <summary>Можно ли показать команду "Сохранить изменения"</summary>
  private bool _allowSave;
  /// <summary>Можно ли показать команду "Отменить чужие изменения"</summary>
  private bool _allowAdminCancel;
  /// <summary>Текущая роль - администраторская</summary>
  private static int isAdminRole = -1;
  /// <summary>Кэш шагов ЖЦ</summary>
  private static IObjectLCStepsCache _cache;
  private bool _ignoreLCStep4CheckOut;

  /// <summary>Можно ли показать команду "Взять на изменение"</summary>
  public bool AllowCheckOut => this._allowCheckOut;

  /// <summary>Можно ли показать  команду "Завершить изменение"</summary>
  public bool AllowCheckIn => this._allowCheckIn;

  /// <summary>Можно ли показать команду "Отменить изменения"</summary>
  public bool AllowCancel => this._allowCancel;

  /// <summary>Можно ли показать команду "Сохранить изменения"</summary>
  public bool AllowSave => this._allowSave;

  /// <summary>Можно ли показать команду "Отменить чужие изменения"</summary>
  public bool AllowAdminCancel => this._allowAdminCancel;

  public bool IgnoreLCStep4CheckOut
  {
    get => this._ignoreLCStep4CheckOut;
    set => this._ignoreLCStep4CheckOut = value;
  }

  /// <summary>Предварительная подготовка</summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  public void Preprocess(ISelectedItems items, IServiceProvider viewServices)
  {
    if (CheckInOutCommandsProvider._cache == null)
      CheckInOutCommandsProvider._cache = CacheManager.Cache("ObjectLCStepsCache") as IObjectLCStepsCache;
    if (CheckInOutCommandsProvider.isAdminRole == -1)
    {
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      CheckInOutCommandsProvider.isAdminRole = service.IsAdmin ? 1 : 0;
      CheckInOutCommandsProvider._currentUserID = service.UserID;
    }
    this._allowCheckOut = true;
    this._allowCheckIn = true;
    this._allowCancel = true;
    this._allowSave = true;
    this._allowAdminCancel = true;
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service1 ? service1.ViewState : ViewStateFlags.None;
    if (items != null && items.Count != 0 && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      return;
    this._allowCheckOut = false;
    this._allowCheckIn = false;
    this._allowCancel = false;
    this._allowSave = false;
    this._allowAdminCancel = false;
  }

  /// <summary>
  /// Выполнить анализ всей полученной коллекции выделенных элементов
  /// (метод используется в ChildrenView в реализации интерфейса ICommandTarget)
  /// </summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  public void CheckSelectedItems(ISelectedItems items, IServiceProvider viewServices)
  {
    this.Preprocess(items, viewServices);
    for (int index = 0; index < items.Count; ++index)
    {
      this.Process(items, index);
      if (!this.CanContinue)
        break;
    }
  }

  /// <summary>
  /// Выполнить анализ полученной коллекции выделенных элементов
  /// </summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="index">Индекс объекта в списке</param>
  public void Process(ISelectedItems items, int index)
  {
    IDBObjectID itemData1 = items.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID;
    IDBCheckedOutByID itemData2 = items.GetItemData(index, typeof (IDBCheckedOutByID)) as IDBCheckedOutByID;
    IDBLCStepID itemData3 = items.GetItemData(index, typeof (IDBLCStepID)) as IDBLCStepID;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBLifecycleStepInfo lcStep = itemData3 != null ? service.GetLCStep(itemData3.LCStepID) : (IDBLifecycleStepInfo) null;
    if (itemData1 != null)
    {
      if (itemData1.Value < 0L)
        this._allowCheckOut = false;
      else
        this._allowCheckIn = false;
    }
    else
    {
      this._allowCheckOut = false;
      this._allowCheckIn = false;
    }
    if (itemData2 != null && lcStep != null)
    {
      this._allowCheckOut = ((itemData2.ObjectID <= 0L || itemData2.CheckedOutBy != 0L ? 0 : (lcStep.ObjectModifyMode == ObjectModifyModes.Checkout ? 1 : (this._ignoreLCStep4CheckOut ? 1 : 0))) & (this._allowCheckOut ? 1 : 0)) != 0;
      this._allowCheckIn = itemData2.CheckedOutBy == CheckInOutCommandsProvider._currentUserID & this._allowCheckIn;
      this._allowSave = (((itemData2.CheckedOutBy != CheckInOutCommandsProvider._currentUserID ? 0 : (lcStep.ObjectModifyMode == ObjectModifyModes.Checkout ? 1 : 0)) | (lcStep.ObjectModifyMode == ObjectModifyModes.InBase ? 1 : 0)) & (this._allowSave ? 1 : 0)) != 0;
      this._allowCancel = this._allowCheckIn & this._allowCancel;
      this._allowAdminCancel = ((CheckInOutCommandsProvider.isAdminRole != 1 || itemData2.CheckedOutBy <= 0L || itemData2.CheckedOutBy == CheckInOutCommandsProvider._currentUserID ? 0 : (lcStep.ObjectModifyMode == ObjectModifyModes.Checkout ? 1 : 0)) & (this._allowAdminCancel ? 1 : 0)) != 0;
    }
    else
    {
      this._allowCheckOut = false;
      this._allowCheckIn = false;
      this._allowCancel = false;
      this._allowSave = false;
      this._allowAdminCancel = false;
    }
  }

  /// <summary>Внести изменения в команды контекстного меню</summary>
  /// <param name="commandsInfo">Сведения о командах контекстного меню</param>
  public virtual void Postprocess(CommandsInfo commandsInfo)
  {
    if (this.AllowCheckOut)
      commandsInfo.Add("CheckOut", new CommandInfo(4, new ClickEventHandler(ObjectCommands.CheckoutCommand)));
    if (this.AllowCheckIn)
      commandsInfo.Add("CheckIn", new CommandInfo(4, new ClickEventHandler(ObjectCommands.CheckinCommand)));
    if (this.AllowSave)
      commandsInfo.Add("SaveChanges", new CommandInfo(4, new ClickEventHandler(ObjectCommands.SaveChangesCommand)));
    if (this.AllowCancel)
      commandsInfo.Add("CancelChanges", new CommandInfo(4, new ClickEventHandler(ObjectCommands.CancelCommand)));
    if (!this.AllowAdminCancel)
      return;
    commandsInfo.Add("AdminCancelChanges", new CommandInfo(4, new ClickEventHandler(ObjectCommands.AdminCancelCommand)));
  }

  /// <summary>Можно ли продолжить анализ выделенных элементов</summary>
  public bool CanContinue
  {
    get
    {
      return this._allowCheckOut || this._allowCheckIn || this._allowCancel || this._allowSave || this._allowAdminCancel;
    }
  }
}
