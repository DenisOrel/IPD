// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.DocumentConfigLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.IO;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

public static class DocumentConfigLoader
{
  private static ImDocumentData LoadDocTemplate(long templateObjId, IUserSession session)
  {
    return DocumentEditorPluginBase.LoadDocumentFromDBObject(session, templateObjId);
  }

  [CanBeNull]
  private static BlankConfig LoadDocConfig(IDBObject dbObject, IUserSession session)
  {
    IDBAttribute attributeById = dbObject.GetAttributeByID(BlankConsts.AttrFile.AttrFileID);
    if (attributeById == null)
      return (BlankConfig) null;
    using (MemoryStream aDestStream = new MemoryStream())
    {
      BlobProcReader blobProcReader = new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
      blobProcReader.ReadData(session);
      if (blobProcReader.Result)
      {
        if (aDestStream.Length != 0L)
        {
          aDestStream.Position = 0L;
          XDocument xdocument = XDocument.Load((Stream) aDestStream);
          if (ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>()?.Load(xdocument.Root) is BlankConfig blankConfig1)
            return blankConfig1;
          BlankConfig blankConfig2 = new BlankConfig();
          blankConfig2.Load(xdocument.Root);
          return blankConfig2;
        }
      }
    }
    return (BlankConfig) null;
  }

  [CanBeNull]
  public static Rules Load(long configObjId, IUserSession session)
  {
    IDBObject dbObject = session.GetObject(configObjId, false);
    if (dbObject == null)
      return (Rules) null;
    if (dbObject.ObjectType != BlankConsts.ObjectType.BlankSetupId)
      throw new ObjectNotFoundException(configObjId);
    Rules rules = new Rules();
    rules.ObjectId = configObjId;
    rules.ShortName = Convert.ToString(dbObject.Attributes.FindByGUID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"))?.Value);
    rules.FullName = Convert.ToString(dbObject.Attributes.FindByGUID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"))?.Value);
    rules.BlankNote = Convert.ToString(dbObject.Attributes.FindByGUID(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"))?.Value);
    IDBAttribute byId1 = dbObject.Attributes.FindByID(BlankConsts.Template.TemplateID);
    if (byId1 == null || byId1.IsNull)
      return rules;
    long asInteger1 = byId1.AsInteger;
    if (asInteger1 != 0L)
      rules.Template = DocumentConfigLoader.LoadDocTemplate(asInteger1, session);
    BlankConfig blankConfig = DocumentConfigLoader.LoadDocConfig(dbObject, session);
    if (blankConfig != null)
      rules.Properties = blankConfig;
    IDBAttribute byGuid = dbObject.Attributes.FindByGUID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    rules.Properties.DocumentName = byGuid == null || byGuid.AsString == string.Empty ? dbObject.Caption : byGuid.AsString;
    IDBAttribute byId2 = dbObject.Attributes.FindByID(BlankConsts.GroupDocument.GroupDocumentID);
    if (byId2 != null && !byId2.IsNull)
    {
      long asInteger2 = byId2.AsInteger;
    }
    return rules;
  }
}
