// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.Postfix4SearchOnlyCalculator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.ClassifierService;

internal class Postfix4SearchOnlyCalculator(
  FormulaPattern formulaPattern,
  CounterTemplate counter,
  ClassifierFormula formula,
  RelationalOperators relationalOperator) : Calculator(formulaPattern, counter, formula, relationalOperator)
{
  public Postfix4SearchOnlyCalculator(
    FormulaPattern formulaPattern,
    CounterTemplate counter,
    ClassifierFormula formula)
    : this(formulaPattern, counter, formula, RelationalOperators.EndString)
  {
  }

  protected override void PrepareDesignationAdditionalConditionStructures(
    List<ConditionStructure> result)
  {
    result.Add(new ConditionStructure(this.attributeID, this.relationalOperator, (object) this.formulaPattern.Postfix4Search, LogicalOperators.OR, 1, true));
  }

  protected override object GetDesignationSearchValue(IUserSession session, string documentTypeCode)
  {
    return (object) DocumentsHelper.AppendDocCode(session, this.formulaPattern.Postfix4Search, documentTypeCode, true);
  }

  protected override ConditionStructure[] GetAdditionalConditionStructures(IUserSession session)
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(this.attributeID, this.relationalOperator, (object) this.formulaPattern.Postfix4Search, LogicalOperators.AND, 0, true)
    };
  }

  protected override string PrepareValue(
    IUserSession session,
    IDocumentTypeSettingsService docSettingsService,
    int docTypeID,
    string value)
  {
    value = base.PrepareValue(session, docSettingsService, docTypeID, value);
    int length = value.LastIndexOf(this.formulaPattern.Postfix4Search);
    if (length >= 0)
      value = value.Substring(0, length);
    return value;
  }
}
