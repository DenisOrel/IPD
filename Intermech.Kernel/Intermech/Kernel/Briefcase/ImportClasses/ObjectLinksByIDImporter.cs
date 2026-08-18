// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportClasses.ObjectLinksByIDImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase.ImportClasses;

internal class ObjectLinksByIDImporter(
  UserSession session,
  List<IDСorresponds> importingObjects,
  List<long> recordKindStated,
  Action<string> addIntoLogFunc) : ObjectLinksImporter(session, importingObjects, recordKindStated, addIntoLogFunc)
{
  protected override long CheckBeforeImport(LinksBase link)
  {
    if (link.OldLinkID == 0L)
      return 0;
    IDСorresponds idСorresponds = this.importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceID == link.OldLinkID));
    if (idСorresponds != null)
      return idСorresponds.HostID;
    this.addIntoLogFunc(string.Format(LocalizationHolder.rm.GetString("Kernel_300"), (object) link.OldLinkID));
    return 0;
  }
}
