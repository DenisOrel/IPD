// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesDatabase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.DataExchange;

public class CaptureChangesDatabase : EntityDatabase
{
  public CaptureChangesDatabase()
    : base((IEntityIndexer) new SectionIndexer())
  {
  }

  public SectionEntity CreateReferencedDBObject(long objectId, int objectType = -1)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (objectType == -1)
      objectType = DBHelper.GetObjectType(objectId);
    ObjectSection sectionObject1 = new ObjectSection();
    sectionObject1.ObjectId = objectId;
    sectionObject1.ObjectType = objectType;
    sectionObject1.ExistenceStatus = ObjectExistenceStatus.ExistingObject;
    DisplaySection sectionObject2 = new DisplaySection()
    {
      DisplayName = $"#{objectId}"
    };
    sectionObject2.QualifiedName = string.Format(LocalizationHolder.rm.GetString("Tools.Components_503"), (object) sectionObject2.DisplayName);
    ObjectActionsSection sectionObject3 = new ObjectActionsSection();
    SectionEntity referencedDbObject = new SectionEntity();
    referencedDbObject.Sections.Set((object) sectionObject1);
    referencedDbObject.Sections.Set((object) sectionObject2);
    referencedDbObject.Sections.Set((object) sectionObject3);
    return referencedDbObject;
  }

  public SectionEntity AddReferencedDBObject(long objectId, int objectType = -1)
  {
    SectionEntity referencedDbObject = this.CreateReferencedDBObject(objectId, objectType);
    this.Add((IEntity) referencedDbObject);
    return referencedDbObject;
  }

  public SectionEntity CreateDocument(string masterFilePath, long objectId = 0)
  {
    if (string.IsNullOrEmpty(masterFilePath))
      throw new ArgumentException();
    FilesSection sectionObject1 = new FilesSection();
    sectionObject1.MasterFile = masterFilePath;
    DisplaySection sectionObject2 = new DisplaySection()
    {
      DisplayName = Path.GetFileName(sectionObject1.MasterFile)
    };
    sectionObject2.QualifiedName = string.Format(LocalizationHolder.rm.GetString("Tools.Components_503"), (object) sectionObject2.DisplayName);
    ObjectSection sectionObject3 = new ObjectSection();
    sectionObject3.ExistenceStatus = objectId == 0L ? ObjectExistenceStatus.NewObject : ObjectExistenceStatus.ExistingObject;
    if (sectionObject3.NewObject)
    {
      sectionObject3.ObjectId = 0L;
      sectionObject3.ObjectType = -1;
    }
    else
    {
      sectionObject3.ObjectId = objectId;
      sectionObject3.ObjectType = DBHelper.GetObjectType(objectId);
    }
    ObjectActionsSection sectionObject4 = new ObjectActionsSection();
    SectionEntity document = new SectionEntity();
    document.Sections.Set((object) sectionObject1);
    document.Sections.Set((object) sectionObject2);
    document.Sections.Set((object) sectionObject3);
    document.Sections.Set((object) sectionObject4);
    return document;
  }

  public SectionEntity AddDocument(string masterFilePath, long objectId = 0)
  {
    SectionEntity document = this.CreateDocument(masterFilePath, objectId);
    this.Add((IEntity) document);
    return document;
  }

  public IEnumerable<SectionEntity> GetRootDocuments()
  {
    return (IEnumerable<SectionEntity>) new SectionEntityEnumAdapter(RootItemSection.GetRootItems(this));
  }

  public SectionEntity GetEntryPointDocument(bool throwIfNotFound)
  {
    SectionEntity entryPoint = RootItemSection.GetEntryPoint(this);
    return entryPoint != null || !throwIfNotFound ? entryPoint : throw new Exception("Не удалось определить документ, с которого началось сохранение изменений.");
  }

  public bool IsEntryPointDocument(SectionEntity document)
  {
    return document != null ? RootItemSection.IsEntryPoint(document) : throw new ArgumentNullException(nameof (document));
  }

  public SectionEntity QueryFirst(IQueryCondition condition)
  {
    return (SectionEntity) this.Query(new EntityQuery(1), condition).TryGetFirstEntity();
  }
}
