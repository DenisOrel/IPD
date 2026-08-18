// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestSchemeObject
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class RequestSchemeObject : 
  DBObject,
  IRequestSchemeObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public RequestSchemeObject(UserSession uSession)
    : base(uSession)
  {
  }

  public RequestSchemeObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public string SchemeName
  {
    get => this.ReadSchemeName();
    set => this.WriteSchemeName(value);
  }

  public string SchemeData
  {
    get => this.ReadStringFromBlob();
    set => this.WriteStringToBlob(value);
  }

  protected void WriteStringToBlob(string value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.TransfSchemeAttrTypeID);
    if (attributeById == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      StreamWriter streamWriter = new StreamWriter((Stream) memoryStream);
      streamWriter.Write(value);
      streamWriter.Flush();
      memoryStream.Position = 0L;
      BlobInformation blobInfo = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "", ArcMethods.ZLibPacked, string.Empty);
      if (!(attributeById is IBlobWriter blobWriter) || !blobWriter.OpenBlob(blobInfo, false))
        return;
      blobWriter.WriteDataBlock(memoryStream.ToArray());
    }
  }

  protected string ReadStringFromBlob()
  {
    string str = "";
    IDBAttribute attributeById = this.GetAttributeByID(Const.TransfSchemeAttrTypeID);
    if (attributeById != null && attributeById is IBlobReader blobReader)
    {
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      try
      {
        if (blobInformation.RealFileSize > 0L)
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            byte[] buffer = blobReader.ReadDataBlock(0);
            if (buffer != null)
            {
              memoryStream.Write(buffer, 0, buffer.Length);
              memoryStream.Position = 0L;
              str = new StreamReader((Stream) memoryStream).ReadToEnd();
            }
          }
        }
      }
      finally
      {
        blobReader.CloseBlob();
      }
    }
    return str;
  }

  protected void WriteSchemeName(string value)
  {
    IDBAttribute attributeById = this.GetAttributeByID(Const.NameAttrTypeID);
    if (attributeById == null)
      return;
    attributeById.AsString = value;
  }

  protected string ReadSchemeName()
  {
    string str = "";
    IDBAttribute attributeById = this.GetAttributeByID(Const.NameAttrTypeID);
    if (attributeById != null)
      str = attributeById.AsString;
    return str;
  }
}
