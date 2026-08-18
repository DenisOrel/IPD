// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.Calculator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;


namespace Intermech.Kernel.Services.ClassifierService;

internal abstract class Calculator : ICalculator
{
  protected FormulaPattern formulaPattern;
  protected CounterTemplate counter;
  protected ClassifierFormula formula;
  protected RelationalOperators relationalOperator;
  protected readonly int documentTypeID;
  protected readonly int attributeID;

  public Calculator(
    FormulaPattern formulaPattern,
    CounterTemplate counter,
    ClassifierFormula formula,
    RelationalOperators relationalOperator)
  {
    this.formulaPattern = formulaPattern;
    this.counter = counter;
    this.formula = formula;
    this.relationalOperator = relationalOperator;
    this.documentTypeID = MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    this.attributeID = MetaDataHelper.GetAttributeTypeID(formula.AttributeGuid);
  }

  public virtual string Calculate(IUserSession session, List<int> objTypes)
  {
    List<long> presentNumbers = new List<long>(objTypes.Count);
    ISearchCondition[] conditionsHandlers = this.GetSearchConditionsHandlers(session);
    IDocumentTypeSettingsService customService = session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService;
    foreach (int objType in objTypes)
    {
      ConditionStructure[] array = this.GetBaseConditions(session, conditionsHandlers, objType).ToArray();
      IDBObjectCollection objectCollection = session.GetObjectCollection(objType);
      objectCollection.ShowAllModifications = true;
      ConditionStructure[] conditionStructureArray = this.attributeID != session.IdentHelper.DesignationID ? ConditionStructure.Join(this.GetAdditionalConditionStructures(session), array) : ConditionStructure.Join(this.GetDesignationAdditionalConditionStructures(session, customService, MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType == -1 ? this.documentTypeID : objType)), array);
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureArray.Length != 0 ? conditionStructureArray : (ConditionStructure[]) null, this.ColumnDescriptors);
      DataTable dataTable = this.formula.Private ? objectCollection.Select(paramSet) : objectCollection.SelectWithLocalObjects(paramSet);
      this.OnAfterSelect();
      Regex regex = new Regex(this.PrepareRegexString("^\\d{1,}"));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        Match match = regex.Match(this.PrepareValue(session, customService, Convert.ToInt32(dataTable.Rows[index][1]), Convert.ToString(dataTable.Rows[index][0])));
        long result;
        if (!string.IsNullOrEmpty(match.Value) && long.TryParse(match.Value, out result) && !presentNumbers.Contains(result))
          presentNumbers.Add(result);
      }
    }
    long num = (long) this.counter.StartValue;
    if (presentNumbers.Count > 0)
      num = (this.formula.UseMissed ? (INumberCalculator) new MissedNumberCalculator() : (INumberCalculator) new LastNumberCalculator()).GetNumber(presentNumbers, (long) this.counter.StartValue, (long) this.counter.Increment);
    return $"{this.formulaPattern.Prefix}{ClassifierFormula.DigitsSymbols}{num}{ClassifierFormula.ValuesSeparator}{this.counter.Increment}{ClassifierFormula.ValuesSeparator}{this.counter.DigitsCount}{ClassifierFormula.ValuesSeparator}{this.counter.MaxValue}{ClassifierFormula.DigitsSymbols}{this.formulaPattern.Postfix}";
  }

  protected virtual object GetDesignationSearchValue(IUserSession session, string documentTypeCode)
  {
    return (object) documentTypeCode;
  }

  protected virtual void OnAfterSelect()
  {
  }

  protected virtual string PrepareValue(
    IUserSession session,
    IDocumentTypeSettingsService docSettingsService,
    int docTypeID,
    string value)
  {
    if (this.attributeID == session.IdentHelper.DesignationID)
    {
      DocumentTypeSettings settings = docSettingsService.GetSettings(session.SessionGUID, docTypeID);
      if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
        value = DocumentsHelper.RemoveDocCode(session, value, settings.DocumentTypeCode);
    }
    if (!string.IsNullOrEmpty(this.formulaPattern.Prefix))
      value = value.Remove(0, this.formulaPattern.Prefix.Length);
    return value;
  }

  protected virtual string PrepareRegexString(string regexString) => regexString + "$";

  protected virtual ConditionStructure[] GetAdditionalConditionStructures(IUserSession session)
  {
    return (ConditionStructure[]) null;
  }

  protected virtual void PrepareDesignationAdditionalConditionStructures(
    List<ConditionStructure> result)
  {
  }

  private ConditionStructure[] GetDesignationAdditionalConditionStructures(
    IUserSession session,
    IDocumentTypeSettingsService settingsService,
    List<int> objectTypes)
  {
    List<ConditionStructure> result = new List<ConditionStructure>();
    this.PrepareDesignationAdditionalConditionStructures(result);
    List<string> stringList = new List<string>();
    bool flag = false;
    foreach (int objectType in objectTypes)
    {
      DocumentTypeSettings settings = settingsService.GetSettings(session.SessionGUID, objectType);
      if (settings.DocumentTypeCodeInDesignation && !string.IsNullOrEmpty(settings.DocumentTypeCode))
      {
        if (!stringList.Contains(settings.DocumentTypeCode))
        {
          result.Add(new ConditionStructure(this.attributeID, this.relationalOperator, this.GetDesignationSearchValue(session, settings.DocumentTypeCode), LogicalOperators.OR, result.Count == 0 ? 1 : 0, true));
          stringList.Add(settings.DocumentTypeCode);
        }
      }
      else
        flag = true;
    }
    if (flag && stringList.Count > 0)
      result.Add(new ConditionStructure(this.attributeID, RelationalOperators.EndString, (object) string.Empty, LogicalOperators.OR, 0, true));
    if (result.Count == 1)
      result[0] = new ConditionStructure(this.attributeID, this.relationalOperator, result[0].Value, LogicalOperators.AND, 0, true);
    else if (result.Count > 1)
      result[result.Count - 1] = new ConditionStructure(this.attributeID, this.relationalOperator, result[result.Count - 1].Value, LogicalOperators.AND, -1, true);
    return result.Count <= 0 ? (ConditionStructure[]) null : result.ToArray();
  }

  private List<ConditionStructure> GetBaseConditions(
    IUserSession session,
    ISearchCondition[] conditions,
    int objTypeID)
  {
    List<ConditionStructure> baseConditions = new List<ConditionStructure>();
    foreach (ISearchCondition condition in conditions)
    {
      ConditionStructure[] conditions1 = condition.GetConditions(session, objTypeID);
      if (conditions1 != null)
        baseConditions.AddRange((IEnumerable<ConditionStructure>) conditions1);
    }
    return baseConditions;
  }

  private ColumnDescriptor[] ColumnDescriptors
  {
    get
    {
      return new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) this.attributeID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.DESC, 0),
        new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
    }
  }

  private ISearchCondition[] GetSearchConditionsHandlers(IUserSession session)
  {
    return new ISearchCondition[3]
    {
      (ISearchCondition) new PrefixSearchCondition(this.formulaPattern.Prefix, this.attributeID),
      (ISearchCondition) new LocalOnlySearchCondition(session),
      (ISearchCondition) new PrivateSearchCondition(this.formula.Private)
    };
  }
}
