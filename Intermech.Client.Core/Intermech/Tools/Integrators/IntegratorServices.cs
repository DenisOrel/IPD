
// Type: Intermech.Tools.Integrators.IntegratorServices
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Содержит утилиты по поиску настроенных интеграторов и проверке их возможностей. Класс является thread-safe.
/// </summary>
public static class IntegratorServices
{
  private static readonly ApplicationServiceRef<IntegratorSettingsCacheManager> integratorSettingsCacheManager = new ApplicationServiceRef<IntegratorSettingsCacheManager>();
  private static readonly ApplicationServiceRef<IFileAttributeEditorService> fileAttributeEditorService = new ApplicationServiceRef<IFileAttributeEditorService>();
  private static readonly Lazy<CrossIntegratorSettingsCache<Dictionary<int, IntegratorObject>>> integratedTypes = new Lazy<CrossIntegratorSettingsCache<Dictionary<int, IntegratorObject>>>((Func<CrossIntegratorSettingsCache<Dictionary<int, IntegratorObject>>>) (() => new CrossIntegratorSettingsCache<Dictionary<int, IntegratorObject>>(IntegratorServices.integratorSettingsCacheManager.Value, new Func<Dictionary<int, IntegratorObject>>(IntegratorServices.CollectIntegratedTypes))));

  /// <summary>
  /// С помощью типа объекта определяет интегратор, который должен использоваться для работы с объектами этого типа.
  /// Если для данного типа объектов интегратор не назначен, то метод вернет null.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>Описание интегратора или null</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
  public static IntegratorObject Find(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException();
    Dictionary<int, IntegratorObject> dictionary;
    lock (IntegratorServices.integratedTypes)
      dictionary = IntegratorServices.integratedTypes.Value.Value;
    IntegratorObject integratorObject;
    dictionary.TryGetValue(objectType, out integratorObject);
    return integratorObject;
  }

  /// <summary>
  /// С помощью типа объекта определяет интегратор, который должен использоваться для работы с объектами этого типа,
  /// а также применимы ли к этому типу объекта общие правила работы с атрибутом "Файл".
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>Описание интегратора или null</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
  [Obsolete("Use the method GetFileHandlingRules instead of this.", true)]
  public static IntegratorWithFileRules FindWithFileRules(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException();
    Dictionary<int, IntegratorObject> dictionary;
    lock (IntegratorServices.integratedTypes)
      dictionary = IntegratorServices.integratedTypes.Value.Value;
    IntegratorObject integrator;
    dictionary.TryGetValue(objectType, out integrator);
    FileAttributeEditMode? attributeEditMode = IntegratorServices.fileAttributeEditorService.Value.GetFileAttributeEditMode(objectType);
    bool commonFileRules = !attributeEditMode.HasValue || attributeEditMode.Value == FileAttributeEditMode.Normal;
    return new IntegratorWithFileRules(integrator, commonFileRules);
  }

  /// <summary>
  /// С помощью типа объекта и XPath-выражения определяет интегратор, который должен использоваться для работы с
  /// объектами этого типа, а также возможности интегратора. Если для данного типа объектов интегратор не назначен,
  /// то метод вернет null.
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <param name="xpath">XPath-выражение</param>
  /// <returns>Описание интегратора или null</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
  public static LookupResult FindAndCheck(int objectType, string xpath)
  {
    if (objectType == -1)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(xpath))
      throw new ArgumentException();
    IntegratorObject integratorObject = IntegratorServices.Find(objectType);
    if (integratorObject == null)
      return (LookupResult) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).Lookup(xpath, integratorObject.Id);
  }

  /// <summary>
  /// Для указанного типа объектов IPS определяет правила работы с атрибутом "Файл".
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа объектов IPS</param>
  /// <returns>Описатель правил работы с атрибутом "Файл"</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объектов IPS</exception>
  public static DBObjectTypeFileHandlingRules GetFileHandlingRules(int objectTypeId)
  {
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов IPS.", nameof (objectTypeId));
    Dictionary<int, IntegratorObject> dictionary;
    lock (IntegratorServices.integratedTypes)
      dictionary = IntegratorServices.integratedTypes.Value.Value;
    IntegratorObject integratorRef;
    dictionary.TryGetValue(objectTypeId, out integratorRef);
    FileAttributeEditMode? attributeEditMode = IntegratorServices.fileAttributeEditorService.Value.GetFileAttributeEditMode(objectTypeId);
    return new DBObjectTypeFileHandlingRules(objectTypeId, integratorRef, attributeEditMode);
  }

  /// <summary>
  /// Проверяет существование объекта интегратора в базе IPS.
  /// </summary>
  /// <param name="integratorId">Идентификатор интегратора</param>
  /// <returns>Признак наличия в базе IPS объекта интегратора</returns>
  public static bool Exists(Guid integratorId)
  {
    return !(integratorId == Guid.Empty) ? ServiceUtils.GetService<IIntegratorServer>((object) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService), true).IsIntegratorExists(integratorId) : throw new ArgumentException();
  }

  /// <summary>
  /// Проверяет наличие затребованного сервиса у указанного интегратора.
  /// </summary>
  /// <typeparam name="T">Тип сервиса интегратора</typeparam>
  /// <param name="integrator">Объект интегратора</param>
  /// <returns>true, если затребованный сервис есть у интегратора, иначе - false</returns>
  /// <exception cref="T:Intermech.FaultException">Клиентская часть интегратора не загружена</exception>
  public static bool HasService<T>(IntegratorObject integrator) where T : class
  {
    IIntegrator integrator1 = ClientContext.Integrators.GetIntegrator(integrator, false);
    return integrator1 != null && ServiceUtils.IsServiceAvailable((object) integrator1, typeof (T));
  }

  /// <summary>
  /// Возвращает затребованный сервис указанного интегратора.
  /// </summary>
  /// <typeparam name="T">Тип сервиса интегратора</typeparam>
  /// <param name="integrator">Объект интегратора</param>
  /// <param name="throwIfNotFound">Признак генерации исключения при отсутствии запрашиваемого сервиса</param>
  /// <returns>Затребованный сервис интегратора или null, если сервис не поддерживается интегратором</returns>
  /// <exception cref="T:Intermech.FaultException">Клиентская часть интегратора не загружена</exception>
  /// <exception cref="T:System.Exception">Затребованный сервис не поддерживается интегратором</exception>
  public static T GetService<T>(IntegratorObject integrator, bool throwIfNotFound) where T : class
  {
    IIntegrator integrator1 = ClientContext.Integrators.GetIntegrator(integrator, throwIfNotFound);
    return integrator1 == null ? default (T) : ServiceUtils.GetService<T>((object) integrator1, throwIfNotFound);
  }

  private static Dictionary<int, IntegratorObject> CollectIntegratedTypes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<LookupResult> lookupResultList = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).Lookup("//LookupData/ObjectType/@guid", false);
      Dictionary<int, IntegratorObject> dictionary = new Dictionary<int, IntegratorObject>(256 /*0x0100*/);
      foreach (LookupResult lookupResult in lookupResultList)
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(lookupResult.XmlData);
        foreach (XmlNode selectNode in xmlDocument.SelectNodes("/FoundNodes/guid"))
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(new Guid(selectNode.InnerXml), false);
          if (objectType != null)
            dictionary[objectType.ObjectType] = lookupResult.Integrator;
        }
      }
      return dictionary;
    }
  }
}
