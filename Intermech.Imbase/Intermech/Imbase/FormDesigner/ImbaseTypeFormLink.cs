// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.FormDesigner.ImbaseTypeFormLink
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Imbase.FormDesigner;

internal class ImbaseTypeFormLink : FormLink
{
  private string _name = LocalizationHolder.rm.GetString("Imbase.Client_88");
  private long _objectID;

  protected ImbaseTypeFormLink()
  {
  }

  public ImbaseTypeFormLink(long objectID)
  {
    if (objectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
      if (dbObject == null)
        return;
      this._objectID = objectID;
      this._name = dbObject.Caption;
      this.ProviderGuid = ImbaseTypeFormLinkProvider.sProviderGuid;
    }
  }

  public override List<int> Attributes
  {
    get
    {
      if (this._objectID == 0L)
        return (List<int>) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._objectID, false);
        if (dbObject1 == null)
          return (List<int>) null;
        List<int> attributes1 = new List<int>();
        IDBAttributeCollection attributes2 = dbObject1.Attributes;
        for (int AttrIndex = 0; AttrIndex < attributes2.Count; ++AttrIndex)
          attributes1.Add(attributes2[AttrIndex].AttributeID);
        IDBAttribute attributeByGuid1 = dbObject1.GetAttributeByGuid(new Guid("cad0020b-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid1 != null)
        {
          long asInteger = attributeByGuid1.AsInteger;
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(asInteger);
          if (dbObject2 != null)
          {
            IDBAttribute attributeByGuid2 = dbObject2.GetAttributeByGuid(new Guid("cad00215-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid2 != null && attributeByGuid2 is IBlobReader blobReader)
            {
              BlobInformation blobInformation = blobReader.OpenBlob(0);
              try
              {
                if (blobInformation.RealFileSize > 0L)
                {
                  byte[] buffer = blobReader.ReadDataBlock(0);
                  if (buffer != null)
                  {
                    using (MemoryStream inStream = new MemoryStream(buffer))
                    {
                      using (MemoryStream memoryStream = new MemoryStream())
                      {
                        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
                        memoryStream.Position = 0L;
                        foreach (DataRow row in (InternalDataCollectionBase) ((DataSet) new BinaryFormatter().Deserialize((Stream) memoryStream)).Tables[0].Rows)
                        {
                          Guid anAttributeGuid = new Guid(Convert.ToString(row[0]));
                          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(anAttributeGuid, false);
                          if (attributeType != null)
                            attributes1.Add(attributeType.AttributeID);
                        }
                      }
                    }
                  }
                }
              }
              finally
              {
                blobReader.CloseBlob();
              }
            }
          }
        }
        return attributes1;
      }
    }
  }

  public long ObjectID => this._objectID;

  public override object Clone()
  {
    ImbaseTypeFormLink imbaseTypeFormLink = new ImbaseTypeFormLink();
    imbaseTypeFormLink._objectID = this._objectID;
    imbaseTypeFormLink._name = this._name;
    imbaseTypeFormLink.ProviderGuid = this.ProviderGuid;
    return (object) imbaseTypeFormLink;
  }

  public override string ToString() => this._name;
}
