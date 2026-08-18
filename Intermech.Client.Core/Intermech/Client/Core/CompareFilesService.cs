
// Type: Intermech.Client.Core.CompareFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.CacheServices;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Сервис, осуществляющий сравнение файлов</summary>
public class CompareFilesService : ICompareFilesService
{
  /// <summary>Словарь соответствия типов обрабатывающим их плагинам</summary>
  private readonly List<ICanCompareObjectsFiles> _comparePlugins = new List<ICanCompareObjectsFiles>();
  /// <summary>
  /// Информация о том, чем и как сравнивать файлы определенных расширений
  /// </summary>
  private List<FilesComparisonSettings> _filesComparisonSettings;

  /// <summary>Регистрируем плагин в сервисе сравнения</summary>
  /// <param name="pluginForAdding">Плагин</param>
  public void AddPluginToCompareFilesService(ICanCompareObjectsFiles pluginForAdding)
  {
    if (pluginForAdding == null || pluginForAdding.TypeIds == null)
      return;
    this.RemoveAbstractAndRepeatedTypes(pluginForAdding);
    this._comparePlugins.Add(pluginForAdding);
  }

  /// <summary>Удаляет информацию о плагине</summary>
  /// <param name="plugin"></param>
  public void DeletePluginFromCompareFilesService(ICanCompareObjectsFiles plugin)
  {
    if (plugin == null)
      return;
    this._comparePlugins.Remove(plugin);
  }

  /// <summary>Получить настройки сравнения файлов через приложения</summary>
  /// <returns>Настройки сравнения файлов через приложения</returns>
  public List<FilesComparisonSettings> GetAllFilesComparisonSettings()
  {
    if (this._filesComparisonSettings == null)
      this.ReadSettingsFromBase();
    return this._filesComparisonSettings;
  }

  public void SaveFilesComparisonSettings(List<FilesComparisonSettings> settings)
  {
    this.WriteSettingsToBase(settings);
  }

  /// <summary>Пишем настройки в базу</summary>
  /// <param name="filesComparisonSettingsInfo"></param>
  public void WriteSettingsToBase(
    List<FilesComparisonSettings> filesComparisonSettingsInfo)
  {
    DataTable table = new DataTable();
    table.BeginLoadData();
    table.Columns.Add("F_PARAM_NAME", typeof (string));
    table.Columns.Add("F_VALUE", typeof (string));
    for (int index = 0; index < filesComparisonSettingsInfo.Count; ++index)
    {
      DataRow row = table.NewRow();
      row["F_PARAM_NAME"] = (object) index;
      row["F_VALUE"] = (object) filesComparisonSettingsInfo[index].ToString();
      table.Rows.Add(row);
    }
    table.EndLoadData();
    table.AcceptChanges();
    if (!(ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service))
      throw new KernelException("Не найден сервис IDBConfigurations");
    service.WriteSection("CLIENT", "FILESCOMPARISON", table, 0L);
    this._filesComparisonSettings = filesComparisonSettingsInfo;
  }

  /// <summary>Сравнить файлы для ДВУХ объектов</summary>
  /// <param name="objectsForCompareIds">Объекты, файлы которых надо будет сравнить</param>
  public void CompareTwoObjectsFiles(ISelectedItems itemsForCompare, FileTypes fileType)
  {
    DBObjectToCompare object1;
    DBObjectToCompare object2;
    using (SessionKeeper sk = new SessionKeeper())
    {
      DBObjectToCompare[] objectsForCompare = CompareFilesService.GetObjectsForCompare(sk, itemsForCompare);
      object1 = objectsForCompare[0];
      object2 = objectsForCompare[1];
      if (fileType != FileTypes.ftAuthentical && CompareFilesService.IsOneOfTheObjectIsCompositDoc(sk, objectsForCompare))
        return;
      ICanCompareObjectsFiles comparingPlugin = this.GetComparingPlugin(object1.ObjectTypeID);
      if (comparingPlugin != null)
      {
        comparingPlugin.CompareFilesFor(object1, object2, fileType);
        return;
      }
    }
    this.CompareFilesWithCommonRules(object1, object2, fileType);
  }

  /// <summary>
  /// Сравнение файлов по общим правилам.
  /// Сначала проверяем на пдф, потом настройки системы, в крайнем случае показываем общую форму.
  /// </summary>
  /// <param name="object1">Объект для сравнеия</param>
  /// <param name="object2">Объект для сравнения</param>
  /// <param name="fileType">Тип файла для сравнения</param>
  public void CompareFilesWithCommonRules(
    DBObjectToCompare object1,
    DBObjectToCompare object2,
    FileTypes fileType)
  {
    ObjectFileInfo fileData1 = this.GetFileData(object1, fileType);
    if (fileData1 == null || fileData1.FileIndex < 0)
      return;
    ObjectFileInfo fileData2 = this.GetFileData(object2, fileType);
    if (fileData2 == null || fileData2.FileIndex < 0)
      return;
    if (Path.GetExtension(fileData1.FileName) == ".pdf" && Path.GetExtension(fileData1.FileName) == Path.GetExtension(fileData2.FileName))
    {
      ICanCompareObjectsFiles comparePdfPlugin = this.FindComparePDFPlugin();
      if (comparePdfPlugin != null)
      {
        comparePdfPlugin.CompareFilesFor(object1, object2, fileType);
        return;
      }
    }
    string @extension = CompareFilesService.GetExtension(fileData1.FileName, fileData2.FileName);
    if (@extension == string.Empty)
    {
      CompareFilesService.ShowCommonCompareForm(fileData1, fileData2);
    }
    else
    {
      FilesComparisonSettings settingsForExtension = this.GetFilesComparisonSettingsForExtension(@extension);
      if (settingsForExtension != null)
        this.CompareFilesWithApplicationInSettings(fileData1.ObjectId, fileData1.FileIndex, fileData2.ObjectId, fileData2.FileIndex, settingsForExtension);
      else
        CompareFilesService.ShowCommonCompareForm(fileData1, fileData2);
    }
  }

  /// <summary>
  /// Ищем, есть ли в системе плагин для сравнения пдф-файлов
  /// </summary>
  /// <returns></returns>
  public ICanCompareObjectsFiles FindComparePDFPlugin()
  {
    foreach (ICanCompareObjectsFiles comparePlugin in this._comparePlugins)
    {
      if (comparePlugin is ICanComparePDFFiles)
        return comparePlugin;
    }
    return (ICanCompareObjectsFiles) null;
  }

  /// <summary>Показать форму общего сравнения файлов</summary>
  /// <param name="fileData1">Инфо о первом файле</param>
  /// <param name="fileData2">Инфо о втором файле</param>
  public static void ShowCommonCompareForm(ObjectFileInfo fileData1, ObjectFileInfo fileData2)
  {
    using (FilesComparisonForm filesComparisonForm = new FilesComparisonForm())
    {
      filesComparisonForm.Init(fileData1, fileData2);
      int num = (int) filesComparisonForm.ShowDialog();
    }
  }

  /// <summary>
  /// Сравнить два файла сторонним приложением, исходя из найденных настроек
  /// </summary>
  /// <param name="object1"></param>
  /// <param name="fileIndex1"></param>
  /// <param name="object2"></param>
  /// <param name="fileIndex2"></param>
  /// <param name="compareSettings"></param>
  private void CompareFilesWithApplicationInSettings(
    long objectId1,
    int fileIndex1,
    long objectId2,
    int fileIndex2,
    FilesComparisonSettings compareSettings)
  {
    PublishedFile objectFile1 = ClientContext.FileVault.ViewArea.Publish((IList<DBObjectState>) ClientContext.FileVault.DBObjectsInfo.CreateStateListForSingleObject(objectId1)).ObjectFiles[fileIndex1];
    PublishedFile objectFile2 = ClientContext.FileVault.ViewArea.Publish((IList<DBObjectState>) ClientContext.FileVault.DBObjectsInfo.CreateStateListForSingleObject(objectId2)).ObjectFiles[fileIndex2];
    Process.Start(new ProcessStartInfo()
    {
      FileName = compareSettings.ProgramExePath,
      Arguments = this.GetArguments(compareSettings.Arguments, objectFile1.FullName, objectFile2.FullName)
    });
  }

  /// <summary>
  /// Проверить на наличие расширений у файлов.
  /// Пока они могут быть разными,т.к., например, файлы doc и docx можно сравнить
  /// </summary>
  /// <param name="choosedFileName1"></param>
  /// <param name="choosedFileName2"></param>
  /// <returns></returns>
  public static string GetExtension(string choosedFileName1, string choosedFileName2)
  {
    if (Path.GetExtension(choosedFileName1) == null)
      return string.Empty;
    string str = Path.GetExtension(choosedFileName2);
    return str == null ? string.Empty : str.Replace(".", "");
  }

  /// <summary>
  /// Проверка на то, составные ли документы, если это документы
  /// </summary>
  /// <param name="object1Type"></param>
  /// <param name="fileComparisonService"></param>
  /// <param name="sk"></param>
  /// <param name="objectsForCompare"></param>
  /// <returns>true, если хотя бы один из документов составной</returns>
  private static bool IsOneOfTheObjectIsCompositDoc(
    SessionKeeper sk,
    DBObjectToCompare[] objectsForCompare)
  {
    int num1 = MetaDataHelper.IsObjectTypeChildOf(objectsForCompare[0].ObjectTypeID, new Guid("cad00070-306c-11d8-b4e9-00304f19f545")) ? 1 : 0;
    if (!(sk.Session.GetCustomService(typeof (IFileComparisonService)) is IFileComparisonService customService))
      throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1705"));
    if (num1 != 0)
    {
      Guid sessionGuid = sk.Session.SessionGUID;
      long[] docIds = new long[2]
      {
        objectsForCompare[0].ObjectID,
        objectsForCompare[1].ObjectID
      };
      if (customService.DocsAreComposite(sessionGuid, docIds))
      {
        int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1698"), LocalizationHolder.rm.GetString("Client.Core_1477"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Получить сформированную строку аргументов с подстановками, замененными на пути к файлам
  /// </summary>
  /// <param name="settingsArguments">строка с подстановками</param>
  /// <param name="publishedFile1FullName">путь к первому файлу</param>
  /// <param name="publishedFile2FullName">путь ко второму файлу</param>
  /// <returns></returns>
  private string GetArguments(
    string settingsArguments,
    string publishedFile1FullName,
    string publishedFile2FullName)
  {
    return settingsArguments.Replace("%file1", publishedFile1FullName).Replace("%file2", publishedFile2FullName);
  }

  /// <summary>
  /// Возвращает первую попавшуюся настройку, которая подписана на обработку файлов данного расширения
  /// Или null, если ничего не найдено
  /// </summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  private FilesComparisonSettings GetFilesComparisonSettingsForExtension(string @extension)
  {
    foreach (FilesComparisonSettings comparisonSetting in this._filesComparisonSettings)
    {
      if (comparisonSetting.ExtensionsAsString.Contains(@extension))
        return comparisonSetting;
    }
    return (FilesComparisonSettings) null;
  }

  /// <summary>Читаем настройки из базы</summary>
  private void ReadSettingsFromBase()
  {
    this._filesComparisonSettings = new List<FilesComparisonSettings>();
    if (!(ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service))
      throw new KernelException("Не найден сервис IDBConfigurations");
    foreach (DataRow row in (InternalDataCollectionBase) service.ReadSection("CLIENT", "FILESCOMPARISON", 0L).Rows)
    {
      string str = Convert.ToString(row[1]);
      if (!string.IsNullOrEmpty(str))
      {
        string[] strArray = str.Split('¦');
        if (strArray.Length >= 4)
        {
          string name = strArray[0];
          string programExePath = strArray[1];
          string extensionsAsString = strArray[2];
          string arguments = strArray[3];
          this._filesComparisonSettings.Add(new FilesComparisonSettings(name, programExePath, arguments, extensionsAsString));
        }
      }
    }
  }

  /// <summary>
  /// Получить информацию об основном файле объекта, который будем сравнивать
  /// </summary>
  /// <param name="objectId"></param>
  /// <param name="fileType">Какой тип файла ищем</param>
  /// <returns>Информация о файле, который будем сравнивать</returns>
  public ObjectFileInfo GetFileData(DBObjectToCompare objectToCompare, FileTypes fileType)
  {
    string str = string.Empty;
    List<ObjectFileInfo> objectFileInfoList = new List<ObjectFileInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(objectToCompare.ObjectID, (object) MetaDataHelper.GetAttributeID((object) new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")), false, false);
      if (objectAttribute == null)
        throw new KernelException($"Не найден атрибут файл для объекта {objectToCompare.NameInMessages}");
      str = $"{objectToCompare.NameInMessages} [{(object) objectToCompare.VersionID}]";
      if (objectAttribute.ValuesCount == 0)
      {
        int num = (int) MessageBox.Show($"Объект {str} не содержит файлов.", "Внимание!", MessageBoxButtons.OK);
        return (ObjectFileInfo) null;
      }
      int num1 = 0;
      for (int index = 0; index < objectAttribute.ValuesCount; ++index)
      {
        objectAttribute.Index = index;
        if (!(objectAttribute is IBlobReader blobReader))
        {
          objectFileInfoList.Add(new ObjectFileInfo(BlobInformation.EmptyBlobInformation(), -1, 0L, str));
        }
        else
        {
          BlobInformation blobInformation = blobReader.OpenBlob(-1);
          if (blobInformation.FileType == fileType)
          {
            objectFileInfoList.Add(new ObjectFileInfo(blobInformation, index, objectToCompare.ObjectID, str));
            ++num1;
          }
          else
            objectFileInfoList.Add(new ObjectFileInfo(BlobInformation.EmptyBlobInformation(), -1, 0L, str));
          blobReader.CloseBlob();
        }
      }
      switch (num1)
      {
        case 0:
          int num2 = (int) MessageBox.Show($"Объект {str} не содержит файлов нужного типа.", "Внимание!", MessageBoxButtons.OK);
          return (ObjectFileInfo) null;
        case 1:
          return objectFileInfoList.First<ObjectFileInfo>((System.Func<ObjectFileInfo, bool>) (x => x.FileName != string.Empty));
      }
    }
    ChoosingFileForm choosingFileForm = new ChoosingFileForm();
    choosingFileForm.Init(str, objectFileInfoList);
    int num3 = (int) choosingFileForm.ShowDialog();
    return choosingFileForm.ChoosedObjectFileInfo.FileName == null ? (ObjectFileInfo) null : choosingFileForm.ChoosedObjectFileInfo;
  }

  /// <summary>Получить плагин для сравнения файлов указанного типа</summary>
  /// <param name="typeId">Тип объектов, файлы для которого сравниваем</param>
  /// <returns>Плагин, сравнивающий данный тип файлов</returns>
  private ICanCompareObjectsFiles GetComparingPlugin(int typeId)
  {
    foreach (ICanCompareObjectsFiles comparePlugin in this._comparePlugins)
    {
      if (comparePlugin.TypeIds.Contains(typeId))
        return comparePlugin;
    }
    int[] parentTypes = ((IObjectTypeHierarchy) ((ICacheServices) ServicesManager.GetService(typeof (ICacheServices))).GetService("ObjectTypeHierarchy")).GetParentTypes(typeId);
    for (int index = 1; index < parentTypes.Length; ++index)
    {
      foreach (ICanCompareObjectsFiles comparePlugin in this._comparePlugins)
      {
        if (comparePlugin.TypeIds.Contains(parentTypes[index]))
          return comparePlugin;
      }
    }
    return (ICanCompareObjectsFiles) null;
  }

  /// <summary>
  /// Удаляет из списка абстрактные типы и типы, которые уже обрабатывает другой плагин
  /// </summary>
  /// <param name="pluginForAdding">Добавляемый плагин</param>
  private void RemoveAbstractAndRepeatedTypes(ICanCompareObjectsFiles pluginForAdding)
  {
    IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
    for (int index = pluginForAdding.TypeIds.Count - 1; index == 0; --index)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(pluginForAdding.TypeIds[index]);
      if (objectType == null)
        pluginForAdding.RemoveTypeId(pluginForAdding.TypeIds[index]);
      else if (objectType.VersionsMode == ObjectVersionModes.Abstract && service != null)
      {
        service.WriteString(LocalizationHolder.rm.GetString("Client.Core_1702"), string.Format(LocalizationHolder.rm.GetString("Client.Core_1703"), (object) pluginForAdding.NameInMessages, (object) objectType.ObjectTypeName));
        pluginForAdding.RemoveTypeId(pluginForAdding.TypeIds[index]);
      }
      else
      {
        foreach (ICanCompareObjectsFiles comparePlugin in this._comparePlugins)
        {
          if (comparePlugin.TypeIds.Contains(pluginForAdding.TypeIds[index]) && service != null)
          {
            service.WriteString(LocalizationHolder.rm.GetString("Client.Core_1702"), string.Format(LocalizationHolder.rm.GetString("Client.Core_1704"), (object) pluginForAdding.NameInMessages, (object) objectType.ObjectTypeName, (object) comparePlugin.NameInMessages));
            pluginForAdding.RemoveTypeId(pluginForAdding.TypeIds[index]);
          }
        }
      }
    }
  }

  /// <summary>
  /// Получить массив из двух объектов для сравнения их файлов
  /// </summary>
  /// <param name="itemssForCompare"></param>
  /// <returns></returns>
  private static DBObjectToCompare[] GetObjectsForCompare(
    SessionKeeper sk,
    ISelectedItems itemsForCompare)
  {
    DBObjectToCompare[] objectsForCompare = new DBObjectToCompare[2];
    if (itemsForCompare.Count == 1)
    {
      if (!(itemsForCompare.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1699"));
      IDBObject dbObject1 = sk.Session.GetObject(itemData.ObjectID, false);
      IDBObject dbObject2 = sk.Session.GetObject(Math.Abs(itemData.ObjectID), false);
      if (dbObject1 == null || dbObject2 == null)
        throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1699"));
      objectsForCompare[0] = new DBObjectToCompare(dbObject1.ObjectID, dbObject1.ObjectType, dbObject1.VersionID, dbObject1.Caption, dbObject1.NameInMessages);
      objectsForCompare[1] = new DBObjectToCompare(dbObject2.ObjectID, dbObject2.ObjectType, dbObject2.VersionID, dbObject2.Caption, dbObject2.NameInMessages);
    }
    else
    {
      IDBTypedObjectID itemData1 = itemsForCompare.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      IDBTypedObjectID itemData2 = itemsForCompare.GetItemData(1, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (itemData1 == null || itemData2 == null)
        throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1699"));
      IDBObject dbObject3 = sk.Session.GetObject(itemData1.ObjectID, false);
      IDBObject dbObject4 = sk.Session.GetObject(itemData2.ObjectID, false);
      if (dbObject3 == null || dbObject4 == null)
        throw new KernelException(LocalizationHolder.rm.GetString("Client.Core_1699"));
      objectsForCompare[0] = new DBObjectToCompare(dbObject3.ObjectID, dbObject3.ObjectType, dbObject3.VersionID, dbObject3.Caption, dbObject3.NameInMessages);
      objectsForCompare[1] = new DBObjectToCompare(dbObject4.ObjectID, dbObject4.ObjectType, dbObject4.VersionID, dbObject4.Caption, dbObject4.NameInMessages);
    }
    return objectsForCompare;
  }
}
