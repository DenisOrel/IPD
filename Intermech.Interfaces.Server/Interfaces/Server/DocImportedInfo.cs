// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DocImportedInfo
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public class DocImportedInfo : ImportedInfo
{
  public Guid DocumentGuid;

  public DocImportedInfo(Guid MainDoc, Guid guid)
    : base(guid, -1L, -1L, TransferedObjectCategory.Object, true)
  {
    this.DocumentGuid = MainDoc;
  }
}
