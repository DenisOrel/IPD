// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ObjectNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ObjectNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid objectGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, objectGuid, (IPropertyFactory) new ObjectPropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    IDBObject attributable = this.session.GetObject(this.GUID, false);
    bool isNew = false;
    Guid propertyValue1 = this.propertyFactory.GetPropertyValue<Guid>("F_GUID");
    categoryID = 2;
    if (attributable == null)
    {
      IDBObjectCollection objectCollection = this.session.GetObjectCollection(this.session.GetObjectType(new Guid(this.rootNode.Attributes["Tag"].Value), true).ObjectType);
      long propertyValue2 = this.propertyFactory.GetPropertyValue<long>("F_PARENT_ID", 0L);
      attributable = propertyValue2 != 0L ? objectCollection.CreateVersion(propertyValue2) : objectCollection.Create();
      isNew = true;
    }
    ObjectAttributesWriter attributesWriter = new ObjectAttributesWriter();
    IDBObject dbObject1 = attributesWriter.CheckOut(attributable);
    if (dbObject1 == null)
    {
      id = (object) Math.Abs(attributable.ObjectID);
    }
    else
    {
      IDBObject dbObject2 = dbObject1;
      if (!dbObject2.GUID.Equals(propertyValue1))
        dbObject2.GUID = propertyValue1;
      if (!dbObject2.ObjectGUID.Equals(this.GUID))
        dbObject2.ObjectGUID = this.GUID;
      if (isNew)
      {
        dbObject2.OwnerID = this.propertyFactory.GetPropertyValue<long>("F_OWNER_ID", dbObject2.OwnerID);
        dbObject2.ProjectID = this.propertyFactory.GetPropertyValue<long>("F_PROJECT_ID", dbObject2.ProjectID);
        dbObject2.Caption = this.propertyFactory.GetPropertyValue<string>("CAPTION", dbObject2.Caption);
      }
      else
      {
        dbObject2.ObjectType = this.propertyFactory.GetObligatoryPropertyValue<int>("F_OBJECT_TYPE", dbObject2.ObjectType);
        dbObject2.OwnerID = this.propertyFactory.GetObligatoryPropertyValue<long>("F_OWNER_ID", dbObject2.OwnerID);
        dbObject2.ProjectID = this.propertyFactory.GetObligatoryPropertyValue<long>("F_PROJECT_ID", dbObject2.ProjectID);
        dbObject2.Caption = this.propertyFactory.GetObligatoryPropertyValue<string>("CAPTION", dbObject2.Caption);
      }
      ObjectPropertyFactory propertyFactory = (ObjectPropertyFactory) this.propertyFactory;
      attributesWriter.WriteAttributes(this.session, dbObject2, propertyFactory.AttributeNodes, (IPropertyFactory) propertyFactory, isNew);
      this.SetRelations(this.session, propertyFactory.RelationFactories);
      if (isNew)
        dbObject2.CommitCreation(true);
      else
        attributesWriter.CheckIn(dbObject2);
      dbObject2.LCStep = this.propertyFactory.GetObligatoryPropertyValue<int>("F_LC_STEP", dbObject2.LCStep);
      ((ICustomImport) ServerServices.GetService(typeof (ICustomImport))).FireCustomImported((object) this, new CustomImportedEventArgs(this.session, 1, (object) dbObject2));
      id = (object) Math.Abs(dbObject2.ObjectID);
      if (!isNew)
        return;
      this.SetAccess(dbObject2 as IDBSecurity, propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", (List<UpdateScriptAccessRight>) null), 1, (long) id);
    }
  }

  private void SetRelations(IUserSession session, List<RelationPropertyFactory> relationFactories)
  {
    session.GetRelationsApplicabilityCollection();
    IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
    RelationAttributesWriter attributesWriter = new RelationAttributesWriter();
    foreach (RelationPropertyFactory relationFactory in relationFactories)
    {
      relationCollection.RelationTypeID = relationFactory.GetPropertyValue<int>("F_RELATION_TYPE");
      NewRelationProperties properties = new NewRelationProperties(relationFactory.GetPropertyValue<CalculateObjectIDValue>("F_PROJ_ID").Value, relationFactory.GetPropertyValue<CalculateIDValue>("F_PART_ID").Value);
      DateTime propertyValue = relationFactory.GetPropertyValue<DateTime>("F_CREATE_DATE", DateTime.MinValue);
      if (propertyValue != DateTime.MinValue)
        properties.BeginDate = propertyValue;
      properties.RelationGUID = relationFactory.RelationGuid;
      IDBRelation relation = session.GetRelation(properties.ProjectObjectID, properties.PartID, false);
      bool isNew = false;
      if (relation == null)
      {
        relation = relationCollection.Create(properties);
        isNew = true;
      }
      else
      {
        if (!relation.GUID.Equals(properties.RelationGUID))
          relation.GUID = properties.RelationGUID;
        relation.CreateDate = relationFactory.GetObligatoryPropertyValue<DateTime>("F_CREATE_DATE", relation.CreateDate);
      }
      attributesWriter.WriteAttributes(session, relation, relationFactory.AttributeNodes, (IPropertyFactory) relationFactory, isNew);
      foreach (ObligatoryElementKey obligatoryElement in relationFactory.ObligatoryElements)
        this.obligatoryObjects.RegisterObligatoryObjectElement(5, (object) relation.GUID, obligatoryElement);
    }
  }
}
