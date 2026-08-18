// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.UnitImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.PortalServices.Import;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class UnitImporter : IUnitImporter
{
  protected ImportArgs args;

  public UnitImporter(ImportArgs args) => this.args = args;

  public abstract ImportedInfo Import();

  public static ImportedInfo Import(ImportArgs args)
  {
    IUnitImporter unitImporter = (IUnitImporter) null;
    switch (args.Unit.Category)
    {
      case TransferedObjectCategory.Object:
      case TransferedObjectCategory.ObjectLink:
      case TransferedObjectCategory.Receipt:
        unitImporter = (IUnitImporter) new ObjectImporter(args);
        break;
      case TransferedObjectCategory.Relation:
        unitImporter = (IUnitImporter) new RelationImporter(args);
        break;
      case TransferedObjectCategory.AttributesContainer:
        unitImporter = (IUnitImporter) new AttributesContainerImporter(args);
        break;
    }
    return unitImporter?.Import();
  }

  protected ImportedInfo FindObject(Guid guid)
  {
    ImportedInfo importedInfo;
    if (this.args.Links != null && this.args.Links.TryGetValue(guid, out importedInfo))
      return importedInfo;
    IDBObject dbObject = this.args.Session.GetObject(guid, false);
    return dbObject == null ? (ImportedInfo) null : (ImportedInfo) new TypedImportedInfo(guid, dbObject.ID, dbObject.ObjectID, TransferedObjectCategory.Object, false, dbObject.ObjectType);
  }

  private bool IsEmptyAttributeGuid(string guid)
  {
    return !GuidHelper.IsGuid(guid) || new Guid(guid) == Guid.Empty;
  }

  protected void ParseAttributes(
    IDBObjectType objType,
    ImportingObject importObject,
    XmlNode rootNode,
    Dictionary<Guid, long> measures,
    ImportReceipt receipt,
    SiteInfo creatorInfo,
    out List<int> addedAttributes)
  {
    UserSession session = this.args.Session as UserSession;
    Dictionary<int, List<AttributeRecord>> attributes = new Dictionary<int, List<AttributeRecord>>();
    for (int i1 = 0; i1 < rootNode.ChildNodes.Count; ++i1)
    {
      string name = rootNode.ChildNodes[i1].Name;
      if (name == PortalConsts.XmlNodeAttribute || name == PortalConsts.XmlRootNodeRemark)
      {
        bool flag1 = name == PortalConsts.XmlRootNodeRemark;
        XmlNode childNode1 = rootNode.ChildNodes[i1];
        AttributeInfo attributeInfo = AttributesFile.GetAttributeInfo(childNode1);
        if (creatorInfo.SystemType != SystemTypes.Search || !SearchAttributes.HandleAttribute(attributeInfo, childNode1, importObject))
        {
          if ((flag1 || this.IsEmptyAttributeGuid(attributeInfo.Guid) && attributeInfo.Name == "Замечания") && attributeInfo.FieldType == FieldTypes.ftFile)
          {
            for (int i2 = 0; i2 < childNode1.ChildNodes.Count; ++i2)
            {
              XmlNode childNode2 = childNode1.ChildNodes[i2];
              if (childNode2.Name == PortalConsts.XmlNodeValueAttribute)
              {
                AttributeValue rec = AttributesFile.GetAttributeValue(childNode2);
                if (!string.IsNullOrEmpty(rec.StringValue) && (((IEnumerable<string>) SearchAttributes.RedliningExtensions).Any<string>((Func<string, bool>) (_ => rec.StringValue.ToLower().EndsWith(_))) || rec.FileType == FileTypes.ftNotContent))
                {
                  attributeInfo.Guid = "cad0004b-306c-11d8-b4e9-00304f19f545";
                  flag1 = true;
                  break;
                }
              }
            }
          }
          IDBAttributeType attrType;
          if (Intermech.Kernel.Briefcase.Helper.FindAttribute(session, out attrType, GuidHelper.IsGuid(attributeInfo.Guid) ? new Guid(attributeInfo.Guid) : Guid.Empty, attributeInfo.Alias, attributeInfo.Name) == CheckResult.None)
          {
            string str = string.Format(LocalizationHolder.rm.GetString("Kernel_1103"), (object) attributeInfo.Guid, (object) attributeInfo.Name, (object) attributeInfo.Alias);
            this.args.EventHelper.AddToTrace(str, Consts.traceAlways, string.Empty);
            receipt?.AddAttributeRecord(importObject, attributeInfo.Name, str);
          }
          else
          {
            bool flag2 = true;
            bool flag3 = false;
            if (attrType.AttributeType == FieldTypes.ftSystem)
            {
              if (ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attrType.AttributeID) == attributeInfo.FieldType)
                flag2 = false;
              flag3 = true;
            }
            if (flag2 && !attrType.IsCompatibleType(attributeInfo.FieldType))
            {
              string str = string.Format(LocalizationHolder.rm.GetString("Kernel_1104"), (object) attributeInfo.Name, (object) attrType.Name);
              this.args.EventHelper.AddToTrace(str, Consts.traceAlways, string.Empty);
              receipt?.AddAttributeRecord(importObject, attributeInfo.Name, str);
            }
            else if (!objType.AnyAttributes && objType.Attributes.GetAttributeByID(attrType.AttributeID, false) == null)
            {
              string str = string.Format(LocalizationHolder.rm.GetString("Kernel_1105"), (object) objType.ObjectTypeName, (object) attributeInfo.Name);
              this.args.EventHelper.AddToTrace(str, Consts.traceAlways, string.Empty);
              receipt?.AddAttributeRecord(importObject, attributeInfo.Name, str);
            }
            else
            {
              bool flag4 = true;
              for (int index = 0; index < childNode1.ChildNodes.Count; ++index)
              {
                XmlNode childNode3 = childNode1.ChildNodes[index];
                if (childNode3.Name == PortalConsts.XmlNodeValueAttribute)
                {
                  if (index > 0 && (attrType.MultipleValued == MultiValueModes.SingleValue || attrType.MultipleValued == MultiValueModes.SingleValueFromList))
                  {
                    string str = string.Format(LocalizationHolder.rm.GetString("Kernel_1106"), (object) attrType.Name);
                    this.args.EventHelper.AddToTrace(str, Consts.traceAlways, string.Empty);
                    if (receipt != null)
                    {
                      receipt.AddAttributeRecord(importObject, attributeInfo.Name, str);
                      break;
                    }
                    break;
                  }
                  AttributeRecord attributeRecord = this.GetAttributeRecord(AttributesFile.GetAttributeValue(childNode3), attributeInfo, attrType, measures, index);
                  attributeRecord.AttributableId = 0L;
                  if (flag1 || attributeRecord.FileType is FileTypes && ((FileTypes) attributeRecord.FileType == FileTypes.ftRedlining || (FileTypes) attributeRecord.FileType == FileTypes.ftNotContent))
                  {
                    RemarkRecord attribute = new RemarkRecord(attributeRecord, childNode1.Attributes["F_SITE_ID"] != null ? childNode1.Attributes["F_SITE_ID"].Value[0] : creatorInfo.Code, childNode1.Attributes["F_MODIFY_DATE"] != null ? DateTimeHelper.ToDateTime(childNode1.Attributes["F_MODIFY_DATE"].Value) : DateTime.Now);
                    importObject.AddRemark(attribute);
                  }
                  else if (!flag3)
                    this.AddAttributeToDictionary(attributes, attributeRecord);
                  flag4 = false;
                }
              }
              if (flag4 && !flag1 && !flag3)
                this.AddAttributeToDictionary(attributes, new AttributeRecord(attrType.AttributeID, 0L));
            }
          }
        }
      }
    }
    addedAttributes = new List<int>(attributes.Count);
    if (attributes.Count > 0)
    {
      foreach (KeyValuePair<int, List<AttributeRecord>> keyValuePair in attributes)
      {
        int num = 0;
        foreach (AttributeRecord attribute in keyValuePair.Value)
        {
          attribute.InlistId = num;
          importObject.AddAttribute(attribute);
          addedAttributes.Add(attribute.AttributeId);
          ++num;
        }
      }
    }
    if (creatorInfo.SystemType != SystemTypes.Search)
      return;
    SearchAttributes.Create((IUserSession) session, objType, importObject, rootNode);
  }

  private void AddAttributeToDictionary(
    Dictionary<int, List<AttributeRecord>> attributes,
    AttributeRecord record)
  {
    List<AttributeRecord> attributeRecordList;
    if (!attributes.TryGetValue(record.AttributeId, out attributeRecordList))
    {
      attributeRecordList = new List<AttributeRecord>();
      attributes.Add(record.AttributeId, attributeRecordList);
    }
    attributeRecordList.Add(record);
  }

  protected AttributeRecord GetAttributeRecord(
    AttributeValue rec,
    AttributeInfo attrInfo,
    IDBAttributeType attrType,
    Dictionary<Guid, long> measures,
    int index)
  {
    IUserSession session = this.args.Session;
    AttributeRecord record = new AttributeRecord()
    {
      AttributeId = attrType.AttributeID,
      InlistId = rec.InListID
    };
    IRecordFormer recordFormer;
    switch (attrType.AttributeType)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        recordFormer = !((IIDLinkTranslate) this.args.Session.GetCustomService(typeof (IIDLinkTranslate))).IsIDLink((attrType as IDBGuid).GUID) ? (IRecordFormer) new IntegerRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path) : (IRecordFormer) new LinkRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftDouble:
        recordFormer = (IRecordFormer) new DoubleRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftDateTime:
        recordFormer = (IRecordFormer) new DateTimeRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftShortBlob:
      case FieldTypes.ftFile:
      case FieldTypes.ftMemo:
      case FieldTypes.ftBlob:
        recordFormer = (IRecordFormer) new BlobRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftObjectLink:
        recordFormer = (IRecordFormer) new ObjectLinkRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftPassword:
        recordFormer = (IRecordFormer) new PasswordRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftBoolean:
        recordFormer = (IRecordFormer) new BooleanRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftMeasured:
        recordFormer = (IRecordFormer) new MeasureRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftGuid:
        recordFormer = (IRecordFormer) new GuidRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      case FieldTypes.ftObjectLinkByID:
        recordFormer = (IRecordFormer) new ObjectLinkByIDRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
      default:
        recordFormer = (IRecordFormer) new StringRecordFormer(this.args.Session, this.args.EventHelper, this.args.Links, measures, this.args.Path);
        break;
    }
    recordFormer.SetRecordValues(attrInfo, attrType, rec, record);
    return record;
  }
}
