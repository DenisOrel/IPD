// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.DocumentConfigSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

public static class DocumentConfigSerializer
{
  private static byte[] SaveDockConfigToByteArray(BlankConfig docConfig)
  {
    XDocument xdocument = new XDocument();
    XElement content = ApplicationServices.Container.GetService<TechCardDocumentConfigSerializeService>()?.Serialize((IDocumentConfigElement) docConfig);
    if (content != null)
      xdocument.Add((object) content);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      xdocument.Save((Stream) memoryStream);
      return memoryStream.ToArray();
    }
  }

  private static void UpdateChildElements(Rules config)
  {
    if (config.Template == null)
      return;
    List<VariantConfig> props = new List<VariantConfig>();
    TableData firstMainTable = config.Template.GetFirstPageTemplate().FindFirstMainTable();
    IEnumerable<TableData> tableDatas = firstMainTable != null ? firstMainTable.Nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (t => t is TableData)).Cast<TableData>() : (IEnumerable<TableData>) null;
    if (tableDatas != null)
    {
      foreach (TableData tableData in tableDatas)
      {
        if (config.Properties.FindElement(tableData.Id) is VariantConfig element)
          props.Add(element);
      }
    }
    config.Properties.SetChildList(props);
    for (int index = config.Properties.Elements.Count - 1; index >= 0; --index)
    {
      if (config.Template.FindNode(config.Properties.Elements[index].Id) == null)
        config.Properties.Elements.RemoveAt(index);
    }
  }

  public static void Save(Rules rules, long rulesObjectId, IUserSession session)
  {
    IDBAttributeType attributeType = session.GetAttributeType(BlankConsts.AttrFile.AttrFileID, false);
    if (attributeType == null)
      return;
    IDBObject dbObject = session.GetObject(rulesObjectId, true);
    if (dbObject == null)
      return;
    int attributeId1 = attributeType.AttributeID;
    int attributeId2 = session.IdentHelper.GetAttributeID(BlankConsts.Template.TemplateGuid);
    int attributeId3 = session.IdentHelper.GetAttributeID("cad00020-306c-11d8-b4e9-00304f19f545");
    int attributeId4 = MetaDataHelper.GetAttributeID((object) "cad0001f-306c-11d8-b4e9-00304f19f545");
    int attributeId5 = MetaDataHelper.GetAttributeID((object) new Guid("cad00021-306c-11d8-b4e9-00304f19f545"));
    List<AttributeValues> attributeValuesList = new List<AttributeValues>()
    {
      new AttributeValues(attributeId4, (object) rules.FullName),
      new AttributeValues(attributeId3, (object) rules.ShortName),
      new AttributeValues(attributeId5, (object) rules.BlankNote)
    };
    if (rules.Template.DBObjectID != -1L)
      attributeValuesList.Add(new AttributeValues(attributeId2, (object) rules.Template.DBObjectID));
    dbObject.SetAttributesValues(attributeValuesList.ToArray());
    DocumentConfigSerializer.UpdateChildElements(rules);
    IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(attributeId1, false);
    byte[] byteArray = DocumentConfigSerializer.SaveDockConfigToByteArray(rules.Properties);
    string fileName = dbAttribute.AsString;
    if (fileName == string.Empty)
      fileName = ServiceUtils.GetService<IFileNamesService>((object) dbObject.Session, true).GetUniqueFileName("BlankSetup.xml", dbObject.ID, dbObject.Session.SessionGUID);
    using (MemoryStream aSourceStream = new MemoryStream(byteArray))
    {
      try
      {
        aSourceStream.Position = 0L;
        BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, fileName, ArcMethods.ZLibPacked, string.Empty);
        new BlobProcWriter(dbObject.ObjectID, AttributableElements.Object, attributeId1, 0, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(dbObject.Session, false);
      }
      finally
      {
        aSourceStream.Close();
      }
    }
    if (!dbObject.IsCreationMode)
      return;
    dbObject.CommitCreation(true);
  }
}
