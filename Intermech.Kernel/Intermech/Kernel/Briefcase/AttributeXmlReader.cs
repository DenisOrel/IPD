// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.AttributeXmlReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class AttributeXmlReader(ImportEventLog eventLog) : 
  BriefcaseXmlReader<AttributeRecord>(eventLog)
{
  protected override string nodeName => BriefcaseConsts.XmlAttributeRecordTag;

  protected override void ReadNode(AttributeRecord record, XmlTextReader reader)
  {
    string name = reader.Name;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
    {
      case 81909005:
        if (!(name == "F_FILESIZE"))
          return;
        this.ReadInt64(reader, ref record.FileSize);
        return;
      case 216890129:
        if (!(name == "F_DOUBLE_VALUE"))
          return;
        this.ReadDouble(reader, ref record.DoubleValue);
        return;
      case 327871542:
        if (!(name == "F_INTEGER_VALUE"))
          return;
        this.ReadInt64(reader, ref record.IntegerValue);
        return;
      case 1005667262:
        if (!(name == "F_NOTE"))
          return;
        this.ReadString(reader, ref record.FileNote);
        return;
      case 1584087058:
        if (!(name == "F_PRJLINK_ID"))
          return;
        break;
      case 2006272319:
        if (!(name == "F_PATH2FILE"))
          return;
        this.ReadString(reader, ref record.Path2File);
        return;
      case 2061457455:
        if (!(name == "F_OBJECT_ID"))
          return;
        break;
      case 2550843615:
        if (!(name == "F_INLIST_ID"))
          return;
        this.ReadInt32(reader, ref record.InlistId);
        return;
      case 2845555317:
        if (!(name == "F_STRING_VALUE"))
          return;
        this.ReadString(reader, ref record.StringValue);
        return;
      case 3202534270:
        if (!(name == "F_ATTRIBUTE_ID"))
          return;
        this.ReadInt32(reader, ref record.AttributeId);
        return;
      case 3505611834:
        if (!(name == "F_ARC_METHOD"))
          return;
        this.ReadInt32(reader, ref record.ArcMethod);
        return;
      case 4074864360:
        if (!(name == "F_DATE_VALUE"))
          return;
        this.ReadDateTime(reader, ref record.DateValue);
        return;
      default:
        return;
    }
    this.ReadInt64(reader, ref record.AttributableId);
  }
}
