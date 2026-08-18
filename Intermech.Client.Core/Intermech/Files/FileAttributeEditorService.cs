
// Type: Intermech.Files.FileAttributeEditorService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Memoization;
using Intermech.Threading;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Files;

/// <summary>
/// Сервис для определения способа редактирования атрибута "Файл" у объектов IPS.
/// Реализация является thread safe.
/// </summary>
internal sealed class FileAttributeEditorService : IFileAttributeEditorService
{
  private Func<int, bool> hasFileAttributeFast;
  private CrossIntegratorSettingsCache<ICollection<int>> internallyEditableObjectTypesCache;

  public FileAttributeEditorService(
    IMetadataChangeMonitor metadataChangeMonitor,
    IntegratorSettingsCacheManager integratorSettingsCacheManager)
  {
    this.hasFileAttributeFast = TableLookupMemoizer<int, bool>.Wrap(new Func<int, bool>(this.HasFileAttributeSlow), (IStateMonitor) metadataChangeMonitor, (ISyncRoot) EmptySyncRoot.Value);
    this.internallyEditableObjectTypesCache = new CrossIntegratorSettingsCache<ICollection<int>>(integratorSettingsCacheManager, new Func<ICollection<int>>(this.GetObjectTypesWithInternalEditModeSlow));
  }

  /// <summary>
  /// Проверяет, имеется ли у указанного типа объектов IPS атрибут "Файл".
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа объектов IPS</param>
  /// <returns>true, если атрибут "Файл" имеется или может быть, false - если такого атрибута нет</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объектов IPS</exception>
  public bool HasFileAttribute(int objectTypeId)
  {
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов IPS.", nameof (objectTypeId));
    return this.hasFileAttributeFast(objectTypeId);
  }

  private bool HasFileAttributeSlow(int objectTypeId)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBObjectTypeInfo objectType = service.GetObjectType(objectTypeId, false);
    return objectType != null && objectType.Attributes.GetAttributeByID(service.FileAttributeID) != null;
  }

  /// <summary>
  /// Возвращает для указанного типа объектов IPS способ редактирования атрибута "Файл".
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа объектов IPS</param>
  /// <returns>Спосо редактирования атрибута "Файл" у объектов указанного типа</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объектов IPS</exception>
  public FileAttributeEditMode? GetFileAttributeEditMode(int objectTypeId)
  {
    if (objectTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объектов IPS.", nameof (objectTypeId));
    return this.HasFileAttribute(objectTypeId) ? new FileAttributeEditMode?(this.GetObjectTypesWithInternalEditMode().Contains(objectTypeId) ? FileAttributeEditMode.Internal : FileAttributeEditMode.Normal) : new FileAttributeEditMode?();
  }

  /// <summary>
  /// Проверяет, следует ли обрабатывать объекты указанного типа по общим правилам работы с атрибутом "Файл".
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>true, если объекты указанного типа обрабатываются по общим правилам, false - если требуется специальная обработка</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
  public bool RequiresCommonFileRules(int objectType)
  {
    if (objectType == -1)
      throw new ArgumentException();
    return !this.GetObjectTypesWithInternalEditMode().Contains(objectType);
  }

  /// <summary>
  /// Возвращает коллекцию идентификаторов типов объектов IPS, у которых атрибут "Файл" должен редактироваться в оперативной памяти
  /// без извлечения на диск в рабочую область файлового хранилища пользователя.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов объектов IPS</returns>
  public ICollection<int> GetObjectTypesWithInternalEditMode()
  {
    lock (this.internallyEditableObjectTypesCache)
      return this.internallyEditableObjectTypesCache.Value;
  }

  private ICollection<int> GetObjectTypesWithInternalEditModeSlow()
  {
    List<LookupResult> lookupResultList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      lookupResultList = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).Lookup("//LookupData[@skipFileManagement = \"true\"]/ObjectType/@guid", false);
    HashSet<int> items = new HashSet<int>();
    foreach (LookupResult lookupResult in lookupResultList)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(lookupResult.XmlData);
      foreach (XmlNode selectNode in xmlDocument.SelectNodes("/FoundNodes/guid"))
      {
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(selectNode.InnerText)))
          items.Add(num);
      }
    }
    return (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) items);
  }
}
