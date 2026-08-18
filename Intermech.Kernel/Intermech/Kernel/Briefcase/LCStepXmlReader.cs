// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.LCStepXmlReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class LCStepXmlReader(ImportEventLog eventLog) : BriefcaseXmlReader<LCStepRecord>(eventLog)
{
  protected override string nodeName => BriefcaseConsts.XmlObjLCStepsRecordTag;

  protected override void ReadNode(LCStepRecord record, XmlTextReader reader)
  {
    switch (reader.Name)
    {
      case "F_OBJECT_ID":
        this.ReadInt64(reader, ref record.ObjectId);
        break;
      case "F_LC_STEP":
        this.ReadInt32(reader, ref record.LCStep);
        break;
      case "F_START_DATE":
        this.ReadDateTime(reader, ref record.LCStartDate);
        break;
    }
  }
}
