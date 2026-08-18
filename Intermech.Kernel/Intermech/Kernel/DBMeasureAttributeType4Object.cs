// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMeasureAttributeType4Object
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class DBMeasureAttributeType4Object(UserSession uSession, DataRow row) : 
  DBAttributeType4Object(uSession, row),
  IDBMeasureAttributeType
{
  private BaseMeasureRuleHelper _RuleHelper;

  public BaseMeasureRuleHelper RuleHelper
  {
    get
    {
      if (this._RuleHelper == null)
        this._RuleHelper = (BaseMeasureRuleHelper) new ServerMeasureRuleHelper(this.ValidationRule, (object) this);
      return this._RuleHelper;
    }
  }

  protected override void SaveRuleFormulaLinks(int childID, string newValue)
  {
    string newFormula = this.RuleHelper.ValidateRuleString(newValue);
    this._AttributeType.SaveFormulaLinks(childID, -1, newFormula, Consts.Attribute4ValidationRule, false);
  }

  public override string ValidationRule
  {
    set => base.ValidationRule = value;
  }

  public string RuleFormula => this.RuleHelper.RuleFormula;

  public long DefaultMeasureID => this.RuleHelper.DefaultMeasureID;

  public bool ShortNameInString => this.RuleHelper.ShortNameInString;

  public bool ConvertToDefaultMeasure => this.RuleHelper.ConvertToDefaultMeasure;

  public void ValidateMuID(long muID)
  {
    (this._AttributeType as IDBMeasureAttributeType).ValidateMuID(muID);
  }

  public long[] GetValidPhysicalValues()
  {
    return (this._AttributeType as IDBMeasureAttributeType).GetValidPhysicalValues();
  }

  public bool IsCompatible(long aMeasureID)
  {
    return (this._AttributeType as IDBMeasureAttributeType).IsCompatible(aMeasureID);
  }
}
