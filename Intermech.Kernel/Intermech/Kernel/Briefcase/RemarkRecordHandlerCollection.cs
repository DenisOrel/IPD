// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RemarkRecordHandlerCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class RemarkRecordHandlerCollection : List<RemarkRecordHandler>
{
  public bool HandleRecord(RemarkRecord record, IDBObject obj)
  {
    foreach (RemarkRecordHandler remarkRecordHandler in (List<RemarkRecordHandler>) this)
    {
      if (remarkRecordHandler.HandleRecord(record, obj))
        return true;
    }
    return false;
  }

  public void OnComplete(IDBObject obj)
  {
    foreach (RemarkRecordHandler remarkRecordHandler in (List<RemarkRecordHandler>) this)
      remarkRecordHandler.OnComplete(obj);
  }
}
