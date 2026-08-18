// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.RelationImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interface;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class RelationImporter(ImportArgs args) : UnitImporter(args)
{
  public override ImportedInfo Import()
  {
    XmlNode rootNode = XmlHelper.ReadMainFile(this.args.Unit, this.args.Path);
    ISitesCacheService customService = (ISitesCacheService) this.args.Session.GetCustomService(typeof (ISitesCacheService));
    RelationInfo relationAttributes = AttributesFile.GetRelationAttributes(rootNode);
    try
    {
      IDBRelationType dbRelationType = (IDBRelationType) null;
      if (GuidHelper.IsGuid(relationAttributes.RelationTypeGuid.ToString()))
        dbRelationType = this.args.Session.GetRelationType(relationAttributes.RelationTypeGuid, false);
      if (dbRelationType == null && relationAttributes.RelationTypeName != null && relationAttributes.RelationTypeName != string.Empty)
        dbRelationType = this.args.Session.GetRelationType(relationAttributes.RelationTypeName, false);
      if (dbRelationType == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1107"), (object) relationAttributes.RelationTypeGuid, (object) relationAttributes.RelationTypeName));
      RelationRecord relationRecord = new RelationRecord()
      {
        RelationType = dbRelationType.RelationType,
        PrjLinkGuid = (object) relationAttributes.Guid,
        CreateDate = (object) (relationAttributes.CreateDate == DateTime.MinValue ? DateTime.UtcNow : relationAttributes.CreateDate)
      };
      ImportedInfo iInfoProject = this.FindObject(relationAttributes.ProjectGuid);
      relationRecord.ProjId = iInfoProject != null ? (object) iInfoProject.ObjectId : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1108"), (object) relationAttributes.ProjectGuid));
      if (iInfoProject.Id < 0L && iInfoProject is DocImportedInfo && dbRelationType.RelationType == this.args.Session.IdentHelper.DocRelationTypeID)
        return new ImportedInfo(relationAttributes.Guid, 0L, 0L, this.args.Unit.Category, false);
      if (iInfoProject.Id < 0L)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1108"), (object) relationAttributes.ProjectGuid));
      ImportedInfo part = this.FindObject(relationAttributes.PartGuid);
      relationRecord.PartId = part != null ? (object) part.Id : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1109"), (object) relationAttributes.PartGuid, (object) relationAttributes.Guid));
      if (part.Id < 0L && part is DocImportedInfo docImportedInfo)
      {
        part = this.FindObject(docImportedInfo.DocumentGuid);
        if (part == null)
          return new ImportedInfo(relationAttributes.Guid, 0L, 0L, this.args.Unit.Category, false);
        relationRecord.PartId = (object) part.Id;
        if (relationRecord.RelationType == this.args.Session.IdentHelper.SPRelationTypeID)
          relationRecord.RelationType = this.args.Session.IdentHelper.DocRelationTypeID;
      }
      relationRecord.CreatorID = 0L;
      if (relationAttributes.CreatorGuid != Guid.Empty)
      {
        ImportedInfo importedInfo = this.FindObject(relationAttributes.CreatorGuid);
        if (importedInfo != null)
          relationRecord.CreatorID = importedInfo.ObjectId;
      }
      if (!SiteIDHelper.IsCompositionForeign(customService, DBHelper.GetSiteID(this.args.Session as UserSession, iInfoProject.ObjectId)))
      {
        IDBRelationsApplicability applicability = this.args.Session.GetRelationsApplicabilityCollection().GetApplicability(dbRelationType.RelationType, DBHelper.GetObjectTypeID(this.args.Session as UserSession, part.ObjectId), DBHelper.GetObjectTypeID(this.args.Session as UserSession, iInfoProject.ObjectId));
        if (applicability != null && applicability.IsContent)
          return new ImportedInfo(relationAttributes.Guid, 0L, 0L, this.args.Unit.Category, false);
      }
      if (ServerServices.ServiceContainer.GetService<IImportUnitHandlerService>(false) is ImportUnitHandlerService service)
        service.HandleImportRelation(relationRecord, iInfoProject, part);
      ImportingRelation briefRelation = new ImportingRelation(relationRecord);
      Dictionary<Guid, long> measures = new Dictionary<Guid, long>(1);
      List<int> intList = new List<int>();
      for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
      {
        if (rootNode.ChildNodes[i].Name == PortalConsts.XmlNodeAttribute)
        {
          XmlNode childNode1 = rootNode.ChildNodes[i];
          AttributeInfo attributeInfo = AttributesFile.GetAttributeInfo(childNode1);
          IDBAttributeType attrType;
          if (Intermech.Kernel.Briefcase.Helper.FindAttribute(this.args.Session as UserSession, out attrType, GuidHelper.IsGuid(attributeInfo.Guid) ? new Guid(attributeInfo.Guid) : Guid.Empty, attributeInfo.Alias, attributeInfo.Name) == CheckResult.None)
            this.args.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1103"), (object) attributeInfo.Guid, (object) attributeInfo.Name, (object) attributeInfo.Alias), Consts.traceAlways, string.Empty);
          else if (!attrType.IsCompatibleType(attributeInfo.FieldType))
            this.args.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1104"), (object) attributeInfo.Name, (object) attrType.Name), Consts.traceAlways, string.Empty);
          else if (!dbRelationType.AnyAttributes && dbRelationType.Attributes.GetAttributeByID(attrType.AttributeID, false) == null)
          {
            this.args.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1158"), (object) dbRelationType.Description, (object) attributeInfo.Name), Consts.traceAlways, string.Empty);
          }
          else
          {
            for (int index = 0; index < childNode1.ChildNodes.Count; ++index)
            {
              XmlNode childNode2 = childNode1.ChildNodes[index];
              if (childNode2.Name == PortalConsts.XmlNodeValueAttribute)
              {
                if (index > 0 && (attrType.MultipleValued == MultiValueModes.SingleValue || attrType.MultipleValued == MultiValueModes.SingleValueFromList))
                {
                  this.args.EventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1106"), (object) attrType.Name), Consts.traceAlways, string.Empty);
                  break;
                }
                AttributeRecord attributeRecord = this.GetAttributeRecord(AttributesFile.GetAttributeValue(childNode2), attributeInfo, attrType, measures, index);
                briefRelation.AddAttribute(new AttributeRecord(attributeRecord, 0L));
                intList.Add(attributeRecord.AttributeId);
              }
            }
          }
        }
      }
      if (relationAttributes.CompositionVersionGuid != Guid.Empty)
      {
        ImportedInfo importedInfo = this.FindObject(relationAttributes.CompositionVersionGuid);
        if (importedInfo != null)
        {
          AttributeRecord rec = new AttributeRecord()
          {
            AttributeId = MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545"),
            InlistId = 0,
            IntegerValue = (object) importedInfo.ObjectId
          };
          briefRelation.AddAttribute(new AttributeRecord(rec, 0L));
        }
        else
          this.args.EventHelper.AddToTrace($"Версия объекта c GUID={relationAttributes.CompositionVersionGuid} указанная в атрибуте связи 'Идентификатор версии в составе' не найдена в базе назначения. Связь: ProjectGuid={relationAttributes.ProjectGuid}, PartGuid={relationAttributes.PartGuid} ", Consts.traceAlways, string.Empty);
      }
      foreach (Intermech.Interface.TypeAttribute typeAttribute in new Attributes4RelationReader(dbRelationType.RelationType, forbiddenAttributeIDs: intList.ToArray()).Read())
        briefRelation.AddAttribute(typeAttribute.ConvertTo(this.args.Session));
      new ImportPublishRelation(this.args.Session as UserSession, briefRelation, false, part.ObjectId).Import(true, true);
      if (briefRelation.Relation.RelationType == MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"))
      {
        Tuple<Guid, Guid, long, List<Guid>> tuple = this.args.Contexts.Find((Predicate<Tuple<Guid, Guid, long, List<Guid>>>) (x => x.Item1.Equals(iInfoProject.Guid)));
        if (tuple != null && !tuple.Item4.Contains(part.Guid))
          tuple.Item4.Add(part.Guid);
      }
      return new ImportedInfo(relationAttributes.Guid, (long) briefRelation.Relation.RelationType, (long) relationRecord.ProjId, this.args.Unit.Category, false)
      {
        BaseVersionId = (long) relationRecord.PartId
      };
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1110"), (object) relationAttributes.Guid, (object) ex.Message), ex);
    }
  }
}
