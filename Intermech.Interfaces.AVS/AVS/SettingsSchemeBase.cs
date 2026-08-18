// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SettingsSchemeBase
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Document.DBCore;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.AVS;

public class SettingsSchemeBase
{
  /// <summary> Сохранение параметров в объект с guid-ом = OwnerGuid </summary>
  protected virtual void SaveParamsDataToObjectAttribute(long ownerObjectId, int attributeId)
  {
    if (ownerObjectId.IsUndefinedId())
      return;
    long aElementID = ownerObjectId;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectActual(ownerObjectId, true);
      if (dbObject != null)
      {
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == 0L)
        {
          dbObject = dbObject.CheckOut();
          flag = true;
        }
        if (dbObject.GetAttributeByID(attributeId) == null)
          dbObject.Attributes.AddAttribute(attributeId, false);
        aElementID = dbObject.ObjectID;
      }
      MemoryStream memoryStream = new MemoryStream();
      try
      {
        this.SaveToXmlDocument(memoryStream);
        memoryStream.Position = 0L;
        BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
        new BlobProcWriter(aElementID, AttributableElements.Object, attributeId, 0, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        memoryStream.Position = 0L;
      }
      finally
      {
        memoryStream.Close();
        if (flag)
          dbObject.CheckIn();
      }
    }
  }

  protected virtual void SaveToXmlDocument(MemoryStream stream)
  {
  }

  protected static SettingsStructure GetSettingsStructure(
    QuickObjectInfo objInfo,
    int holderObjType)
  {
    AVSDocumentTypeSettings settingsForTemplate = AVSDocumentsSettings.Instance.GetDocumentTypeSettingsForTemplate(objInfo.VersionGuid, out InheritanceSettingsLevel _);
    SettingsStructure settingsStructure;
    if (settingsForTemplate != null)
    {
      settingsStructure = settingsForTemplate.SettingsInheritanceStructure;
    }
    else
    {
      AVSDocumentTypeSettings typeForDbObjectType = AVSDocumentsSettings.Instance.GetDefaultDocumentTypeForDBObjectType(holderObjType, AVSDocumentType.Specification);
      settingsStructure = typeForDbObjectType == null ? (SettingsStructure) new UserAVSDocumentSettingsStructure() : typeForDbObjectType.SettingsInheritanceStructure;
    }
    return settingsStructure;
  }
}
