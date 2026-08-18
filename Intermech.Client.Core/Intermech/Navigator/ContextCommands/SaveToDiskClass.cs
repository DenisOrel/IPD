
// Type: Intermech.Navigator.ContextCommands.SaveToDiskClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextCommands;

/// <summary>Класс для команды "Сохранить на диск"</summary>
public class SaveToDiskClass : CustomBackgroundTask, ISaveToDiskClass
{
  /// <summary>список id версий объектов, которые уже обработаны</summary>
  private List<long> processedObjects = new List<long>();
  /// <summary>путь к папке</summary>
  private string selectedPath;
  /// <summary>список связей, по которым проводить выгрузку файлов</summary>
  private List<int> relations = new List<int>();
  /// <summary>в каком формате сохраняем документы Интермех</summary>
  private ImDocumentFormat format;
  /// <summary>список описаний выделенных объектов</summary>
  private List<IDBTypedObjectID> dbTypedObjectIDs;
  /// <summary>куда считывается инфо из ридера</summary>
  private MemoryStream ms;
  /// <summary>true, если заменить все</summary>
  private bool replaceAll;
  /// <summary>true, если пропустить все</summary>
  private bool discardAll;
  /// <summary>читает файлы</summary>
  private BlobProcReader reader;
  /// <summary>окно с вопросом о перезаписи</summary>
  private OverwritePromptForm overwrite = new OverwritePromptForm();
  /// <summary>расширения документов Интермех (старые)</summary>
  private List<string> imDocExt = new List<string>((IEnumerable<string>) new string[7]
  {
    ".imd",
    ".bln",
    ".rev",
    ".cc",
    ".rep",
    ".cmp",
    ".lib"
  });
  /// <summary>расширения документов Интермех (новые)</summary>
  private List<string> ipsImDocExt = new List<string>((IEnumerable<string>) new string[6]
  {
    ".imdx",
    ".spx",
    ".pex",
    ".revx",
    ".idcx",
    ".idc"
  });
  /// <summary>сохранямый документ - документ Интермех</summary>
  private bool intermechDocument;
  /// <summary>
  /// wmf-файлы генерируются по шаблону baseFilename + "#" + i.ToString() + ".wmf"
  /// В списке храним базовые имена для wmf-файлов.
  /// При повторной попытке сохранить комнплект с такими же базовм именем,
  /// попросим пользователя переименовать/заменить/отменить
  /// </summary>
  private Dictionary<string, long> baseWmfFileNames = new Dictionary<string, long>();
  private RenameMode fem;
  /// <summary>Настройки фильтрации</summary>
  private FiltrationSettings filtrSettings;
  /// <summary>заблокировать ли конфигуратор</summary>
  private bool blockConfig = true;
  /// <summary>Сохранять точные спецификации?</summary>
  private bool isExact;
  /// <summary>Суффикс для точных спецификаций</summary>
  private string suffix;
  /// <summary>id юзера</summary>
  private long userID;
  /// <summary>
  /// id версии корневого объекта,
  ///  для построения запроса в соответствии за указанными опциями
  /// </summary>
  private long topObjectID;
  /// <summary>
  /// id тип корневого объекта (заказ или комплектация),
  ///  для построения запроса в соответствии за указанными опциями
  /// </summary>
  private int topObjectTypeID = -1;
  /// <summary>
  /// набор id-ков конфигурируемых объектов,
  /// для которых будут сохранены точные спецификации
  /// </summary>
  private List<long> configObjectIDs = new List<long>();
  /// <summary>Тип атрибута для переименования файла</summary>
  private string selectedAttributeType;
  /// <summary>ID атрибута для переименования файла</summary>
  private int selectedAttributeID;
  /// <summary>Создавать ли иерархию папок</summary>
  private bool createHierarchy;
  /// <summary>Поддержка длинных путей</summary>
  private bool longPathSupport;
  /// <summary>сохранять совместимые подписи</summary>
  private bool saveCompatibleSigns;
  /// <summary>
  /// добавлять идентификатор версии в имена аутентичных файлов
  /// </summary>
  private bool addObjectVersionToAuthFilenamesWhenSave;
  /// <summary>Включен ли фильтр по типу объекта</summary>
  private bool objectTypesFiltr;
  /// <summary>Список типов объектов, у которых выгружаем файлы</summary>
  private List<int> objectTypes = new List<int>();
  /// <summary>недопустимые символы в имени файла</summary>
  private char[] InvalidPathChars = Path.GetInvalidPathChars();
  private char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
  private List<ISaveToDiskProcessor> saveToDiskProcessorList;
  /// <summary>
  /// Флаг сохранения имен аутентичных файлов с номером версии
  /// </summary>
  private bool flagAddObjectVersionToAuthFilenamesWhenSave;

  /// <summary>путь к папке</summary>
  public string SelectedPath => this.selectedPath;

  /// <summary>Расширение сохраняемого документа</summary>
  public ImDocumentFormat DocumentFormat => this.format;

  /// <summary>конструктор</summary>
  /// <param name="serviceProvider">контейнер сервисов</param>
  /// <param name="selectedPath">папка, куда будем сохранять файлы</param>
  /// <param name="format">в каком формате будем сохранять документы Интермех </param>
  /// <param name="relations">список связей, по которым проводить выгрузку файлов</param>
  /// <param name="isExact">сохранять точные спецификации?</param>
  /// <param name="suffix">суффикс для точных спецификаций</param>
  /// <param name="objectTypesFiltr">включен фильтр по типу объектов?</param>
  /// <param name="objectTypes">список выбранных типов объектов для фильтрации</param>
  /// <param name="createHierarchy">создавать иерархию папок?</param>
  /// <param name="longPathSupport">поддержка длинных путей</param>
  /// <param name="saveCompatibleSigns">сохранять совместимые подписи (сохранять другие не имеет смысла - невозможно проверить правильность подписи)</param>
  /// <param name="addObjectVersionToAuthFilenamesWhenSave">добавлять версию объекта к имени аутентичных файлов</param>
  /// <param name="selectedAttributeID"></param>
  /// <param name="saveToDiskProcessorList">интерфейсы для сохранения по дополнительным параметрам</param>
  /// <param name="dbTypedObjectIDs">выделенные объекты</param>
  public SaveToDiskClass(
    string selectedPath,
    ImDocumentFormat format,
    List<IMSRelationType> relations,
    bool isExact,
    string suffix,
    bool objectTypesFiltr,
    List<int> objectTypes,
    bool createHierarchy,
    bool longPathSupport,
    bool saveCompatibleSigns,
    bool addObjectVersionToAuthFilenamesWhenSave,
    int selectedAttributeID,
    List<ISaveToDiskProcessor> saveToDiskProcessorList,
    List<IDBTypedObjectID> dbTypedObjectIDs)
  {
    this._name = LocalizationHolder.rm.GetString("Client.Core_1214") + selectedPath;
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
    this._maxValue = 100;
    this.dbTypedObjectIDs = dbTypedObjectIDs;
    this.selectedPath = Intermech.Consts.PathPrefix(longPathSupport) + selectedPath;
    this.format = format;
    this.isExact = isExact;
    this.suffix = suffix;
    this.createHierarchy = createHierarchy;
    this.longPathSupport = longPathSupport;
    this.saveCompatibleSigns = saveCompatibleSigns;
    this.addObjectVersionToAuthFilenamesWhenSave = addObjectVersionToAuthFilenamesWhenSave;
    this.objectTypesFiltr = objectTypesFiltr;
    this.objectTypes = objectTypes;
    this.selectedAttributeID = selectedAttributeID;
    this.selectedAttributeType = MetaDataHelper.GetAttributeTypeName(selectedAttributeID);
    this.saveToDiskProcessorList = saveToDiskProcessorList;
    if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
      this.userID = service.UserID;
    relations.ForEach((Action<IMSRelationType>) (item => this.relations.Add(item.RelationTypeID)));
    this.flagAddObjectVersionToAuthFilenamesWhenSave = AuthFilesHolder.GetAddObjectVersionToAuthFilenamesWhenSave();
  }

  /// <summary>сохраняет файл</summary>
  /// <param name="attr">атрибут, из которого выгружаем файлы</param>
  /// <param name="folderPath">папка, в которую будем сохранять файлы</param>
  /// <param name="flagAddObjectVersionToAuthFilenamesWhenSave">флаг дополнительной обработки имен аутентичных файлов</param>
  /// <param name="versionable">признак версионности currentObject при flagAddObjectVersionToAuthFilenamesWhenSave == true</param>
  /// <param name="originalBIfileName">информация для журнала событий, оригинальное имя файла из BlobInformation: если == null, то непосредственного сохранения на диск в функции не было; если != null, значит было сохранение на диск непосредственно в процедуре; но могло еще быть сохранение на диск дополнительных файлов из других объектов на стороне, если из процедуры дёргался сторонний интерфейс</param>
  private void ProperlySave(
    IDBAttribute attr,
    string folderPath,
    IDBObject currentObject,
    bool flagAddObjectVersionToAuthFilenamesWhenSave,
    ObjectVersionModes versionable,
    out string originalBIfileName)
  {
    DialogResult dialogResult = DialogResult.None;
    originalBIfileName = (string) null;
    using (new RemoteLock((object) attr))
    {
      using (new RemoteLock((object) currentObject))
      {
        IBlobReader blobReader = attr as IBlobReader;
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        blobReader.CloseBlob();
        FileTypes fileType = blobInformation.FileType;
        if (!(blobInformation.FileName != string.Empty))
          return;
        string fileName = blobInformation.FileName;
        this.intermechDocument = this.CheckDocumentType(ref fileName);
        int startIndex = 0;
        int num = fileName.IndexOf("\\", 1);
        string str1 = folderPath + "\\";
        for (; num != -1; num = fileName.IndexOf("\\", startIndex + 2))
        {
          str1 = !(fileName[0] == '\\' & startIndex == 0) ? str1 + fileName.Substring(startIndex, num - startIndex) : str1 + fileName.Substring(startIndex + 1, num - startIndex - 1);
          Directory.CreateDirectory(str1);
          startIndex = num;
        }
        bool flag1 = false;
        IDBAttribute dbAttribute = (IDBAttribute) null;
        if (this.selectedAttributeType != string.Empty)
        {
          flag1 = true;
          dbAttribute = currentObject.GetAttributeByID(this.selectedAttributeID);
        }
        string str2;
        if (((attr.Index != 0 ? 0 : (this.selectedAttributeType != string.Empty ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
        {
          if (dbAttribute == null)
          {
            str2 = Path.Combine(str1, Path.GetFileName(fileName));
            if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service)
              service.WriteString(LocalizationHolder.rm.GetString("OutputView_SavingToDisk"), $"{str2}: {string.Format(LocalizationHolder.rm.GetString("Message_CantRenameFileWithAttribute"), (object) currentObject.NameInMessages)} {this.selectedAttributeType}.");
          }
          else
          {
            fileName = this.RemoveInvalidChars(dbAttribute.AsString) + Path.GetExtension(fileName);
            str2 = Path.Combine(str1, fileName);
          }
        }
        else
          str2 = Path.Combine(str1, Path.GetFileName(fileName));
        if (fileType == FileTypes.ftAuthentical && flagAddObjectVersionToAuthFilenamesWhenSave && versionable == ObjectVersionModes.MultiVersion)
          str2 = AuthFilesHolder.GetAuthFilenamesWithVersion(str2, currentObject.VersionID);
        this._name = LocalizationHolder.rm.GetString("Client.Core_1213") + str2;
        this.OnChanged(BackgroundTaskChangedType.Text);
        bool flag2 = false;
        if (this.intermechDocument && this.format == ImDocumentFormat.WmfFormat)
        {
          if (this.baseWmfFileNames.ContainsKey(str2))
          {
            this.fem = RenameMode.WmfMode;
            flag2 = true;
          }
        }
        else
        {
          this.fem = RenameMode.NormalMode;
          flag2 = File.Exists(str2);
        }
        BarManager service1 = ServicesManager.GetService(typeof (BarManager)) as BarManager;
        if (flag2 & !this.replaceAll & !this.discardAll)
        {
          Control.CheckForIllegalCrossThreadCalls = false;
          long oldFileSize = this.fem != RenameMode.NormalMode ? this.baseWmfFileNames[str2] : new FileInfo(str2).Length;
          this.overwrite = new OverwritePromptForm(str2, fileName, oldFileSize, blobInformation.RealFileSize, this.fem);
          dialogResult = this.overwrite.ShowDialog((IWin32Window) service1.OwnerForm);
          Control.CheckForIllegalCrossThreadCalls = true;
          if (dialogResult == DialogResult.Cancel)
            return;
          this.replaceAll = this.overwrite.ReplaceAll;
          this.discardAll = this.overwrite.DiscardAll;
          str2 = this.overwrite.FileName;
        }
        if (!this.intermechDocument)
        {
          this.ms = new MemoryStream();
          this.reader = new BlobProcReader(attr.DBObjectID, AttributableElements.Object, attr.AttributeID, attr.Index, 0, (Stream) this.ms, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          this.reader.ReadData();
          this.ms.Position = 0L;
        }
        if (dialogResult == DialogResult.No | flag2 & this.discardAll)
          return;
        while (true)
        {
          try
          {
            bool flag3;
            if (this.intermechDocument)
            {
              bool updateLinks = !this.saveCompatibleSigns;
              flag3 = this.ConvertIntermechDocument(attr, str2, blobInformation.ModifyDate, updateLinks);
              if (this.format == ImDocumentFormat.WmfFormat)
              {
                if (this.baseWmfFileNames.ContainsKey(str2))
                  this.baseWmfFileNames[str2] = blobInformation.RealFileSize;
                else
                  this.baseWmfFileNames.Add(str2, blobInformation.RealFileSize);
              }
            }
            else
            {
              using (FileStream destination = new FileStream(str2, FileMode.Create))
                this.ms.CopyTo((Stream) destination);
              this.ms.Close();
              File.SetLastWriteTime(str2, blobInformation.ModifyDate);
              flag3 = true;
            }
            if (!flag3)
              break;
            originalBIfileName = blobInformation.FileName;
            break;
          }
          catch (IOException ex)
          {
            if (File.Exists(str2))
            {
              Control.CheckForIllegalCrossThreadCalls = false;
              RenameFileForm renameFileForm = new RenameFileForm(str2);
              if (renameFileForm.ShowDialog((IWin32Window) service1.OwnerForm) == DialogResult.OK)
              {
                if (File.Exists(str2.Replace(str2, renameFileForm.FileName)))
                  renameFileForm.FileName = str2;
                if (renameFileForm.FileName.CompareTo(str2) != 0)
                  str2 = str2.Replace(str2, renameFileForm.FileName);
              }
              Control.CheckForIllegalCrossThreadCalls = true;
            }
            else
              throw;
          }
        }
      }
    }
  }

  /// <summary>Сохранить точную спецификацию</summary>
  /// <param name="objectID">id объекта, для которого будем создавать спецификацию</param>
  /// <param name="objectTypeID">id типа объекта, для которого будем создавать спецификацию</param>
  /// <param name="relationID">id связи, которой объект, для которого будем создавать спецификацию, входит в родительский</param>
  /// <param name="relationTypeID">id типа связи, которой объект, для которого будем создавать спецификацию, входит в родительский</param>
  /// <param name="parts"></param>
  private void SaveSpecification(
    long objectID,
    int objectTypeID,
    long relationID,
    int relationTypeID,
    List<string> parts)
  {
    string str = this.selectedPath;
    if (this.createHierarchy)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(this.selectedPath);
      foreach (string part in parts)
      {
        DirectoryInfo[] directories = directoryInfo.GetDirectories(part, SearchOption.TopDirectoryOnly);
        directoryInfo = directories.Length == 0 ? directoryInfo.CreateSubdirectory(part) : directories[0];
      }
      str = directoryInfo.FullName;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      RelationPair relationPair = new RelationPair(0L, this.topObjectID, this.topObjectTypeID, relationID, sessionKeeper.Session.UserID, objectID, relationTypeID, objectTypeID);
      ISpecificationSaveService service = ServicesManager.GetService(typeof (ISpecificationSaveService)) as ISpecificationSaveService;
      ServicesManager.GetService(typeof (IFiltrationService));
      int objectType = objectTypeID;
      long objectId = objectID;
      RelationPair configureCompositionRoot = relationPair;
      string ownerId = this.filtrSettings.OwnerID;
      string suffix = this.suffix;
      string filePath = str;
      service.SaveSpecification(objectType, objectId, configureCompositionRoot, ownerId, suffix, filePath, false);
    }
  }

  /// <summary>находит файлы для сохранения</summary>
  public void Saving()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    this.processedObjects.Clear();
    if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service1)
      service1.ClearText(LocalizationHolder.rm.GetString("OutputView_SavingToDisk"));
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFiltrationService service2 = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
        IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        this.filtrSettings = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, service2.FiltrationServiceOwnerID, true);
        this.filtrSettings = this.filtrSettings.Clone() as FiltrationSettings;
        this.filtrSettings.OwnerID = Guid.NewGuid().ToString();
        customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this.filtrSettings.OwnerID, this.filtrSettings);
        if (this.filtrSettings.Tags != null && this.filtrSettings.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] != null)
          this.blockConfig = (bool) this.filtrSettings.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"];
        for (int index = 0; index < this.dbTypedObjectIDs.Count; ++index)
        {
          if (this._state == BackgroundTaskState.Stopped)
            return;
          IDBTypedObjectID dbTypedObjectId = this.dbTypedObjectIDs[index];
          if (MetaDataHelper.IsPdmRootObjectType(dbTypedObjectId.ObjectType))
          {
            this.topObjectID = dbTypedObjectId.ObjectID;
            this.topObjectTypeID = dbTypedObjectId.ObjectType;
          }
          else
          {
            this.topObjectID = 0L;
            this.topObjectTypeID = -1;
          }
          IDBObject currentObject = sessionKeeper.Session.GetObject(dbTypedObjectId.ObjectID);
          List<string> parts = new List<string>();
          this.SaveFilesForObject(currentObject, 0L, -1, 0L, -1, parts);
          if (this._state == BackgroundTaskState.Stopped)
            return;
          this.SaveFilesForChildren(currentObject.ObjectID, currentObject.ObjectType, 0L, 1, parts);
        }
      }
      this._state = BackgroundTaskState.Terminated;
      this.OnChanged(BackgroundTaskChangedType.State);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      this._state = BackgroundTaskState.Error;
      this.OnChanged(BackgroundTaskChangedType.State);
      this._result = (object) ex.Message;
      this.OnChanged(BackgroundTaskChangedType.Result);
    }
    finally
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this.filtrSettings.OwnerID, (FiltrationSettings) null);
    }
    if (this._state == BackgroundTaskState.Terminated)
    {
      this._result = (object) LocalizationHolder.rm.GetString("Client.Core_1360");
      this.OnChanged(BackgroundTaskChangedType.Result);
      Thread.Sleep(1000);
      this.OnChanged(BackgroundTaskChangedType.Dispose);
    }
    this._canStop = false;
  }

  private string GetCorrectFilenameByCaption(IDBObject iDBObject)
  {
    string str = iDBObject.Caption.Trim().Trim(this.InvalidPathChars).Trim(' ').Replace(":", "");
    return OSHelper.ReplaceForbiddenSymbols(string.IsNullOrEmpty(str) ? string.Format(LocalizationHolder.rm.GetString("Client.Core_1361"), (object) iDBObject.ObjectID, (object) MetaDataHelper.GetObjectTypeName(iDBObject.ObjectType)) : str, ' ');
  }

  /// <summary>
  ///  найти для объекта атрибут файл,  сохранить все файлы из данного атрибута
  /// </summary>
  /// <param name="currentObject">объект, для которого сохраняем файлы</param>
  /// <param name="parentObjectID">id версии объекта, в который входит данный объекта</param>
  /// <param name="parentObjectTypeID">id типа родительского объекта</param>
  /// <param name="relationID"> id связи, которой данный объект входит в родительский</param>
  /// <param name="relationTypeID">id типа связи, которой данный объект входит в родительский</param>
  /// <param name="parts"></param>
  private void SaveFilesForObject(
    IDBObject currentObject,
    long parentObjectID,
    int parentObjectTypeID,
    long relationID,
    int relationTypeID,
    List<string> parts)
  {
    if (this._state == BackgroundTaskState.Stopped)
      return;
    using (new RemoteLock((object) currentObject))
    {
      if (this.createHierarchy)
      {
        string filenameByCaption = this.GetCorrectFilenameByCaption(currentObject);
        if (!parts.Contains(filenameByCaption))
          parts.Add(filenameByCaption);
      }
      if (this.isExact && this.topObjectID != 0L && this.topObjectTypeID != -1 && MetaDataHelper.IsPdmConfigurableObjectType(parentObjectTypeID) && MetaDataHelper.IsObjectTypeChildOf(currentObject.ObjectType, MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545")))
        return;
      this._name = string.Format(LocalizationHolder.rm.GetString("Client.Core_1362"), (object) currentObject.NameInMessages);
      this.OnChanged(BackgroundTaskChangedType.Text);
      this._value = 0;
      this.MaximumValue = 1;
      this.OnChanged(BackgroundTaskChangedType.Value);
      IDBAttribute fileAttribute = this.FindFileAttribute(currentObject, 0L, -1);
      this._value = 1;
      this.OnChanged(BackgroundTaskChangedType.Value);
      if (fileAttribute != null && fileAttribute.ValuesCount > 0)
      {
        ObjectVersionModes versionable = ObjectVersionModes.Abstract;
        if (this.flagAddObjectVersionToAuthFilenamesWhenSave)
          versionable = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(currentObject.ObjectType).Versionable;
        string str = this.selectedPath;
        if (this.createHierarchy)
        {
          DirectoryInfo directoryInfo = new DirectoryInfo(this.selectedPath);
          foreach (string part in parts)
          {
            DirectoryInfo[] directories = directoryInfo.GetDirectories(part, SearchOption.TopDirectoryOnly);
            directoryInfo = directories.Length == 0 ? directoryInfo.CreateSubdirectory(part) : directories[0];
          }
          str = directoryInfo.FullName;
        }
        this._value = 0;
        this.OnChanged(BackgroundTaskChangedType.Value);
        this.MaximumValue = fileAttribute.ValuesCount;
        List<string> stringList = new List<string>();
        for (int index = 0; index < fileAttribute.ValuesCount; ++index)
        {
          fileAttribute.Index = index;
          if (this._state == BackgroundTaskState.Stopped)
            return;
          string originalBIfileName = (string) null;
          this.ProperlySave(fileAttribute, str, currentObject, this.flagAddObjectVersionToAuthFilenamesWhenSave, versionable, out originalBIfileName);
          if (originalBIfileName != null && originalBIfileName != string.Empty)
            stringList.Add(originalBIfileName);
          ++this._value;
          this.OnChanged(BackgroundTaskChangedType.Value);
        }
        if (stringList.Count > 0)
        {
          string note = string.Join(Environment.NewLine, stringList.ToArray());
          ClientEventLog.AddEvent4Attributable(currentObject.ObjectID, AttributableElements.Object, note, ActionType.SaveToDisk, EventlogRecordType.AccessGranted);
        }
        if (this.saveCompatibleSigns)
        {
          string filenameByCaption = this.GetCorrectFilenameByCaption(currentObject);
          this.CompatibleSignsSave(Path.Combine(str, filenameByCaption + SignConsts.SignFolderPostfix), currentObject);
        }
        if (this.saveToDiskProcessorList != null)
        {
          for (int index = 0; index < this.saveToDiskProcessorList.Count; ++index)
            this.saveToDiskProcessorList[index].Save((ISaveToDiskClass) this, str, currentObject.ObjectID);
        }
      }
      if (!this.isExact || this.topObjectID == 0L || this.topObjectTypeID == -1 || !MetaDataHelper.IsPdmConfigurableObjectType(currentObject.ObjectType) || !MetaDataHelper.HasApplicability(currentObject.ObjectType, MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545"), MetaDataHelper.GetRelationTypeID(ExpertObjGUIDs.linkDocForIzd)))
        return;
      this._value = 0;
      this.OnChanged(BackgroundTaskChangedType.Value);
      this.MaximumValue = 1;
      if (MetaDataHelper.GetRelationType(new Guid(ExpertObjGUIDs.linkDocForIzd)) != null)
        this.SaveSpecification(currentObject.ObjectID, currentObject.TypeID, relationID, relationTypeID, parts);
      ++this._value;
      this.OnChanged(BackgroundTaskChangedType.Value);
    }
  }

  /// <summary>
  /// определить, есть ли совместимые подписи, и если есть, то создать folder и свалить их в туда
  /// </summary>
  /// <param name="folder"></param>
  /// <param name="currentObject"></param>
  private void CompatibleSignsSave(string folder, IDBObject currentObject)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag1 = false;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(SignConsts.GraphAttrTypeGuid);
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(SignConsts.CryptoSignObjectTypeGuid, new DBRecordSetParams((ConditionStructure[]) null)
      {
        Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(0, RelationalOperators.EntersIn, (object) Math.Abs(currentObject.ObjectID), LogicalOperators.AND, 0, false)
        },
        Columns = new object[2]
        {
          (object) -2,
          (object) SignConsts.SignVersionAttrTypeID
        }
      }).Rows)
      {
        if (HashProcs.IsCompatibleSign(Convert.ToInt32(row[1])))
        {
          if (!flag1)
          {
            if (!Directory.Exists(folder))
              Directory.CreateDirectory(folder);
            flag1 = true;
          }
          IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(row[0]));
          if (dbObject != null)
          {
            bool flag2 = false;
            string empty1 = string.Empty;
            if (attributeType != null)
            {
              string asString = dbObject.Attributes.FindByGUID(SignConsts.GraphAttrTypeGuid).AsString;
              int index = attributeType.PossibleValues.IndexOf((object) asString);
              if (index != -1)
              {
                empty1 = Convert.ToString(attributeType.PossibleValuesDescriptions[index]);
                flag2 = true;
              }
            }
            string empty2 = string.Empty;
            long asInteger1 = dbObject.Attributes.FindByGUID(SignConsts.RankAttrTypeGuid).AsInteger;
            string caption1 = sessionKeeper.Session.GetObject(asInteger1).Caption;
            long asInteger2 = dbObject.Attributes.FindByGUID(SignConsts.SignUpAttrTypeGuid).AsInteger;
            string caption2 = sessionKeeper.Session.GetObject(asInteger2).Caption;
            string str = $"{(flag2 ? empty1 + " - " : string.Empty)}{caption1} - {caption2}";
            IDBAttribute byGuid1 = dbObject.Attributes.FindByGUID(SignConsts.EDSAttrTypeGuid);
            if (byGuid1 is IBlobReader blobReader1 && blobReader1.OpenBlob(-1).RealFileSize > 0L)
            {
              using (MemoryStream aDestStream = new MemoryStream())
              {
                new BlobProcReader(byGuid1, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
                aDestStream.Position = 0L;
                string base64String = Convert.ToBase64String(aDestStream.ToArray());
                File.WriteAllText(Path.Combine(folder, str + SignConsts.SignFileExtension), base64String, Encoding.ASCII);
              }
            }
            IDBAttribute byGuid2 = dbObject.Attributes.FindByGUID(SignConsts.SignDataSequenceAttrTypeGuid);
            if (byGuid2 is IBlobReader blobReader2 && blobReader2.OpenBlob(-1).RealFileSize > 0L)
            {
              using (MemoryStream aDestStream = new MemoryStream())
              {
                new BlobProcReader(byGuid2, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
                aDestStream.Position = 0L;
                HashContent hashContent = new HashContent();
                hashContent.Load((Stream) aDestStream);
                if (hashContent.Files.Count > 0)
                {
                  using (TextWriter textWriter = (TextWriter) new StreamWriter(Path.Combine(folder, str + SignConsts.HashOrderFileExtension), false, Encoding.Default))
                  {
                    for (int index = 0; index < hashContent.Files.Count; ++index)
                      textWriter.WriteLine(hashContent.Files[index]);
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  /// <summary>найти и вернуть для объекта атрибут Файл</summary>
  /// <param name="currentObject">id версии объекта, для которого сохраняем файлы</param>
  /// <param name="parentObjectID">id версии объекта, в который входит данный объекта</param>
  /// <param name="relationTypeID">id типа  связи, которым данный объект входит в родительский</param>
  /// <returns>атрибут Файл или null если такого атрибута у объекта нет</returns>
  private IDBAttribute FindFileAttribute(
    IDBObject currentObject,
    long parentObjectID,
    int relationTypeID)
  {
    IDBAttribute fileAttribute = (IDBAttribute) null;
    try
    {
      currentObject.SaveToDisk();
    }
    catch (AccessDeniedException ex)
    {
      AccessDeniedExceptionForm.OnExceptionHandler((object) currentObject, new ExceptionEventArgs((Exception) ex));
      return fileAttribute;
    }
    if ((currentObject.TypeID != MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545") || !this.configObjectIDs.Contains(parentObjectID) || relationTypeID != MetaDataHelper.GetRelationTypeID(ExpertObjGUIDs.linkDocForIzd)) && (!this.objectTypesFiltr || this.objectTypesFiltr && this.objectTypes.Contains(currentObject.TypeID)))
      fileAttribute = currentObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
    return fileAttribute;
  }

  /// <summary>ищем состав объекта</summary>
  /// <param name="parentObjectID">id объекта, состав которого ищем</param>
  /// <param name="parentObjectTypeID">id типа объекта, состав которого ищем</param>
  /// <param name="relationID">id связи, которой parentObjectID входит в родительский (для создания relPair и поиска конфигурируемого состав)</param>
  /// <param name="relationTypeID">id типа связи, которой parentObjectID входит в родительский (для создания relPair и поиска конфигурируемого состав)</param>
  /// <param name="parts"></param>
  private void SaveFilesForChildren(
    long parentObjectID,
    int parentObjectTypeID,
    long relationID,
    int relationTypeID,
    List<string> parts)
  {
    if (this._state == BackgroundTaskState.Stopped)
      return;
    foreach (int relation in this.relations)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relation);
        relationCollection.LocalTypesMode = true;
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[3]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) ObligatoryObjectAttributes.F_RELATION_TYPE
        });
        if (!this.blockConfig && this.topObjectID != 0L && this.topObjectTypeID != -1 && MetaDataHelper.IsPdmConfigurableRelationType(relation))
        {
          if (paramSet.Tags == null)
            paramSet.Tags = new HybridDictionary();
          paramSet.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
          RelationPair relationPair = new RelationPair(0L, this.topObjectID, this.topObjectTypeID, relationID, sessionKeeper.Session.UserID, parentObjectID, relationTypeID, parentObjectTypeID);
          paramSet.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) relationPair;
        }
        relationCollection.FiltrationOwnerID = this.filtrSettings.OwnerID;
        DataTable dataTable = relationCollection.ConsistFrom(paramSet, parentObjectID);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64_1 = Convert.ToInt64(row[0]);
            long int64_2 = Convert.ToInt64(row[1]);
            int int32 = Convert.ToInt32(row[2]);
            IDBObject currentObject = sessionKeeper.Session.GetObject(int64_1, false);
            if (!this.processedObjects.Contains(int64_1))
            {
              this.processedObjects.Add(int64_1);
              List<string> stringList = new List<string>((IEnumerable<string>) parts);
              this.SaveFilesForObject(currentObject, parentObjectID, parentObjectTypeID, int64_2, int32, parts);
              if (this._state == BackgroundTaskState.Stopped)
                return;
              this.SaveFilesForChildren(int64_1, currentObject.ObjectType, int64_2, int32, parts);
              parts = stringList;
            }
          }
        }
      }
    }
  }

  /// <summary>Определить, может, это документ интермех</summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private bool CheckDocumentType(ref string fileName)
  {
    string str = Path.GetExtension(fileName);
    int num = this.imDocExt.Contains(str) ? 1 : 0;
    bool flag = num != 0 || this.ipsImDocExt.Contains(str);
    if (num != 0 && this.format == ImDocumentFormat.XmlFormat)
      fileName = Path.ChangeExtension(fileName, ".imdx");
    if (flag && this.format == ImDocumentFormat.PdfFormat)
      fileName = Path.ChangeExtension(fileName, ".pdf");
    return flag;
  }

  /// <summary>Конвертируем в указанный пользователем формат</summary>
  /// <param name="fileAttribute">Файловый атрибут содержащий документ</param>
  /// <param name="fileName">Имя файла на диске, в который сохраняется документ</param>
  /// <param name="dt">Дата и время</param>
  /// <param name="updateLinks">Обновлять ссылки в документе при сохранении на диск</param>
  private bool ConvertIntermechDocument(
    IDBAttribute fileAttribute,
    string fileName,
    DateTime dt,
    bool updateLinks)
  {
    bool flag = false;
    if (ServicesManager.GetService(typeof (IDocumentConverter)) is IDocumentConverter service)
    {
      if (this.format == ImDocumentFormat.XmlFormat)
      {
        service.ConvertToXml(fileAttribute, fileName, updateLinks);
        if (File.Exists(fileName))
        {
          File.SetLastWriteTime(fileName, dt);
          flag = true;
        }
      }
      else if (this.format == ImDocumentFormat.WmfFormat)
      {
        service.ConvertToWmf(fileAttribute, fileName);
        if (File.Exists(fileName))
        {
          File.SetLastWriteTime(fileName, dt);
          flag = true;
        }
      }
      else
      {
        service.ConvertToPdf(fileAttribute, fileName, false);
        if (File.Exists(fileName))
        {
          File.SetLastWriteTime(fileName, dt);
          flag = true;
        }
      }
    }
    return flag;
  }

  private string RemoveInvalidChars(string fileName)
  {
    foreach (char invalidFileNameChar in this.InvalidFileNameChars)
      fileName = fileName.Replace(invalidFileNameChar.ToString(), "");
    fileName = fileName.Trim();
    return fileName;
  }
}
