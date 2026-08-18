// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.TempFormula
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// TempFormula class is a temporary placeholder for editor dialog
/// </summary>
[Serializable]
public class TempFormula : ISaveLoad, ISerializable, ICloneable
{
  public List<Token> infixForm = new List<Token>();
  public List<Token> postfixForm = new List<Token>();
  public bool Cond = true;
  public bool DropMeasure;
  public bool AutoConvert = true;
  public DataType resType = DataType.Boolean;
  public bool isArray;
  public List<AttribPair> usedAttrs;
  public List<PairName> pairNames;
  public List<string> attrGUIDs;
  public List<string> objTypeGUIDs;
  public List<long> objectLinks;
  public static readonly string FormulaFormat = "Expert Formula Format";
  public bool CanReturnEmpty = true;
  public bool StringWasConverted;
  private List<int> infixIndex = new List<int>();

  public TempFormula(bool needInit = false)
  {
    if (!needInit)
      return;
    this.Init();
  }

  public TempFormula(DataType resType, bool needInit = false)
  {
    this.Cond = false;
    this.resType = resType;
    if (!needInit)
      return;
    this.Init();
  }

  public void Init()
  {
    this.usedAttrs = new List<AttribPair>();
    this.pairNames = new List<PairName>();
    this.objectLinks = new List<long>();
    this.attrGUIDs = new List<string>();
    this.objTypeGUIDs = new List<string>();
  }

  public void Copy(TempFormula other)
  {
    this.Clear();
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (BinaryWriter bw = new BinaryWriter((Stream) memoryStream))
      {
        other.Save(bw);
        memoryStream.Position = 0L;
        using (BinaryReader br = new BinaryReader((Stream) memoryStream))
          this.Load(br, ExpertConsts.FormulaVersion);
      }
    }
    this.Cond = other.Cond;
    for (int index = 0; index < other.usedAttrs.Count; ++index)
      this.usedAttrs.Add(other.usedAttrs[index].Clone() as AttribPair);
  }

  public Token this[int index]
  {
    get => index < 0 || index >= this.infixForm.Count ? (Token) null : this.infixForm[index];
    set
    {
      if (index < 0 || index >= this.infixForm.Count)
        return;
      this.infixForm[index] = value;
    }
  }

  public int Count => this.infixForm.Count;

  public void Clear()
  {
    this.infixForm.Clear();
    this.postfixForm.Clear();
    if (this.usedAttrs != null)
      this.usedAttrs.Clear();
    if (this.pairNames != null)
      this.pairNames.Clear();
    if (this.objectLinks != null)
      this.objectLinks.Clear();
    if (this.attrGUIDs != null)
      this.attrGUIDs.Clear();
    if (this.objTypeGUIDs == null)
      return;
    this.objTypeGUIDs.Clear();
  }

  public string Text
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.infixForm.Count; ++index)
        stringBuilder.Append(this.infixForm[index].text);
      return stringBuilder.ToString();
    }
  }

  [DebuggerStepThrough]
  public override string ToString() => this.TrueText();

  [DebuggerStepThrough]
  public string TrueText()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.infixForm.Count; ++index)
      stringBuilder.Append(this.infixForm[index].trueText);
    return stringBuilder.ToString();
  }

  public string FullText()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.infixForm.Count; ++index)
    {
      Token token = this.infixForm[index];
      stringBuilder.Append(token.fullText(this));
    }
    return stringBuilder.ToString();
  }

  public string TextWithCursor(int curToken, out int curCharPos)
  {
    bool flag = false;
    curCharPos = -1;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.infixForm.Count; ++index)
    {
      if (!flag && index > curToken)
      {
        curCharPos = stringBuilder.Length;
        stringBuilder.Append("|");
        flag = true;
      }
      Token token = this.infixForm[index];
      stringBuilder.Append(token.text);
    }
    if (!flag)
    {
      curCharPos = stringBuilder.Length;
      stringBuilder.Append("|");
    }
    return stringBuilder.ToString();
  }

  public int GetTokenByPos(int charPos)
  {
    for (int index = 0; index < this.infixForm.Count; ++index)
    {
      Token token = this.infixForm[index];
      if (token.StartPos <= charPos && token.StartPos + token.text.Length >= charPos)
        return index;
    }
    return -1;
  }

  public void UpdateTokenBegs()
  {
    int num = 0;
    for (int index = 0; index < this.infixForm.Count; ++index)
    {
      Token token = this.infixForm[index];
      token.StartPos = num;
      num += token.text.Length;
    }
  }

  public override bool Equals(object obj)
  {
    if (!(obj is TempFormula))
      return false;
    TempFormula tempFormula = obj as TempFormula;
    for (int index = 0; index < this.infixForm.Count; ++index)
    {
      if (index < tempFormula.infixForm.Count && this[index].text != tempFormula[index].text)
        return false;
    }
    return true;
  }

  public override int GetHashCode() => base.GetHashCode();

  public void Load(BinaryReader br, int Version)
  {
    this.infixForm.Clear();
    int num1 = br.ReadInt32();
    for (int index = 0; index < num1; ++index)
      this.infixForm.Add(new Token(ref br, Version));
    int num2 = br.ReadInt32();
    for (int index = 0; index < num2; ++index)
      this.postfixForm.Add(new Token(ref br, Version));
    this.resType = (DataType) br.ReadInt32();
    if (this.usedAttrs == null)
      this.usedAttrs = new List<AttribPair>();
    int num3 = br.ReadInt32();
    for (int index = 0; index < num3; ++index)
      this.usedAttrs.Add(new AttribPair(br.ReadInt32(), br.ReadInt32()));
    if (this.pairNames == null)
      this.pairNames = new List<PairName>();
    int num4 = br.ReadInt32();
    for (int index = 0; index < num4; ++index)
      this.pairNames.Add(new PairName(ref br));
    if (this.objectLinks == null)
      this.objectLinks = new List<long>();
    int num5 = br.ReadInt32();
    for (int index = 0; index < num5; ++index)
      this.objectLinks.Add(br.ReadInt64());
    if (this.objTypeGUIDs == null)
      this.objTypeGUIDs = new List<string>();
    int num6 = br.ReadInt32();
    for (int index = 0; index < num6; ++index)
      this.objTypeGUIDs.Add(br.ReadString());
    if (this.attrGUIDs == null)
      this.attrGUIDs = new List<string>();
    int num7 = br.ReadInt32();
    for (int index = 0; index < num7; ++index)
      this.attrGUIDs.Add(br.ReadString());
    this.isArray = Version >= 102 && br.ReadInt32() != 0;
  }

  public void Save(BinaryWriter bw) => this.SaveAsVer(bw, ExpertConsts.FormulaVersion);

  public void SaveAsVer(BinaryWriter bw, int Version)
  {
    bw.Write(this.infixForm.Count);
    for (int index = 0; index < this.infixForm.Count; ++index)
      this.infixForm[index].SaveAsVer(bw, Version);
    bw.Write(this.postfixForm.Count);
    for (int index = 0; index < this.postfixForm.Count; ++index)
      this.postfixForm[index].SaveAsVer(bw, Version);
    bw.Write((int) this.resType);
    if (this.usedAttrs != null)
    {
      bw.Write(this.usedAttrs.Count);
      for (int index = 0; index < this.usedAttrs.Count; ++index)
      {
        AttribPair usedAttr = this.usedAttrs[index];
        bw.Write(usedAttr.attribID);
        bw.Write(usedAttr.objTypeID);
      }
    }
    else
      bw.Write(0);
    if (this.pairNames != null)
    {
      bw.Write(this.pairNames.Count);
      for (int index = 0; index < this.pairNames.Count; ++index)
        this.pairNames[index].Save(ref bw);
    }
    else
      bw.Write(0);
    if (this.objectLinks != null)
    {
      bw.Write(this.objectLinks.Count);
      for (int index = 0; index < this.objectLinks.Count; ++index)
        bw.Write(this.objectLinks[index]);
    }
    else
      bw.Write(0);
    if (this.objTypeGUIDs != null)
    {
      bw.Write(this.objTypeGUIDs.Count);
      for (int index = 0; index < this.objTypeGUIDs.Count; ++index)
      {
        string str = "";
        if (this.objTypeGUIDs[index] != null)
          str = Convert.ToString(this.objTypeGUIDs[index]);
        bw.Write(str);
      }
    }
    else
      bw.Write(0);
    if (this.attrGUIDs != null)
    {
      bw.Write(this.attrGUIDs.Count);
      for (int index = 0; index < this.attrGUIDs.Count; ++index)
        bw.Write(this.attrGUIDs[index]);
    }
    else
      bw.Write(0);
    bw.Write(this.isArray ? 1 : 0);
  }

  protected TempFormula(SerializationInfo info, StreamingContext context)
  {
    this.Init();
    Dictionary<string, object> paramsValue = SerializationInfoHelper.GetParamsValue(info);
    this.Cond = Convert.ToBoolean(paramsValue[nameof (Cond)]);
    this.resType = (DataType) Convert.ToInt32(paramsValue["RT"]);
    int int32_1 = Convert.ToInt32(paramsValue["IF_N"]);
    for (int index = 0; index < int32_1; ++index)
      this.infixForm.Add(this.LoadToken(paramsValue, "IF_" + index.ToString()));
    int int32_2 = Convert.ToInt32(paramsValue["PF_N"]);
    for (int index = 0; index < int32_2; ++index)
      this.postfixForm.Add(this.LoadToken(paramsValue, "PF_" + index.ToString()));
    int int32_3 = Convert.ToInt32(paramsValue["UA_N"]);
    for (int index = 0; index < int32_3; ++index)
      this.usedAttrs.Add(new AttribPair(Convert.ToInt32(paramsValue[$"UA_{index.ToString()}a"]), Convert.ToInt32(paramsValue[$"UA_{index.ToString()}o"])));
    int int32_4 = Convert.ToInt32(paramsValue["PN_N"]);
    for (int index = 0; index < int32_4; ++index)
      this.pairNames.Add(this.LoadPairName(paramsValue, "PN_" + index.ToString()));
    int int32_5 = Convert.ToInt32(paramsValue["AG_N"]);
    for (int index = 0; index < int32_5; ++index)
      this.attrGUIDs.Add(Convert.ToString(paramsValue["AG_" + index.ToString()]));
    int int32_6 = Convert.ToInt32(paramsValue["OG_N"]);
    for (int index = 0; index < int32_6; ++index)
      this.objTypeGUIDs.Add(Convert.ToString(paramsValue["OG_" + index.ToString()]));
    int int32_7 = Convert.ToInt32(paramsValue["OL_N"]);
    for (int index = 0; index < int32_7; ++index)
      this.objectLinks.Add(Convert.ToInt64(paramsValue["OL_" + index.ToString()]));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Cond", this.Cond);
    info.AddValue("RT", (int) this.resType);
    info.AddValue("IF_N", this.infixForm.Count);
    for (int index = 0; index < this.infixForm.Count; ++index)
      this.SaveToken(info, "IF_" + index.ToString(), this.infixForm[index]);
    info.AddValue("PF_N", this.postfixForm.Count);
    for (int index = 0; index < this.postfixForm.Count; ++index)
      this.SaveToken(info, "PF_" + index.ToString(), this.postfixForm[index]);
    if (this.usedAttrs != null)
    {
      info.AddValue("UA_N", this.usedAttrs.Count);
      for (int index = 0; index < this.usedAttrs.Count; ++index)
      {
        AttribPair usedAttr = this.usedAttrs[index];
        info.AddValue($"UA_{index.ToString()}a", usedAttr.attribID);
        info.AddValue($"UA_{index.ToString()}o", usedAttr.objTypeID);
      }
    }
    else
      info.AddValue("UA_N", 0);
    if (this.pairNames != null)
    {
      info.AddValue("PN_N", this.pairNames.Count);
      for (int index = 0; index < this.pairNames.Count; ++index)
        this.SavePairName(info, "PN_" + index.ToString(), this.pairNames[index]);
    }
    else
      info.AddValue("PN_N", 0);
    if (this.attrGUIDs != null)
    {
      info.AddValue("AG_N", this.attrGUIDs.Count);
      for (int index = 0; index < this.attrGUIDs.Count; ++index)
        info.AddValue("AG_" + index.ToString(), (object) this.attrGUIDs[index]);
    }
    else
      info.AddValue("AG_N", 0);
    if (this.objTypeGUIDs != null)
    {
      info.AddValue("OG_N", this.objTypeGUIDs.Count);
      for (int index = 0; index < this.objTypeGUIDs.Count; ++index)
        info.AddValue("OG_" + index.ToString(), (object) this.objTypeGUIDs[index]);
    }
    else
      info.AddValue("OG_N", 0);
    if (this.objectLinks != null)
    {
      info.AddValue("OL_N", this.objectLinks.Count);
      for (int index = 0; index < this.objectLinks.Count; ++index)
        info.AddValue("OL_" + index.ToString(), this.objectLinks[index]);
    }
    else
      info.AddValue("OL_N", 0);
  }

  private void SaveToken(SerializationInfo info, string prefix, Token t)
  {
    info.AddValue(prefix + nameof (t), (int) t.type);
    info.AddValue(prefix + "x", t._Guid == "" ? (object) t.trueText : (object) $"{t._Guid}@{t.trueText}");
    info.AddValue(prefix + "v", t.iValue);
    info.AddValue(prefix + "f", t.fValue);
    info.AddValue(prefix + "i", t.info);
    info.AddValue(prefix + "spt", (int) t.spt);
  }

  private Token LoadToken(Dictionary<string, object> sinfo, string prefix)
  {
    TokenType int32_1 = (TokenType) Convert.ToInt32(sinfo[prefix + "t"]);
    string text = Convert.ToString(sinfo[prefix + "x"]);
    string str = "";
    int length = text.IndexOf('@');
    if (length >= 0)
    {
      if (length > 0)
        str = text.Substring(0, length);
      text = text.Remove(0, length + 1);
    }
    else
      str = "";
    long int64 = Convert.ToInt64(sinfo[prefix + "v"]);
    double num = Convert.ToDouble(sinfo[prefix + "f"]);
    int int32_2 = Convert.ToInt32(sinfo[prefix + "i"]);
    SelectionParameterTypes selectionParameterTypes = SelectionParameterTypes.sptNone;
    try
    {
      object obj = (object) null;
      sinfo.TryGetValue(prefix + "spt", out obj);
      if (obj != null)
        selectionParameterTypes = (SelectionParameterTypes) obj;
    }
    catch
    {
    }
    return new Token(int32_1, text)
    {
      iValue = int64,
      fValue = num,
      info = int32_2,
      _Guid = str,
      spt = selectionParameterTypes
    };
  }

  private void SavePairName(SerializationInfo info, string prefix, PairName pn)
  {
    info.AddValue(prefix + "as", (object) pn.attrShortName);
    info.AddValue(prefix + "al", (object) pn.attrLongName);
    info.AddValue(prefix + "os", (object) pn.objTypeShortName);
    info.AddValue(prefix + "ol", (object) pn.objTypeLongName);
    info.AddValue(prefix + "t", (int) pn.ft);
    info.AddValue(prefix + "m", pn.Multi);
  }

  private PairName LoadPairName(Dictionary<string, object> sinfo, string prefix)
  {
    string aShortName = Convert.ToString(sinfo[prefix + "as"]);
    string str1 = Convert.ToString(sinfo[prefix + "al"]);
    string str2 = Convert.ToString(sinfo[prefix + "os"]);
    string str3 = Convert.ToString(sinfo[prefix + "ol"]);
    FieldTypes int32 = (FieldTypes) Convert.ToInt32(sinfo[prefix + "t"]);
    bool boolean = Convert.ToBoolean(sinfo[prefix + "m"]);
    string aLongName = str1;
    string oShortName = str2;
    string oLongName = str3;
    int ft = (int) int32;
    int num = boolean ? 1 : 0;
    return new PairName(aShortName, aLongName, oShortName, oLongName, (FieldTypes) ft, num != 0);
  }

  public object Clone()
  {
    TempFormula tempFormula = new TempFormula(true);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (BinaryWriter bw = new BinaryWriter((Stream) memoryStream))
      {
        this.Save(bw);
        memoryStream.Position = 0L;
        using (BinaryReader br = new BinaryReader((Stream) memoryStream))
        {
          tempFormula.Load(br, ExpertConsts.FormulaVersion);
          tempFormula.Cond = this.Cond;
        }
      }
    }
    return (object) tempFormula;
  }

  public void WriteToXML(ref XmlTextWriter writer) => this.WriteToXML(ref writer, "Formula");

  public void WriteToXML(ref XmlTextWriter writer, string root)
  {
    writer.WriteStartElement(root);
    writer.WriteElementString("res-type", Convert.ToString((int) this.resType));
    writer.WriteStartElement("infix-form");
    writer.WriteAttributeString("count", Convert.ToString(this.infixForm.Count));
    for (int index = 0; index < this.infixForm.Count; ++index)
      this.infixForm[index].WriteToXML(ref writer);
    writer.WriteEndElement();
    writer.WriteStartElement("postfix-form");
    writer.WriteAttributeString("count", Convert.ToString(this.postfixForm.Count));
    for (int index = 0; index < this.postfixForm.Count; ++index)
      this.postfixForm[index].WriteToXML(ref writer);
    writer.WriteEndElement();
    if (this.usedAttrs != null && this.usedAttrs.Count > 0)
    {
      writer.WriteStartElement("Used-Attrs");
      writer.WriteAttributeString("count", Convert.ToString(this.usedAttrs.Count));
      for (int index = 0; index < this.usedAttrs.Count; ++index)
      {
        writer.WriteStartElement("Attr-Info");
        AttribPair usedAttr = this.usedAttrs[index];
        writer.WriteElementString("attrID", Convert.ToString(usedAttr.attribID));
        writer.WriteElementString("objTypeID", Convert.ToString(usedAttr.objTypeID));
        writer.WriteStartElement("Full-Attr");
        writer.WriteElementString("aGUID", this.attrGUIDs[index].ToString());
        writer.WriteElementString("oGUID", this.objTypeGUIDs[index].ToString());
        writer.WriteEndElement();
        PairName pairName = this.pairNames[index];
        writer.WriteStartElement("Pair-Name");
        writer.WriteElementString("attr-shortname", pairName.attrShortName);
        writer.WriteElementString("attr-longname", pairName.attrLongName);
        writer.WriteElementString("objType-shortname", pairName.objTypeShortName);
        writer.WriteElementString("objType-longname", pairName.objTypeLongName);
        writer.WriteElementString("field-type", Convert.ToString((int) pairName.ft));
        writer.WriteElementString("multi", pairName.Multi ? "Y" : "N");
        writer.WriteEndElement();
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    if (this.objectLinks != null && this.objectLinks.Count > 0)
    {
      writer.WriteStartElement("Object-Links");
      writer.WriteAttributeString("count", Convert.ToString(this.objectLinks.Count));
      for (int index = 0; index < this.objectLinks.Count; ++index)
        writer.WriteElementString("ID", Convert.ToString(this.objectLinks[index]));
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
  }

  public TempFormula(XmlNode node)
  {
    if (node.NodeType != XmlNodeType.Element || !node.Name.ToLower().StartsWith("form"))
      throw new AbortException("Wrong XML node for Formula!");
    this.Init();
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "res-type")
        this.resType = (DataType) Convert.ToInt32(childNode1.InnerText);
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "infix-form")
      {
        if (childNode1.HasChildNodes)
        {
          for (int i = 0; i < childNode1.ChildNodes.Count; ++i)
            this.infixForm.Add(new Token(childNode1.ChildNodes[i]));
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "postfix-form")
      {
        if (childNode1.HasChildNodes)
        {
          for (int i = 0; i < childNode1.ChildNodes.Count; ++i)
            this.postfixForm.Add(new Token(childNode1.ChildNodes[i]));
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Used-Attrs")
      {
        if (childNode1.Attributes != null)
        {
          Convert.ToInt32(childNode1.Attributes[0].Value);
          if (childNode1.HasChildNodes)
          {
            for (int i = 0; i < childNode1.ChildNodes.Count; ++i)
            {
              XmlNode childNode2 = childNode1.ChildNodes[i];
              if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "Attr-Info")
              {
                int att = 0;
                int num = 0;
                string str1 = "";
                string str2 = "";
                string aShortName = "";
                string aLongName = "";
                string oShortName = "";
                string oLongName = "";
                int ft = 0;
                bool multis = false;
                foreach (XmlNode childNode3 in childNode2.ChildNodes)
                {
                  if (childNode3.NodeType == XmlNodeType.Element && childNode3.Name == "attrID")
                    att = Convert.ToInt32(childNode3.InnerText);
                  else if (childNode3.NodeType == XmlNodeType.Element && childNode3.Name == "objTypeID")
                    num = Convert.ToInt32(childNode3.InnerText);
                  else if (childNode3.NodeType == XmlNodeType.Element && childNode3.Name == "Full-Attr")
                  {
                    if (childNode3.HasChildNodes)
                    {
                      str1 = childNode3.ChildNodes[0].InnerText;
                      str2 = childNode3.ChildNodes[1].InnerText;
                    }
                  }
                  else if (childNode3.NodeType == XmlNodeType.Element && childNode3.Name == "Pair-Name")
                  {
                    foreach (XmlNode childNode4 in childNode3.ChildNodes)
                    {
                      if (childNode4.NodeType == XmlNodeType.Element && childNode4.Name == "attr-shortname")
                        aShortName = childNode4.InnerText;
                      else if (childNode4.NodeType == XmlNodeType.Element && childNode4.Name == "attr-longname")
                        aLongName = childNode4.InnerText;
                      else if (childNode4.NodeType == XmlNodeType.Element && childNode4.Name == "objType-shortname")
                        oShortName = childNode4.InnerText;
                      else if (childNode4.NodeType == XmlNodeType.Element && childNode4.Name == "objType-longname")
                        oLongName = childNode4.InnerText;
                      else if (childNode4.NodeType == XmlNodeType.Element && childNode4.Name == "field-type")
                        ft = Convert.ToInt32(childNode4.InnerText);
                      else if (childNode4.NodeType == XmlNodeType.Element && childNode4.Name == "multi")
                        multis = childNode4.InnerText == "Y";
                    }
                    this.usedAttrs.Add(new AttribPair(att, num));
                    this.pairNames.Add(new PairName(aShortName, aLongName, oShortName, oLongName, (FieldTypes) ft, multis));
                    this.attrGUIDs.Add(str1);
                    this.objTypeGUIDs.Add(str2);
                  }
                }
              }
            }
          }
        }
      }
      else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Object-Links")
      {
        if (this.objectLinks == null)
          this.objectLinks = new List<long>();
        if (childNode1.HasChildNodes)
        {
          foreach (XmlNode childNode5 in childNode1.ChildNodes)
          {
            if (childNode5.NodeType == XmlNodeType.Element && childNode5.Name == "ID")
              this.objectLinks.Add(Convert.ToInt64(childNode5.InnerText));
          }
        }
      }
    }
  }

  public bool Compile(out int BadToken, out string errorMsg)
  {
    this.postfixForm.Clear();
    errorMsg = "";
    BadToken = -1;
    if (this.CanReturnEmpty && this.Count == 0)
      return true;
    BadToken = this.CheckBraces(ref errorMsg);
    if (errorMsg != "")
      return false;
    BadToken = this.CheckPlacement(ref errorMsg);
    if (errorMsg != "")
      return false;
    try
    {
      BadToken = this.MainCompile(ref errorMsg);
    }
    catch (Exception ex)
    {
      errorMsg = ex.Message;
    }
    if (errorMsg != "")
      return false;
    this.StringWasConverted = false;
    BadToken = this.CheckDataTypes(ref errorMsg);
    if (!(errorMsg != ""))
      return errorMsg == "";
    if (this.AutoConvert)
    {
      this.ConvertAllStrings();
      errorMsg = "";
      try
      {
        this.MainCompile(ref errorMsg);
        errorMsg = "";
        BadToken = this.CheckDataTypes(ref errorMsg);
      }
      catch (Exception ex)
      {
        errorMsg = ex.Message;
      }
    }
    return errorMsg == "";
  }

  private int CheckBraces(ref string errorMsg)
  {
    ArrayList arrayList = new ArrayList();
    for (int index1 = 0; index1 < this.Count; ++index1)
    {
      Token token = this[index1];
      switch (token.type)
      {
        case TokenType.BinaryOper:
          if (token.text == ":")
          {
            bool flag = false;
            for (int index2 = 0; index2 < arrayList.Count; ++index2)
            {
              if ((string) arrayList[index2] == "{")
              {
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              errorMsg = LocalizationHolder.rm.GetString("Expert_90");
              return index1;
            }
            break;
          }
          break;
        case TokenType.OpeningBrace:
        case TokenType.FuncCall:
          if (token.type == TokenType.FuncCall)
          {
            arrayList.Add((object) "(");
            break;
          }
          arrayList.Add((object) token.text);
          break;
        case TokenType.ClosingBrace:
          string str = "";
          if (arrayList.Count > 0)
            str = (string) arrayList[arrayList.Count - 1];
          if (str == "" || token.text == ")" && str != "(" || token.text == "}" && str != "{" || token.text == "]" && str != "[")
          {
            errorMsg = LocalizationHolder.rm.GetString("Expert_88");
            return index1;
          }
          arrayList.RemoveAt(arrayList.Count - 1);
          break;
        case TokenType.Divider:
          if (token.text == ", " && arrayList.Count == 0)
          {
            errorMsg = LocalizationHolder.rm.GetString("Expert_89");
            return index1;
          }
          break;
      }
    }
    if (arrayList.Count <= 0)
      return -1;
    errorMsg = LocalizationHolder.rm.GetString("Expert_91");
    return this.Count - 1;
  }

  private string ValidateToken(int Index, TempFormula.ValidateType vt)
  {
    Token token = (Token) null;
    if (Index > 0)
      token = this[Index - 1];
    switch (vt)
    {
      case TempFormula.ValidateType.NotAfterOperator:
        if (Index > 0 && (token.type == TokenType.UnaryOper || token.type == TokenType.BinaryOper))
          return LocalizationHolder.rm.GetString("Expert_95");
        break;
      case TempFormula.ValidateType.NotAfterOperand:
        if (Index > 0 && (token.type == TokenType.Attribute || token.type == TokenType.Date || token.type == TokenType.Float || token.type == TokenType.Integer || token.type == TokenType.ObjectLink || token.type == TokenType.String || token.type == TokenType.ClosingBrace))
          return LocalizationHolder.rm.GetString("Expert_94");
        break;
      case TempFormula.ValidateType.NotAfterDivider:
        if (Index > 0 && token.type == TokenType.Divider)
          return LocalizationHolder.rm.GetString("Expert_92");
        break;
      case TempFormula.ValidateType.NotAfterOpenBrace:
        if (Index > 0 && (token.type == TokenType.OpeningBrace || token.type == TokenType.FuncCall))
          return LocalizationHolder.rm.GetString("Expert_93");
        break;
      case TempFormula.ValidateType.OnlyAfterOperand:
        if (Index > 0 && token.type != TokenType.Attribute && token.type != TokenType.Date && token.type != TokenType.Float && token.type != TokenType.Integer && token.type != TokenType.ObjectLink && token.type != TokenType.String && token.type != TokenType.Measured && token.type != TokenType.ClosingBrace)
          return LocalizationHolder.rm.GetString("Expert_97");
        break;
      case TempFormula.ValidateType.NotFirst:
        if (Index == 0)
          return LocalizationHolder.rm.GetString("Expert_96");
        break;
      case TempFormula.ValidateType.NotLast:
        if (Index == this.Count - 1)
          return LocalizationHolder.rm.GetString("Expert_98");
        break;
      case TempFormula.ValidateType.OnlyAfterAttribute:
        if (Index > 0 && token.type != TokenType.Attribute)
          return LocalizationHolder.rm.GetString("Expert_200");
        break;
    }
    return "";
  }

  private int CheckPlacement(ref string errorMsg)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      Token token = this[index];
      switch (token.type)
      {
        case TokenType.UnaryOper:
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotLast);
          if (errorMsg == "" && token.text == "->")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.OnlyAfterAttribute);
          if (errorMsg != "")
            return index;
          break;
        case TokenType.BinaryOper:
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterDivider);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterOpenBrace);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterOperator);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotFirst);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotLast);
          if (errorMsg != "")
            return index;
          break;
        case TokenType.OpeningBrace:
        case TokenType.FuncCall:
          if (token.text == "[" && errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.OnlyAfterAttribute);
          if (token.text != "[" && errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterOperand);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotLast);
          if (errorMsg != "")
            return index;
          break;
        case TokenType.ClosingBrace:
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterDivider);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterOperator);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotFirst);
          if (errorMsg != "")
            return index;
          break;
        case TokenType.Integer:
        case TokenType.Float:
        case TokenType.String:
        case TokenType.Date:
        case TokenType.ObjectLink:
        case TokenType.Attribute:
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotAfterOperand);
          if (errorMsg != "")
            return index;
          break;
        case TokenType.Divider:
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.OnlyAfterOperand);
          if (errorMsg == "")
            errorMsg = this.ValidateToken(index, TempFormula.ValidateType.NotLast);
          if (errorMsg != "")
            return index;
          break;
      }
    }
    return -1;
  }

  private void PopToken(List<TempFormula.OpStackItem> stack, bool PlusOne)
  {
    TempFormula.OpStackItem opStackItem = stack[stack.Count - 1];
    stack.RemoveAt(stack.Count - 1);
    switch (opStackItem.t.type)
    {
      case TokenType.BinaryOper:
        if (opStackItem.t.text.Trim() == LocalizationHolder.rm.GetString("Expert_99"))
        {
          Token token = this.postfixForm[opStackItem.postIndex];
          token.iValue = (long) this.postfixForm.Count;
          if (PlusOne)
            ++token.iValue;
          token.text += token.iValue.ToString();
          break;
        }
        if (opStackItem.t.text.Trim() == LocalizationHolder.rm.GetString("Expert_100"))
        {
          Token token = this.postfixForm[opStackItem.postIndex];
          token.iValue = (long) this.postfixForm.Count;
          if (PlusOne)
            ++token.iValue;
          token.text += token.iValue.ToString();
          break;
        }
        this.postfixForm.Add(new Token(opStackItem.t));
        this.infixIndex.Add(opStackItem.tokIndex);
        break;
      case TokenType.OpeningBrace:
        if (opStackItem.t.text == "{")
        {
          Token token = new Token(TokenType.Command, "FPACK_")
          {
            info = 2,
            iValue = (long) this.GetStackItems(opStackItem.postIndex)
          };
          token.text += token.iValue.ToString();
          this.postfixForm.Add(token);
          this.infixIndex.Add(opStackItem.tokIndex);
        }
        if (!(opStackItem.t.text == "["))
          break;
        Token token1 = new Token(TokenType.Command, "ARR_GET_");
        token1.info = 3;
        int stackItems = this.GetStackItems(opStackItem.postIndex);
        token1.text += stackItems.ToString();
        Token token2 = this.postfixForm[opStackItem.postIndex - 1];
        token1.iValue = (long) token2.info;
        this.postfixForm.RemoveAt(opStackItem.postIndex - 1);
        this.postfixForm.Add(token1);
        this.infixIndex.Add(opStackItem.tokIndex);
        break;
      default:
        if (this.SpecFunction(opStackItem.t))
          break;
        this.postfixForm.Add(new Token(opStackItem.t));
        this.infixIndex.Add(opStackItem.tokIndex);
        break;
    }
  }

  private TempFormula.OpStackItem TopOper(List<TempFormula.OpStackItem> stack)
  {
    return stack.Count == 0 ? (TempFormula.OpStackItem) null : stack[stack.Count - 1];
  }

  private bool SpecFunction(Token t)
  {
    if (t.type != TokenType.FuncCall)
      return false;
    FuncData funcData = ExpertFunc.funcs(t.info);
    if (funcData.func == FormulaFunc.STR)
    {
      Token token = this.postfixForm[this.postfixForm.Count - 1];
      token.iValue = token.type == TokenType.Attribute ? (long) token.info : throw new AbortException(LocalizationHolder.rm.GetString("Expert_102"));
      token.info = ExpertFunc.GetFuncIndex(FormulaFunc.STR);
      token.text = $"{LocalizationHolder.rm.GetString("Expert_101")}{token.text})";
      token.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func == FormulaFunc.skipNull)
    {
      Token token = this.postfixForm[this.postfixForm.Count - 1];
      token.iValue = token.type == TokenType.Attribute ? (long) token.info : throw new AbortException(LocalizationHolder.rm.GetString("Expert_222"));
      token.info = ExpertFunc.GetFuncIndex(FormulaFunc.skipNull);
      token.text = $"{LocalizationHolder.rm.GetString("Expert_221")}{token.text})";
      token.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func == FormulaFunc.skipNull_0)
    {
      Token token = this.postfixForm[this.postfixForm.Count - 1];
      token.iValue = token.type == TokenType.Attribute ? (long) token.info : throw new AbortException(LocalizationHolder.rm.GetString("Expert_224"));
      token.info = ExpertFunc.GetFuncIndex(FormulaFunc.skipNull_0);
      token.text = $"{LocalizationHolder.rm.GetString("Expert_223")}{token.text})";
      token.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func == FormulaFunc.skipNull_1)
    {
      Token token = this.postfixForm[this.postfixForm.Count - 1];
      token.iValue = token.type == TokenType.Attribute ? (long) token.info : throw new AbortException(LocalizationHolder.rm.GetString("Expert_250"));
      token.info = ExpertFunc.GetFuncIndex(FormulaFunc.skipNull_1);
      token.text = $"{LocalizationHolder.rm.GetString("Expert_249")}{token.text})";
      token.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func == FormulaFunc.def)
    {
      Token token = this.postfixForm[this.postfixForm.Count - 1];
      token.iValue = token.type == TokenType.Attribute ? (long) token.info : throw new AbortException(LocalizationHolder.rm.GetString("Expert_199"));
      token.info = ExpertFunc.GetFuncIndex(FormulaFunc.def);
      token.text = $"def({token.text})";
      token.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func == FormulaFunc.flag_a)
    {
      Token token1 = this.postfixForm[this.postfixForm.Count - 1];
      Token token2 = this.postfixForm[this.postfixForm.Count - 2];
      if (token1.type != TokenType.Attribute || token2.type != TokenType.Integer)
        throw new AbortException(LocalizationHolder.rm.GetString("Expert_104"));
      token1.iValue = (long) token1.info;
      token1.info = ExpertFunc.GetFuncIndex(FormulaFunc.flag_a);
      token1.fValue = (double) token2.iValue;
      token1.text = $"{LocalizationHolder.rm.GetString("Expert_103")}{Convert.ToString(token2.iValue)},{token1.text})";
      this.postfixForm.RemoveAt(this.postfixForm.Count - 2);
      token1.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func == FormulaFunc.str_list)
    {
      Token token3 = this.postfixForm[this.postfixForm.Count - 1];
      Token token4 = this.postfixForm[this.postfixForm.Count - 2];
      if (token3.type != TokenType.Attribute || token4.type != TokenType.String)
        throw new AbortException(LocalizationHolder.rm.GetString("Expert_244"));
      token3.iValue = (long) token3.info;
      token3.info = ExpertFunc.GetFuncIndex(FormulaFunc.str_list);
      token3.text = $"{LocalizationHolder.rm.GetString("Expert_243")}?, {token3.text})";
      token3.type = TokenType.FuncCall;
      return true;
    }
    if (funcData.func != FormulaFunc.ref_list)
      return false;
    Token token5 = this.postfixForm[this.postfixForm.Count - 1];
    Token token6 = this.postfixForm[this.postfixForm.Count - 2];
    Token token7 = this.postfixForm[this.postfixForm.Count - 3];
    if (token5.type != TokenType.Attribute || token6.type != TokenType.Attribute || token7.type != TokenType.String)
      throw new AbortException(LocalizationHolder.rm.GetString("Expert_246"));
    token5.iValue = (long) token5.info;
    token5.info = ExpertFunc.GetFuncIndex(FormulaFunc.ref_list);
    token5.fValue = (double) token6.info;
    token5.text = $"{LocalizationHolder.rm.GetString("Expert_242")}?, {token6.text}, {token5.text})";
    this.postfixForm.RemoveAt(this.postfixForm.Count - 2);
    token5.type = TokenType.FuncCall;
    return true;
  }

  private int MainCompile(ref string errorMsg)
  {
    this.postfixForm.Clear();
    this.infixIndex.Clear();
    List<TempFormula.OpStackItem> stack = new List<TempFormula.OpStackItem>();
    for (int index = 0; index < this.Count; ++index)
    {
      Token t1 = this[index];
      switch (t1.type)
      {
        case TokenType.UnaryOper:
          if (t1.text == "->")
          {
            this.postfixForm[this.postfixForm.Count - 1].iValue = (long) Token._Ref;
            break;
          }
          if (t1.info == Token.LeftAssoc)
          {
            stack.Add(new TempFormula.OpStackItem(index, this.postfixForm.Count, t1));
            break;
          }
          this.postfixForm.Add(new Token(t1));
          this.infixIndex.Add(index);
          break;
        case TokenType.BinaryOper:
          bool flag1 = t1.text.Trim() == LocalizationHolder.rm.GetString("Expert_105");
          bool flag2 = t1.text.Trim() == LocalizationHolder.rm.GetString("Expert_106");
          if (stack.Count == 0)
          {
            stack.Add(new TempFormula.OpStackItem(index, this.postfixForm.Count, t1));
          }
          else
          {
            while (stack.Count > 0)
            {
              Token t2 = this.TopOper(stack).t;
              if (t2.StackPrec >= t1.InputPrec && (t2.StackPrec != t1.InputPrec || t1.info != Token.RightAssoc) && (t2.StackPrec != t1.InputPrec || !(flag1 | flag2)))
                this.PopToken(stack, flag1 | flag2);
              else
                break;
            }
            stack.Add(new TempFormula.OpStackItem(index, this.postfixForm.Count, t1));
          }
          if (flag1)
          {
            this.postfixForm.Add(new Token(TokenType.Command, "jYES_")
            {
              info = 0,
              iValue = 0L
            });
            this.infixIndex.Add(index);
          }
          if (flag2)
          {
            this.postfixForm.Add(new Token(TokenType.Command, "jNOT_")
            {
              info = 1,
              iValue = 0L
            });
            this.infixIndex.Add(index);
            break;
          }
          break;
        case TokenType.OpeningBrace:
        case TokenType.FuncCall:
          stack.Add(new TempFormula.OpStackItem(index, this.postfixForm.Count, t1));
          break;
        case TokenType.ClosingBrace:
          TempFormula.OpStackItem opStackItem1 = (TempFormula.OpStackItem) null;
          while (stack.Count > 0)
          {
            opStackItem1 = this.TopOper(stack);
            if (opStackItem1.t.type != TokenType.OpeningBrace && opStackItem1.t.type != TokenType.FuncCall)
              this.PopToken(stack, false);
            else
              break;
          }
          if (opStackItem1 != null && opStackItem1.t.type == TokenType.FuncCall)
          {
            FuncData funcData = ExpertFunc.funcs(opStackItem1.t.info);
            int stackItems = this.GetStackItems(opStackItem1.postIndex);
            if (stackItems < funcData.parmTypes.Length)
            {
              errorMsg = string.Format(LocalizationHolder.rm.GetString("Expert_107"), (object) funcData.parmTypes.Length);
              return index;
            }
            if (stackItems > funcData.parmTypes.Length)
            {
              errorMsg = string.Format(LocalizationHolder.rm.GetString("Expert_108"), (object) funcData.parmTypes.Length);
              return index;
            }
          }
          this.PopToken(stack, false);
          if (errorMsg != "")
            return index;
          break;
        case TokenType.Integer:
        case TokenType.Float:
        case TokenType.String:
        case TokenType.Date:
        case TokenType.ObjectLink:
        case TokenType.Attribute:
        case TokenType.Measured:
        case TokenType.Boolean:
          this.postfixForm.Add(new Token(t1));
          this.infixIndex.Add(index);
          break;
        case TokenType.Divider:
          if (t1.text == ", ")
          {
            while (stack.Count > 0)
            {
              TempFormula.OpStackItem opStackItem2 = this.TopOper(stack);
              if (opStackItem2.t.type != TokenType.OpeningBrace && opStackItem2.t.type != TokenType.FuncCall)
                this.PopToken(stack, false);
              else
                break;
            }
            break;
          }
          break;
      }
    }
    while (stack.Count > 0)
      this.PopToken(stack, false);
    return -1;
  }

  private bool Contains(DataType[] types, DataType thisType)
  {
    for (int index = 0; index < types.Length; ++index)
    {
      if (thisType == types[index])
        return true;
    }
    return false;
  }

  private bool CheckBinaryOper(
    ArrayList TypeStack,
    ref string errorMsg,
    params DataType[] AllowedTypes)
  {
    DataType type1 = (DataType) TypeStack[TypeStack.Count - 2];
    DataType type2 = (DataType) TypeStack[TypeStack.Count - 1];
    if (!this.Contains(AllowedTypes, type1))
    {
      errorMsg = LocalizationHolder.rm.GetString("Expert_109");
      return false;
    }
    if (!this.Contains(AllowedTypes, type2))
    {
      errorMsg = LocalizationHolder.rm.GetString("Expert_110");
      return false;
    }
    if (type1 == type2)
    {
      TypeStack.RemoveAt(TypeStack.Count - 1);
      return true;
    }
    for (int index = 0; index < AllowedTypes.Length; ++index)
    {
      if (type1 == AllowedTypes[index])
      {
        TypeStack.RemoveAt(TypeStack.Count - 1);
        break;
      }
      if (type2 == AllowedTypes[index])
      {
        TypeStack.RemoveAt(TypeStack.Count - 2);
        break;
      }
    }
    return true;
  }

  private bool OneOf(DataType dt, params DataType[] dtArr)
  {
    for (int index = 0; index < dtArr.Length; ++index)
    {
      if (dt == dtArr[index])
        return true;
    }
    return false;
  }

  /// <summary>Можно ли один DataType привести к другому?</summary>
  /// <param name="dtCurr">Текущий тип</param>
  /// <param name="dtMust">Тип, к которому можно привести</param>
  /// <returns>true, если исходный тип можно привести к нужному</returns>
  public static bool CanBeConverted(DataType dtCurr, DataType dtMust)
  {
    if (dtCurr == dtMust)
      return true;
    switch (dtMust)
    {
      case DataType.Integer:
      case DataType.Date:
        return dtCurr == DataType.String || dtCurr == DataType.ObjectLink || dtCurr == DataType.ObjectIdLink;
      case DataType.Float:
        return dtCurr == DataType.String || dtCurr == DataType.Integer;
      case DataType.String:
        return dtCurr != DataType.Packet;
      case DataType.Boolean:
      case DataType.Packet:
        return false;
      case DataType.ObjectLink:
      case DataType.ObjectIdLink:
        return dtCurr == DataType.Integer;
      default:
        return false;
    }
  }

  /// Return number of invalid token
  public int CheckDataTypes(ref string errorMsg)
  {
    DataType dataType1 = DataType.Packet;
    ArrayList TypeStack = new ArrayList();
    for (int index1 = 0; index1 < this.postfixForm.Count; ++index1)
    {
      Token token = this.postfixForm[index1];
      switch (token.type)
      {
        case TokenType.UnaryOper:
          if (token.text == "->")
            TypeStack.RemoveAt(TypeStack.Count - 1);
          if (token.text.Trim() == LocalizationHolder.rm.GetString("Expert_15") && (DataType) TypeStack[TypeStack.Count - 1] != DataType.Boolean)
          {
            errorMsg = LocalizationHolder.rm.GetString("Expert_123");
            return this.infixIndex[index1];
          }
          break;
        case TokenType.BinaryOper:
          if (TypeStack.Count < 2)
          {
            errorMsg = LocalizationHolder.rm.GetString("Expert_111");
            return this.infixIndex[index1];
          }
          DataType dataType2 = (DataType) TypeStack[TypeStack.Count - 2];
          DataType dataType3 = (DataType) TypeStack[TypeStack.Count - 1];
          switch (token.text.Trim())
          {
            case "*":
            case "-":
            case "/":
            case "^":
              if (!this.CheckBinaryOper(TypeStack, ref errorMsg, DataType.Measured, DataType.Float, DataType.Integer))
                return this.infixIndex[index1];
              continue;
            case "+":
              if (!this.CheckBinaryOper(TypeStack, ref errorMsg, DataType.String, DataType.Measured, DataType.Float, DataType.Integer))
                return this.infixIndex[index1];
              continue;
            case ":":
              if (!this.OneOf(dataType2, DataType.Integer, DataType.Measured, DataType.Float, DataType.String, DataType.ObjectLink, DataType.ObjectIdLink))
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_115");
                return this.infixIndex[index1];
              }
              if (!this.OneOf(dataType3, DataType.Integer, DataType.Measured, DataType.Float, DataType.String, DataType.ObjectLink, DataType.ObjectIdLink))
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_116");
                return this.infixIndex[index1];
              }
              if (dataType2 != dataType3)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_117");
                return this.infixIndex[index1];
              }
              TypeStack.RemoveAt(TypeStack.Count - 1);
              TypeStack.RemoveAt(TypeStack.Count - 1);
              TypeStack.Add((object) DataType.Diap);
              continue;
            case "<":
            case "<=":
            case "<>":
            case "=":
            case ">":
            case ">=":
              if (dataType2 == dataType3 || TempFormula.CanBeConverted(dataType2, dataType3) || TempFormula.CanBeConverted(dataType3, dataType2))
              {
                TypeStack.RemoveAt(TypeStack.Count - 1);
                TypeStack.RemoveAt(TypeStack.Count - 1);
                TypeStack.Add((object) DataType.Boolean);
                continue;
              }
              errorMsg = LocalizationHolder.rm.GetString("Expert_114");
              return this.infixIndex[index1];
            case "?":
              if (!this.OneOf(dataType2, DataType.Integer, DataType.Float, DataType.String, DataType.Measured, DataType.ObjectLink, DataType.ObjectIdLink))
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_118");
                return this.infixIndex[index1];
              }
              if (dataType3 != DataType.Packet)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_119");
                return this.infixIndex[index1];
              }
              TypeStack.RemoveAt(TypeStack.Count - 1);
              TypeStack.RemoveAt(TypeStack.Count - 1);
              TypeStack.Add((object) DataType.Boolean);
              continue;
            case "И":
            case "ИЛИ":
              if (!this.CheckBinaryOper(TypeStack, ref errorMsg, DataType.Boolean))
                return this.infixIndex[index1];
              continue;
            default:
              continue;
          }
        case TokenType.FuncCall:
          FuncData funcData = ExpertFunc.funcs(token.info);
          if (funcData.func == FormulaFunc.flag_a)
          {
            TypeStack.Add((object) DataType.Boolean);
            break;
          }
          if (funcData.func == FormulaFunc.STR)
          {
            TypeStack.Add((object) DataType.String);
            break;
          }
          if (funcData.func == FormulaFunc.skipNull)
          {
            TypeStack.Add((object) DataType.String);
            break;
          }
          if (funcData.func == FormulaFunc.skipNull_0 || funcData.func == FormulaFunc.skipNull_1)
          {
            TypeStack.Add((object) DataType.Float);
            break;
          }
          if (funcData.func == FormulaFunc.def)
          {
            TypeStack.Add((object) DataType.Boolean);
            break;
          }
          if (funcData.func == FormulaFunc.str_list)
          {
            if ((DataType) TypeStack[TypeStack.Count - 1] != DataType.String)
              errorMsg = LocalizationHolder.rm.GetString("Expert_245");
            TypeStack.RemoveAt(TypeStack.Count - 1);
            TypeStack.Add((object) DataType.String);
            break;
          }
          if (funcData.func == FormulaFunc.ref_list)
          {
            if ((DataType) TypeStack[TypeStack.Count - 1] != DataType.String)
              errorMsg = LocalizationHolder.rm.GetString("Expert_247");
            TypeStack.RemoveAt(TypeStack.Count - 1);
            TypeStack.Add((object) DataType.String);
            break;
          }
          if (TypeStack.Count < funcData.parmTypes.Length)
          {
            errorMsg = string.Format(LocalizationHolder.rm.GetString("Expert_120"), (object) funcData.parmTypes.Length);
            return this.infixIndex[index1];
          }
          bool flag;
          do
          {
            flag = false;
            for (int index2 = 0; index2 < funcData.parmTypes.Length; ++index2)
            {
              if (!TempFormula.CanBeConverted((DataType) TypeStack[TypeStack.Count - funcData.parmTypes.Length + index2], funcData.parmTypes[index2]))
              {
                if (!this.TryAnotherFunc(index1))
                {
                  errorMsg = string.Format(LocalizationHolder.rm.GetString("Expert_121"), (object) (index2 + 1));
                  errorMsg += DataTypeConvertor.DataTypeName(funcData.parmTypes[index2]);
                  return this.infixIndex[index1];
                }
                funcData = ExpertFunc.funcs(this.postfixForm[index1].info);
                flag = true;
                break;
              }
            }
          }
          while (flag);
          for (int index3 = 1; index3 <= funcData.parmTypes.Length; ++index3)
            TypeStack.RemoveAt(TypeStack.Count - 1);
          if (this.DropMeasure && funcData.result == DataType.Measured)
          {
            TypeStack.Add((object) DataType.Float);
            break;
          }
          TypeStack.Add((object) funcData.result);
          break;
        case TokenType.Integer:
          TypeStack.Add((object) DataType.Integer);
          break;
        case TokenType.Float:
          TypeStack.Add((object) DataType.Float);
          break;
        case TokenType.String:
          TypeStack.Add((object) DataType.String);
          break;
        case TokenType.Date:
          TypeStack.Add((object) DataType.Date);
          break;
        case TokenType.ObjectLink:
          TypeStack.Add((object) DataType.ObjectLink);
          break;
        case TokenType.Attribute:
          if (token.iValue != (long) Token._Ref)
          {
            int info = token.info;
            PairName pairName = this.pairNames[info];
            AttribPair usedAttr = this.usedAttrs[info];
            if (pairName.Multi)
            {
              dataType1 = DataType.Packet;
            }
            else
            {
              FieldTypes attrType = pairName.ft;
              if (pairName.ft == FieldTypes.ftSystem)
                attrType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) usedAttr.attribID);
              try
              {
                dataType1 = DataTypeConvertor.AttrType2DataType(attrType);
              }
              catch (EInvalidAttrType ex)
              {
                errorMsg = ex.Message;
                return this.infixIndex[index1];
              }
            }
            if (this.DropMeasure && dataType1 == DataType.Measured)
            {
              TypeStack.Add((object) DataType.Float);
              break;
            }
            TypeStack.Add((object) dataType1);
            break;
          }
          break;
        case TokenType.Command:
          switch (token.info)
          {
            case 0:
            case 1:
              if (TypeStack.Count < 1)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_122");
                return this.infixIndex[index1];
              }
              if ((DataType) TypeStack[TypeStack.Count - 1] != DataType.Boolean)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_123");
                return this.infixIndex[index1];
              }
              TypeStack.RemoveAt(TypeStack.Count - 1);
              continue;
            case 2:
              if ((long) TypeStack.Count < token.iValue)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_124");
                return this.infixIndex[index1];
              }
              for (int index4 = 0; (long) index4 < token.iValue; ++index4)
                TypeStack.RemoveAt(TypeStack.Count - 1);
              TypeStack.Add((object) DataType.Packet);
              continue;
            case 3:
              int int32 = Convert.ToInt32(token.text.Substring(token.text.Length - 1));
              if (int32 > 2)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_260");
                return this.infixIndex[index1];
              }
              if (TypeStack.Count < int32)
              {
                errorMsg = LocalizationHolder.rm.GetString("Expert_124");
                return this.infixIndex[index1];
              }
              for (int index5 = 0; index5 < int32; ++index5)
                TypeStack.RemoveAt(TypeStack.Count - 1);
              AttribPair usedAttr1 = this.usedAttrs[(int) token.iValue];
              IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(usedAttr1.attribID);
              TypeStack.Add((object) DataTypeConvertor.AttrType2DataType(attributeType.FieldType, usedAttr1.attribID));
              continue;
            default:
              continue;
          }
        case TokenType.Measured:
          if (this.DropMeasure && dataType1 == DataType.Measured)
          {
            TypeStack.Add((object) DataType.Float);
            break;
          }
          TypeStack.Add((object) DataType.Measured);
          break;
        case TokenType.Boolean:
          TypeStack.Add((object) DataType.Boolean);
          break;
      }
    }
    if (TypeStack.Count < 1)
      errorMsg = LocalizationHolder.rm.GetString("Expert_125");
    else if (TypeStack.Count > 1)
    {
      errorMsg = LocalizationHolder.rm.GetString("Expert_126");
    }
    else
    {
      DataType dtCurr = (DataType) TypeStack[TypeStack.Count - 1];
      if (!TempFormula.CanBeConverted(dtCurr, this.resType))
        errorMsg = !this.DropMeasure || this.resType != DataType.Measured || dtCurr != DataType.Float ? (!this.isArray || dtCurr != DataType.Packet ? (dtCurr != DataType.Measured || this.resType != DataType.Float ? LocalizationHolder.rm.GetString("Expert_127") + DataTypeConvertor.DataTypeName(this.resType) : "-" + LocalizationHolder.rm.GetString("Expert_183")) : "") : "";
    }
    return -1;
  }

  private bool TryAnotherFunc(int i)
  {
    int info = this.postfixForm[i].info;
    string text = ExpertFunc.funcs(info).text;
    for (int index = info + 1; index < ExpertFunc._funcs.Length; ++index)
    {
      if (ExpertFunc._funcs[index].text == text)
      {
        this.postfixForm[i].info = index;
        return true;
      }
    }
    return false;
  }

  public void ConvertAllStrings()
  {
    int index = 0;
    while (index < this.infixForm.Count)
    {
      Token t = this.infixForm[index];
      if (t.type == TokenType.FuncCall && (t.info == ExpertFunc.GetFuncIndex(FormulaFunc.def) || t.info == ExpertFunc.GetFuncIndex(FormulaFunc.str_list) || t.info == ExpertFunc.GetFuncIndex(FormulaFunc.ref_list)))
        index += 2;
      else if (t.type == TokenType.String || t.type == TokenType.Attribute && this.GetAttrDataType(t) == DataType.String)
      {
        this.infixForm.Insert(index, new Token(TokenType.FuncCall, LocalizationHolder.rm.GetString("Expert_205") + "(")
        {
          info = ExpertFunc.GetFuncIndex(FormulaFunc.s_float)
        });
        Token token = new Token(TokenType.ClosingBrace, ")");
        this.infixForm.Insert(index + 2, token);
        index += 2;
        this.StringWasConverted = true;
      }
      else
        ++index;
    }
    this.UpdateTokenBegs();
  }

  internal DataType GetAttrDataType(Token t)
  {
    int info = t.info;
    PairName pairName = this.pairNames[info];
    AttribPair usedAttr = this.usedAttrs[info];
    if (pairName.Multi)
      return DataType.Packet;
    FieldTypes attrType = pairName.ft;
    if (pairName.ft == FieldTypes.ftSystem)
      attrType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) usedAttr.attribID);
    try
    {
      return DataTypeConvertor.AttrType2DataType(attrType);
    }
    catch
    {
      return DataType.Unknown;
    }
  }

  private int GetStackItems(int fromIndex)
  {
    int stackItems = 0;
    for (int index = fromIndex; index < this.postfixForm.Count; ++index)
    {
      Token token = this.postfixForm[index];
      switch (token.type)
      {
        case TokenType.BinaryOper:
          --stackItems;
          break;
        case TokenType.FuncCall:
          FuncData funcData = ExpertFunc.funcs(token.info);
          if (funcData.func == FormulaFunc.STR || funcData.func == FormulaFunc.skipNull || funcData.func == FormulaFunc.skipNull_0 || funcData.func == FormulaFunc.skipNull_1)
          {
            ++stackItems;
            break;
          }
          stackItems = stackItems - funcData.parmTypes.Length + 1;
          break;
        case TokenType.Integer:
        case TokenType.Float:
        case TokenType.String:
        case TokenType.Date:
        case TokenType.ObjectLink:
        case TokenType.Measured:
          ++stackItems;
          break;
        case TokenType.Attribute:
          if (token.iValue != (long) Token._Ref)
          {
            ++stackItems;
            break;
          }
          break;
        case TokenType.Command:
          switch ((ExpCommandType) token.info)
          {
            case ExpCommandType.JumpTrue:
            case ExpCommandType.JumpFalse:
              --stackItems;
              continue;
            case ExpCommandType.FormPackage:
              stackItems = stackItems - (int) token.iValue + 1;
              continue;
            default:
              continue;
          }
        default:
          return stackItems;
      }
    }
    return stackItems;
  }

  public void InsertToken(int newPos, Token t)
  {
    t.AssignStackInfo();
    this.infixForm.Insert(newPos, t);
    this.UpdateTokenBegs();
  }

  public void AddToken(Token t)
  {
    t.AssignStackInfo();
    this.infixForm.Add(t);
    this.UpdateTokenBegs();
  }

  public bool AddAttributeToken(IUserSession ius, Guid attrGuid, Guid objTypeGuid)
  {
    IDBAttributeType attributeType1 = ius.GetAttributeType(attrGuid, false);
    if (attributeType1 == null)
      return false;
    int attributeId = attributeType1.AttributeID;
    int num = -1;
    string shortName = attributeType1.ShortName;
    string name = attributeType1.Name;
    FieldTypes attributeType2 = attributeType1.AttributeType;
    bool multis = attributeType1.MultipleValued == MultiValueModes.MultiValues || attributeType1.MultipleValued == MultiValueModes.MultiValuesFromList;
    string oShortName = "";
    string oLongName = "";
    IDBObjectType objectType = ius.GetObjectType(objTypeGuid, false);
    if (objectType != null)
    {
      num = objectType.ObjectType;
      oShortName = objectType.ObjectTypeShortName;
      oLongName = objectType.ObjectTypeName;
    }
    int index1 = -1;
    for (int index2 = 0; index2 < this.usedAttrs.Count; ++index2)
    {
      AttribPair usedAttr = this.usedAttrs[index2];
      if (usedAttr.attribID == attributeId && (num == -1 && usedAttr.objTypeID == 0 || num != -1 && usedAttr.objTypeID == num))
      {
        index1 = index2;
        break;
      }
    }
    if (index1 < 0)
    {
      AttribPair attribPair;
      PairName pairName;
      if (num != -1)
      {
        attribPair = new AttribPair(attributeId, num);
        pairName = new PairName(shortName, name, oShortName, oLongName, attributeType2, multis);
      }
      else
      {
        attribPair = new AttribPair(attributeId);
        pairName = new PairName(shortName, name, "", "", attributeType2, multis);
      }
      this.usedAttrs.Add(attribPair);
      this.pairNames.Add(pairName);
      this.attrGUIDs.Add(attrGuid.ToString());
      this.objTypeGUIDs.Add(num == -1 ? "" : objTypeGuid.ToString());
      index1 = this.usedAttrs.Count - 1;
    }
    this.infixForm.Add(new Token(TokenType.Attribute, this.pairNames[index1].ShortName)
    {
      info = index1
    });
    this.UpdateTokenBegs();
    return true;
  }

  /// <summary>
  /// Собирает типы объектов и атрибутов из usedAttrs в объекты attrs и objs
  /// </summary>
  /// <param name="attrs"></param>
  /// <param name="objs"></param>
  /// <returns>true, если какие-то атрибуты были</returns>
  public bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    for (int index = 0; index < this.usedAttrs.Count; ++index)
    {
      string attrGuiD = this.attrGUIDs[index];
      string objTypeGuiD = this.objTypeGUIDs[index];
      attrs.Add(new IdGuid(attrGuiD, -1));
      objs.Add(new IdGuid(objTypeGuiD, -1));
    }
    return this.usedAttrs.Count > 0;
  }

  /// <summary>Исправить неправильные идентификаторы в usedAttrs</summary>
  /// <param name="attrs"></param>
  /// <param name="objs"></param>
  /// <returns>true, если что-то изменилось</returns>
  public bool FixIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.usedAttrs != null)
    {
      for (int index1 = 0; index1 < this.usedAttrs.Count; ++index1)
      {
        AttribPair usedAttr = this.usedAttrs[index1];
        string attrGuiD = this.attrGUIDs[index1];
        string str = this.objTypeGUIDs[index1];
        if (usedAttr.objTypeID == -1 && str != "")
        {
          this.objTypeGUIDs[index1] = "";
          str = "";
          flag = true;
        }
        int index2 = -1;
        for (int index3 = 0; index3 < attrs.Count; ++index3)
        {
          if (attrs[index3].sGuid == attrGuiD)
          {
            index2 = index3;
            break;
          }
        }
        if (index2 >= 0 && attrs[index2].Id != usedAttr.attribID)
        {
          usedAttr.attribID = attrs[index2].Id;
          flag = true;
        }
        int index4 = -1;
        for (int index5 = 0; index5 < objs.Count; ++index5)
        {
          if (objs[index5].sGuid == str)
          {
            index4 = index5;
            break;
          }
        }
        if (index4 >= 0 && objs[index4].Id != usedAttr.objTypeID)
        {
          usedAttr.objTypeID = objs[index4].Id;
          flag = true;
        }
      }
    }
    return flag;
  }

  /// <summary>
  /// Заполняет массивы типов атрибутов и объектов, исправляет ИД в usedAttrs, infixForm не трогает вообще :(
  /// </summary>
  /// <param name="ius"></param>
  /// <returns></returns>
  public bool FixInfixForm(IUserSession ius)
  {
    List<IdGuid> attrs = new List<IdGuid>();
    List<IdGuid> objs = new List<IdGuid>();
    if (this.attrGUIDs != null)
    {
      for (int index = 0; index < this.attrGUIDs.Count; ++index)
      {
        string str = Convert.ToString(this.attrGUIDs[index]);
        if (str != "")
        {
          bool flag = false;
          foreach (IdGuid idGuid in attrs)
          {
            if (idGuid.sGuid == str)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(str));
            if (attributeTypeId != -1)
              attrs.Add(new IdGuid(str, attributeTypeId));
          }
        }
      }
    }
    if (this.objTypeGUIDs != null)
    {
      for (int index = 0; index < this.objTypeGUIDs.Count; ++index)
      {
        string str = Convert.ToString(this.objTypeGUIDs[index]);
        if (str != "")
        {
          bool flag = false;
          foreach (IdGuid idGuid in objs)
          {
            if (idGuid.sGuid == str)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(str));
            if (objectTypeId != -1)
              objs.Add(new IdGuid(str, objectTypeId));
          }
        }
      }
    }
    return this.FixIDs(attrs, objs);
  }

  /// <summary>Заполняет GUID'ы для тех токенов, в которых они нужны</summary>
  /// <param name="ius"></param>
  /// <returns></returns>
  public bool CreateGUIDs(IUserSession ius)
  {
    bool guiDs = false;
    foreach (Token token in this.infixForm)
    {
      if (token.type == TokenType.Integer && token._Guid == "" && token.spt == SelectionParameterTypes.sptObject && token.iValue != 0L)
      {
        QuickObjectInfo objectInfo = ius.GetObjectInfo(token.iValue);
        token._Guid = objectInfo.VersionGuid.ToString();
        guiDs = true;
      }
    }
    foreach (Token token in this.postfixForm)
    {
      if (token.type == TokenType.Integer && token._Guid == "" && token.spt == SelectionParameterTypes.sptObject && token.iValue != 0L)
      {
        QuickObjectInfo objectInfo = ius.GetObjectInfo(token.iValue);
        token._Guid = objectInfo.VersionGuid.ToString();
        guiDs = true;
      }
    }
    return guiDs;
  }

  /// <summary>Заново собрать ссылки на объекты по всем токенам</summary>
  public void FillObjectLinks()
  {
    this.objectLinks.Clear();
    foreach (Token token in this.infixForm)
    {
      if (token.type == TokenType.Integer && token._Guid != "" && token.spt == SelectionParameterTypes.sptObject && this.objectLinks.IndexOf(token.iValue) < 0)
        this.objectLinks.Add(token.iValue);
    }
  }

  public void BeautifyInfixForm()
  {
    foreach (Token token in this.infixForm)
      token.Beautify();
  }

  public void CheckAllTokens(IUserSession ius)
  {
    foreach (Token token in this.infixForm)
    {
      if (token.type == TokenType.Integer && token._Guid != "")
      {
        IDBObject dbObject = ius.GetObject(token.iValue, false);
        if (dbObject == null || ((IDBLifecycleLevel) dbObject).LevelID == ius.IdentHelper.DeletedID)
          token.state = TokenState.ObjNotFound;
        else if (token.trueText != dbObject.Caption)
          token.state = TokenState.ObjCaptionChanged;
      }
    }
  }

  /// <summary>
  /// Поправить все идентификаторы в формуле (плевать на эффективность, это вызывается только после перекачки по запросу юзера)
  /// </summary>
  /// <returns>true, если что-то было изменено</returns>
  public bool FixIdentsComplete(IUserSession ius)
  {
    bool flag = false;
    if (this.usedAttrs != null)
    {
      for (int index = 0; index < this.usedAttrs.Count; ++index)
      {
        AttribPair usedAttr = this.usedAttrs[index];
        int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(this.attrGUIDs[index]);
        if (attributeTypeId1 != -1 && usedAttr.attribID != attributeTypeId1)
        {
          usedAttr.attribID = attributeTypeId1;
          flag = true;
        }
        string objTypeGuiD = this.objTypeGUIDs[index];
        if (objTypeGuiD != null)
        {
          int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(objTypeGuiD);
          if (attributeTypeId2 != -1 && usedAttr.objTypeID != attributeTypeId2)
          {
            usedAttr.objTypeID = attributeTypeId2;
            flag = true;
          }
        }
      }
    }
    foreach (Token token in this.infixForm)
    {
      if (token.type == TokenType.Integer && token._Guid != "" && token.spt == SelectionParameterTypes.sptObject)
      {
        int index = this.objectLinks.IndexOf(token.iValue);
        QuickObjectInfo objectInfo = ius.GetObjectInfo(new Guid(token._Guid));
        if (!objectInfo.Empty && token.iValue != objectInfo.ObjectID)
        {
          if (index >= 0)
            this.objectLinks[index] = objectInfo.ObjectID;
          token.iValue = objectInfo.ObjectID;
          token.text = Convert.ToString(objectInfo.ObjectID);
          flag = true;
        }
        if (index < 0)
        {
          this.objectLinks.Add(token.iValue);
          flag = true;
        }
      }
    }
    foreach (Token token in this.postfixForm)
    {
      if (token.type == TokenType.Integer && token._Guid != "" && token.spt == SelectionParameterTypes.sptObject)
      {
        int index = this.objectLinks.IndexOf(token.iValue);
        QuickObjectInfo objectInfo = ius.GetObjectInfo(new Guid(token._Guid));
        if (!objectInfo.Empty && token.iValue != objectInfo.ObjectID)
        {
          if (index >= 0)
            this.objectLinks[index] = objectInfo.ObjectID;
          token.iValue = objectInfo.ObjectID;
          flag = true;
        }
        if (index < 0)
        {
          this.objectLinks.Add(token.iValue);
          flag = true;
        }
      }
    }
    return flag;
  }

  public bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (this.usedAttrs != null)
    {
      for (int index = 0; index < this.usedAttrs.Count; ++index)
      {
        AttribPair usedAttr = this.usedAttrs[index];
        int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(this.attrGUIDs[index]);
        if (attributeTypeId1 != -1)
          eoi.AddAttrType(attributeTypeId1);
        else
          flag = false;
        string objTypeGuiD = this.objTypeGUIDs[index];
        if (objTypeGuiD != null && objTypeGuiD != "")
        {
          int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(objTypeGuiD);
          if (attributeTypeId2 != -1)
            eoi.AddObjType(attributeTypeId2);
        }
      }
    }
    foreach (Token token in this.infixForm)
    {
      if (token.type == TokenType.Integer && token._Guid != "" && token.spt != SelectionParameterTypes.sptNone)
        eoi.AddObjLink(new Guid(token._Guid), ius);
    }
    return flag;
  }

  /// <summary>Заменить fromAttr на toAttr</summary>
  /// <param name="fromAttr">Старый атрибут</param>
  /// <param name="toAttr">Новый атрибут</param>
  /// <returns>true, если атрибут был найден и заменен</returns>
  public bool PerformAttrChange(IDBAttributeType fromAttr, IDBAttributeType toAttr)
  {
    bool flag = false;
    if (this.usedAttrs != null)
    {
      for (int index = 0; index < this.usedAttrs.Count; ++index)
      {
        AttribPair usedAttr = this.usedAttrs[index];
        if (usedAttr.attribID == fromAttr.AttributeID)
        {
          this.usedAttrs[index] = new AttribPair(toAttr.AttributeID, usedAttr.objTypeID);
          PairName pairName = this.pairNames[index];
          pairName.attrShortName = toAttr.ShortName;
          pairName.attrLongName = toAttr.Name;
          pairName.UpdateRealNames();
          this.pairNames[index] = pairName;
          this.attrGUIDs[index] = fromAttr.GUID.ToString();
          foreach (Token token in this.infixForm)
          {
            if (token.type == TokenType.Attribute && token.info == index)
              token.text = pairName.ShortName;
          }
          foreach (Token token in this.postfixForm)
          {
            if (token.type == TokenType.Attribute && token.info == index)
              token.text = pairName.ShortName;
          }
          flag = true;
          break;
        }
      }
    }
    return flag;
  }

  private enum ValidateType
  {
    NotAfterOperator,
    NotAfterOperand,
    NotAfterDivider,
    NotAfterOpenBrace,
    OnlyAfterOperand,
    NotFirst,
    NotLast,
    OnlyAfterAttribute,
  }

  private class OpStackItem
  {
    public int tokIndex;
    public int postIndex;
    public Token t;

    public OpStackItem(int Index, int pIndex, Token t)
    {
      this.tokIndex = Index;
      this.postIndex = pIndex;
      this.t = t;
    }
  }
}
