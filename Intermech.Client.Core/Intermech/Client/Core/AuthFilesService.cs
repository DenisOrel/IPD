
// Type: Intermech.Client.Core.AuthFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core;

internal sealed class AuthFilesService : IAuthFilesService
{
  private static DialogResult authReplaceAll;

  /// <summary>
  /// Вернуть список типов файлов, которыми могут быть аутентичные файлы
  /// </summary>
  /// <returns></returns>
  public List<string> GetPossibleAuthFileTypes()
  {
    return DocumentTypeSettings.SplitAdditionalFileExts((ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("CLIENT", "AUTHFILES", "AUTHFILESEXTENSIONS", "", DBConfigMode.GlobalOnly));
  }

  public event AuthFileNeedGenerateEventHandler AuthFileNeedGenerate;

  public void FireAuthFileNeedGenerate(AuthFileNeedGenerateEventArgs eventArgs)
  {
    AuthFileNeedGenerateEventHandler fileNeedGenerate = this.AuthFileNeedGenerate;
    if (fileNeedGenerate == null)
      return;
    fileNeedGenerate((object) this, eventArgs);
  }

  public event AuthFileAssignEventHandler AuthFileAssignEvent;

  public void FireEventAuthFileAssign(AuthFileAssignEventArgs eventArgs)
  {
    AuthFileAssignEventHandler authFileAssignEvent = this.AuthFileAssignEvent;
    if (authFileAssignEvent == null)
      return;
    authFileAssignEvent((object) this, eventArgs);
    if (!eventArgs.IsHandled)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(eventArgs.ObjectId);
      if (dbObject == null)
        return;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return;
      FileAttribute4ObjectChangedEventArgs e = new FileAttribute4ObjectChangedEventArgs(attributeByGuid.AttributeID, dbObject.ObjectID);
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) e);
    }
  }

  /// <summary>
  /// Проверить аутентичные файлы и при необходимости перегенерировать
  /// </summary>
  /// <param name="items">Список объектов</param>
  /// <param name="askForNotInternals">Задавать ли вопросы в режиме обновления, надо ли обновлять аутентичные файлы для не внутренних документов IPS. Вопросы не задаются для внутренних документов в любом режиме, а также для не внутренних в режиме создания)</param>
  /// <param name="updateMode">Режим: false: перегенерировать в любом случае (режим создания), или true: проверять необходимость создания файлов (режим обновления)</param>
  /// <returns>true - проверка выполнена; false - результаты проверки не гарантируют актуальности аутентичных файлов (напр, пользователь не подтвердил необходимость перегенерации аутентичных файлов)</returns>
  public bool CheckAuthFiles(ISelectedItems items, bool updateMode, bool askForNotInternals = true)
  {
    List<AuthFileNeedGenerateEventArgs> generateEventArgsList = new List<AuthFileNeedGenerateEventArgs>();
    List<AuthFileNeedGenerateEventArgs> collection = new List<AuthFileNeedGenerateEventArgs>();
    AuthFilesService.authReplaceAll = DialogResult.None;
    HybridDictionary hybridDictionary = new HybridDictionary();
    for (int index1 = 0; index1 < items.Count; ++index1)
    {
      IDBTypedObjectID itemData = items.GetItemData(index1, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      AuthFileNeedGenerateEventArgs eventArgs = new AuthFileNeedGenerateEventArgs(itemData.ObjectType, itemData.ObjectID);
      this.FireAuthFileNeedGenerate(eventArgs);
      bool flag1 = !updateMode;
      int num = eventArgs.InternalDocument ? 1 : 0;
      if (eventArgs.IsHandled && eventArgs.NeedGenerate)
        flag1 = true;
      if (!eventArgs.IsHandled && updateMode)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
          bool flag2 = false;
          IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
          if (dbObject != null)
          {
            bool flag3 = false;
            DocumentTypeSettings documentTypeSettings = new DocumentTypeSettings();
            hybridDictionary[(object) itemData.ObjectID] = (object) dbObject.Caption;
            IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid1 != null)
            {
              IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad0013a-306c-11d8-b4e9-00304f19f545"));
              DateTime t1 = attributeByGuid2 == null || attributeByGuid2.IsNull ? DateTime.MinValue : attributeByGuid2.AsDateTime;
              for (int index2 = 0; index2 < attributeByGuid1.ValuesCount; ++index2)
              {
                attributeByGuid1.Index = index2;
                if (attributeByGuid1 is IBlobReader blobReader)
                {
                  BlobInformation blobInformation = blobReader.OpenBlob(-1);
                  if (Path.GetExtension(blobInformation.FileName).Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
                  {
                    if (!flag3)
                    {
                      documentTypeSettings = customService.GetSettings(sessionKeeper.Session.SessionGUID, dbObject.ObjectType);
                      flag3 = true;
                    }
                    if (documentTypeSettings.DocumentFileExt.Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
                    {
                      flag2 = true;
                      break;
                    }
                  }
                  if (blobInformation.FileType == FileTypes.ftAuthentical)
                  {
                    flag2 = true;
                    if (DateTime.Compare(t1, blobInformation.ModifyDate) > 0)
                    {
                      flag1 = true;
                      break;
                    }
                  }
                }
              }
            }
          }
          if (!flag2)
            flag1 = true;
        }
      }
      if (flag1)
      {
        if (eventArgs.InternalDocument)
          generateEventArgsList.Add(eventArgs);
        else
          collection.Add(eventArgs);
      }
    }
    bool flag = true;
    if (updateMode)
    {
      flag = false;
      if (!askForNotInternals)
      {
        flag = true;
      }
      else
      {
        List<Tuple<long, int, string>> objList = new List<Tuple<long, int, string>>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          for (int index = 0; index < collection.Count; ++index)
          {
            string str = (string) null;
            if (hybridDictionary.Contains((object) collection[index].ObjectId))
            {
              str = (string) hybridDictionary[(object) collection[index].ObjectId];
            }
            else
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(collection[index].ObjectId, false);
              if (dbObject != null)
                str = dbObject.Caption;
            }
            if (str != null)
            {
              Tuple<long, int, string> tuple = new Tuple<long, int, string>(collection[index].ObjectId, collection[index].ObjectType, str);
              objList.Add(tuple);
            }
          }
        }
        if (objList.Count > 0)
        {
          if (new AuthFilesAskListForm().ShowDialog(objList) != DialogResult.Yes)
            return false;
          flag = true;
        }
      }
    }
    if (flag)
      generateEventArgsList.AddRange((IEnumerable<AuthFileNeedGenerateEventArgs>) collection);
    OpenFileDialog openFileDialog = (OpenFileDialog) null;
    for (int index3 = 0; index3 < generateEventArgsList.Count; ++index3)
    {
      AuthFileNeedGenerateEventArgs generateEventArgs = generateEventArgsList[index3];
      AuthFileAssignEventArgs eventArgs = new AuthFileAssignEventArgs(generateEventArgs.ObjectType, generateEventArgs.ObjectId);
      this.FireEventAuthFileAssign(eventArgs);
      if (!eventArgs.IsHandled && !updateMode)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (openFileDialog == null)
          {
            List<string> possibleAuthFileTypes = this.GetPossibleAuthFileTypes();
            openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = ClientContext.FileVault.WorkArea.AreaPath;
            if (possibleAuthFileTypes != null && possibleAuthFileTypes.Count > 0)
            {
              string str1 = string.Empty;
              for (int index4 = 0; index4 < possibleAuthFileTypes.Count; ++index4)
              {
                if (index4 > 0)
                  str1 += ";";
                str1 = $"{str1}*{possibleAuthFileTypes[index4]}";
              }
              string str2 = string.Format(LocalizationHolder.rm.GetString("Client.AuthFiles_Filter"), (object) str1) + "|" + LocalizationHolder.rm.GetString("Client.Core_1306");
              openFileDialog.Filter = str2;
            }
            else
              openFileDialog.Filter = LocalizationHolder.rm.GetString("Client.Core_1306");
          }
          IDBObject dbObject = sessionKeeper.Session.GetObject(generateEventArgs.ObjectId);
          if (dbObject != null)
            openFileDialog.Title = $"Назначение аутентичных файлов вручную для \"{dbObject.Caption}\"";
          else
            continue;
        }
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          string[] fileNames = openFileDialog.FileNames;
          this.AssignFileWithFilenames(generateEventArgs.ObjectId, fileNames, new AuthFileReplaceEventHandler(AuthFilesService.ReplaceAuthFile));
          if (AuthFilesService.authReplaceAll == DialogResult.Cancel)
            break;
        }
      }
    }
    return true;
  }

  /// <summary>
  /// Сохранить аутентичные файлы в папку.
  /// Проверка на актуальность не производится.
  /// Для проверки на актуальность вызывать CheckAuthFiles
  /// </summary>
  /// <param name="items">Список объектов</param>
  /// <param name="folderPath">Папка для сохранения, должна существовать</param>
  /// <param name="onAuthFileNameResolve">При null возможны коллизии, если папка была не пуста</param>
  public void SaveAuthFiles(
    ISelectedItems items,
    string folderPath,
    AuthFileSaveNameResolveHandler onAuthFileNameResolve)
  {
    bool filenamesWhenSave1 = AuthFilesHolder.GetAddObjectVersionToAuthFilenamesWhenSave();
    IMSAttributeType imsAT = (IMSAttributeType) null;
    int filenamesWhenSave2 = AuthFilesHolder.GetSuffixAttributeForAuthFilenamesWhenSave(out imsAT);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index1 = 0; index1 < items.Count; ++index1)
      {
        IDBTypedObjectID itemData = items.GetItemData(index1, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
        if (dbObject != null)
        {
          string caption = dbObject.Caption;
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            ObjectVersionModes objectVersionModes = ObjectVersionModes.Abstract;
            int versionId = 0;
            if (filenamesWhenSave1)
            {
              objectVersionModes = sessionKeeper.Session.GetObjectType(dbObject.ObjectType).Versionable;
              versionId = dbObject.VersionID;
            }
            string suffix = string.Empty;
            if (filenamesWhenSave2 != 0)
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(filenamesWhenSave2);
              if (attributeById != null)
                suffix = attributeById.AsString;
            }
            using (new RemoteLock((object) attributeByGuid))
            {
              for (int index2 = 0; index2 < attributeByGuid.ValuesCount; ++index2)
              {
                attributeByGuid.Index = index2;
                if (attributeByGuid is IBlobReader blobReader)
                {
                  BlobInformation blobInformation = blobReader.OpenBlob(-1);
                  if (blobInformation.FileType == FileTypes.ftAuthentical)
                  {
                    string str1 = folderPath;
                    string[] strArray = blobInformation.FileName.Split(Path.DirectorySeparatorChar);
                    for (int index3 = 0; index3 < strArray.Length - 1; ++index3)
                    {
                      str1 = Path.Combine(str1, strArray[index3]);
                      if (!Directory.Exists(str1))
                        Directory.CreateDirectory(str1);
                    }
                    string str2 = strArray[strArray.Length - 1];
                    if (filenamesWhenSave1 && objectVersionModes == ObjectVersionModes.MultiVersion)
                      str2 = AuthFilesHolder.GetAuthFilenamesWithVersion(str2, versionId);
                    if (suffix != null && suffix != string.Empty)
                      str2 = AuthFilesHolder.GetAuthFilenamesWithSuffix(str2, suffix);
                    if (File.Exists(Path.Combine(str1, str2)) && onAuthFileNameResolve != null)
                    {
                      AuthFileSaveNameResolveArgs eventArgs = new AuthFileSaveNameResolveArgs(str2, str1, itemData.ObjectID, caption, blobInformation.BlobID);
                      onAuthFileNameResolve((object) this, eventArgs);
                      str2 = eventArgs.FileName;
                    }
                    string path = Path.Combine(str1, str2);
                    using (FileStream aDestStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                      new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
                    File.SetLastWriteTime(path, blobInformation.ModifyDate);
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  private static void ReplaceAuthFile(object sender, AuthFileReplaceEventArgs eventArgs)
  {
    if (AuthFilesService.authReplaceAll == DialogResult.OK)
    {
      eventArgs.WhatDo = DialogResult.OK;
    }
    else
    {
      DialogResult dialogResult = IMMessageBox.Show(MessageDialogs.msgQuery, string.Format(LocalizationHolder.rm.GetString(nameof (ReplaceAuthFile)), (object) eventArgs.AuthFile), new IMMessageBoxButton[4]
      {
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("ReplaceAuthFileYes"), DialogResult.Yes),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("ReplaceAuthFileYesForAll"), DialogResult.OK),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("ReplaceAuthFileNo"), DialogResult.No),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("ReplaceAuthFileCancel"), DialogResult.Cancel)
      }, IMMessageBoxImage.Question);
      AuthFilesService.authReplaceAll = dialogResult;
      eventArgs.WhatDo = dialogResult;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectId">идент. версии объекта</param>
  /// <param name="authFiles">список аутентичных файлов (если есть)</param>
  /// <returns></returns>
  public bool AssignFileWithFilenames(
    long objectId,
    string[] authFiles,
    AuthFileReplaceEventHandler onAuthFileReplace)
  {
    bool flag1 = false;
    int attributeID = 0;
    long objectID = 0;
    List<string> stringList1 = new List<string>((IEnumerable<string>) authFiles);
    List<string> stringList2 = new List<string>();
    List<bool> boolList = new List<bool>();
    List<string> stringList3 = new List<string>();
    List<int> intList = new List<int>();
    for (int index = 0; index < stringList1.Count; ++index)
    {
      stringList2.Add(Path.GetFileName(stringList1[index]));
      bool flag2 = PathUtils.IsPlacedIn(stringList1[index], ClientContext.FileVault.WorkArea.AreaPath);
      boolList.Add(flag2);
      stringList3.Add(flag2 ? PathUtils.GetRelativePath(stringList1[index], ClientContext.FileVault.WorkArea.AreaPath, RelativePathOptions.None) : stringList2[index]);
    }
    this.GetPossibleAuthFileTypes();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      if (dbObject == null)
        return false;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return false;
      objectID = objectId;
      attributeID = attributeByGuid.AttributeID;
      using (new RemoteLock((object) attributeByGuid))
      {
        for (int index1 = 0; index1 < attributeByGuid.ValuesCount; ++index1)
        {
          attributeByGuid.Index = index1;
          if (attributeByGuid is IBlobReader blobReader)
          {
            BlobInformation aBlobInformation = blobReader.OpenBlob(-1);
            if (aBlobInformation.FileType == FileTypes.ftAuthentical)
            {
              string fileName = aBlobInformation.FileName;
              bool flag3 = false;
              for (int index2 = 0; index2 < stringList1.Count; ++index2)
              {
                if (stringList3[index2].Equals(fileName, StringComparison.InvariantCultureIgnoreCase))
                {
                  flag3 = true;
                  intList.Add(index2);
                  break;
                }
              }
              if (flag3)
              {
                bool flag4 = true;
                if (onAuthFileReplace != null)
                {
                  AuthFileReplaceEventArgs eventArgs = new AuthFileReplaceEventArgs(objectId, stringList1[intList[intList.Count - 1]], DialogResult.Yes);
                  onAuthFileReplace((object) this, eventArgs);
                  if (eventArgs.WhatDo == DialogResult.Cancel)
                    return true;
                  flag4 = eventArgs.WhatDo == DialogResult.Yes || eventArgs.WhatDo == DialogResult.OK;
                }
                if (flag4)
                {
                  using (FileStream aSourceStream = new FileStream(stringList1[intList[intList.Count - 1]], FileMode.Open, FileAccess.Read))
                  {
                    aBlobInformation.ModifyDate = File.GetLastWriteTime(stringList1[intList[intList.Count - 1]]);
                    new BlobProcWriter(attributeByGuid, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
                    flag1 = true;
                  }
                }
              }
            }
          }
        }
        IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
        for (int index = 0; index < stringList1.Count; ++index)
        {
          if (intList.IndexOf(index) == -1)
          {
            customService.StartTransaction();
            try
            {
              int num = attributeByGuid.AddValue((object) FileTypes.ftAuthentical);
              attributeByGuid.Index = num;
              if (attributeByGuid is IBlobReader blobReader)
              {
                BlobInformation aBlobInformation = blobReader.OpenBlob(-1) with
                {
                  FileType = FileTypes.ftAuthentical,
                  FileName = stringList3[index],
                  ArcMethod = ArcMethods.ZLibPacked,
                  ModifyDate = File.GetLastWriteTime(stringList1[index])
                };
                using (FileStream aSourceStream = new FileStream(stringList1[index], FileMode.Open, FileAccess.Read))
                  new BlobProcWriter(attributeByGuid, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
              }
              customService.Commit();
              flag1 = true;
            }
            catch (Exception ex)
            {
              customService.Rollback();
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
        }
      }
    }
    if (flag1)
    {
      FileAttribute4ObjectChangedEventArgs e = new FileAttribute4ObjectChangedEventArgs(attributeID, objectID);
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) e);
    }
    return true;
  }
}
