// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IntegratorException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators;

public class IntegratorException : Exception
{
  private readonly string integratorName;

  public IntegratorException(string integratorName, string message)
    : base(message)
  {
    this.integratorName = !string.IsNullOrEmpty(integratorName) ? integratorName : throw new ArgumentException("Не задано значение аргумента метода.", nameof (integratorName));
  }

  public string IntegratorName => this.integratorName;
}
