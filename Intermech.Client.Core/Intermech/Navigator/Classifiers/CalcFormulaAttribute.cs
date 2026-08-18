
// Type: Intermech.Navigator.Classifiers.CalcFormulaAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections;


namespace Intermech.Navigator.Classifiers;

/// <summary>Атрибут в расчетной формуле</summary>
public sealed class CalcFormulaAttribute
{
  public string AttrGUID = string.Empty;
  public string AttrName = string.Empty;
  public int AttrID = -1;
  public FieldTypes AttrType;
  public bool IsSystemType;
  public bool IsAttrList;
  public ArrayList AttrPossibleValues;

  public CalcFormulaAttribute(IUserSession session, string attrGuid)
  {
    if (!MyAttributeHelper.GetAttrInfo(attrGuid, ref this.AttrName, ref this.AttrID, ref this.AttrType, ref this.IsSystemType, ref this.IsAttrList, ref this.AttrPossibleValues))
      return;
    this.AttrGUID = attrGuid;
  }

  public CalcFormulaAttribute(int attrID)
  {
    if (!MyAttributeHelper.GetAttrInfo(attrID, ref this.AttrName, ref this.AttrGUID, ref this.AttrType, ref this.IsSystemType, ref this.IsAttrList, ref this.AttrPossibleValues))
      return;
    this.AttrID = attrID;
  }

  public CalcFormulaAttribute(string attrGuid)
  {
    if (!MyAttributeHelper.GetAttrInfo(attrGuid, ref this.AttrName, ref this.AttrID, ref this.AttrType, ref this.IsSystemType, ref this.IsAttrList, ref this.AttrPossibleValues))
      return;
    this.AttrGUID = attrGuid;
  }
}
