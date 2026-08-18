// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RemarkRecordHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal abstract class RemarkRecordHandler
{
  protected int attributeID;
  protected List<RemarkRecord> records;

  public RemarkRecordHandler()
    : this(0)
  {
  }

  public RemarkRecordHandler(int attributeID)
  {
    this.attributeID = attributeID;
    this.records = new List<RemarkRecord>();
  }

  public abstract bool HandleRecord(RemarkRecord record, IDBObject obj);

  public abstract void OnComplete(IDBObject obj);
}
