// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierService.LastNumberCalculator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Generic;


namespace Intermech.Kernel.Services.ClassifierService;

internal sealed class LastNumberCalculator : INumberCalculator
{
  public long GetNumber(List<long> presentNumbers, long startValue, long increment)
  {
    presentNumbers.Sort();
    long presentNumber = presentNumbers[presentNumbers.Count - 1];
    return presentNumber < startValue ? startValue + increment : presentNumber + increment;
  }
}
