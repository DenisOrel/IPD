// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertFormulable
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Expert.Server;

public abstract class ExpertFormulable : 
  ExpertObject,
  IExpertFormulable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public Token[] infixForm;
  public Token[] postfixForm;
  public DataType resType = DataType.Boolean;
  public long[] formLinks;

  public ExpertFormulable(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._objType = ExpertObjType.Formula;
  }

  protected override void LoadBLOBData(byte[] data)
  {
    TempFormula tempFormula = new TempFormula();
    using (MemoryStream input = new MemoryStream(data))
    {
      if (this.GetType() != typeof (ExpertCond))
        tempFormula.Cond = false;
      using (BinaryReader br = new BinaryReader((Stream) input))
      {
        FormulaHeader formulaHeader = new FormulaHeader(br);
        tempFormula.Load(br, formulaHeader.Version);
      }
    }
    this.infixForm = (Token[]) Array.CreateInstance(typeof (Token), tempFormula.Count);
    for (int index = 0; index < tempFormula.Count; ++index)
      this.infixForm[index] = tempFormula.infixForm[index];
    this.postfixForm = (Token[]) Array.CreateInstance(typeof (Token), tempFormula.postfixForm.Count);
    for (int index = 0; index < tempFormula.postfixForm.Count; ++index)
      this.postfixForm[index] = tempFormula.postfixForm[index];
    this.resType = tempFormula.resType;
    this.formLinks = (long[]) Array.CreateInstance(typeof (long), tempFormula.objectLinks.Count);
    for (int index = 0; index < tempFormula.objectLinks.Count; ++index)
      this.formLinks[index] = tempFormula.objectLinks[index];
  }

  protected override byte[] SaveBLOBData()
  {
    MemoryStream output = new MemoryStream();
    TempFormula shortTempFormula = this.GetShortTempFormula();
    using (BinaryWriter bw = new BinaryWriter((Stream) output))
    {
      FormulaHeader.Write(bw);
      shortTempFormula.Save(bw);
    }
    return output.GetBuffer();
  }

  public TempFormula GetShortTempFormula()
  {
    TempFormula shortTempFormula = new TempFormula();
    shortTempFormula.resType = this.resType;
    if (this.infixForm != null)
    {
      for (int index = 0; index < this.infixForm.Length; ++index)
        shortTempFormula.infixForm.Add(this.infixForm[index].CloneToken());
    }
    if (this.postfixForm != null)
    {
      for (int index = 0; index < this.postfixForm.Length; ++index)
        shortTempFormula.postfixForm.Add(this.postfixForm[index].CloneToken());
    }
    if (this.formLinks != null)
    {
      if (shortTempFormula.objectLinks == null)
        shortTempFormula.objectLinks = new List<long>();
      else
        shortTempFormula.objectLinks.Clear();
      for (int index = 0; index < this.formLinks.Length; ++index)
        shortTempFormula.objectLinks.Add(this.formLinks[index]);
    }
    return shortTempFormula;
  }

  public TempFormula GetTempFormula()
  {
    TempFormula shortTempFormula = this.GetShortTempFormula();
    shortTempFormula.Init();
    shortTempFormula.Cond = this.GetType() == typeof (ExpertCond);
    if (this.attribs != null)
    {
      for (int index = 0; index < this.attribs.Length; ++index)
      {
        shortTempFormula.usedAttrs.Add(this.attribs[index].Clone() as AttribPair);
        PairName attName = ExpertServer.es.attNames[this.attribs[index]];
        shortTempFormula.pairNames.Add(attName);
        shortTempFormula.attrGUIDs.Add(this.attrGUIDs[index]);
        shortTempFormula.objTypeGUIDs.Add(this.objTypeGUIDs[index]);
      }
    }
    if (shortTempFormula.objectLinks == null)
      shortTempFormula.objectLinks = new List<long>();
    else
      shortTempFormula.objectLinks.Clear();
    if (this.formLinks != null)
    {
      for (int index = 0; index < this.formLinks.Length; ++index)
        shortTempFormula.objectLinks.Add(this.formLinks[index]);
    }
    shortTempFormula.DropMeasure = this.DropMeasure;
    shortTempFormula.AutoConvert = this.AutoConvert;
    return shortTempFormula;
  }

  public virtual void SetTempFormula(TempFormula tf)
  {
    this.infixForm = (Token[]) Array.CreateInstance(typeof (Token), tf.Count);
    for (int index = 0; index < tf.Count; ++index)
      this.infixForm[index] = tf.infixForm[index].CloneToken();
    this.postfixForm = (Token[]) Array.CreateInstance(typeof (Token), tf.postfixForm.Count);
    for (int index = 0; index < tf.postfixForm.Count; ++index)
      this.postfixForm[index] = tf.postfixForm[index].CloneToken();
    this.resType = tf.resType;
    this.attribs = (AttribPair[]) Array.CreateInstance(typeof (AttribPair), tf.usedAttrs.Count);
    this.attrGUIDs = (string[]) Array.CreateInstance(typeof (string), tf.usedAttrs.Count);
    this.objTypeGUIDs = (string[]) Array.CreateInstance(typeof (string), tf.usedAttrs.Count);
    for (int index = 0; index < tf.usedAttrs.Count; ++index)
    {
      this.attribs[index] = (AttribPair) tf.usedAttrs[index].Clone();
      if (!ExpertServer.es.attNames.ContainsKey(this.attribs[index]))
        ExpertServer.es.attNames[this.attribs[index]] = (PairName) tf.pairNames[index].Clone();
      this.attrGUIDs[index] = tf.attrGUIDs[index];
      this.objTypeGUIDs[index] = tf.objTypeGUIDs[index];
      if (!ExpertServer.es.idents.ContainsKey(new Guid(this.attrGUIDs[index])))
        ExpertServer.es.idents.GetOrAdd(new Guid(this.attrGUIDs[index]), (long) this.attribs[index].attribID);
      if (this.objTypeGUIDs[index] != null && this.objTypeGUIDs[index] != "" && !ExpertServer.es.idents.ContainsKey(new Guid(this.objTypeGUIDs[index])))
        ExpertServer.es.idents.GetOrAdd(new Guid(this.objTypeGUIDs[index]), (long) this.attribs[index].objTypeID);
    }
    this.formLinks = (long[]) Array.CreateInstance(typeof (long), tf.objectLinks.Count);
    for (int index = 0; index < tf.objectLinks.Count; ++index)
      this.formLinks[index] = tf.objectLinks[index];
    this.DropMeasure = tf.DropMeasure;
    this.AutoConvert = tf.AutoConvert;
    if (!(this._Name == ""))
      return;
    this._Name = tf.Text;
  }

  public void UpdateObject(TempFormula tf)
  {
    this.SetTempFormula(tf);
    AttributeValues[] valuesList1 = this.SaveData();
    bool flag = true;
    foreach (AttributeValues attributeValues in valuesList1)
    {
      if (attributeValues.AttributeID == ExpertConsts.Consts._attrObjName)
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      AttributeValues[] valuesList2 = new AttributeValues[valuesList1.Length + 1];
      valuesList1.CopyTo((Array) valuesList2, 0);
      valuesList2[valuesList2.Length - 1] = new AttributeValues(ExpertConsts.Consts._attrObjName, (object) this._Name);
      this.SetAttributesValues(valuesList2, false, false);
    }
    else
      this.SetAttributesValues(valuesList1, false, false);
  }

  protected override List<long> CollectObjectLinks()
  {
    List<long> longList = base.CollectObjectLinks();
    if (this.formLinks != null)
    {
      foreach (long formLink in this.formLinks)
      {
        if (longList.IndexOf(formLink) < 0)
          longList.Add(formLink);
      }
    }
    return longList;
  }

  public bool DropMeasure
  {
    get => ((ulong) this._Flags & 1UL) > 0UL;
    set
    {
      if (value)
        this._Flags |= 1L;
      else
        this._Flags &= 4294967294L;
    }
  }

  public bool AutoConvert
  {
    get => ((ulong) this._Flags & 2UL) > 0UL;
    set
    {
      if (value)
        this._Flags |= 2L;
      else
        this._Flags &= 4294967293L;
    }
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    TempFormula tempFormula = this.GetTempFormula();
    bool flag = tempFormula.FixIdentsComplete(ius);
    if (this.cond != null)
      flag = flag || this.cond.FixIdentsComplete(ius);
    if (flag)
      this.UpdateObject(tempFormula);
    return flag;
  }

  public override bool CreateGUIDs(IUserSession ius)
  {
    TempFormula tempFormula = this.GetTempFormula();
    bool guiDs = tempFormula.CreateGUIDs(ius);
    if (this.cond != null)
      guiDs = guiDs || this.cond.CreateGUIDs(ius);
    if (guiDs)
      this.UpdateObject(tempFormula);
    return guiDs;
  }

  public override bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    TempFormula tempFormula = this.GetTempFormula();
    int num = tempFormula.PerformAttrChange(fromAttribute, toAttribute) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.UpdateObject(tempFormula);
    return num != 0;
  }
}
