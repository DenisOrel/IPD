// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.CalculatorFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel.Services.ClassifierService;

internal class CalculatorFactory
{
  public static ICalculator GetCalculator(
    IUserSession session,
    ClassifierFormula formula,
    string parentFormula)
  {
    string str = CalculatorFactory.PreparePattern(session, parentFormula + formula.Formula);
    CounterTemplate numberCounterTemplate = StringFormula.GetNumberCounterTemplate(str);
    if (numberCounterTemplate.Empty)
      return (ICalculator) new ConstCalculator(str, numberCounterTemplate, formula);
    FormulaPattern formulaPattern = FormulaPattern.Create(str, numberCounterTemplate);
    if (string.IsNullOrEmpty(formulaPattern.Postfix))
      return (ICalculator) new PrefixOnlyCalculator(formulaPattern, numberCounterTemplate, formula);
    if (string.IsNullOrEmpty(formulaPattern.Postfix4Search))
      return (ICalculator) new PostfixOnlyCalculator(formulaPattern, numberCounterTemplate, formula);
    return formulaPattern.EndString ? (ICalculator) new Postfix4SearchOnlyCalculator(formulaPattern, numberCounterTemplate, formula) : (ICalculator) new FullFormulaCalculator(formulaPattern, numberCounterTemplate, formula);
  }

  private static string PreparePattern(IUserSession session, string pattern)
  {
    return StringFormula.ReplaceDatePart(session, pattern);
  }
}
