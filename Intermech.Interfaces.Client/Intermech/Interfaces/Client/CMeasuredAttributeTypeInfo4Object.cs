// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CMeasuredAttributeTypeInfo4Object
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Обработчик описателя атрибута применительно к типу объектов, выраженного в единицах измерения
/// </summary>
internal class CMeasuredAttributeTypeInfo4Object(
  MetadataInfoParentContext serviceContext,
  DataRow attr_row,
  DataRow attr4type_row) : CAttributeTypeInfo4Object(serviceContext, attr_row, attr4type_row), IDBMeasureAttributeType
{
  protected ClientMeasureRuleHelper _RuleHelper;

  public ClientMeasureRuleHelper RuleHelper
  {
    get
    {
      if (this._RuleHelper == null)
        this._RuleHelper = new ClientMeasureRuleHelper(this.ValidationRule, (object) this);
      return this._RuleHelper;
    }
  }

  public string RuleFormula => this.RuleHelper.RuleFormula;

  public long DefaultMeasureID => this.RuleHelper.DefaultMeasureID;

  public bool ShortNameInString => this.RuleHelper.ShortNameInString;

  public bool ConvertToDefaultMeasure => this.RuleHelper.ConvertToDefaultMeasure;

  public long[] GetValidPhysicalValues()
  {
    if (this.SizeType <= 0L)
      return this.GetMDValuesInt64("OBJ_LINKS_ID");
    return new long[1]{ this.SizeType };
  }

  public bool IsCompatible(long aMeasureID)
  {
    return this.RuleHelper.IsCompatible((IDBMeasureAttributeType) this, aMeasureID);
  }

  public void ValidateMuID(long muID)
  {
    this.RuleHelper.ValidateMuID((IDBMeasureAttributeType) this, muID);
  }
}
