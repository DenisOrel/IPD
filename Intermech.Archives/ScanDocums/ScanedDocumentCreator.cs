// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.ScanedDocumentCreator
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.IO;

#nullable disable
namespace Intermech.Archives.ScanDocums;

/// <summary>Создатель сканированых документов</summary>
internal class ScanedDocumentCreator
{
  /// <summary>Идентификатор текущего объекта</summary>
  private long ipsCurentObjectId;
  /// <summary>Формат файла</summary>
  private string extFileImgFormat = string.Empty;
  /// <summary>идентификатор типа объекта Документ</summary>
  private int documentTypeId = -1;
  /// <summary>идентификатор тарибута типа "Файл"</summary>
  private int dataAttrTypeID;
  /// <summary>
  /// Предыдущий тип объекта выбраный пользователем в диалоге создания объекта
  /// </summary>
  public static int lastSelectedobjecttypeId = -1;

  /// <summary>
  /// Конструктор.
  /// Создатель сканированых документов
  /// </summary>
  public ScanedDocumentCreator()
  {
    this.dataAttrTypeID = MetaDataHelper.GetAttributeType(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")).AttributeID;
    this.documentTypeId = MetaDataHelper.GetObjectType(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).ObjectTypeID;
  }

  /// <summary>Деструктор</summary>
  ~ScanedDocumentCreator()
  {
  }

  /// <summary>Создать документ</summary>
  public void CreateDocument()
  {
    IObjectCreatorService service1 = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    int num = ScanedDocumentCreator.lastSelectedobjecttypeId;
    if (num == -1)
      num = this.documentTypeId;
    int selectedID = num;
    long objectByTypeDialog = service1.CreateObjectByTypeDialog((int[]) null, selectedID);
    switch (objectByTypeDialog)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        this.ClearDocumentInfo();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this.ipsCurentObjectId = objectByTypeDialog;
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ipsCurentObjectId, true);
          if (dbObject == null)
            break;
          int typeId = dbObject.TypeID;
          ScanedDocumentCreator.lastSelectedobjecttypeId = typeId;
          this.extFileImgFormat = this.GetTypeSettings(sessionKeeper.Session, typeId).DocumentFileExt;
          ScanerDocumentService service2 = ServicesManager.GetService(typeof (IScanerDocumentService)) as ScanerDocumentService;
          service2.OnImageTransfer += new EventHandler(this.scanerService_OnImageTransfer);
          service2.OnEndScaning += new EventHandler(this.scanerService_OnEndScaning);
          service2.AcquireDoc(this.extFileImgFormat);
        }
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog);
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) e);
        break;
    }
  }

  /// <summary>Вызвать диалог выбора сканирующего устройства</summary>
  public void SelectDevice()
  {
    (ServicesManager.GetService(typeof (IScanerDocumentService)) as ScanerDocumentService).SelectDevice();
  }

  /// <summary>Завершение сканирования</summary>
  private void EndScaning()
  {
    ScanerDocumentService service = ServicesManager.GetService(typeof (IScanerDocumentService)) as ScanerDocumentService;
    service.OnImageTransfer -= new EventHandler(this.scanerService_OnImageTransfer);
    service.OnEndScaning -= new EventHandler(this.scanerService_OnEndScaning);
    this.ClearDocumentInfo();
  }

  /// <summary>Обработка события завершения сканирования</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void scanerService_OnEndScaning(object sender, EventArgs e) => this.EndScaning();

  /// <summary>Очистить информация о документе</summary>
  private void ClearDocumentInfo()
  {
    this.ipsCurentObjectId = 0L;
    this.extFileImgFormat = string.Empty;
  }

  /// <summary>Обработка события передачи данных</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void scanerService_OnImageTransfer(object sender, EventArgs e)
  {
    if (sender == null)
      return;
    this.AddFile2Object(sender as byte[]);
  }

  /// <summary>добавить блоб</summary>
  /// <param name="blobBytes">поток байт</param>
  private void AddFile2Object(byte[] blobBytes)
  {
    if (this.ipsCurentObjectId == 0L || blobBytes == null)
      return;
    using (MemoryStream aSourceStream = new MemoryStream(blobBytes))
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = (ClientCommons.GetAttributable(this.ipsCurentObjectId, AttributableElements.Object, sessionKeeper.Session) ?? throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_91"), (object) this.ipsCurentObjectId))).GetAttributeByID(this.dataAttrTypeID);
          if (attributeById == null)
            throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_92"), (object) this.dataAttrTypeID, (object) this.ipsCurentObjectId));
          int aIndex = 0;
          if (!attributeById.IsNull)
          {
            aIndex = attributeById.AddValue((object) null);
            attributeById.Index = aIndex;
          }
          string fileName = $"img{this.ipsCurentObjectId}_{aIndex}{this.extFileImgFormat}";
          aSourceStream.Position = 0L;
          BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, fileName, ArcMethods.NotPacked, string.Empty);
          new BlobProcWriter(this.ipsCurentObjectId, AttributableElements.Object, this.dataAttrTypeID, aIndex, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        }
      }
      finally
      {
        aSourceStream.Close();
      }
    }
  }

  /// <summary>Получить информацию о типе</summary>
  /// <param name="session"></param>
  /// <param name="ipsDocumetObjectTypeId"></param>
  private DocumentTypeSettings GetTypeSettings(IUserSession session, int ipsDocumetObjectTypeId)
  {
    return (session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(session.SessionGUID, ipsDocumetObjectTypeId);
  }
}
