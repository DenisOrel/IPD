// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.UnitSettingsFile
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class UnitSettingsFile
{
  [CanBeNull]
  public static Dictionary<int, OfficeDocumentTypeSettingsForUnit> Read([NotNull] IDBObject unitContainer)
  {
    IDBAttribute attributeById = unitContainer.GetAttributeByID(OfficeConsts.AttrUnitSettingsID);
    if (attributeById != null && !attributeById.IsNull)
    {
      IBlobReader blobReader = (IBlobReader) attributeById;
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
                return (Dictionary<int, OfficeDocumentTypeSettingsForUnit>) new BinaryFormatter().Deserialize((Stream) memoryStream);
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
    return (Dictionary<int, OfficeDocumentTypeSettingsForUnit>) null;
  }

  public static void Save(
    [CanBeNull] Dictionary<int, OfficeDocumentTypeSettingsForUnit> settings,
    [NotNull] IDBObject unitContainer)
  {
    if (settings == null)
      return;
    IDBAttribute dbAttribute = unitContainer.GetAttributeByID(OfficeConsts.AttrUnitSettingsID) ?? unitContainer.Attributes.AddAttribute(OfficeConsts.AttrUnitSettingsID, false);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) memoryStream, (object) settings);
      memoryStream.Position = 0L;
      using (MemoryStream outStream = new MemoryStream())
      {
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) memoryStream, 9);
        IBlobWriter blobWriter = (IBlobWriter) dbAttribute;
        BlobInformation blobInfo = new BlobInformation(memoryStream.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, "Настройки канцелярии");
        if (!blobWriter.OpenBlob(blobInfo, false))
          return;
        blobWriter.WriteDataBlock(outStream.ToArray());
      }
    }
  }
}
