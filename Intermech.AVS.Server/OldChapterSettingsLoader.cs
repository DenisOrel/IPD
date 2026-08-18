// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Server.OldChapterSettingsLoader
// Assembly: Intermech.AVS.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DD9587A9-B8FC-4A8A-AB7E-E4D2C61BABE8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AVS.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS.Server;

internal class OldChapterSettingsLoader : IWriteReadXml
{
  private List<AdditionalChapterSettings> AdditionalChapters { get; } = new List<AdditionalChapterSettings>();

  public static void CopyOldSettingsToDBObjects(IUserSession session)
  {
    if (OldChapterSettingsLoader.IsChapterDbObjectExists())
      return;
    List<AdditionalChapterSettings> additionalChapters = OldChapterSettingsLoader.LoadAdditionalChapterSettings(session, new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"));
    if (additionalChapters.Count <= 0)
      return;
    OldChapterSettingsLoader.SaveChapterSettingsToDbObjects(session, additionalChapters);
  }

  private static bool IsChapterDbObjectExists()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjTypeChapterGuid).RecordsExists((ConditionStructure[]) null);
  }

  private static void SaveChapterSettingsToDbObjects(
    IUserSession session,
    List<AdditionalChapterSettings> additionalChapters)
  {
    if (additionalChapters == null)
      throw new ArgumentNullException(nameof (additionalChapters));
    IDBObjectCollection objectCollection = session.GetObjectCollection(AvsIDCache.ObjTypeChapterGuid);
    foreach (AdditionalChapterSettings additionalChapter in additionalChapters)
      OldChapterSettingsLoader.SaveChapterSettingsToDbObject(session, additionalChapter, objectCollection);
  }

  private static void SaveChapterSettingsToDbObject(
    IUserSession session,
    AdditionalChapterSettings chapterSettings,
    IDBObjectCollection objectCollection)
  {
    if (session.GetObject(chapterSettings.ChapterGuid, false) != null)
      return;
    IDBObject dbObject = objectCollection.Create();
    dbObject.SetAttributesValues(new AttributeValues[2]
    {
      new AttributeValues(-12, (object) chapterSettings.ChapterGuid),
      new AttributeValues(session.IdentHelper.NameID, (object) chapterSettings.Caption)
    });
    if (dbObject.IsCreationMode)
      dbObject.CommitCreation(true, true);
    chapterSettings.ChapterID = dbObject.ObjectID;
  }

  private static List<AdditionalChapterSettings> LoadAdditionalChapterSettings(
    IUserSession session,
    Guid ownerObjectGuid)
  {
    OldChapterSettingsLoader rootObject = new OldChapterSettingsLoader();
    using (MemoryStream aDestStream = new MemoryStream())
    {
      IDBObject dbObject = session.GetObject(ownerObjectGuid, false);
      if (dbObject != null)
      {
        IDBAttributeType attributeType = session.GetAttributeType(AvsIDCache.AttrConstructorDocumentPropertiesGuid, false);
        if (attributeType == null)
          return rootObject.AdditionalChapters;
        int attributeId = attributeType.AttributeID;
        IDBAttribute attributeById = dbObject.GetAttributeByID(attributeId);
        if (attributeById != null)
        {
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
            WriteReadXmlHelper.LoadFromXmlDocument(session, (Stream) aDestStream, (IWriteReadXml) rootObject, "AVSCommonPropertiesSchema");
        }
      }
    }
    return rootObject.AdditionalChapters;
  }

  private static ColumnDescriptor[] CreateColumnDescriptors()
  {
    return new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
  }

  bool IWriteReadXml.ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (!(readArgs.Reader.LocalName == "AdditionalChapters"))
      return false;
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    WriteReadXmlHelper.ReadListFromXml((IList) this.AdditionalChapters, typeof (AdditionalChapterSettings), readArgs);
    return true;
  }

  void IWriteReadXml.WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      if (this.AdditionalChapters == null)
        return;
      WriteReadXmlHelper.WriteListToXml("AdditionalChapters", (IList) this.AdditionalChapters, "Chapter", xw, objectRefId);
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  void IWriteReadXml.ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }
}
