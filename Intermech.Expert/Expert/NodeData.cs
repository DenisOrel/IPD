// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.NodeData
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Localization;
using System;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Storage of all data for a tree node</summary>
public class NodeData
{
  public int modTag = -1;
  public int opTag = -1;
  public ModParm mods;
  public OpParm ops;

  public NodeData()
  {
  }

  public NodeData(XmlNode elem, int modTag, int opTag)
  {
    this.modTag = modTag;
    this.opTag = opTag;
    Type modNodeType = NodeData.GetModNodeType(modTag);
    if (modNodeType != (Type) null)
      this.mods = (ModParm) Activator.CreateInstance(modNodeType);
    Type opNodeType = NodeData.GetOpNodeType(opTag);
    if (opNodeType != (Type) null)
      this.ops = (OpParm) Activator.CreateInstance(opNodeType);
    if (!elem.HasChildNodes)
      return;
    XmlNode childNode1 = elem.ChildNodes[0];
    if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Mod-Parms" && this.mods != null)
      this.mods.LoadFromXML(childNode1, modTag);
    if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Op-Parms")
    {
      this.ops.LoadFromXML(childNode1, opTag);
    }
    else
    {
      if (elem.ChildNodes.Count <= 1)
        return;
      XmlNode childNode2 = elem.ChildNodes[1];
      if (childNode2.NodeType != XmlNodeType.Element || !(childNode2.Name == "Op-Parms"))
        return;
      this.ops.LoadFromXML(childNode2, opTag);
    }
  }

  public void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteAttributeString("modTag", Convert.ToString(this.modTag));
    writer.WriteAttributeString("opTag", Convert.ToString(this.opTag));
    if (this.mods != null)
    {
      writer.WriteStartElement("Mod-Parms");
      this.mods.WriteToXML(ref writer);
      writer.WriteEndElement();
    }
    if (this.ops == null)
      return;
    writer.WriteStartElement("Op-Parms");
    this.ops.WriteToXML(ref writer);
    writer.WriteEndElement();
  }

  public static Type GetModNodeType(int modTag)
  {
    if (modTag < 0)
      return (Type) null;
    if (modTag <= 5)
      return typeof (ModParmFormula);
    if (modTag == 6)
      return typeof (ModParmLoop);
    if (modTag == 7 || modTag == 8)
      return typeof (ModParmSort);
    return modTag == 68 ? typeof (ModParmVersion) : (Type) null;
  }

  public static Type GetOpNodeType(int opTag)
  {
    if (opTag < 0)
      return (Type) null;
    if (opTag >= 9 && opTag <= 14)
      return typeof (OpParmObject);
    if (opTag >= 15 && opTag <= 17 || opTag == 39)
      return typeof (OpParmCond);
    switch (opTag)
    {
      case 18:
        return typeof (OpParmSetting);
      case 19:
        return typeof (OpParmFillFld);
      case 20:
        return typeof (OpParmCreateFld);
      case 21:
        return typeof (OpParmSelFld);
      default:
        if (opTag == 24 || opTag == 51)
          return typeof (OpParmType);
        if (opTag >= 25 && opTag <= 27)
          return typeof (OpParmExpObj);
        switch (opTag)
        {
          case 32 /*0x20*/:
            return typeof (OpParmDocControl);
          case 40:
            return typeof (OpParmObject);
          case 43:
            return typeof (OpParmUserProc);
          case 44:
            return typeof (OpParmVersionRule);
          case 49:
            return typeof (OpCreateDoc);
          case 50:
            return typeof (OpCreateComplect);
          case 53:
            return typeof (OpParmGlobRoot);
          case 54:
            return typeof (OpParmGlobForType);
          case 66:
            return typeof (OpParmTiLink);
          default:
            return (Type) null;
        }
    }
  }

  public string GetShortMod() => NodeData.GetShortMod(this.modTag, this.mods);

  public static string GetShortMod(int modTag, ModParm mods)
  {
    string str1 = "";
    string shortMod;
    switch (modTag)
    {
      case 0:
      case 1:
      case 2:
      case 3:
      case 4:
      case 5:
        ModParmFormula modParmFormula = mods as ModParmFormula;
        switch (modTag)
        {
          case 0:
            str1 = LocalizationHolder.rm.GetString("Expert_130");
            break;
          case 1:
            str1 = LocalizationHolder.rm.GetString("Expert_131");
            break;
          case 2:
            str1 = LocalizationHolder.rm.GetString("Expert_132");
            break;
          case 3:
            str1 = LocalizationHolder.rm.GetString("Expert_133");
            break;
          case 4:
            str1 = LocalizationHolder.rm.GetString("Expert_134");
            break;
          case 5:
            str1 = LocalizationHolder.rm.GetString("Expert_135");
            break;
        }
        shortMod = (modParmFormula.tf == null ? str1 + "???" : str1 + modParmFormula.tf.Text) + ")";
        break;
      case 6:
        ModParmLoop modParmLoop = mods as ModParmLoop;
        if (modParmLoop.whileLoop)
        {
          string str2 = LocalizationHolder.rm.GetString("Expert_136");
          shortMod = (modParmLoop.tf == null ? str2 + "???" : str2 + modParmLoop.tf.Text) + ")";
          break;
        }
        string str3 = LocalizationHolder.rm.GetString("Expert_137");
        string str4;
        if (modParmLoop.attrGUID != "" && modParmLoop.tf != null)
          str4 = str3 + modParmLoop.attrText + LocalizationHolder.rm.GetString("Expert_138") + Convert.ToString(modParmLoop.startWith) + LocalizationHolder.rm.GetString("Expert_139") + modParmLoop.tf.Text;
        else
          str4 = str3 + "???";
        shortMod = str4 + ")";
        break;
      case 7:
        ModParmSort modParmSort1 = mods as ModParmSort;
        string str5 = LocalizationHolder.rm.GetString("Expert_140");
        if (modParmSort1.sortAttrs != null && modParmSort1.sortAttrs.Count > 0)
        {
          for (int index = 0; index < modParmSort1.sortAttrs.Count; ++index)
          {
            if (index > 0)
              str5 += ", ";
            str5 += modParmSort1.sortAttrTexts[index];
          }
        }
        else
          str5 += "???";
        shortMod = str5 + ")";
        break;
      case 8:
        ModParmSort modParmSort2 = mods as ModParmSort;
        string str6 = LocalizationHolder.rm.GetString("Expert_141");
        if (modParmSort2.sortAttrs != null && modParmSort2.sortAttrs.Count > 0)
        {
          for (int index = 0; index < modParmSort2.sortAttrs.Count; ++index)
          {
            if (index > 0)
              str6 += ", ";
            str6 += modParmSort2.sortAttrTexts[index];
          }
        }
        else
          str6 += "???";
        shortMod = str6 + "); ";
        if (modParmSort2.groupAttrs != null && modParmSort2.groupAttrs.Count > 0)
        {
          string str7 = shortMod + LocalizationHolder.rm.GetString("Expert_142");
          for (int index = 0; index < modParmSort2.groupAttrs.Count; ++index)
          {
            if (index > 0)
              str7 += ", ";
            str7 += modParmSort2.groupAttrTexts[index];
          }
          shortMod = str7 + ")";
          break;
        }
        break;
      case 68:
        ModParmVersion modParmVersion = mods as ModParmVersion;
        shortMod = !modParmVersion.forAllVersions ? LocalizationHolder.rm.GetString("Expert_272") : LocalizationHolder.rm.GetString("Expert_273");
        switch (modParmVersion.sortMode)
        {
          case VerSort.VerId:
            shortMod += LocalizationHolder.rm.GetString("Expert_274");
            break;
          case VerSort.LCStepId:
            shortMod += LocalizationHolder.rm.GetString("Expert_275");
            break;
          case VerSort.LevelId:
            shortMod += LocalizationHolder.rm.GetString("Expert_276");
            break;
          case VerSort.CreationDate:
            shortMod += LocalizationHolder.rm.GetString("Expert_277");
            break;
          case VerSort.ModifyDate:
            shortMod += LocalizationHolder.rm.GetString("Expert_278");
            break;
          case VerSort.ModGroupId:
            shortMod += LocalizationHolder.rm.GetString("Expert_279");
            break;
        }
        break;
      default:
        return LocalizationHolder.rm.GetString("Expert_143");
    }
    return shortMod;
  }

  public string GetShortOp() => NodeData.GetShortOp(this.opTag, this.ops);

  public static string GetShortOp(int opTag, OpParm ops)
  {
    string shortOp = "";
    switch (opTag)
    {
      case 9:
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
        OpParmObject opParmObject = ops as OpParmObject;
        switch (opTag - 9)
        {
          case 0:
            shortOp = LocalizationHolder.rm.GetString("Expert_144");
            break;
          case 1:
            shortOp = LocalizationHolder.rm.GetString("Expert_145");
            break;
          case 2:
            shortOp = LocalizationHolder.rm.GetString("Expert_146");
            break;
          case 3:
            shortOp = LocalizationHolder.rm.GetString("Expert_147");
            break;
          case 4:
            shortOp = LocalizationHolder.rm.GetString("Expert_148");
            break;
          case 5:
            shortOp = LocalizationHolder.rm.GetString("Expert_149");
            break;
        }
        if (opParmObject.excerptName != "")
          shortOp = $"{shortOp}[{opParmObject.excerptName}] ";
        if (opParmObject.cond != null)
          shortOp = $"{shortOp}({opParmObject.cond.Text}) ";
        if (opParmObject.Dups)
        {
          shortOp += LocalizationHolder.rm.GetString("Expert_150");
          break;
        }
        break;
      case 15:
      case 16 /*0x10*/:
      case 17:
      case 39:
        OpParmCond opParmCond = ops as OpParmCond;
        switch (opTag - 15)
        {
          case 0:
            shortOp = LocalizationHolder.rm.GetString("Expert_151");
            break;
          case 1:
            shortOp = LocalizationHolder.rm.GetString("Expert_152");
            break;
          case 2:
            shortOp = LocalizationHolder.rm.GetString("Expert_153");
            break;
          default:
            if (opTag == 39)
            {
              shortOp = LocalizationHolder.rm.GetString("Expert_154");
              break;
            }
            break;
        }
        if (opParmCond.cond != null)
        {
          shortOp = $"{shortOp}{LocalizationHolder.rm.GetString("Expert_155")}{opParmCond.cond.Text})";
          break;
        }
        break;
      case 18:
        OpParmSetting opParmSetting = ops as OpParmSetting;
        if (opParmSetting.attrText != "")
        {
          string str1 = LocalizationHolder.rm.GetString("Expert_162");
          if (opParmSetting.objTypeText != "")
            str1 = $"{str1}{opParmSetting.objTypeText}.";
          shortOp = $"{str1}{opParmSetting.attrText}=";
          string str2 = "";
          if (opParmSetting.tf != null)
            str2 = opParmSetting.tf.Text;
          switch (opParmSetting.setKind)
          {
            case ExpertSettingKind.setKindValue:
              shortOp += str2;
              break;
            case ExpertSettingKind.setKindByTable:
              shortOp += LocalizationHolder.rm.GetString("Expert_169");
              break;
            case ExpertSettingKind.setKindSum:
              shortOp = $"{shortOp}{LocalizationHolder.rm.GetString("Expert_163")}{str2})";
              break;
            case ExpertSettingKind.setKindAverage:
              shortOp = $"{shortOp}{LocalizationHolder.rm.GetString("Expert_164")}{str2})";
              break;
            case ExpertSettingKind.setKindNumber:
              shortOp += LocalizationHolder.rm.GetString("Expert_165");
              break;
            case ExpertSettingKind.setKindMinimum:
              shortOp = $"{shortOp}{LocalizationHolder.rm.GetString("Expert_166")}{str2})";
              break;
            case ExpertSettingKind.setKindMaximum:
              shortOp = $"{shortOp}{LocalizationHolder.rm.GetString("Expert_167")}{str2})";
              break;
            case ExpertSettingKind.setKindList:
              shortOp = $"{shortOp}{LocalizationHolder.rm.GetString("Expert_168")}{str2})";
              break;
          }
        }
        else
          break;
        break;
      case 19:
        OpParmFillFld opParmFillFld = ops as OpParmFillFld;
        if (opParmFillFld.FldID != "" && (opParmFillFld.attrGUID != "" || opParmFillFld.tf != null))
        {
          string str3 = LocalizationHolder.rm.GetString("Expert_170") + opParmFillFld.FldID;
          if (opParmFillFld.AddAttrText != "")
            str3 = $"{str3}+{opParmFillFld.AddAttrText}";
          string str4 = str3 + ") = ";
          if (opParmFillFld.fillFormula() && opParmFillFld.tf != null)
          {
            shortOp = str4 + opParmFillFld.tf.Text;
            break;
          }
          if (opParmFillFld.fillAttr())
            str4 += "\"";
          if (opParmFillFld.objTypeText != "")
            str4 = $"{str4}{opParmFillFld.objTypeText}.";
          shortOp = $"{str4}{opParmFillFld.attrText}\"";
          break;
        }
        break;
      case 20:
        OpParmCreateFld opParmCreateFld = ops as OpParmCreateFld;
        if (opParmCreateFld.FldID != "")
        {
          string str5 = LocalizationHolder.rm.GetString("Expert_171") + opParmCreateFld.FldID;
          if (opParmCreateFld.AddAttrText != "")
            str5 = $"{str5}+{opParmCreateFld.AddAttrText}";
          string str6 = str5 + ")";
          if (opParmCreateFld.SaveIDAttrGUID != "")
            str6 = $"{str6}-> \"{opParmCreateFld.SaveIDAttrText}\"";
          shortOp = !opParmCreateFld.makeNewCurrent ? str6 + LocalizationHolder.rm.GetString("Expert_174") : (!opParmCreateFld.fillChildren ? str6 + LocalizationHolder.rm.GetString("Expert_173") : str6 + LocalizationHolder.rm.GetString("Expert_172"));
          break;
        }
        break;
      case 21:
        OpParmSelFld opParmSelFld = ops as OpParmSelFld;
        string str7 = LocalizationHolder.rm.GetString("Expert_175");
        shortOp = opParmSelFld.tf == null ? $"{str7}{opParmSelFld.FldId})" : $"{str7}{opParmSelFld.tf.Text})";
        break;
      case 24:
      case 51:
        OpParmType opParmType = ops as OpParmType;
        shortOp = $"{(opParmType.cond == null ? "" : $"{LocalizationHolder.rm.GetString("Expert_176")}{opParmType.cond.Text},")}{LocalizationHolder.rm.GetString("Expert_177")}{opParmType.objTypeText})";
        break;
      case 25:
      case 26:
      case 27:
        OpParmExpObj opParmExpObj = ops as OpParmExpObj;
        string str8 = (opParmExpObj.cond == null ? "" : $"{LocalizationHolder.rm.GetString("Expert_178")}{opParmExpObj.cond.Text},") + LocalizationHolder.rm.GetString("Expert_179");
        switch (opTag)
        {
          case 25:
            str8 += LocalizationHolder.rm.GetString("Expert_180");
            break;
          case 26:
            str8 += LocalizationHolder.rm.GetString("Expert_181");
            break;
          case 27:
            str8 += LocalizationHolder.rm.GetString("Expert_182");
            break;
        }
        shortOp = $"{str8}{opParmExpObj.objTypeText}\"";
        break;
      case 32 /*0x20*/:
        shortOp = LocalizationHolder.rm.GetString("Expert_160");
        break;
      case 40:
        shortOp = LocalizationHolder.rm.GetString("Expert_156");
        break;
      case 43:
        OpParmUserProc opParmUserProc = ops as OpParmUserProc;
        switch (opParmUserProc.type)
        {
          case ExpertCalling.callProc:
            shortOp = $"{LocalizationHolder.rm.GetString("Expert_157")}{opParmUserProc.procName}\"";
            break;
          case ExpertCalling.callUserProc:
            shortOp = $"{LocalizationHolder.rm.GetString("Expert_158")}{opParmUserProc.procName}\"";
            break;
          case ExpertCalling.callScript:
            shortOp = $"{LocalizationHolder.rm.GetString("Expert_159")}{opParmUserProc.procName}\"";
            break;
          case ExpertCalling.callScenario:
            shortOp = $"{LocalizationHolder.rm.GetString("Expert_216")}{opParmUserProc.procName}\" {LocalizationHolder.rm.GetString("Expert_217")}{(object) opParmUserProc.parm1})";
            break;
        }
        break;
      case 44:
        OpParmVersionRule opParmVersionRule = ops as OpParmVersionRule;
        shortOp = LocalizationHolder.rm.GetString("Expert_161");
        if (opParmVersionRule.ruleCapt != "")
        {
          shortOp = $"{shortOp} ({opParmVersionRule.ruleCapt})";
          break;
        }
        break;
      case 49:
        OpCreateDoc opCreateDoc = ops as OpCreateDoc;
        shortOp = $"{LocalizationHolder.rm.GetString("Expert_201")} ({opCreateDoc.objTypeText})";
        break;
      case 50:
        OpCreateComplect opCreateComplect = ops as OpCreateComplect;
        shortOp = $"{LocalizationHolder.rm.GetString("Expert_202")} ({opCreateComplect.objTypeText})";
        break;
      case 53:
        shortOp = LocalizationHolder.rm.GetString("Expert_228");
        break;
      case 54:
        OpParmGlobForType opParmGlobForType = ops as OpParmGlobForType;
        shortOp = LocalizationHolder.rm.GetString("Expert_229") + opParmGlobForType.GetObjTypesStr();
        break;
      case 66:
        OpParmTiLink opParmTiLink = ops as OpParmTiLink;
        shortOp = string.Format(LocalizationHolder.rm.GetString("Expert_267"), (object) opParmTiLink.TiDocTypeName);
        break;
    }
    return shortOp;
  }
}
