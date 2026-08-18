// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportTaskDataV2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportTaskDataV2 : ImportTaskDataV1
{
  protected override void OnReadImportedInfo(BinaryReader reader, ImportedInfo importedInfo)
  {
    base.OnReadImportedInfo(reader, importedInfo);
    importedInfo.BaseVersionId = reader.ReadInt64();
  }

  protected override void OnWriteImportedInfo(BinaryWriter writer, ImportedInfo importedInfo)
  {
    base.OnWriteImportedInfo(writer, importedInfo);
    writer.Write(importedInfo.BaseVersionId);
  }

  protected override void OnBeforeSaveLinks(BinaryWriter writer)
  {
    base.OnBeforeSaveLinks(writer);
    writer.Write(-2);
  }
}
