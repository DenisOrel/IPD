// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.PrefixOnlyCalculator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class PrefixOnlyCalculator(
  FormulaPattern formulaPattern,
  CounterTemplate counter,
  ClassifierFormula formula) : Calculator(formulaPattern, counter, formula, RelationalOperators.EndString)
{
  protected override object GetDesignationSearchValue(IUserSession session, string documentTypeCode)
  {
    return (object) documentTypeCode;
  }
}
