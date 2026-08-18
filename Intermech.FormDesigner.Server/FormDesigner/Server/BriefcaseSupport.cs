// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.BriefcaseSupport
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.FormDesigner.Server;

public class BriefcaseSupport : ICategoryExport
{
  private int _formAttr;

  public BriefcaseSupport()
  {
    this._formAttr = MetaDataHelper.GetAttributeTypeID(new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
  }

  public string ExporterName => "FormDesigner.BriefcaseSupport";

  public long[] GetLinkedObjectVersions(IUserSession session, int category, object id)
  {
    return (long[]) null;
  }

  public ExportAttribute[] GetLinkedDataByAttribute(
    IUserSession session,
    AttributableElements kind,
    long id,
    IDBAttributable iDBAttributable,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent)
  {
    ExportAttribute[] linkedDataByAttribute = (ExportAttribute[]) null;
    if (attributeId == this._formAttr)
    {
      XElement xml = this.GetXML(session, id);
      if (xml != null)
      {
        List<XElement> list = xml.Descendants((XName) "Property").Where<XElement>((Func<XElement, bool>) (el => (string) el.Attribute((XName) "Name") == "SelectionGuid")).ToList<XElement>();
        if (list.Count > 0)
        {
          List<object> objectList = new List<object>(list.Count);
          foreach (XElement xelement in list)
          {
            long objectId = this.ConvertFromStringToObjectID(session, xelement.Value);
            if (objectId != 0L)
              objectList.Add((object) objectId);
          }
          linkedDataByAttribute = new ExportAttribute[1]
          {
            new ExportAttribute(1, objectList.ToArray())
          };
        }
      }
    }
    return linkedDataByAttribute;
  }

  public bool ProcessShortBlobs => true;

  private long ConvertFromStringToObjectID(IUserSession session, string strGuid)
  {
    long objectId = 0;
    if (GuidHelper.IsGuid(strGuid))
    {
      Guid objectGUID = new Guid(strGuid);
      if (objectGUID != Guid.Empty)
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(objectGUID);
        if (!objectInfo.Empty)
          objectId = objectInfo.ObjectID;
      }
    }
    return objectId;
  }

  private XElement GetXML(IUserSession session, long objID)
  {
    XElement xml = (XElement) null;
    IDBObject objectActualCopy = session.GetObjectActualCopy(objID, false);
    if (objectActualCopy != null)
    {
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(this._formAttr);
      if (attributeById != null && !attributeById.IsNull)
      {
        using (MemoryStream aDestStream = new MemoryStream())
        {
          BlobProcReader blobProcReader = new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader.ReadData(session);
          if (blobProcReader.Result)
          {
            if (aDestStream.Length > 0L)
            {
              aDestStream.Position = 0L;
              xml = XElement.Load((Stream) aDestStream);
            }
          }
        }
      }
    }
    return xml;
  }
}
