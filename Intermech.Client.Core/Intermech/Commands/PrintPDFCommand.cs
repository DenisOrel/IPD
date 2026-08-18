
// Type: Intermech.Commands.PrintPDFCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;


namespace Intermech.Commands;

/// <summary>Команда вызова центра печати pdf-документов</summary>
internal class PrintPDFCommand : SelectedItemsCommand
{
  private IAuthFilesService authFilesService;
  private IExceptionHandlerService exceptionHandlerService;
  private IFileVault fileVault;
  private IPdfPrintCenterService pdfPrintCenterService;

  public PrintPDFCommand(
    IAuthFilesService authFilesService,
    IExceptionHandlerService exceptionHandlerService,
    IFileVault fileVault,
    IPdfPrintCenterService pdfPrintCenterService)
    : base("PrintPDF")
  {
    if (authFilesService == null)
      throw new ArgumentNullException(nameof (authFilesService));
    if (exceptionHandlerService == null)
      throw new ArgumentNullException(nameof (exceptionHandlerService));
    if (fileVault == null)
      throw new ArgumentNullException(nameof (fileVault));
    if (pdfPrintCenterService == null)
      throw new ArgumentNullException(nameof (pdfPrintCenterService));
    this.authFilesService = authFilesService;
    this.exceptionHandlerService = exceptionHandlerService;
    this.fileVault = fileVault;
    this.pdfPrintCenterService = pdfPrintCenterService;
  }

  protected override void DoExecute()
  {
    ISelectedItems items = this.Items;
    if (!this.authFilesService.CheckAuthFiles(items, true))
      return;
    List<IDBObjectID> objectIds = this.GetObjectIDs(items);
    List<IPDMDocumentInfo> pdfs = this.GetPdfs(objectIds);
    List<string> objectsWithoutPdf = this.GetObjectsWithoutPdf(objectIds.Select<IDBObjectID, string>((Func<IDBObjectID, string>) (item => item.Caption)), pdfs.Select<IPDMDocumentInfo, string>((Func<IPDMDocumentInfo, string>) (item => item.ObjectName)));
    if (pdfs.Any<IPDMDocumentInfo>())
    {
      try
      {
        this.pdfPrintCenterService.AddFilesToPrintCenter(pdfs);
      }
      catch (Exception ex)
      {
        this.exceptionHandlerService.ShowException(ex);
      }
    }
    if (!objectsWithoutPdf.Any<string>())
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("В следующих выбранных объектах отсутствуют pdf-файлы: ");
    stringBuilder.Append(string.Join(", ", objectsWithoutPdf.Select<string, string>((Func<string, string>) (item => $"\"{item}\""))));
    stringBuilder.Append(".");
    int num = (int) MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
  }

  /// <summary>
  /// Получает id объектов типа IDBObjectID из коллекции <paramref name="selectedItems" />
  /// </summary>
  private List<IDBObjectID> GetObjectIDs(ISelectedItems selectedItems)
  {
    List<IDBObjectID> objectIds = new List<IDBObjectID>();
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (selectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
        objectIds.Add(itemData);
    }
    return objectIds;
  }

  /// <summary>
  /// Возвращает коллекцию из имён pdf-документов, содержащихся в <paramref name="objectID" />
  /// </summary>
  /// <param name="objectID">Id объекта</param>
  private List<string> GetObjectPdfs(IDBObjectID objectID)
  {
    return this.fileVault.ViewArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForSingleObject(objectID.Value)).ObjectFiles.Where<PublishedFile>((Func<PublishedFile, bool>) (file => PathUtils.IsSamePath(Path.GetExtension(file.FullName), ".pdf"))).ToList<PublishedFile>().Select<PublishedFile, string>((Func<PublishedFile, string>) (file => file.FullName)).ToList<string>();
  }

  /// <summary>
  /// Возвращает список имён объектов, в которых не содержатся pdf-документы
  /// </summary>
  /// <param name="objects">Исходный список объектов</param>
  /// <param name="objectsWithPdf">Список объектов, содержащих pdf-документы</param>
  private List<string> GetObjectsWithoutPdf(
    IEnumerable<string> objects,
    IEnumerable<string> objectsWithPdf)
  {
    return objects.Except<string>(objectsWithPdf).ToList<string>();
  }

  /// <summary>
  /// Возвращает коллекцию с информацией о pdf-документах, содержащихся в списке объектов <paramref name="objectIDs" />
  /// </summary>
  /// <param name="objectIDs">Список id объектов</param>
  private List<IPDMDocumentInfo> GetPdfs(List<IDBObjectID> objectIDs)
  {
    List<IPDMDocumentInfo> pdfs = new List<IPDMDocumentInfo>();
    List<string> stringList = new List<string>();
    foreach (IDBObjectID objectId in objectIDs)
    {
      List<string> objectPdfs = this.GetObjectPdfs(objectId);
      if (objectPdfs.Any<string>())
      {
        IPDMDocumentInfo documentInfo = this.pdfPrintCenterService.CreateDocumentInfo(objectId.Caption, objectPdfs);
        pdfs.Add(documentInfo);
      }
    }
    return pdfs;
  }
}
