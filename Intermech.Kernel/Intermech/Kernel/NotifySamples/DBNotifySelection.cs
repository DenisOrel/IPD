// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.NotifySamples.DBNotifySelection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel.NotifySamples;

internal sealed class DBNotifySelection(UserSession uSession, DataTable objectsTable) : DBSelection(uSession, objectsTable)
{
  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    if (attribute.AttributeID == NotifySamplesConst.NotifyPeriodAttr || attribute.AttributeID == NotifySamplesConst.SampleConditionsAttr || attribute.AttributeID == NotifySamplesConst.NotifyModeAttr)
      this.UserSession.SetNotifySamplesUpdateFlag();
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }

  protected override void DoPurge(long DeleteMode)
  {
    this.UserSession.SetNotifySamplesUpdateFlag();
    base.DoPurge(DeleteMode);
  }
}
