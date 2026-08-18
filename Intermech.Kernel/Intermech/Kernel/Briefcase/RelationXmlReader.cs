// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RelationXmlReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class RelationXmlReader(ImportEventLog eventLog) : BriefcaseXmlReader<RelationRecord>(eventLog)
{
  protected override string nodeName => BriefcaseConsts.XmlRelationRecordTag;

  protected override void ReadNode(RelationRecord record, XmlTextReader reader)
  {
    string name = reader.Name;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
    {
      case 208998511:
        if (!(name == "F_RELATION_TYPE"))
          break;
        this.ReadInt32(reader, ref record.RelationType);
        break;
      case 1022271103:
        if (!(name == "F_PROJ_ID"))
          break;
        this.ReadGuid(reader, ref record.ProjId);
        break;
      case 1111463170:
        if (!(name == "F_PRJ_GUID"))
          break;
        this.ReadGuid(reader, ref record.PrjLinkGuid);
        break;
      case 1584087058:
        if (!(name == "F_PRJLINK_ID"))
          break;
        this.ReadInt64(reader, ref record.PrjLinkId);
        break;
      case 1993068110:
        if (!(name == "F_REL_CREATOR"))
          break;
        this.ReadInt64(reader, ref record.CreatorID);
        break;
      case 2317509993:
        if (!(name == "F_PART_ID"))
          break;
        this.ReadGuid(reader, ref record.PartId);
        break;
      case 3866071311:
        if (!(name == "F_CREATE_DATE"))
          break;
        this.ReadDateTime(reader, ref record.CreateDate);
        break;
    }
  }
}
