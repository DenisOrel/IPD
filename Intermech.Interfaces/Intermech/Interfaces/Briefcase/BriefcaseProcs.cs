
// Type: Intermech.Interfaces.Briefcase.BriefcaseProcs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces.Briefcase
{
    public class BriefcaseProcs
    {
      public static bool OpenXML(
        string fileName,
        out FileStream fileStream,
        out XmlTextWriter xmlWriter,
        string startElementName)
      {
        fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
        xmlWriter = new XmlTextWriter((Stream) fileStream, Encoding.UTF8);
        xmlWriter.Formatting = Formatting.Indented;
        xmlWriter.Indentation = 2;
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement(startElementName);
        return true;
      }

      public static bool CloseXML(ref FileStream fileStream, ref XmlTextWriter xmlWriter)
      {
        xmlWriter.WriteEndElement();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
        xmlWriter = (XmlTextWriter) null;
        fileStream = (FileStream) null;
        return true;
      }

      private static ExportAttribute ReadExportAttribute(
        IUserSession session,
        XmlTextReader contentReader)
      {
        ExportAttribute exportAttribute = new ExportAttribute();
        while (contentReader.Read())
        {
          if (contentReader.NodeType == XmlNodeType.Element)
          {
            switch (contentReader.Name)
            {
              case "F_CATEGORY_ID":
                exportAttribute.Category = Convert.ToInt32(contentReader.ReadInnerXml());
                continue;
              case "F_OBJECT_ID":
                exportAttribute.Identifiers = new object[1]
                {
                  (object) contentReader.ReadInnerXml()
                };
                continue;
              default:
                continue;
            }
          }
          else if (contentReader.NodeType == XmlNodeType.EndElement && contentReader.Name == BriefcaseConsts.XmlExportAttributeRecordTag)
          {
            object identifilerObject = BriefcaseProcs.GetIdentifilerObject(session, exportAttribute.Category, (object) new Guid(exportAttribute.Identifiers[0].ToString()));
            ref ExportAttribute local = ref exportAttribute;
            object[] objArray;
            if (identifilerObject == null)
              objArray = new object[1]
              {
                (object) new Guid(exportAttribute.Identifiers[0].ToString())
              };
            else
              objArray = new object[1]{ identifilerObject };
            local.Identifiers = objArray;
            return exportAttribute;
          }
        }
        return exportAttribute;
      }

      public static ExportAttribute[] ReadBriefcaseContent(IUserSession session, string aXmlFilename)
      {
        ArrayList arrayList = new ArrayList();
        XmlTextReader contentReader = new XmlTextReader(aXmlFilename);
        try
        {
          if (contentReader == null)
            return (ExportAttribute[]) null;
          while (contentReader.Read())
          {
            if (contentReader.NodeType == XmlNodeType.Element && contentReader.Name == BriefcaseConsts.XmlExportAttributeRecordTag)
              arrayList.Add((object) BriefcaseProcs.ReadExportAttribute(session, contentReader));
          }
        }
        finally
        {
          contentReader.Close();
        }
        return (ExportAttribute[]) arrayList.ToArray(typeof (ExportAttribute));
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="session"></param>
      /// <param name="category"></param>
      /// <param name="id"></param>
      /// <returns></returns>
      public static object GetIdentifilerObject(IUserSession session, int category, object id)
      {
        bool flag = id is Guid;
        switch (category)
        {
          case 0:
            return (object) null;
          case 1:
            IDBObject dbObject = flag ? session.GetObject((Guid) id, false) : session.GetObject(Convert.ToInt64(id), false);
            if (dbObject != null)
              return flag ? (object) dbObject.ObjectID : (object) dbObject.ObjectGUID;
            break;
          case 3:
            IDBAttributeType dbAttributeType = flag ? session.GetAttributeType((Guid) id, false) : session.GetAttributeType(Convert.ToInt32(id), false);
            if (dbAttributeType != null)
              return flag ? (object) dbAttributeType.AttributeID : (object) (dbAttributeType as IDBGuid).GUID;
            break;
          case 4:
            IDBObjectType dbObjectType = flag ? session.GetObjectType((Guid) id, false) : session.GetObjectType(Convert.ToInt32(id), false);
            if (dbObjectType != null)
              return flag ? (object) dbObjectType.ObjectType : (object) (dbObjectType as IDBGuid).GUID;
            break;
          case 6:
            IDBRelationType dbRelationType = flag ? session.GetRelationType((Guid) id, false) : session.GetRelationType(Convert.ToInt32(id), false);
            if (dbRelationType != null)
              return flag ? (object) dbRelationType.RelationType : (object) (dbRelationType as IDBGuid).GUID;
            break;
          case 8:
            IDBLifecycleLevelType lifecycleLevelType = flag ? session.GetLifecycleLevel((Guid) id, false) : session.GetLifecycleLevel(Convert.ToInt32(id), false);
            if (lifecycleLevelType != null)
              return flag ? (object) lifecycleLevelType.LevelID : (object) lifecycleLevelType.GUID;
            break;
          case 9:
            IDBLanguageType dbLanguageType = flag ? session.GetLanguage((Guid) id, false) : session.GetLanguage((string) id, false);
            if (dbLanguageType != null)
              return flag ? (object) dbLanguageType.LanguageID : (object) dbLanguageType.GUID;
            break;
          case 11:
            IDBSubjectAreaType dbSubjectAreaType = flag ? session.GetSubjectAreaType((Guid) id, false) : session.GetSubjectAreaType(Convert.ToChar(id), false);
            if (dbSubjectAreaType != null)
              return flag ? (object) dbSubjectAreaType.AreaID : (object) (dbSubjectAreaType as IDBGuid).GUID;
            break;
          case 12:
            IDBAttributesGroup dbAttributesGroup = flag ? session.GetAttributesGroup((Guid) id, false) : session.GetAttributesGroup(Convert.ToInt32(id), false);
            if (dbAttributesGroup != null)
              return flag ? (object) dbAttributesGroup.GroupID : (object) (dbAttributesGroup as IDBGuid).GUID;
            break;
        }
        return (object) null;
      }

      public static BriefcaseAttributes ReadBriefcaseAttributes(string aXmlFilename)
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        int aVersion = 0;
        DateTime aExportDate = DateTime.MinValue;
        DateTime aLastSystemUpdate = DateTime.MinValue;
        bool aClosed = false;
        bool includeLocalization = false;
        Guid siteGuid = Guid.Empty;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(aXmlFilename);
        XmlNode xmlNode = xmlDocument.SelectSingleNode($"//{BriefcaseConsts.XmlConfigurationTag}//{BriefcaseConsts.XmlBriefcaseTag}");
        if (xmlNode != null)
        {
          for (int i = 0; i < xmlNode.Attributes.Count; ++i)
          {
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlCommentTag)
              empty2 = xmlNode.Attributes[i].Value;
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlNameTag)
              empty1 = xmlNode.Attributes[i].Value;
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlVersionTag)
              aVersion = Convert.ToInt32(xmlNode.Attributes[i].Value);
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlExportDateTag)
            {
              DateTime dateTime = Convert.ToDateTime(xmlNode.Attributes[i].Value, (IFormatProvider) CultureInfo.InvariantCulture);
              aExportDate = dateTime == DateTime.MinValue ? dateTime : dateTime.ToLocalTime();
            }
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlLastSystemUpdateTag)
            {
              DateTime dateTime = Convert.ToDateTime(xmlNode.Attributes[i].Value, (IFormatProvider) CultureInfo.InvariantCulture);
              aLastSystemUpdate = dateTime == DateTime.MinValue ? dateTime : dateTime.ToLocalTime();
            }
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlClosedFlag)
              aClosed = Convert.ToBoolean(xmlNode.Attributes[i].Value);
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlIncludeLocalization)
              includeLocalization = Convert.ToBoolean(xmlNode.Attributes[i].Value);
            if (xmlNode.Attributes[i].Name == BriefcaseConsts.XmlSiteGuid && GuidHelper.IsGuid(xmlNode.Attributes[i].Value))
              siteGuid = new Guid(xmlNode.Attributes[i].Value);
          }
        }
        return new BriefcaseAttributes(empty1, empty2, aVersion, aExportDate, aLastSystemUpdate, aClosed, includeLocalization, siteGuid);
      }

      public static bool WriteBriefcaseAttributes(
        string aXmlFilename,
        BriefcaseAttributes aBriefcaseAttributes)
      {
        XmlDocument xmlDocument = new XmlDocument();
        if (File.Exists(aXmlFilename))
          xmlDocument.Load(aXmlFilename);
        XmlElement newChild1 = (XmlElement) xmlDocument.SelectSingleNode($"//{BriefcaseConsts.XmlConfigurationTag}//{BriefcaseConsts.XmlBriefcaseTag}");
        if (newChild1 == null)
        {
          XmlNode newChild2 = xmlDocument.SelectSingleNode("//" + BriefcaseConsts.XmlConfigurationTag);
          if (newChild2 == null)
          {
            newChild2 = (XmlNode) xmlDocument.CreateElement(BriefcaseConsts.XmlConfigurationTag);
            xmlDocument.AppendChild(newChild2);
          }
          newChild1 = xmlDocument.CreateElement(BriefcaseConsts.XmlBriefcaseTag);
          newChild2.AppendChild((XmlNode) newChild1);
        }
        newChild1.SetAttribute(BriefcaseConsts.XmlNameTag, aBriefcaseAttributes.Name);
        newChild1.SetAttribute(BriefcaseConsts.XmlCommentTag, aBriefcaseAttributes.Comment);
        newChild1.SetAttribute(BriefcaseConsts.XmlVersionTag, aBriefcaseAttributes.Version.ToString());
        XmlElement xmlElement1 = newChild1;
        string xmlExportDateTag = BriefcaseConsts.XmlExportDateTag;
        DateTime universalTime;
        string str1;
        if (!(aBriefcaseAttributes.ExportDate == DateTime.MinValue))
        {
          universalTime = aBriefcaseAttributes.ExportDate.ToUniversalTime();
          str1 = universalTime.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
          str1 = aBriefcaseAttributes.ExportDate.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        xmlElement1.SetAttribute(xmlExportDateTag, str1);
        XmlElement xmlElement2 = newChild1;
        string lastSystemUpdateTag = BriefcaseConsts.XmlLastSystemUpdateTag;
        string str2;
        if (!(aBriefcaseAttributes.LastSystemUpdate == DateTime.MinValue))
        {
          universalTime = aBriefcaseAttributes.LastSystemUpdate.ToUniversalTime();
          str2 = universalTime.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
          str2 = aBriefcaseAttributes.LastSystemUpdate.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        xmlElement2.SetAttribute(lastSystemUpdateTag, str2);
        newChild1.SetAttribute(BriefcaseConsts.XmlClosedFlag, aBriefcaseAttributes.Closed.ToString());
        newChild1.SetAttribute(BriefcaseConsts.XmlIncludeLocalization, aBriefcaseAttributes.IncludeLocalization.ToString());
        if (aBriefcaseAttributes.SiteGuid != Guid.Empty)
          newChild1.SetAttribute(BriefcaseConsts.XmlSiteGuid, aBriefcaseAttributes.SiteGuid.ToString());
        xmlDocument.Save(aXmlFilename);
        return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="xtw"></param>
      /// <param name="tag"></param>
      /// <param name="s"></param>
      public static void WriteToXML(XmlTextWriter xtw, string tag, string s)
      {
        xtw.WriteStartElement(tag);
        xtw.WriteString(s);
        xtw.WriteEndElement();
        xtw.Flush();
      }

      public static void WriteAttributeToXML(XmlTextWriter xtw, string attrname, string s)
      {
        xtw.WriteStartAttribute(attrname);
        xtw.WriteString(s);
        xtw.WriteEndAttribute();
        xtw.Flush();
      }

      public static void WriteToMetadataExportListXml(
        XmlTextWriter briefcaseMetadataExportListXML,
        MetadataRecord mr)
      {
        briefcaseMetadataExportListXML.WriteStartElement(BriefcaseConsts.XmlMetadataRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseMetadataExportListXML, BriefcaseConsts.XmlCategoryTag, mr.Category.ToString());
        BriefcaseProcs.WriteToXML(briefcaseMetadataExportListXML, BriefcaseConsts.XmlIdTag, mr.Id.ToString());
        BriefcaseProcs.WriteToXML(briefcaseMetadataExportListXML, BriefcaseConsts.XmlExternalTag, mr.ExternalId.ToString());
        briefcaseMetadataExportListXML.WriteEndElement();
        briefcaseMetadataExportListXML.Flush();
      }

      public static void WriteToExportContentXml(
        IUserSession session,
        XmlTextWriter briefcaseExportContentXML,
        ExportAttribute aExportAttributes)
      {
        for (int index = 0; index < aExportAttributes.Identifiers.Length; ++index)
        {
          object identifilerObject = BriefcaseProcs.GetIdentifilerObject(session, aExportAttributes.Category, aExportAttributes.Identifiers[index]);
          if (identifilerObject != null)
          {
            briefcaseExportContentXML.WriteStartElement(BriefcaseConsts.XmlExportAttributeRecordTag);
            BriefcaseProcs.WriteToXML(briefcaseExportContentXML, "F_OBJECT_ID", identifilerObject.ToString());
            BriefcaseProcs.WriteToXML(briefcaseExportContentXML, "F_CATEGORY_ID", aExportAttributes.Category.ToString());
            briefcaseExportContentXML.WriteEndElement();
          }
        }
        briefcaseExportContentXML.Flush();
      }

      public static void WriteToObjectsXml(XmlTextWriter briefcaseObjectsXML, ObjectRecord or)
      {
        briefcaseObjectsXML.WriteStartElement(BriefcaseConsts.XmlObjectRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OBJECT_ID", or.Object_id.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OBJECTGUID", or.ObjectGuid.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_ID", or.Id.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_IDGUID", or.IdGuid.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_LC_STEP", or.Lc_step.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_VERSION_ID", or.VersionId.ToString());
        if (or.ParentVersionId != -1L)
          BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_PARENT_ID", or.ParentVersionId.ToString());
        if (or.ChkoutBy != 0L)
          BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_CHKOUT_BY", or.ChkoutBy.ToString());
        if (or.ChkoutGuid != null)
          BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_CHKOUTGUID", or.ChkoutGuid.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OBJECT_VER_TYPE", or.ObjectVerType.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OBJECT_TYPE", or.ObjectType.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OWNER_ID", or.OwnerId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OWNERGUID", or.OwnerGuid.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_MODIFY_DATE", or.ModifyDate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_LEVEL_ID", or.LevelId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_OBJ_CREATE", or.ObjCreate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "CAPTION", or.Caption.ToString());
        if (or.ProjectId > 0L)
          BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_PROJECT_ID", or.ProjectId.ToString());
        if (or.ProjectGuid != null)
          BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_PROJECTGUID", or.ProjectGuid.ToString());
        if (or.CreatorID > 0L)
          BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_CREATOR_ID", or.CreatorID.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjectsXML, "F_ACCESS", or.AccessLevel.ToString());
        briefcaseObjectsXML.WriteEndElement();
        briefcaseObjectsXML.Flush();
      }

      public static void WriteToObjAttributesXml(
        XmlTextWriter briefcaseObjAttributesXML,
        AttributeRecord oar)
      {
        briefcaseObjAttributesXML.WriteStartElement(BriefcaseConsts.XmlAttributeRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_ATTRIBUTE_ID", oar.AttributeId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_OBJECT_ID", oar.AttributableId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_INLIST_ID", oar.InlistId.ToString());
        if (oar.IntegerValue != null)
        {
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_INTEGER_VALUE", oar.IntegerValue.ToString());
          if (oar.IntegerGuid != null)
            BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_INTEGERGUID", oar.IntegerGuid.ToString());
        }
        if (oar.DoubleValue != null)
        {
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_DOUBLE_VALUE", ((double) oar.DoubleValue).ToString((IFormatProvider) CultureInfo.InvariantCulture));
          if (oar.DoubleGuid != null)
            BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_DOUBLEGUID", oar.DoubleGuid.ToString());
        }
        if (oar.StringValue != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_STRING_VALUE", oar.StringValue.ToString());
        if (oar.DateValue != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_DATE_VALUE", XmlConvert.ToString((DateTime) oar.DateValue, XmlDateTimeSerializationMode.Unspecified));
        if (oar.FileSize != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_FILESIZE", Convert.ToInt64(oar.FileSize).ToString());
        if (oar.ArcMethod != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_ARC_METHOD", ((int) oar.ArcMethod).ToString());
        if (oar.FileNote != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_NOTE", ((string) oar.FileNote).ToString());
        if (oar.Path2File != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_PATH2FILE", oar.Path2File.ToString());
        if (oar.FileType != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_LINKTYPE", ((int) oar.FileType).ToString());
        if (oar.FileAuthor != null)
          BriefcaseProcs.WriteToXML(briefcaseObjAttributesXML, "F_AUTHOR", ((string) oar.FileAuthor).ToString());
        briefcaseObjAttributesXML.WriteEndElement();
        briefcaseObjAttributesXML.Flush();
      }

      public static void WriteToObjLCStepsXml(XmlTextWriter briefcaseObjLCStepsXML, LCStepRecord lc)
      {
        briefcaseObjLCStepsXML.WriteStartElement(BriefcaseConsts.XmlObjLCStepsRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseObjLCStepsXML, "F_OBJECT_ID", lc.ObjectId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjLCStepsXML, "F_LC_STEP", lc.LCStep.ToString());
        BriefcaseProcs.WriteToXML(briefcaseObjLCStepsXML, "F_START_DATE", lc.LCStartDate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        briefcaseObjLCStepsXML.WriteEndElement();
        briefcaseObjLCStepsXML.Flush();
      }

      public static void WriteToRelationsXml(XmlTextWriter briefcaseRelationsXML, RelationRecord rr)
      {
        briefcaseRelationsXML.WriteStartElement(BriefcaseConsts.XmlRelationRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_PRJLINK_ID", rr.PrjLinkId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_PRJ_GUID", rr.PrjLinkGuid.ToString());
        BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_PROJ_ID", rr.ProjId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_PART_ID", rr.PartId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_RELATION_TYPE", rr.RelationType.ToString());
        if (rr.CreateDate != null)
          BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_CREATE_DATE", XmlConvert.ToString(Convert.ToDateTime(rr.CreateDate), XmlDateTimeSerializationMode.Unspecified));
        if (rr.CreatorID != 0L)
          BriefcaseProcs.WriteToXML(briefcaseRelationsXML, "F_REL_CREATOR", rr.CreatorID.ToString());
        briefcaseRelationsXML.WriteEndElement();
        briefcaseRelationsXML.Flush();
      }

      public static void WriteToRelAttributesXml(
        XmlTextWriter briefcaseRelAttributesXML,
        AttributeRecord rar)
      {
        briefcaseRelAttributesXML.WriteStartElement(BriefcaseConsts.XmlAttributeRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_ATTRIBUTE_ID", rar.AttributeId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_PRJLINK_ID", rar.AttributableId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_INLIST_ID", rar.InlistId.ToString());
        if (rar.IntegerValue != null)
        {
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_INTEGER_VALUE", rar.IntegerValue.ToString());
          if (rar.IntegerGuid != null)
            BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_INTEGERGUID", rar.IntegerGuid.ToString());
        }
        if (rar.DoubleValue != null)
        {
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_DOUBLE_VALUE", ((double) rar.DoubleValue).ToString((IFormatProvider) CultureInfo.InvariantCulture));
          if (rar.DoubleGuid != null)
            BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_DOUBLEGUID", rar.DoubleGuid.ToString());
        }
        if (rar.StringValue != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_STRING_VALUE", rar.StringValue.ToString());
        if (rar.DateValue != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_DATE_VALUE", XmlConvert.ToString((DateTime) rar.DateValue, XmlDateTimeSerializationMode.Unspecified));
        if (rar.FileSize != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_FILESIZE", Convert.ToInt64(rar.FileSize).ToString());
        if (rar.ArcMethod != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_ARC_METHOD", ((int) rar.ArcMethod).ToString());
        if (rar.FileNote != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_NOTE", ((string) rar.FileNote).ToString());
        if (rar.Path2File != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_PATH2FILE", rar.Path2File.ToString());
        if (rar.FileType != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_LINKTYPE", ((int) rar.FileType).ToString());
        if (rar.FileAuthor != null)
          BriefcaseProcs.WriteToXML(briefcaseRelAttributesXML, "F_AUTHOR", ((string) rar.FileAuthor).ToString());
        briefcaseRelAttributesXML.WriteEndElement();
        briefcaseRelAttributesXML.Flush();
      }

      public static void WriteToContextsXml(XmlTextWriter briefcaseContextsXML, ContextRecord cr)
      {
        List<string> stringList = new List<string>();
        if (cr.ObjectIDs != null)
        {
          for (int index = 0; index < cr.ObjectIDs.Count; ++index)
            stringList.Add(cr.ObjectIDs[index].ToString());
        }
        briefcaseContextsXML.WriteStartElement(BriefcaseConsts.XmlContextsRecordTag);
        BriefcaseProcs.WriteAttributeToXML(briefcaseContextsXML, BriefcaseConsts.XmlContextIDAttributeName, cr.Id.ToString());
        BriefcaseProcs.WriteAttributeToXML(briefcaseContextsXML, BriefcaseConsts.XmlContextModificationIDAttributeName, cr.ContextId.ToString());
        BriefcaseProcs.WriteAttributeToXML(briefcaseContextsXML, BriefcaseConsts.XmlContextContentAttributeName, string.Join(";", stringList.ToArray()));
        briefcaseContextsXML.WriteEndElement();
        briefcaseContextsXML.Flush();
      }

      public static void WriteToSecurityXml(XmlTextWriter briefcaseSecurityXML, SecurityRecord sr)
      {
        briefcaseSecurityXML.WriteStartElement(BriefcaseConsts.XmlSecurityRecordTag);
        BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_CATEGORY_ID", sr.CategoryID.ToString());
        BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_CATEGORY_TYPE", sr.CategoryType.ToString());
        BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_RIGHT_ID", sr.RightId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_USER_ID", sr.UserId.ToString());
        BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_RIGHT_TYPE", sr.RightType.ToString());
        if (sr.OwnerId != null && sr.OwnerId != DBNull.Value)
          BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_OWNER_ID", sr.OwnerId.ToString());
        if (sr.BeginDate != null && sr.BeginDate != DBNull.Value)
          BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_BEGIN_DATE", XmlConvert.ToString(Convert.ToDateTime(sr.BeginDate), XmlDateTimeSerializationMode.Unspecified));
        if (sr.EndDate != null && sr.EndDate != DBNull.Value)
          BriefcaseProcs.WriteToXML(briefcaseSecurityXML, "F_END_DATE", XmlConvert.ToString(Convert.ToDateTime(sr.EndDate), XmlDateTimeSerializationMode.Unspecified));
        briefcaseSecurityXML.WriteEndElement();
        briefcaseSecurityXML.Flush();
      }

      /// <summary>Проверка портфеля</summary>
      /// <param name="briefcase"></param>
      /// <param name="ErrorMessage"></param>
      /// <returns></returns>
      public static bool CheckBriefcase(
        IUserSession session,
        BriefcaseLocation briefcase,
        out string ErrorMessage)
      {
        session.GetBriefcase();
        BriefcaseAttributes briefcaseAttributes;
        try
        {
          briefcaseAttributes = BriefcaseProcs.ReadBriefcaseAttributes(Path.Combine(briefcase.Path, "BriefcaseConfig.xml"));
        }
        catch
        {
          ErrorMessage = LocalizationHolder.rm.GetString("Interfaces.Briefcase_200");
          return false;
        }
        if (!briefcaseAttributes.Closed)
        {
          ErrorMessage = LocalizationHolder.rm.GetString("Interfaces.Briefcase_201");
          return false;
        }
        ErrorMessage = string.Empty;
        return true;
      }

      /// <summary>Читаем метаданные из XML</summary>
      /// <param name="BriefcaseFolder"></param>
      /// <returns></returns>
      public static DataSet[] ReadMetaDataXML(string BriefcaseFolder)
      {
        try
        {
          DataSet dataSet1 = new DataSet("SYSTEM");
          dataSet1.ReadXmlSchema(Path.Combine(BriefcaseFolder, "Metadata.xsd"));
          int num1 = (int) dataSet1.ReadXml(Path.Combine(BriefcaseFolder, "Metadata.xml"));
          DataSet dataSet2 = new DataSet("IMPORT");
          dataSet2.ReadXmlSchema(Path.Combine(BriefcaseFolder, "MetadataExportList.xsd"));
          int num2 = (int) dataSet2.ReadXml(Path.Combine(BriefcaseFolder, "MetadataExportList.xml"));
          return new DataSet[2]{ dataSet1, dataSet2 };
        }
        catch
        {
          return (DataSet[]) null;
        }
      }

      public static DataTable ReadObjectsXML(string BriefcaseFolder)
      {
        try
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema(Path.Combine(BriefcaseFolder, "Objects.xsd"));
          int num = (int) dataSet.ReadXml(Path.Combine(BriefcaseFolder, "Objects.xml"), XmlReadMode.Auto);
          return dataSet.Tables["OBJECTS"];
        }
        catch
        {
          return (DataTable) null;
        }
      }

      public static object ProcessIfDecimal(int category, object id)
      {
        if (id != null && id is Decimal)
        {
          switch (category)
          {
            case 1:
            case 2:
            case 5:
            case 10:
              id = (object) Convert.ToInt64(id);
              break;
            case 3:
            case 4:
            case 6:
            case 7:
            case 8:
            case 12:
            case 16 /*0x10*/:
              id = (object) Convert.ToInt32(id);
              break;
          }
        }
        return id;
      }

      /// <summary>Очистка папки портфеля - только файлы портфеля!</summary>
      /// <param name="briefcaseFolder"></param>
      /// <param name="exception"></param>
      /// <returns></returns>
      public static bool DeleteBriefcase(
        string briefcaseFolder,
        bool removeFolder,
        out Exception exception)
      {
        exception = (Exception) null;
        if (!Directory.Exists(briefcaseFolder))
          return true;
        foreach (string directory in Directory.GetDirectories(briefcaseFolder, "*.*"))
        {
          try
          {
            if (BriefcaseConsts.BriefcaseFolders.IndexOf(Path.GetFileName(directory).ToUpper()) != -1)
              Directory.Delete(directory, true);
          }
          catch (Exception ex)
          {
            exception = ex;
            return false;
          }
        }
        foreach (string file in Directory.GetFiles(briefcaseFolder, "*.*"))
        {
          try
          {
            if (BriefcaseConsts.BriefcaseFiles.IndexOf(Path.GetFileName(file).ToUpper()) != -1)
              File.Delete(file);
          }
          catch (Exception ex)
          {
            exception = ex;
            return false;
          }
        }
        if (removeFolder)
        {
          try
          {
            Directory.Delete(briefcaseFolder, true);
          }
          catch (Exception ex)
          {
            exception = ex;
            return false;
          }
        }
        return true;
      }

      /// <summary>Создает структуру папок</summary>
      /// <param name="localFolder"></param>
      /// <param name="bfs"></param>
      /// <returns></returns>
      public static void CreateBriefcaseFolderStructure(string localFolder, BriefcaseFilesStructure bfs)
      {
        new DirectoryInfo(localFolder).Create();
        for (int index = 0; index < bfs.Folders.Count; ++index)
          Directory.CreateDirectory(localFolder + Path.DirectorySeparatorChar.ToString() + bfs.Folders[index]);
      }
    }
}
