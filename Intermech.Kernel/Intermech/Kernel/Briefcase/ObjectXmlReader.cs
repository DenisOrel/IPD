// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ObjectXmlReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class ObjectXmlReader(ImportEventLog eventLog) : BriefcaseXmlReader<ObjectRecord>(eventLog)
{
  protected override string nodeName => BriefcaseConsts.XmlObjectRecordTag;

  protected override void ReadNode(ObjectRecord record, XmlTextReader reader)
  {
    string name = reader.Name;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
    {
      case 481223550:
        if (!(name == "F_LC_STEP"))
          break;
        this.ReadInt32(reader, ref record.Lc_step);
        break;
      case 513164297:
        if (!(name == "F_PROJECT_ID"))
          break;
        this.ReadInt64(reader, ref record.ProjectId);
        break;
      case 583133738:
        if (!(name == "F_IDGUID"))
          break;
        this.ReadGuid(reader, ref record.IdGuid);
        break;
      case 731486661:
        if (!(name == "CAPTION"))
          break;
        this.ReadString(reader, ref record.Caption);
        break;
      case 839465890:
        if (!(name == "F_MODIFICATION_ID"))
          break;
        this.ReadInt64(reader, ref record.ModificationID);
        break;
      case 925427746:
        if (!(name == "F_CHKOUT_BY"))
          break;
        this.ReadInt64(reader, ref record.ChkoutBy);
        break;
      case 1232060511:
        if (!(name == "F_ID"))
          break;
        this.ReadInt64(reader, ref record.Id);
        break;
      case 1264598014:
        if (!(name == "F_VERSION_ID"))
          break;
        this.ReadInt32(reader, ref record.VersionId);
        break;
      case 1282030694:
        if (!(name == "F_PROJECTGUID"))
          break;
        this.ReadGuid(reader, ref record.ProjectGuid);
        break;
      case 1307812216:
        if (!(name == "F_LEVEL_ID"))
          break;
        this.ReadInt32(reader, ref record.LevelId);
        break;
      case 1402509459:
        if (!(name == "F_CHKOUTGUID"))
          break;
        this.ReadGuid(reader, ref record.ChkoutGuid);
        break;
      case 1734964382:
        if (!(name == "F_CREATOR_ID"))
          break;
        this.ReadInt64(reader, ref record.CreatorID);
        break;
      case 2061457455:
        if (!(name == "F_OBJECT_ID"))
          break;
        this.ReadInt64(reader, ref record.Object_id);
        break;
      case 2470829812:
        if (!(name == "F_OBJ_CREATE"))
          break;
        this.ReadDateTime(reader, ref record.ObjCreate);
        break;
      case 2609587561:
        if (!(name == "F_MODIFY_DATE"))
          break;
        this.ReadDateTime(reader, ref record.ModifyDate);
        break;
      case 3089607204:
        if (!(name == "F_OWNERGUID"))
          break;
        this.ReadGuid(reader, ref record.OwnerGuid);
        break;
      case 3176900722:
        if (!(name == "F_OBJECT_TYPE"))
          break;
        this.ReadInt32(reader, ref record.ObjectType);
        break;
      case 3214915211:
        if (!(name == "F_OWNER_ID"))
          break;
        this.ReadInt64(reader, ref record.OwnerId);
        break;
      case 3643405172:
        if (!(name == "F_OBJECT_VER_TYPE"))
          break;
        this.ReadInt32(reader, ref record.ObjectVerType);
        break;
      case 3668470296:
        if (!(name == "F_OBJECTGUID"))
          break;
        this.ReadGuid(reader, ref record.ObjectGuid);
        break;
      case 3901475796:
        if (!(name == "F_ACCESS"))
          break;
        this.ReadInt32(reader, ref record.AccessLevel);
        break;
    }
  }
}
