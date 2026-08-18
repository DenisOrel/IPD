// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.PostfixOnlyCalculator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class PostfixOnlyCalculator(
  FormulaPattern formulaPattern,
  CounterTemplate counter,
  ClassifierFormula formula) : Calculator(formulaPattern, counter, formula, RelationalOperators.EndString)
{
  protected override string PrepareValue(
    IUserSession session,
    IDocumentTypeSettingsService docSettingsService,
    int docTypeID,
    string value)
  {
    value = base.PrepareValue(session, docSettingsService, docTypeID, value);
    int startIndex = value.LastIndexOf(this.formulaPattern.Postfix);
    if (startIndex >= 0)
      value = value.Remove(startIndex);
    return value;
  }

  protected override string PrepareRegexString(string regexString) => regexString;
}
