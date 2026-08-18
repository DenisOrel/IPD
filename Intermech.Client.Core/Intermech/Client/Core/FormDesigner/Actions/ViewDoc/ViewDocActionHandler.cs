
// Type: Intermech.Client.Core.FormDesigner.Actions.ViewDoc.ViewDocActionHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Commands.CommandCache;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using System;


namespace Intermech.Client.Core.FormDesigner.Actions.ViewDoc;

/// <summary>Обработчик действия "Просмотр документа" кнопки</summary>
internal class ViewDocActionHandler : IFormDesignerActionHandler
{
  /// <summary>
  /// Получить ID версии объекта (ObjectId), с возможностью получения актуальной версии именно для текущего пользователя.
  /// </summary>
  /// <param name="actParams">Параметры действия "Просмотр документа"</param>
  /// <param name="strictlyActual">Флаг "Строго для актуальной версии объекта" - если == true, то ID объекта
  /// будет получаться с сервера как ID актуальной версии объекта для текущего пользователя.
  /// Если == false, то ID будет браться из кеша на клиенте без учёта актуальности объекта (архивный). </param>
  /// <returns>ID версии объекта (ObjectId)</returns>
  private static long GetViewDocObjId(ViewDocActionParams actParams, bool strictlyActual)
  {
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(actParams.DocumentGuid);
    if (objectInfo.Empty)
      throw new ApplicationException(LocalizationHolder.GetString("FormDesigner_Fail_Get_Obj_Info"));
    if (!strictlyActual)
      return objectInfo.ObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectActualCopy(objectInfo.ObjectID, true).ObjectID;
  }

  /// <summary>
  /// Получить свой кастомный IServiceProvider с перекрытием (обход) некоторых ограничений "родного".
  /// </summary>
  /// <param name="desForm"></param>
  /// <returns></returns>
  private static IServiceProvider GetCustomViewServices(DesForm desForm)
  {
    AdvancedServiceContainer serviceProvider = new AdvancedServiceContainer(desForm.ServiceProvider ?? (IServiceProvider) ApplicationServices.Container);
    IViewState service = ServiceUtils.GetService<IViewState>((object) serviceProvider, false);
    serviceProvider.AddService(typeof (IViewState), (object) new ViewStateService((service != null ? service.ViewState : ViewStateFlags.None).RemoveFlags<ViewStateFlags>(ViewStateFlags.InDialog)));
    return (IServiceProvider) serviceProvider;
  }

  /// <summary>
  /// Получить таблицу команд для объекта отображаемого документа
  /// </summary>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="desForm">Форма</param>
  /// <param name="objVersionId">ID версии объекта</param>
  /// <returns></returns>
  private static CommandsTable GetCmdTable(IServiceProvider viewServices, long objVersionId)
  {
    ISelectedItems selectedItemsForObject = SelectedItemsHelper.CreateSelectedItemsForObject(objVersionId);
    return ApplicationServices.Container.GetService<ICommandCacheService>().GetCommandsTable(selectedItemsForObject, viewServices, false);
  }

  /// <summary>Выполнение команды просмотра файла документа</summary>
  /// <param name="viewServices">Сервисы просмотра</param>
  /// <param name="cmdTable">Таблица команд построенная для заданного в параметрах действия объекта.</param>
  private static void InvokeViewDocCommand(IServiceProvider viewServices, CommandsTable cmdTable)
  {
    NavigatorTreeView service = ApplicationServices.Container.GetService<NavigatorTreeView>(false);
    bool flag = service == null || service.DisableChangeSelectedNodeDuringNotificationProcessing;
    try
    {
      if (service != null)
        service.DisableChangeSelectedNodeDuringNotificationProcessing = true;
      Intermech.Navigator.ContextMenu.Services.InvokeCommand("ViewDocument", cmdTable, viewServices);
    }
    finally
    {
      if (service != null)
        service.DisableChangeSelectedNodeDuringNotificationProcessing = flag;
    }
  }

  /// <summary>
  /// Проверка корректности входных параметров обработчика нажатия кнопки и преобразование некоторых из них в специфичные
  /// для данного действия ("Просмотр документа").
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns> paramsOk - общий итоговый результат, всё ок, или не ок и работать дальше нельзя.
  /// attrBtn - AttrButton полученный из входного object button.
  /// desForm - DesForm полученный из object form.
  /// actParams - ViewDocActionParams полученные из данных кнопки attrBtn.
  /// </returns>
  private static (bool paramsOk, AttrButton attrBtn, DesForm desForm, ViewDocActionParams actParams) CheckInputHandlerParams(
    object button,
    object form)
  {
    return !(button is AttrButton attrButton) || !(form is DesForm desForm) || !(attrButton.FormDesignerActionParams is ViewDocActionParams designerActionParams) || designerActionParams.DocumentGuid == Guid.Empty ? (false, (AttrButton) null, (DesForm) null, (ViewDocActionParams) null) : (true, attrButton, desForm, designerActionParams);
  }

  /// <summary>
  /// Создаёт и возвращает таблицу команд и свой кастомный контейнер сервисов с использованием которого формировалась возвращаемая таблица команд.
  /// </summary>
  /// <param name="desForm"></param>
  /// <param name="actParams"></param>
  /// <param name="strictlyForActualObjectVersion">Флаг "Строго для актуальной версии объекта", если == true,
  /// то версия объекта (данные), для которого строится таблица команд, будет браться с сервера с учётом актуальности по отношению к текущему пользователю
  /// (что долго и затратно по ресурсам, но точнее отражает текущее состояние документа объекта).
  /// Если == false, то будет использоваться версия объекта из кеша на клиенте (быстро, не затратно по ресурсам, и это будет текущая архивная копия данных объекта)
  /// </param>
  /// <returns>Таблица команд, Кастомный провайдер сервисов с использованием которого формировалась возвращаемая таблица команд.</returns>
  private static (CommandsTable cmdTable, IServiceProvider viewServices) GetCmdTableWithCustomServices(
    DesForm desForm,
    ViewDocActionParams actParams,
    bool strictlyForActualObjectVersion = false)
  {
    IServiceProvider customViewServices = ViewDocActionHandler.GetCustomViewServices(desForm);
    return (ViewDocActionHandler.GetCmdTable(customViewServices, ViewDocActionHandler.GetViewDocObjId(actParams, strictlyForActualObjectVersion)), customViewServices);
  }

  /// <summary>Событие на нажатие кнопки.</summary>
  /// <param name="button">Кнопка (AttrButton)</param>
  /// <param name="form">Форма (DesForm)</param>
  public void ButtonPressed(object button, object form)
  {
    (bool paramsOk, AttrButton _, DesForm desForm, ViewDocActionParams actParams) = ViewDocActionHandler.CheckInputHandlerParams(button, form);
    if (!paramsOk)
      return;
    (CommandsTable cmdTable, IServiceProvider viewServices) = ViewDocActionHandler.GetCmdTableWithCustomServices(desForm, actParams, true);
    if (cmdTable?["ViewDocument"] == null)
      return;
    ViewDocActionHandler.InvokeViewDocCommand(viewServices, cmdTable);
  }

  /// <summary>Проверка состояния кнопки Enabled/Disabled.</summary>
  /// <param name="button">Кнопка (AttrButton)</param>
  /// <param name="form">Форма (DesForm)</param>
  /// <returns>true - если кнопка должна быть Enabled</returns>
  public bool ButtonEnabled(object button, object form)
  {
    (bool paramsOk, AttrButton _, DesForm desForm, ViewDocActionParams actParams) = ViewDocActionHandler.CheckInputHandlerParams(button, form);
    return paramsOk && ViewDocActionHandler.GetCmdTableWithCustomServices(desForm, actParams).cmdTable?["ViewDocument"] != null;
  }
}
