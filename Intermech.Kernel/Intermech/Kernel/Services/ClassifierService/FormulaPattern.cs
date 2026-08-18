// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.FormulaPattern
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class FormulaPattern
{
  public string Value { get; private set; }

  public string Prefix { get; private set; }

  public string Postfix { get; private set; }

  public string Postfix4Search { get; private set; }

  public bool EndString { get; private set; }

  public FormulaPattern(string pattern) => this.Value = pattern;

  public static FormulaPattern Create(string pattern, CounterTemplate counter)
  {
    string str1 = pattern.Substring(0, counter.StartIndex);
    string str2 = pattern.Substring(counter.EndIndex + 1, pattern.Length - counter.EndIndex - 1);
    string str3 = string.Empty;
    bool flag = true;
    if (str2 != string.Empty)
    {
      int length = str2.IndexOf(ClassifierFormula.CalculateSeparator);
      if (length >= 0)
      {
        str3 = str2.Substring(0, length);
        str2 = str3 + str2.Substring(length + ClassifierFormula.CalculateSeparator.Length, str2.Length - length - ClassifierFormula.CalculateSeparator.Length);
        pattern = pattern.Replace(ClassifierFormula.CalculateSeparator, string.Empty);
        if (str3 != string.Empty)
        {
          if (str3.IndexOf('_') >= 0)
            throw new Exception("Нельзя использовать символ _ после счетчика в формуле классификатора.");
          if (str3.IndexOf('*') >= 0)
            throw new Exception("Нельзя использовать символ * после счетчика в формуле классификатора.");
          str3 += "*";
          flag = false;
        }
      }
      else
        str3 = str2;
    }
    if (!flag)
      str3 = "*" + str3;
    return new FormulaPattern(pattern)
    {
      Prefix = str1,
      Postfix = str2,
      Postfix4Search = str3,
      EndString = flag
    };
  }

  public void RestorePostfix4Search()
  {
    if (string.IsNullOrEmpty(this.Postfix4Search) || this.EndString)
      return;
    this.Postfix4Search = this.Postfix4Search.Trim('*');
  }
}
