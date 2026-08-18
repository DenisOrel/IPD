// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckImportingData
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckImportingData : ImportBriefcaseBase
{
  private AttributeXmlReader _attributeXmlReader;
  private ObjectXmlReader _objectXmlReader;

  public CheckImportingData(
    UserSession session,
    ImportEventLog eventLog,
    SetImportProgressEventHandler setImportProgressEvent)
    : base(session, eventLog, setImportProgressEvent)
  {
    this._objectXmlReader = new ObjectXmlReader(eventLog);
    this._attributeXmlReader = new AttributeXmlReader(eventLog);
  }

  public List<CheckMetadataLogItem> Check(
    string briefcasePath,
    Guid briefcase,
    int countObjects,
    List<FoundObjectInfo> findObjectsToObject)
  {
    List<CheckMetadataLogItem> checkMetadataLogItemList = new List<CheckMetadataLogItem>();
    BriefcaseImportProgress importProgress = new BriefcaseImportProgress(OperationType.CheckingData);
    this.SetImportProgress(briefcase, importProgress);
    DataSet[] dataSetArray = BriefcaseProcs.ReadMetaDataXML(briefcasePath);
    XmlTextReader reader1 = new XmlTextReader(Path.Combine(briefcasePath, "Objects.xml"));
    XmlTextReader reader2 = new XmlTextReader(Path.Combine(briefcasePath, "ObjAttributes.xml"));
    FileInfo fileInfo = new FileInfo(Path.Combine(briefcasePath, "Objects.xml"));
    try
    {
      int num = 0;
      AttributeRecord attribute1 = new AttributeRecord();
      while (reader1.Read())
      {
        if (reader1.NodeType == XmlNodeType.Element && reader1.Name == BriefcaseConsts.XmlObjectRecordTag)
        {
          ImportingObject briefObject = new ImportingObject(this._objectXmlReader.Read(reader1));
          if (briefObject.Object.Object_id > 0L)
          {
            bool flag = true;
            if (attribute1.AttributeId != 0)
            {
              if (attribute1.AttributableId == briefObject.Object.Object_id)
                briefObject.AddAttribute(attribute1);
              else
                flag = false;
            }
            if (flag)
            {
              while (reader2.Read())
              {
                if (reader2.NodeType == XmlNodeType.Element && reader2.Name == BriefcaseConsts.XmlAttributeRecordTag)
                {
                  AttributeRecord attribute2 = this._attributeXmlReader.Read(reader2);
                  if (attribute2.AttributableId != briefObject.Object.Object_id)
                  {
                    attribute1 = attribute2;
                    break;
                  }
                  briefObject.AddAttribute(attribute2);
                  attribute1 = new AttributeRecord();
                }
              }
            }
            CheckObject checkObject = new CheckObject(this.session, dataSetArray[0], briefObject);
            checkObject.Check();
            if (checkObject.Log.Count > 0)
              checkMetadataLogItemList.AddRange((IEnumerable<CheckMetadataLogItem>) checkObject.Log);
            findObjectsToObject.Add(new FoundObjectInfo(briefObject.Object.Object_id, checkObject.ObjectID));
            ++num;
            importProgress.Percent = 100 * num / countObjects;
            this.SetImportProgress(briefcase, importProgress);
          }
        }
      }
    }
    finally
    {
      reader1.Close();
      reader2.Close();
    }
    return checkMetadataLogItemList;
  }
}
