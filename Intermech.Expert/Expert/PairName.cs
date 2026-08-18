// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.PairName
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System;
using System.IO;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// PairName holds different names for AttribPair - used as Hashtable value
/// </summary>
public class PairName : ICloneable
{
  public string attrShortName;
  public string attrLongName;
  public string objTypeShortName;
  public string objTypeLongName;
  public FieldTypes ft;
  public bool Multi;
  protected string realShortName;
  protected string realLongName;

  public PairName()
  {
  }

  public PairName(
    string aShortName,
    string aLongName,
    string oShortName,
    string oLongName,
    FieldTypes ft,
    bool multis)
  {
    this.attrShortName = aShortName;
    this.attrLongName = aLongName;
    this.objTypeShortName = oShortName;
    this.objTypeLongName = oLongName;
    this.ft = ft;
    this.Multi = multis;
    this.UpdateRealNames();
  }

  public PairName(ref BinaryReader br)
  {
    this.attrShortName = br.ReadString();
    this.attrLongName = br.ReadString();
    this.objTypeLongName = br.ReadString();
    this.objTypeShortName = br.ReadString();
    this.ft = (FieldTypes) br.ReadInt32();
    this.Multi = br.ReadBoolean();
    this.UpdateRealNames();
  }

  internal void UpdateRealNames()
  {
    this.realLongName = !(this.objTypeLongName != "") ? this.attrLongName : $"{this.objTypeLongName}.{this.attrLongName}";
    if (this.objTypeShortName != "")
    {
      if (this.attrShortName != "")
        this.realShortName = $"{this.objTypeShortName}.{this.attrShortName}";
      else
        this.realShortName = $"{this.objTypeShortName}.<{this.attrLongName}>";
    }
    else if (this.objTypeLongName != "")
    {
      if (this.attrShortName != "")
        this.realShortName = $"<{this.objTypeLongName}>.{this.attrShortName}";
      else
        this.realShortName = $"<{this.objTypeLongName}>.<{this.attrLongName}>";
    }
    else if (this.attrShortName != "")
      this.realShortName = this.attrShortName;
    else
      this.realShortName = $"<{this.attrLongName}>";
  }

  public static string GetShortFTDescr(FieldTypes ft)
  {
    switch (ft)
    {
      case FieldTypes.ftUnknown:
        return LocalizationHolder.rm.GetString("Expert_36");
      case FieldTypes.ftString:
        return LocalizationHolder.rm.GetString("Expert_37");
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        return LocalizationHolder.rm.GetString("Expert_38");
      case FieldTypes.ftDouble:
        return LocalizationHolder.rm.GetString("Expert_39");
      case FieldTypes.ftDateTime:
        return LocalizationHolder.rm.GetString("Expert_41");
      case FieldTypes.ftShortBlob:
        return LocalizationHolder.rm.GetString("Expert_42");
      case FieldTypes.ftFile:
        return LocalizationHolder.rm.GetString("Expert_43");
      case FieldTypes.ftExternalLink:
        return LocalizationHolder.rm.GetString("Expert_44");
      case FieldTypes.ftObjectLink:
        return LocalizationHolder.rm.GetString("Expert_45");
      case FieldTypes.ftPassword:
        return LocalizationHolder.rm.GetString("Expert_46");
      case FieldTypes.ftMemo:
        return LocalizationHolder.rm.GetString("Expert_47");
      case FieldTypes.ftBlob:
        return "BLOB";
      case FieldTypes.ftBoolean:
        return LocalizationHolder.rm.GetString("Expert_48");
      case FieldTypes.ftMeasured:
        return LocalizationHolder.rm.GetString("Expert_40");
      case FieldTypes.ftSystem:
        return LocalizationHolder.rm.GetString("Expert_49");
      case FieldTypes.ftObjectLinkByID:
        return LocalizationHolder.rm.GetString("Expert_45id");
      default:
        return "";
    }
  }

  public static string GetLongFTDescr(FieldTypes ft)
  {
    switch (ft)
    {
      case FieldTypes.ftUnknown:
        return LocalizationHolder.rm.GetString("Expert_184");
      case FieldTypes.ftString:
        return LocalizationHolder.rm.GetString("Expert_185");
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        return LocalizationHolder.rm.GetString("Expert_186");
      case FieldTypes.ftDouble:
        return LocalizationHolder.rm.GetString("Expert_187");
      case FieldTypes.ftDateTime:
        return LocalizationHolder.rm.GetString("Expert_189");
      case FieldTypes.ftShortBlob:
        return LocalizationHolder.rm.GetString("Expert_190");
      case FieldTypes.ftFile:
        return LocalizationHolder.rm.GetString("Expert_191");
      case FieldTypes.ftExternalLink:
        return LocalizationHolder.rm.GetString("Expert_192");
      case FieldTypes.ftObjectLink:
        return LocalizationHolder.rm.GetString("Expert_193");
      case FieldTypes.ftPassword:
        return LocalizationHolder.rm.GetString("Expert_194");
      case FieldTypes.ftMemo:
        return LocalizationHolder.rm.GetString("Expert_195");
      case FieldTypes.ftBlob:
        return LocalizationHolder.rm.GetString("Expert_196");
      case FieldTypes.ftBoolean:
        return LocalizationHolder.rm.GetString("Expert_197");
      case FieldTypes.ftMeasured:
        return LocalizationHolder.rm.GetString("Expert_188");
      case FieldTypes.ftSystem:
        return LocalizationHolder.rm.GetString("Expert_198");
      case FieldTypes.ftObjectLinkByID:
        return LocalizationHolder.rm.GetString("Expert_193id");
      default:
        return "";
    }
  }

  public string GetShortTypeDescr()
  {
    string shortFtDescr = PairName.GetShortFTDescr(this.ft);
    return this.Multi ? $"{{{shortFtDescr}}}" : shortFtDescr;
  }

  public string ShortName
  {
    get => this.realShortName;
    set => this.realShortName = value;
  }

  public string LongName
  {
    get => this.realLongName;
    set => this.realLongName = value;
  }

  public string DefShortName => $"{this.objTypeShortName}.{this.attrShortName}";

  public string DefLongName => $"{this.objTypeLongName}.{this.attrLongName}";

  public DataType GetDataType()
  {
    return this.Multi ? DataType.Packet : DataTypeConvertor.AttrType2DataType(this.ft);
  }

  public void Save(ref BinaryWriter bw)
  {
    bw.Write(this.attrShortName);
    bw.Write(this.attrLongName);
    if (this.objTypeLongName == null)
      this.objTypeLongName = "";
    if (this.objTypeShortName == null)
      this.objTypeShortName = "";
    bw.Write(this.objTypeLongName);
    bw.Write(this.objTypeShortName);
    bw.Write((int) this.ft);
    bw.Write(this.Multi);
  }

  public object Clone()
  {
    return (object) new PairName(this.attrShortName, this.attrLongName, this.objTypeShortName, this.objTypeLongName, this.ft, this.Multi);
  }
}
