// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Receipt
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

public abstract class Receipt
{
  protected ReceiptTypes receiptType;
  protected DataTable content;

  public Guid PacketGUID { get; private set; }

  public Receipt(ReceiptTypes receiptType, bool isNew, Guid packetGuid)
  {
    this.receiptType = receiptType;
    this.PacketGUID = packetGuid;
    if (!isNew)
      return;
    this.content = this.CreateTable();
  }

  public DataTable Content => this.content;

  protected virtual DataTable CreateTable()
  {
    return new DataTable()
    {
      Columns = {
        {
          "CAPTION",
          typeof (string)
        },
        {
          "cad0001f-306c-11d8-b4e9-00304f19f545",
          typeof (string)
        },
        {
          "cad00020-306c-11d8-b4e9-00304f19f545",
          typeof (string)
        },
        {
          "F_OBJECT_TYPE",
          typeof (string)
        },
        {
          "F_VERSION_ID",
          typeof (int)
        },
        {
          "F_OBJECT_ID",
          typeof (long)
        },
        {
          "F_GUID",
          typeof (string)
        },
        {
          "F_OBJ_GUID",
          typeof (string)
        },
        {
          "F_FILENAME",
          typeof (string)
        },
        {
          "cad00770-306c-11d8-b4e9-00304f19f545",
          typeof (int)
        }
      }
    };
  }

  protected string FormingFileNames(IDBObject obj)
  {
    StringBuilder stringBuilder = new StringBuilder();
    IDBAttributeCollection attributes = obj.Attributes;
    for (int AttrIndex = 0; AttrIndex < obj.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = obj.Attributes[AttrIndex];
      if (attribute.DataType == FieldTypes.ftFile)
      {
        for (int index = 0; index < attribute.ValuesCount; ++index)
        {
          attribute.Index = index;
          if (!attribute.IsNull)
          {
            IBlobReader blobReader = attribute as IBlobReader;
            BlobInformation blobInformation = blobReader.OpenBlob(0);
            try
            {
              if (stringBuilder.Length > 0)
                stringBuilder.Append(", ");
              stringBuilder.Append($"\"{blobInformation.FileName}\"({StringsHelper.GetSizeString(blobInformation.RealFileSize)})");
            }
            finally
            {
              blobReader.CloseBlob();
            }
          }
        }
      }
    }
    return stringBuilder.ToString();
  }
}
