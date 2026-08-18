// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributeNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class AttributeNode : XMLPropertyNode<AttributeValues>
{
  private readonly string _directory;
  private Guid _attributeGuid;

  public AttributeNode(IUserSession session, XmlNode node, string nodeID, string directory)
    : base(session, node, nodeID, false)
  {
    this._directory = directory;
    this._attributeGuid = new Guid(nodeID);
    this.ReadValue(session, node);
  }

  protected override void ReadValue(IUserSession session, XmlNode node)
  {
    IDBAttributeType attributeType = session.GetAttributeType(this._attributeGuid, true);
    AttributeValues attributeValues = new AttributeValues(attributeType.AttributeID, attributeType.AttributeType, attributeType.MultipleValued, attributeType.Computed)
    {
      AttributeGuid = (attributeType as IDBGuid).GUID
    };
    List<object> objectList = new List<object>();
    if (node.HasChildNodes)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "PropValue")
        {
          UpdateScriptAttributeValue scriptAttributeValue = new UpdateScriptAttributeValue()
          {
            InLisID = Convert.ToInt32(childNode.Attributes["Value"].Value)
          };
          if (childNode.Attributes["IntegerValue"] != null && childNode.Attributes["IntegerValue"].Value != string.Empty)
            scriptAttributeValue.IntegerValue = (long) Convert.ToInt32(childNode.Attributes["IntegerValue"].Value);
          if (childNode.Attributes["DoubleValue"] != null && childNode.Attributes["DoubleValue"].Value != string.Empty)
            scriptAttributeValue.DoubleValue = Convert.ToDouble(childNode.Attributes["DoubleValue"].Value, (IFormatProvider) CultureInfo.InvariantCulture);
          if (childNode.Attributes["StringValue"] != null && childNode.Attributes["StringValue"].Value != string.Empty)
            scriptAttributeValue.StringValue = childNode.Attributes["StringValue"].Value;
          if (childNode.Attributes["DateValue"] != null && childNode.Attributes["DateValue"].Value != string.Empty)
            scriptAttributeValue.DateTimeValue = Convert.ToDateTime(childNode.Attributes["DateValue"].Value, (IFormatProvider) CultureInfo.InvariantCulture);
          if (childNode.Attributes["TagValue"] != null && childNode.Attributes["TagValue"].Value != string.Empty)
            scriptAttributeValue.Tag = (object) childNode.Attributes["TagValue"].Value;
          switch (attributeType.AttributeType)
          {
            case FieldTypes.ftString:
              objectList.Add((object) scriptAttributeValue.StringValue);
              continue;
            case FieldTypes.ftInteger:
              if (scriptAttributeValue.Tag != null && GuidHelper.IsGuid(scriptAttributeValue.Tag.ToString()))
              {
                Guid objectGUID = new Guid(scriptAttributeValue.Tag.ToString());
                if (ServerServices.GetService(typeof (IIDLinkTranslate)) is IIDLinkTranslate service && service.IsIDLink(attributeValues.AttributeGuid))
                {
                  IDBObject dbObject = session.GetObject(objectGUID, false);
                  if (dbObject != null)
                  {
                    objectList.Add((object) dbObject.ObjectID);
                    continue;
                  }
                  continue;
                }
                continue;
              }
              objectList.Add((object) scriptAttributeValue.IntegerValue);
              continue;
            case FieldTypes.ftDouble:
              objectList.Add((object) scriptAttributeValue.DoubleValue);
              continue;
            case FieldTypes.ftDateTime:
              objectList.Add((object) scriptAttributeValue.DateTimeValue);
              continue;
            case FieldTypes.ftShortBlob:
            case FieldTypes.ftFile:
            case FieldTypes.ftBlob:
              BlobRecord blobRecord = new BlobRecord()
              {
                FileName = scriptAttributeValue.StringValue,
                ModifyDate = scriptAttributeValue.DateTimeValue,
                RealFileSize = Convert.ToInt64(scriptAttributeValue.IntegerValue)
              };
              if (scriptAttributeValue.Tag is string && ((string) scriptAttributeValue.Tag).Length > 0)
              {
                string[] strArray = ((string) scriptAttributeValue.Tag).Split('|');
                FileInfo fileInfo = new FileInfo(Path.Combine(this._directory, strArray[0]));
                blobRecord.ArcMethod = strArray.Length == 1 ? ArcMethods.NotPacked : (ArcMethods) Convert.ToInt32(strArray[1]);
                if (fileInfo.Exists && fileInfo.Length > 0L)
                {
                  bool flag = false;
                  if (blobRecord.RealFileSize == 0L)
                  {
                    if (blobRecord.ArcMethod == ArcMethods.ZLibPacked)
                    {
                      using (MemoryStream outStream = new MemoryStream())
                      {
                        using (FileStream inStream = File.OpenRead(fileInfo.FullName))
                        {
                          ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
                          blobRecord.Data = new byte[inStream.Length];
                          inStream.Read(blobRecord.Data, 0, Convert.ToInt32(inStream.Length));
                          flag = true;
                        }
                        blobRecord.RealFileSize = outStream.Length;
                      }
                    }
                    else
                      blobRecord.RealFileSize = fileInfo.Length;
                  }
                  if (!flag)
                  {
                    using (FileStream fileStream = File.OpenRead(fileInfo.FullName))
                    {
                      blobRecord.Data = new byte[fileStream.Length];
                      fileStream.Read(blobRecord.Data, 0, Convert.ToInt32(fileStream.Length));
                    }
                  }
                }
              }
              else
              {
                blobRecord.ArcMethod = ArcMethods.NotPacked;
                blobRecord.Data = (byte[]) null;
              }
              objectList.Add((object) blobRecord);
              continue;
            case FieldTypes.ftExternalLink:
              ExternalLink externalLink = new ExternalLink()
              {
                Id = scriptAttributeValue.IntegerValue,
                Value = scriptAttributeValue.StringValue,
                BaseID = scriptAttributeValue.DoubleValue
              };
              objectList.Add((object) externalLink);
              continue;
            case FieldTypes.ftObjectLink:
              if (scriptAttributeValue.Tag != null && GuidHelper.IsGuid(scriptAttributeValue.Tag.ToString()))
              {
                IDBObject dbObject = session.GetObject(new Guid(scriptAttributeValue.Tag.ToString()), false);
                if (dbObject != null)
                {
                  objectList.Add((object) dbObject.ObjectID);
                  continue;
                }
                continue;
              }
              continue;
            case FieldTypes.ftPassword:
              Password password = new Password()
              {
                Value = scriptAttributeValue.StringValue,
                Date = scriptAttributeValue.DateTimeValue
              };
              objectList.Add((object) password);
              continue;
            case FieldTypes.ftMemo:
              if (scriptAttributeValue.StringValue.Length > 0)
              {
                FileInfo fileInfo = new FileInfo(Path.Combine(this._directory, scriptAttributeValue.StringValue));
                if (fileInfo.Exists && fileInfo.Length > 0L)
                {
                  using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate((int) fileInfo.Length))
                  {
                    StringBuilder stringBuilder = objectPoolScope.Object;
                    using (FileStream input = File.OpenRead(fileInfo.FullName))
                    {
                      using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8))
                      {
                        while (input.Position < input.Length)
                          stringBuilder.Append(binaryReader.ReadChar());
                      }
                    }
                    objectList.Add((object) stringBuilder.ToString());
                    continue;
                  }
                }
                continue;
              }
              continue;
            case FieldTypes.ftBoolean:
              objectList.Add((object) (scriptAttributeValue.IntegerValue == 1L));
              continue;
            case FieldTypes.ftMeasured:
              MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(scriptAttributeValue.StringValue);
              if (measuredValue != null)
              {
                objectList.Add((object) measuredValue);
                continue;
              }
              continue;
            case FieldTypes.ftAutoInc:
              objectList.Add((object) scriptAttributeValue.IntegerValue);
              continue;
            case FieldTypes.ftGuid:
              if (scriptAttributeValue.StringValue != string.Empty && GuidHelper.IsGuid(scriptAttributeValue.StringValue))
              {
                objectList.Add((object) new Guid(scriptAttributeValue.StringValue));
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    if (objectList.Count > 0)
      attributeValues.Values = objectList.ToArray();
    this.Value = (object) attributeValues;
  }
}
