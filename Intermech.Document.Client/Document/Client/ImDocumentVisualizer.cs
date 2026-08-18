// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImDocumentVisualizer
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core.Visualizers;
using Intermech.Controls;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.Map;
using Intermech.Search.Interfaces.Signs;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Визуализатор документа Интермех для Show.NET</summary>
public class ImDocumentVisualizer : IVisualizer
{
  internal static void Initialize(System.IServiceProvider serviceProvider)
  {
    IVisualizerService visualizerService = (serviceProvider != null ? serviceProvider.GetService<IVisualizerService>(false) : (IVisualizerService) null) ?? ServicesManager.GetService<IVisualizerService>(false);
    if (visualizerService == null)
      return;
    ImDocumentVisualizer documentVisualizer = new ImDocumentVisualizer();
    for (int index = 0; index < ImDocumentData.ImDocumentFileExtensions.Count; ++index)
      visualizerService.AddVisualizer(ImDocumentData.ImDocumentFileExtensions[index], (IVisualizer) documentVisualizer);
    for (int index = 0; index < ImDocumentData.OldBlankExtensions.Count; ++index)
      visualizerService.AddVisualizer(ImDocumentData.OldBlankExtensions[index], (IVisualizer) documentVisualizer);
    for (int index = 0; index < ImDocumentData.OldImDocumentExtensions.Count; ++index)
      visualizerService.AddVisualizer(ImDocumentData.OldImDocumentExtensions[index], (IVisualizer) documentVisualizer);
    for (int index = 0; index < ImDocumentData.ImDocumentExternalFileExtensionsVisualizer.Count; ++index)
      visualizerService.AddVisualizer(ImDocumentData.ImDocumentExternalFileExtensionsVisualizer[index], (IVisualizer) documentVisualizer);
  }

  /// <summary>Создает один или несколько объектов для визуализации из представленных данных</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="valueIndex">Индекс файла в файловом атрибуте с множеством значений</param>
  /// <param name="fileName">Имя файла</param>
  /// <returns>Объект для просмотра</returns>
  public MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data)
  {
    bool flag = false;
    int num1 = -1;
    Guid objectGuid = Guid.Empty;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
        num1 = objectInfo.ObjectTypeID;
        objectGuid = objectInfo.VersionGuid;
        flag = MetaDataHelper.IsObjectTypeChildOf(num1, DocIDCache.ObjType_Specification);
      }
      if (flag && ServicesManager.GetService(typeof (IAVSClientService)) is IAVSClientService service)
      {
        List<string> reasonList;
        if (service.SpecificationIsNeedUpdate(objectId, num1, objectGuid, out reasonList))
        {
          string str1 = "Спецификация может не соответствовать изделию!";
          string str2 = "Открыть спецификацию в редакторе AVS, чтобы обновить?*";
          string str3 = "Будет показана последняя сохраненная версия.";
          string reasonMessage;
          if (!DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(LaunchType.Edit, objectId, out reasonMessage).Item1)
          {
            string str4 = "Невозможно обновить спецификацию.\r\nПричина: " + reasonMessage;
            int num2 = (int) IMMessageBox.Show("AVS", $"{str1}\r\n{str4}\r\n{str3}\r\n\r\n\r\nСписок несоответствий:", MessageBoxButtons.OK, (IList<string>) reasonList);
          }
          else
          {
            string str5 = "* Иначе " + str3.ToLower();
            if (IMMessageBox.Show("AVS", $"{str1}\r\n\r\n{str2}\r\n{str5}\r\n\r\n\r\nСписок несоответствий:", MessageBoxButtons.YesNo, (IList<string>) reasonList) == DialogResult.Yes)
            {
              service.EditAVSDocument(objectId, num1, true, false);
              data = (byte[]) null;
            }
          }
        }
        if (service.GetViewDocument(objectId, num1) is ImDocument viewDocument)
          return (MapObject) new ImDocumentShowObject(viewDocument);
      }
      Guid documentComplect = DocumentEditorPlugin.GetContextDocumentComplect(objectId);
      if (data != null)
      {
        if (!(ImDocument.LoadFromStream((Stream) new MemoryStream(data), "", out DocumentFileType _, true, true, true) is ImDocument imDocument))
          throw new Exception(LocalizationHolder.rm.GetString("Document.Model_168"));
        CheckSumService serv = new CheckSumService();
        if (serv.CanSetChecksum() && !imDocument.IsTemplate)
        {
          Stream stream = (Stream) new MemoryStream(data);
          DocumentEditorPlugin.Instance.UpdateCheckSum((IUserSession) null, serv, (ImDocumentData) imDocument, stream, true);
        }
        if (objectId != 0L && objectId != -1L)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
            if (dbObject != null)
              DocumentEditorPlugin.Instance.SetDocumentDBObject((ImDocumentData) imDocument, dbObject);
          }
        }
        if (imDocument.LoadFromStreamThread != null && imDocument.LoadFromStreamThread.IsAlive)
          imDocument.LoadFromStreamThread.Priority = ThreadPriority.Lowest;
        imDocument.DocumentComplectObjectGuid = documentComplect;
        DocumentEditorPlugin.UpdateDocumentDBObject(imDocument, objectId, true, true);
        return (MapObject) new ImDocumentShowObject(imDocument);
      }
      if (objectId != -1L)
      {
        ImDocument document = (ImDocument) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject docObject = sessionKeeper.Session.GetObject(objectId);
          if (docObject != null)
            document = DocumentEditorPlugin.LoadDocumentFromDBObject(docObject, valueIndex, documentComplect, true, false, true, true) as ImDocument;
        }
        if (document != null && document.LoadFromStreamThread != null && document.LoadFromStreamThread.IsAlive)
          document.LoadFromStreamThread.Priority = ThreadPriority.Lowest;
        return (MapObject) new ImDocumentShowObject(document);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return (MapObject) null;
  }
}
