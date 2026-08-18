// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmData
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

/// <summary>Storing operator parms for editing</summary>
public struct OpParmData
{
  public TempFormula tf;
  public TempFormula tf2;
  public TempFormula tf3;
  public List<string> dA_GUIDs;
  public List<string> dA_Texts;
  public List<string> objGUIDs;
  public List<string> linkTexts;
  public List<string> objTexts;
  public List<string> dA_Checks;
  public List<string> forTexts;
  public List<string> forGUIDs;
  public List<bool> forOT_Only;
  public List<int> linkIDs;
  public List<int> ltForOT;
  public FieldTypes attrType;
  public bool b1;
  public bool b2;
  public bool b3;
  public bool b4;
  public bool b5;
  public bool b6;
  public string s1;
  public string s2;
  public string s3;
  public string s4;
  public string s5;
  public string s6;
  public string st1;
  public string st2;
  public string st3;
  public string st4;
  public int settingMod;
  public long exID;
  public List<Triple> listTable;

  public void Init()
  {
    this.tf = new TempFormula(true);
    this.tf2 = new TempFormula(true);
    this.tf3 = new TempFormula(true);
    this.dA_GUIDs = new List<string>();
    this.dA_Texts = new List<string>();
    this.dA_Checks = new List<string>();
    this.linkIDs = new List<int>();
    this.objGUIDs = new List<string>();
    this.linkTexts = new List<string>();
    this.objTexts = new List<string>();
    this.listTable = new List<Triple>();
    this.ltForOT = new List<int>();
    this.forTexts = new List<string>();
    this.forGUIDs = new List<string>();
    this.forOT_Only = new List<bool>();
  }

  public void Clear()
  {
    if (this.tf != null)
      this.tf.Clear();
    if (this.tf2 != null)
      this.tf2.Clear();
    if (this.tf3 != null)
      this.tf3.Clear();
    this.b1 = false;
    this.b2 = false;
    this.b3 = false;
    this.b4 = false;
    this.b5 = false;
    this.b6 = false;
    this.s1 = "";
    this.s2 = "";
    this.s3 = "";
    this.s4 = "";
    this.s5 = "";
    this.s6 = "";
    this.st1 = "";
    this.st2 = "";
    this.st3 = "";
    if (this.dA_GUIDs != null)
      this.dA_GUIDs.Clear();
    if (this.dA_Texts != null)
      this.dA_Texts.Clear();
    if (this.dA_Checks != null)
      this.dA_Checks.Clear();
    if (this.linkIDs != null)
      this.linkIDs.Clear();
    if (this.objGUIDs != null)
      this.objGUIDs.Clear();
    if (this.linkTexts != null)
      this.linkTexts.Clear();
    if (this.objTexts != null)
      this.objTexts.Clear();
    if (this.listTable != null)
      this.listTable.Clear();
    if (this.ltForOT != null)
      this.ltForOT.Clear();
    if (this.forTexts != null)
      this.forTexts.Clear();
    if (this.forGUIDs != null)
      this.forGUIDs.Clear();
    if (this.forOT_Only != null)
      this.forOT_Only.Clear();
    this.settingMod = 0;
    this.exID = 0L;
  }
}
