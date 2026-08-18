
// Type: Intermech.Navigator.Classifiers.ClassifierCalcFormula
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;


namespace Intermech.Navigator.Classifiers;

/// <summary>Классификатор в разрезе расчетной формулы</summary>
public class ClassifierCalcFormula
{
  private readonly string _calcFormulaAttributeGuid = "cad001d7-306c-11d8-b4e9-00304f19f545";

  public ClassifierCalcFormula(long classifierID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.Initialize(sessionKeeper.Session, classifierID);
  }

  public ClassifierCalcFormula(IUserSession session, long classifierID)
  {
    this.Initialize(session, classifierID);
  }

  private void Initialize(IUserSession session, long classifierID)
  {
    IDBObject dbObject = session.GetObject(classifierID);
    if (dbObject == null)
      return;
    this.ClassifierID = classifierID;
    IDBAttributeCollection attributes = dbObject.Attributes;
    IDBAttribute byGuid = dbObject.Attributes.FindByGUID(new Guid(this._calcFormulaAttributeGuid));
    if (byGuid == null)
      return;
    this.CalcFormulaAttributeID = byGuid.AttributeID;
    List<ClassifierAttribute> classifierAttributeList = new List<ClassifierAttribute>();
    ArrayList arrayList = new ArrayList();
    foreach (object obj in byGuid.Values)
    {
      if (obj != null && Convert.ToString(obj) != string.Empty)
      {
        FormulaRecord attributeAndFormula = CalcFormulaRules.GetAttributeAndFormula(Convert.ToString(obj));
        if (attributeAndFormula.AttributeGuid != string.Empty)
        {
          CalcFormulaAttribute attributeValue = new CalcFormulaAttribute(session, attributeAndFormula.AttributeGuid);
          if (attributeValue != null && attributeValue.AttrGUID != string.Empty)
          {
            classifierAttributeList.Add(new ClassifierAttribute(attributeValue, CalcFormulaRules.GetFormula(session, attributeValue.AttrType, (object) attributeAndFormula.Formula, true), attributeAndFormula.SizeControl, attributeAndFormula.UseMissed));
            arrayList.Add(obj);
          }
        }
      }
    }
    this.CalcFormulaValue = (object[]) arrayList.ToArray(typeof (object));
    this.Attributes = classifierAttributeList.ToArray();
  }

  public void AddAttribute(
    CalcFormulaAttribute attr,
    MyElement formula,
    bool sizeControl,
    bool useMissed)
  {
    ClassifierAttribute classifierAttribute = new ClassifierAttribute(attr, formula, sizeControl, useMissed, ClassifierAttributesAction.Create);
    FormulaRecord formulaRecord = new FormulaRecord(attr.AttrGUID, Convert.ToString(formula.Value), sizeControl, useMissed);
    if (this.Attributes != null)
    {
      ClassifierAttribute[] classifierAttributeArray = new ClassifierAttribute[this.Attributes.Length + 1];
      for (int index = 0; index < this.Attributes.Length; ++index)
        classifierAttributeArray[index] = this.Attributes[index];
      classifierAttributeArray[this.Attributes.Length] = classifierAttribute;
      this.Attributes = classifierAttributeArray;
      object[] objArray = new object[this.CalcFormulaValue.Length + 1];
      for (int index = 0; index < this.CalcFormulaValue.Length; ++index)
        objArray[index] = this.CalcFormulaValue[index];
      objArray[this.CalcFormulaValue.Length] = (object) formulaRecord.ToString();
      this.CalcFormulaValue = objArray;
    }
    else
    {
      this.Attributes = new ClassifierAttribute[1]
      {
        classifierAttribute
      };
      this.CalcFormulaValue = new object[1]
      {
        (object) formulaRecord.ToString()
      };
    }
  }

  public void ChangeAttribute(
    CalcFormulaAttribute attr,
    MyElement newFormula,
    bool sizeControl,
    bool useMissed)
  {
    if (this.Attributes != null)
    {
      bool flag = false;
      for (int index = 0; index < this.Attributes.Length; ++index)
      {
        if (this.Attributes[index].AttributeValue.AttrGUID == attr.AttrGUID)
        {
          this.Attributes[index].Formula = newFormula;
          FormulaRecord formulaRecord = new FormulaRecord(attr.AttrGUID, Convert.ToString(newFormula.Value), sizeControl, useMissed);
          this.CalcFormulaValue[index] = (object) formulaRecord.ToString();
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this.AddAttribute(attr, newFormula, sizeControl, useMissed);
    }
    else
      this.AddAttribute(attr, newFormula, sizeControl, useMissed);
  }

  public void DeleteAttribute(CalcFormulaAttribute[] attr)
  {
    for (int index1 = 0; index1 < attr.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.Attributes.Length; ++index2)
      {
        if (this.Attributes[index2].AttributeValue.AttrGUID == attr[index1].AttrGUID)
          this.Attributes[index2].Action = ClassifierAttributesAction.Delete;
      }
    }
  }

  public void ApplyChanges()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.ApplyChanges(sessionKeeper.Session);
  }

  public void ApplyChanges(IUserSession session)
  {
    if (this.Attributes == null || this.Attributes.Length == 0)
      return;
    IDBAttributeCollection attributes = session.GetObject(this.ClassifierID).Attributes;
    IDBAttribute byId = attributes.FindByID(this.CalcFormulaAttributeID);
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < this.Attributes.Length; ++index)
    {
      if (this.Attributes[index] != null && this.Attributes[index].Action != ClassifierAttributesAction.Delete)
        arrayList.Add(this.CalcFormulaValue[index]);
    }
    if (byId != null)
    {
      IDBAttribute dbAttribute = byId;
      object[] objArray;
      if (arrayList.Count <= 0)
        objArray = new object[1]{ (object) string.Empty };
      else
        objArray = (object[]) arrayList.ToArray(typeof (object));
      dbAttribute.Values = objArray;
    }
    else
    {
      if (arrayList.Count <= 0)
        return;
      IDBAttributeType attributeType = session.GetAttributeType(new Guid(this._calcFormulaAttributeGuid));
      attributes.AddAttribute(attributeType.AttributeID, false, (object[]) arrayList.ToArray(typeof (object)));
    }
  }

  public ClassifierAttribute[] Attributes { get; private set; }

  /// <summary>ID классификатора</summary>
  public long ClassifierID { get; private set; } = -1;

  public int CalcFormulaAttributeID { get; private set; } = -1;

  public object[] CalcFormulaValue { get; private set; }

  public void FormCalcFormulaValue(int index)
  {
    ClassifierAttribute attribute = this.Attributes[index];
    FormulaRecord formulaRecord = new FormulaRecord(attribute.AttributeValue.AttrGUID, Convert.ToString(attribute.Formula.Value, (IFormatProvider) CultureInfo.InvariantCulture), attribute.SizeControl, attribute.UseMissed);
    this.CalcFormulaValue[index] = (object) formulaRecord.ToString();
  }
}
