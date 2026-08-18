// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RedLineRemarkRecordHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel.Briefcase;

internal sealed class RedLineRemarkRecordHandler : RemarkRecordHandler
{
  public RedLineRemarkRecordHandler()
    : base(MetaDataHelper.GetAttributeTypeID("cad0036f-306c-11d8-b4e9-00304f19f545"))
  {
  }

  public override bool HandleRecord(RemarkRecord record, IDBObject obj) => false;

  public override void OnComplete(IDBObject obj)
  {
  }
}
