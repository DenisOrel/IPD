// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ModParmData
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

/// <summary>Storing modifier parms for editing</summary>
public struct ModParmData
{
  public TempFormula tf;
  public List<string> sortGUIDs;
  public List<string> groupGUIDs;
  public List<string> sortTexts;
  public List<string> groupTexts;
  public List<bool> sortChecks;
  public List<bool> groupChecks;
  public bool ForLoop;
  public bool Bool1;
  public string ForAttrGUID;
  public string ForAttrText;
  public int startValue;

  public void Init()
  {
    this.tf = new TempFormula(true);
    this.sortGUIDs = new List<string>();
    this.groupGUIDs = new List<string>();
    this.sortTexts = new List<string>();
    this.groupTexts = new List<string>();
    this.sortChecks = new List<bool>();
    this.groupChecks = new List<bool>();
  }

  public void Clear()
  {
    if (this.tf != null)
      this.tf.Clear();
    if (this.sortGUIDs != null)
    {
      this.sortGUIDs.Clear();
      this.sortTexts.Clear();
      this.sortChecks.Clear();
    }
    if (this.groupGUIDs != null)
    {
      this.groupGUIDs.Clear();
      this.groupTexts.Clear();
      this.groupChecks.Clear();
    }
    this.ForLoop = false;
    this.ForAttrGUID = "";
    this.ForAttrText = "";
    this.Bool1 = false;
    this.startValue = 0;
  }
}
