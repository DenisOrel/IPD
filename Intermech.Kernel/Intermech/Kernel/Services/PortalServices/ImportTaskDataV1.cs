// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportTaskDataV1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

internal class ImportTaskDataV1 : IImportTaskData
{
  public Dictionary<Guid, ImportedInfo> ReadLinks(int count, BinaryReader reader)
  {
    Dictionary<Guid, ImportedInfo> dictionary = new Dictionary<Guid, ImportedInfo>();
    for (int index = 0; index < count; ++index)
    {
      Guid guid = this.ReadGuid(reader);
      long id = reader.ReadInt64();
      long objectId = reader.ReadInt64();
      TransferedObjectCategory category = (TransferedObjectCategory) reader.ReadInt32();
      bool isNew = reader.ReadBoolean();
      SystemTypes systemType = (SystemTypes) reader.ReadInt32();
      ImportedInfo importedInfo = category != TransferedObjectCategory.Object || id != -1L || objectId != -1L ? new ImportedInfo(guid, id, objectId, category, isNew, systemType) : (ImportedInfo) new DocImportedInfo(this.ReadGuid(reader), guid);
      this.OnReadImportedInfo(reader, importedInfo);
      dictionary.Add(guid, importedInfo);
    }
    return dictionary;
  }

  protected virtual void OnReadImportedInfo(BinaryReader reader, ImportedInfo importedInfo)
  {
  }

  protected virtual void OnWriteImportedInfo(BinaryWriter writer, ImportedInfo importedInfo)
  {
  }

  public void SaveLinks(Dictionary<Guid, ImportedInfo> data, BinaryWriter writer)
  {
    this.OnBeforeSaveLinks(writer);
    if (data != null && data.Count > 0)
    {
      writer.Write(data.Count);
      foreach (KeyValuePair<Guid, ImportedInfo> keyValuePair in data)
      {
        writer.Write(keyValuePair.Key.ToString().ToCharArray());
        writer.Write(keyValuePair.Value.Id);
        writer.Write(keyValuePair.Value.ObjectId);
        writer.Write((int) keyValuePair.Value.Category);
        writer.Write(keyValuePair.Value.IsNew);
        writer.Write((int) keyValuePair.Value.SystemType);
        if (keyValuePair.Value is DocImportedInfo)
          writer.Write((keyValuePair.Value as DocImportedInfo).DocumentGuid.ToString().ToCharArray());
        this.OnWriteImportedInfo(writer, keyValuePair.Value);
      }
    }
    else
      writer.Write(0);
  }

  protected virtual void OnBeforeSaveLinks(BinaryWriter writer)
  {
  }

  protected Guid ReadGuid(BinaryReader reader)
  {
    string g = Helper.GetString(36, reader);
    return !string.IsNullOrEmpty(g) ? new Guid(g) : Guid.Empty;
  }
}
