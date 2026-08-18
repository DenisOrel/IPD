// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIDocumentApiService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public class CIDocumentApiService : IDocumentCADApiService
{
  private static readonly TraceSwitch saveOrderTrace = new TraceSwitch("Tools.DataExchange.SaveOrder", "", "0");
  private static readonly Guid techAreaGuid = new Guid("cad0085e-306c-11d8-b4e9-00304f19f545");
  private readonly CICaptureChangesDriver driver;
  private readonly CaptureChangesDriverContext driverContext;
  private readonly IFileVault fileVault;
  private readonly ICADInterfaceService cadService;
  private readonly IApplicationFileTypes fileTypeService;
  private readonly IStandardPartLibraryService stdLibraryService;
  private readonly ICADSettingsService settingsService;
  private readonly IModelDrawingsService modelDrawingsService;
  private readonly CICommonApiOperations commonOperations;

  public CIDocumentApiService(
    CICaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    this.driver = driver;
    this.driverContext = driverContext;
    this.fileVault = ClientContext.FileVault;
    this.cadService = ServiceUtils.GetService<ICADInterfaceService>((object) driver.Integrator, true);
    this.fileTypeService = ServiceUtils.GetService<IApplicationFileTypes>((object) driver.Integrator, true);
    this.stdLibraryService = ServiceUtils.GetService<IStandardPartLibraryService>((object) driver.Integrator, true);
    this.settingsService = ServiceUtils.GetService<ICADSettingsService>((object) driver.Integrator, true);
    this.modelDrawingsService = ServiceUtils.GetService<IModelDrawingsService>((object) driver.Integrator, true);
    this.commonOperations = new CICommonApiOperations(driver, this.fileVault);
  }

  protected CICaptureChangesDriver CIDriver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  protected CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  protected CICommonApiOperations CommonOperations
  {
    [DebuggerStepThrough] get => this.commonOperations;
  }

  /// <summary>
  /// Возвращает объект анализатора файловый зависимостей документа. Метод может вернуть null, если у документа не может быть файловых зависимостей.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Анализатор файловых зависимостей или null</returns>
  public IFileDependenciesHandler TryGetFileDependenciesHandler(SectionEntity docItem)
  {
    CADSettings cadSettings = this.settingsService.GetCADSettings();
    return (IFileDependenciesHandler) new CIDependenciesBuilder(this.CIDriver, this.DriverContext)
    {
      CollectAssociativeDependencies = cadSettings.EnableCADLinkTypeAttribute
    };
  }

  /// <summary>
  /// Возвращает имя виртуального атрибута документа, в котором сохраняется имя типа документа. У новых документов, импортируемых в IPS, этот атрибут может быть заполнен пользователем вручную.
  /// Метод может вернуть null или пустую строку, если подходящего атрибута в файле документа нет.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Имя виртуального атрибута в файле документа для хранения имени типа документа</returns>
  public string GetDocumentTypeAttributeName(SectionEntity docItem)
  {
    return CADDocumentResources.EMB_DocumentTypeAttribute;
  }

  /// <summary>
  /// <para>
  /// Позволяет определить тип для нового импортируемого документа, прочитав его из файла документа. Если тип документа не может быть
  /// определен однозначно, то метод должен вернуть все возможные типы документов.</para>
  /// <para>
  /// Этот метод вызывается даже тогда, когда метод <see cref="M:Intermech.Tools.Integrators.CADInterface.CIDocumentApiService.GetDocumentTypeAttributeName(Intermech.Data.SectionEntities.SectionEntity)" /> возвращает null или пустую строку.
  /// Так сделано потому, что иногда тип документа можно определить эвристически без явного хранения имени типа в файле документа.
  /// При реализации метода также нужно учитывать, что он вызывается в самом начале анализа импортируемого документа, и его рабочий элемент практически пуст.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список возможных типов для импортируемого документа</returns>
  public List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    if (!this.fileTypeService.IsApplicationFile(FilesSection.GetMasterFile(docItem)))
      throw new NotSupportedException();
    if (this.stdLibraryService.Mode != StandardLibraryMode.NotSupported && PathUtils.IsPlacedIn(FilesSection.GetMasterFile(docItem), Path.Combine(this.fileVault.WorkArea.AreaPath, this.stdLibraryService.FolderName)))
      return CollectionUtils.CreateList<LocalId<int>>((LocalId<int>) this.CIDriver.IntegratorSettings.StandardPartType);
    Tuple<List<MechanicalDocumentKind>, List<LocalId<int>>> andTypesByFilename = this.GetPossibleDocumentKindsAndTypesByFilename(docItem);
    List<MechanicalDocumentKind> possibleKinds = andTypesByFilename.Item1;
    List<LocalId<int>> possibleTypes = andTypesByFilename.Item2;
    LocalId<int> localId = this.TryReadDocumentType(docItem, (ICollection<LocalId<int>>) possibleTypes);
    if (localId != null)
      return CollectionUtils.CreateList<LocalId<int>>(localId);
    List<LocalId<int>> localIdList = this.TryReadDesignDocumentType(docItem, (ICollection<LocalId<int>>) possibleTypes);
    if (localIdList != null)
      return localIdList;
    if (possibleKinds.Count > 1 && this.FilterPossibleDocumentKinds(docItem, possibleKinds))
      possibleTypes.RemoveAll((Predicate<LocalId<int>>) (docType => !possibleKinds.Contains(this.CIDriver.GetMechanicalDocumentKindByType(docType.Id))));
    if (this.IsTechnologicalRole() && possibleTypes.Exists((Predicate<LocalId<int>>) (x => !PDMHelper.IsDocumentWithArticles(x.Id))))
      possibleTypes.RemoveAll((Predicate<LocalId<int>>) (x => PDMHelper.IsDocumentWithArticles(x.Id)));
    return possibleTypes;
  }

  private bool IsTechnologicalRole()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      char areaId = sessionKeeper.Session.GetSubjectAreaType(CIDocumentApiService.techAreaGuid, true).AreaID;
      if (sessionKeeper.Session.AreaID.IndexOf(areaId) >= 0)
        return true;
    }
    return false;
  }

  private Tuple<List<MechanicalDocumentKind>, List<LocalId<int>>> GetPossibleDocumentKindsAndTypesByFilename(
    SectionEntity docItem)
  {
    List<MechanicalDocumentKind> possibleKinds = new List<MechanicalDocumentKind>((IEnumerable<MechanicalDocumentKind>) Enum.GetValues(typeof (MechanicalDocumentKind)));
    possibleKinds.Remove(MechanicalDocumentKind.StandardModel);
    if (this.CIDriver.IntegratorSettings.NewDrawingMode != NewDrawingMode.Document)
    {
      possibleKinds.Remove(MechanicalDocumentKind.AssemblyDrawing);
      possibleKinds.Remove(MechanicalDocumentKind.PartDrawing);
    }
    List<LocalId<int>> localIdList = this.CIDriver.FilterDocumentTypesByExtension(docItem, this.CIDriver.IntegratorSettings.GetCommonFileDocumentTypes(), false);
    localIdList.RemoveAll((Predicate<LocalId<int>>) (docType => !possibleKinds.Contains(this.CIDriver.GetMechanicalDocumentKindByType(docType.Id))));
    possibleKinds.Clear();
    foreach (LocalId<int> localId in localIdList)
    {
      MechanicalDocumentKind documentKindByType = this.CIDriver.GetMechanicalDocumentKindByType(localId.Id);
      if (!possibleKinds.Contains(documentKindByType))
        possibleKinds.Add(documentKindByType);
    }
    return Tuple.Create<List<MechanicalDocumentKind>, List<LocalId<int>>>(possibleKinds, localIdList);
  }

  public LocalId<int> TryReadDocumentType(
    SectionEntity docItem,
    ICollection<LocalId<int>> possibleTypes)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (possibleTypes == null)
      throw new ArgumentNullException(nameof (possibleTypes));
    if (possibleTypes.Count == 0)
      return (LocalId<int>) null;
    string docType = this.TryReadTypeAttribute(docItem, CADDocumentResources.EMB_DocumentTypeAttribute);
    return string.IsNullOrEmpty(docType) ? (LocalId<int>) null : CollectionUtils.Find<LocalId<int>>((IEnumerable<LocalId<int>>) possibleTypes, (Predicate<LocalId<int>>) (item => string.Compare(item.ToString(), docType, true) == 0)) ?? (LocalId<int>) null;
  }

  public List<LocalId<int>> TryReadDesignDocumentType(
    SectionEntity docItem,
    ICollection<LocalId<int>> possibleTypes)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (possibleTypes == null)
      throw new ArgumentNullException(nameof (possibleTypes));
    if (possibleTypes.Count == 0)
      return (List<LocalId<int>>) null;
    string designType = this.TryReadTypeAttribute(docItem, CADDocumentResources.EMB_DesignTypeAttribute);
    if (string.IsNullOrEmpty(designType))
      return (List<LocalId<int>>) null;
    List<LocalId<int>> localIdList = this.CIDriver.FilterDocumentTypesByDesignType(docItem, possibleTypes, designType);
    return localIdList.Count != 0 ? localIdList : (List<LocalId<int>>) null;
  }

  private string TryReadTypeAttribute(SectionEntity docItem, string typeAttribute)
  {
    return CADDocumentHelper.ReadAttributes((IServiceProvider) this.CIDriver.Integrator, docItem.Sections.Get<CIDocumentData>().Document, (ICollection<StringKey>) new StringKey[1]
    {
      (StringKey) typeAttribute
    }, (DecodeAttributesOptions) null).Bag.Read<string>((StringKey) typeAttribute, (string) null);
  }

  private bool FilterPossibleDocumentKinds(
    SectionEntity docItem,
    List<MechanicalDocumentKind> possibleKinds)
  {
    bool flag = false;
    if (possibleKinds.Count > 1 && CIDocumentApiService.CanBeModelDrawing(possibleKinds))
    {
      if (this.IsPossibleModelDrawing(docItem))
      {
        possibleKinds.RemoveAll(new Predicate<MechanicalDocumentKind>(CIDocumentApiService.IsNotModelDrawingKind));
        return true;
      }
      possibleKinds.RemoveAll(new Predicate<MechanicalDocumentKind>(CIDocumentApiService.IsModelDrawingKind));
      flag = true;
    }
    if (possibleKinds.Count > 1 && possibleKinds.Contains(MechanicalDocumentKind.PartModel))
    {
      if (docItem.Sections.Get<CIDocumentData>().Document.DocumentType == CADDocumentType.Part)
      {
        possibleKinds.RemoveAll((Predicate<MechanicalDocumentKind>) (docKind => docKind != MechanicalDocumentKind.PartModel));
        return true;
      }
      possibleKinds.RemoveAll((Predicate<MechanicalDocumentKind>) (docKind => docKind == MechanicalDocumentKind.PartModel));
      flag = true;
    }
    return flag;
  }

  private static bool CanBeModelDrawing(List<MechanicalDocumentKind> possibleKinds)
  {
    return possibleKinds.Exists(new Predicate<MechanicalDocumentKind>(CIDocumentApiService.IsModelDrawingKind)) && possibleKinds.Exists(new Predicate<MechanicalDocumentKind>(CIDocumentApiService.IsNotModelDrawingKind));
  }

  private bool IsPossibleModelDrawing(SectionEntity docItem)
  {
    return this.CIDriver.IntegratorSettings.DrawingSuffixes.Count != 0 ? this.modelDrawingsService.IsDrawingFileName(FilesSection.GetMasterFile(docItem)) : docItem.Sections.Get<CIDocumentData>().Document.DocumentType == CADDocumentType.Drawing;
  }

  private static bool IsModelDrawingKind(MechanicalDocumentKind documentKind)
  {
    return documentKind == MechanicalDocumentKind.AssemblyDrawing || documentKind == MechanicalDocumentKind.PartDrawing;
  }

  private static bool IsNotModelDrawingKind(MechanicalDocumentKind documentKind)
  {
    return documentKind != MechanicalDocumentKind.AssemblyDrawing && documentKind != MechanicalDocumentKind.PartDrawing;
  }

  private ICollection<StringKey> GetDocumentFileAttributes(SectionEntity docItem)
  {
    return this.settingsService.SynchronizedDocumentAttributes.GetAttributes(ObjectSection.TryGetObjectType(docItem), false);
  }

  protected virtual IAttributeCodec GetDocumentCodec(SectionEntity docItem)
  {
    return this.cadService.OpenDocuments.GetCodec((IOpenDocument) CADInterfaceAdapters.AsOpenDocument(docItem.Sections.Get<CIDocumentData>().Document));
  }

  protected virtual IValueBagContainer GetDocumentAttributeContainer(SectionEntity docItem)
  {
    return (IValueBagContainer) CADInterfaceAdapters.AsValueBagContainer(docItem.Sections.Get<CIDocumentData>().Document);
  }

  /// <summary>
  /// Читает и возвращает значения свойств документа, хранящиеся в его файле. Если отсутствует API или возможность записи свойство обратно в документ,
  /// то возвращаемые значения параметров должны быть read-only, а свойство ContainerValues.IsOpenMetadata должно быть установлено в false.
  /// </summary>
  /// <remarks>
  /// Свойства документа - это именованные значения, хранящиеся в файле документа и доступные для изменения средствами редактора документа. Они
  /// используются для хранения атрибутов объекта IPS.
  /// </remarks>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Контейнер со свойствами документа, прочитанными из файла</returns>
  public ContainerValues ReadDocumentProperties(SectionEntity docItem)
  {
    return this.GetDocumentCodec(docItem).ReadFileProperties(this.GetDocumentAttributeContainer(docItem), this.GetDocumentFileAttributes(docItem));
  }

  /// <summary>
  /// Записывает измененные значения свойств документа в файл. Метод вызывается только при наличии изменений в свойствах. Метод должен записать
  /// только те значения, которые может, остальные он должен игнорировать.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <param name="fileProperties">Контейнер со свойствами документа</param>
  /// <returns>true, если запись в документ имела место</returns>
  public bool WriteDocumentProperties(SectionEntity docItem, ContainerValues fileProperties)
  {
    return this.GetDocumentCodec(docItem).Formatter.Write(this.GetDocumentAttributeContainer(docItem), fileProperties);
  }

  /// <summary>
  /// Выполняет сохранение файла измененного документа на диск. Этот метод вызывается в двух случаях: если интегратор изменял документ в процессе его
  /// анализа, а также если документ взят на редактирование. Реализация этого метода должна проверить, открыт ли документ в приложении, а также имеет
  /// ли он несохраненные изменения. Только в этом случае метод должен обновить файл документа на диске.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  public void SaveDocumentFile(SectionEntity docItem)
  {
    CIDocumentData ciDocumentData = docItem.Sections.Get<CIDocumentData>();
    bool needSave = ciDocumentData.Document.Modified && !ciDocumentData.Document.ReadOnly;
    if (needSave)
      ciDocumentData.Document.Save();
    if (CIDocumentApiService.saveOrderTrace.TraceInfo)
      CIDocumentApiService.TraceSaveOrder(docItem, needSave);
    FilesSection filesSection = docItem.Sections.Get<FilesSection>();
    if (filesSection.Satellites.Count <= 0)
      return;
    string masterExt = Path.GetExtension(filesSection.MasterFile);
    foreach (string allAs in CollectionUtils.FindAllAsList<string>((ICollection<string>) filesSection.Satellites, (Predicate<string>) (satelliteFile => PathUtils.IsSamePath(Path.GetExtension(satelliteFile), masterExt))))
    {
      CADDocumentProxy openDocument = this.CIDriver.CADSystem.FindOpenDocument(allAs);
      if (openDocument != null && PathUtils.IsSamePath(openDocument.FullName, allAs) && openDocument.Modified && !openDocument.ReadOnly)
        openDocument.Save();
    }
  }

  private static void TraceSaveOrder(SectionEntity docItem, bool needSave)
  {
    Trace.WriteLine(string.Format(needSave ? "SAVE ORDER: '{0}' is saved because it's marked as modified" : "SAVE ORDER: '{0}' is skipped because it's marked as not modified", (object) FilesSection.GetMasterFile(docItem)));
  }

  /// <summary>
  /// Выполняет преобразование прочитанных ранее свойств файла в значения атрибутов документа. Декодированные значения атрибутов должны быть
  /// доступны для модификации, независимо от значений свойства ReadOnly у исходных параметров.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с атрибутами документа</returns>
  public ValueBag DecodeDocumentAttributes(SectionEntity docItem, ContainerValues fileProperties)
  {
    DecodeAttributesOptions decodeOptions = this.CIDriver.Operations.Documents.GetDecodeOptions(docItem);
    DecodeAttributesParams decodeParams = new DecodeAttributesParams(this.GetDocumentAttributeContainer(docItem), this.GetDocumentFileAttributes(docItem), fileProperties, decodeOptions);
    return this.GetDocumentCodec(docItem).Decode(decodeParams);
  }

  /// <summary>
  /// Выполняет преобразование значений атрибутов документа в свойства файла. Если отсутствует API или возможность записи свойств в файл,
  /// то этот метод не должен что-либо делать.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <param name="attributeKeys">Список имен преобразуемых атрибутов</param>
  /// <param name="attributes">Контейнер с атрибутами документа</param>
  /// <param name="fileProperties">Контейнер с параметрами документа</param>
  public void EncodeDocumentAttributes(
    SectionEntity docItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
    EncodeAttributesOptions encodeOptions = this.CIDriver.Operations.Documents.GetEncodeOptions(docItem);
    this.GetDocumentCodec(docItem).Encode(new EncodeAttributesParams(this.GetDocumentAttributeContainer(docItem), attributeKeys, attributes, fileProperties, encodeOptions)
    {
      ContainerDisplayName = DisplaySection.GetQualifiedName(docItem)
    });
  }

  /// <summary>
  /// Позволяет обработать значения атрибутов документа непосредственно перед синхронизацией значений между файлом документа и объектом документа в базе IPS.
  /// </summary>
  /// <param name="documentItem">Рабочий элемент документа</param>
  /// <param name="workingSet">Рабочий набор атрибутов документа, используемый для заполнения, корректировки и преобразования значений</param>
  /// <param name="databaseSet">Набор атрибутов документа, прочитанный из базы данных</param>
  /// <exception cref="T:ArgumentNullException">documentItem || workingSet || databaseSet</exception>
  public void ProcessDocumentAttributes(
    SectionEntity documentItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (workingSet == null)
      throw new ArgumentNullException(nameof (workingSet));
    if (databaseSet == null)
      throw new ArgumentNullException(nameof (databaseSet));
  }

  /// <summary>
  /// Возвращает список имен атрибутов, значения которых необходимо перенести из файла документа в объект IPS. В данный список можно не включать ряд атрибутов, копируемых
  /// всегда - обозначение, наименование, тип документа, код документа. Если список атрибутов содержит атрибуты, которые не могут существовать у документа
  /// данного типа, то такие атрибуты будут проигнорированы.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список имен атрибутов</returns>
  public virtual ICollection<StringKey> GetDocumentSyncAttributes(SectionEntity docItem)
  {
    return this.GetDocumentFileAttributes(docItem);
  }

  /// <summary>
  /// Читает и возвращает значения атрибутов связи между документами. Метод может возвращать null, если у приложения нет таких атрибутов.
  /// </summary>
  /// <param name="projectDocument">Родительский документ</param>
  /// <param name="partDocument">Дочерний документ</param>
  /// <returns>Контейнер с значениями атрибутов или null</returns>
  public ValueBag TryReadDocumentRelationAttributes(
    SectionEntity projectDocument,
    SectionEntity partDocument)
  {
    if (!this.settingsService.GetCADSettings().EnableCADLinkTypeAttribute)
      return (ValueBag) null;
    CIAssociativeDependencies associativeDependencies = projectDocument.Sections.Get<CIAssociativeDependencies>((CIAssociativeDependencies) null);
    if (associativeDependencies == null)
      return (ValueBag) null;
    CADLinkTypes cadLinkTypes = associativeDependencies.Files.Contains(FilesSection.GetMasterFile(partDocument)) ? CADLinkTypes.Associative : CADLinkTypes.Structural;
    ValueBag valueBag = new ValueBag();
    valueBag.AddWithFlag((StringKey) IDCache.Default.CADLinkType.Text, (object) cadLinkTypes, NamedFlags.ReadOnly);
    valueBag.AcceptChanges();
    return valueBag;
  }

  /// <summary>
  /// <para>
  /// Возвращает дополнительные файлы документа, отличающиеся от мастер-файла документа не только расширением файла. Для определения таких файлов следует использовать
  /// API приложения, с которым осуществляется интеграция. Если у документов приложения нет таких дополнительных файлов, то этот метод должен вернуть
  /// пустой список.</para>
  /// <para>Те дополнительные файлы, которые указаны в настройках типа документа в базе IPS, будут добавлены к документу автоматически.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список абсолютных путей к дополнительным файлам документа</returns>
  public List<string> GetSatelliteFiles(SectionEntity docItem)
  {
    CIDocumentData ciDocumentData = docItem.Sections.Get<CIDocumentData>();
    List<string> satelliteFiles = new List<string>(ciDocumentData.AllConfigurations.Count);
    foreach (ModelConfigurationProxy allConfiguration in (IEnumerable<ModelConfigurationProxy>) ciDocumentData.AllConfigurations)
    {
      if (!string.IsNullOrEmpty(allConfiguration.FullPath))
        satelliteFiles.Add(allConfiguration.FullPath);
    }
    return satelliteFiles;
  }

  /// <summary>
  /// Возвращает персональные дополнительные файлы документа. Это такие файлы, которые не должны копироваться, при использовании этого документа в качестве прототипа.
  /// Как правило, это файлы конфигураций модели детали или сборочной единицы. Если у документа таких файлов нет, то этот метод должен вернуть пустой список.
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список абсолютных путей к дополнительным файлам документа</returns>
  public List<string> GetPrivateFiles(SectionEntity docItem)
  {
    CIDocumentData ciDocumentData = docItem.Sections.Get<CIDocumentData>();
    if (!ciDocumentData.Document.HasConfigurations)
      return (List<string>) null;
    ICollection<ModelConfigurationProxy> allConfigurations = ciDocumentData.AllConfigurations;
    List<string> privateFiles = new List<string>(allConfigurations.Count);
    foreach (ModelConfigurationProxy configurationProxy in (IEnumerable<ModelConfigurationProxy>) allConfigurations)
    {
      string fullPath = configurationProxy.FullPath;
      if (!string.IsNullOrEmpty(fullPath))
        privateFiles.Add(fullPath);
    }
    return privateFiles;
  }

  /// <summary>
  /// Возвращает информацию об изделиях, которые выпускаются по документу. Метод возвращает не готовые сущности для изделий, а объекты-заготовки,
  /// которые позже будут использованы стандартным обработчиком изделий для создания сущностей изделий.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Контейнер с заготовками сущностей изделий</returns>
  /// <exception cref="T:ArgumentNullException">documentItem</exception>
  public virtual ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    ICollection<InitialArticleData> result = (ICollection<InitialArticleData>) new LinkedList<InitialArticleData>();
    if (PDMHelper.IsDocumentWithArticles(ObjectSection.GetObjectType(documentItem)))
    {
      this.EmitDocumentArticles(documentItem, result);
      this.EmitSatelliteArticles(documentItem, result);
      this.EmitStandaloneArticles(documentItem, result);
    }
    return result;
  }

  private void EmitDocumentArticles(
    SectionEntity documentItem,
    ICollection<InitialArticleData> result)
  {
    CIDocumentData ciDocumentData = documentItem.Sections.Get<CIDocumentData>();
    this.EmitConfigurationArticles(ciDocumentData.AllConfigurations, ciDocumentData.Document.FullName, ArticleInitialDocumentType.Normal, result);
  }

  private void EmitConfigurationArticles(
    ICollection<ModelConfigurationProxy> configurations,
    string documentMasterFilePath,
    ArticleInitialDocumentType initialDocumentType,
    ICollection<InitialArticleData> result)
  {
    foreach (ModelConfigurationProxy configuration in (IEnumerable<ModelConfigurationProxy>) configurations)
    {
      if (!CADDocumentHelper.IsArticleCreationDenied((IServiceProvider) this.CIDriver.Integrator, configuration))
        result.Add(this.EmitConfigurationArticle(configuration, documentMasterFilePath, initialDocumentType));
    }
  }

  protected InitialArticleData EmitConfigurationArticle(
    ModelConfigurationProxy configuration,
    string documentMasterFilePath,
    ArticleInitialDocumentType initialDocumentType)
  {
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    if (string.IsNullOrEmpty(documentMasterFilePath))
      throw new ArgumentException("Не задан путь к мастер-файлу документа.", nameof (documentMasterFilePath));
    if (!Path.IsPathRooted(documentMasterFilePath))
      throw new ArgumentException("Путь к мастер-файлу документа должен быть задан в абсолютной форме.", nameof (documentMasterFilePath));
    InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.Autodetect);
    initialArticleData.DisplayName = this.CommonOperations.MakeArticleDisplayName((string) configuration.Name, configuration.FullPath, documentMasterFilePath);
    initialArticleData.ArticleKey = this.CommonOperations.MakeArticleKey((string) configuration.Name, documentMasterFilePath);
    initialArticleData.InitialDocumentType = initialDocumentType;
    initialArticleData.CustomSections.Set((object) new CIArticleData()
    {
      Configuration = configuration
    });
    return initialArticleData;
  }

  private void EmitSatelliteArticles(
    SectionEntity modelItem,
    ICollection<InitialArticleData> result)
  {
    foreach (string satellite in (Collection<string>) modelItem.Sections.Get<FilesSection>().Satellites)
    {
      if (this.fileTypeService.IsApplicationFile(satellite) && this.DriverContext.Database.QueryFirst((IQueryCondition) new BinaryCondition((object) CISatelliteModelWithArticles.PathRef, BinaryOperator.Equal, (object) satellite)) != null)
      {
        CADDocumentProxy cadDocumentProxy = this.CIDriver.CADSystem.OpenDocument(satellite, false);
        this.EmitConfigurationArticles(cadDocumentProxy.GetAllConfigurations(), cadDocumentProxy.FullName, ArticleInitialDocumentType.Hidden, result);
      }
    }
  }

  private void EmitStandaloneArticles(
    SectionEntity modelItem,
    ICollection<InitialArticleData> result)
  {
    MechanicalDocumentKind? mechanicalDocumentKind = this.CIDriver.TryGetMechanicalDocumentKind(modelItem);
    if (!mechanicalDocumentKind.HasValue || mechanicalDocumentKind.Value != MechanicalDocumentKind.AssemblyModel)
      return;
    foreach (AssemblyStructureRecord assemblyStructureRecord in this.CommonOperations.GetArticleStructureCached(modelItem))
    {
      if (CADDocumentHelper.ReadPDMFlag((IServiceProvider) this.CIDriver.Integrator, assemblyStructureRecord.ComponentConfiguration) == 1 && this.IsUniqueArticleConfiguration(assemblyStructureRecord.ComponentConfiguration, assemblyStructureRecord.ComponentMasterFile, result))
        result.Add(this.EmitConfigurationArticle(assemblyStructureRecord.ComponentConfiguration, assemblyStructureRecord.ComponentMasterFile, ArticleInitialDocumentType.None));
    }
  }

  private bool IsUniqueArticleConfiguration(
    ModelConfigurationProxy configuration,
    string documentMasterFilePath,
    ICollection<InitialArticleData> articleBlanks)
  {
    string newArticleKey = this.CommonOperations.MakeArticleKey((string) configuration.Name, documentMasterFilePath);
    return ArticleSection.FindArticleByKey(this.DriverContext.Database, newArticleKey) == null && !CollectionUtils.Exists<InitialArticleData>((IEnumerable<InitialArticleData>) articleBlanks, (Predicate<InitialArticleData>) (articleBlank => PathUtils.IsSamePath(articleBlank.ArticleKey, newArticleKey)));
  }
}
