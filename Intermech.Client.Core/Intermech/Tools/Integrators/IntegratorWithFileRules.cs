
// Type: Intermech.Tools.Integrators.IntegratorWithFileRules
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Integrators;

[Obsolete("Use the method GetFileHandlingRules instead of this.", true)]
public struct IntegratorWithFileRules(IntegratorObject integrator, bool commonFileRules)
{
  public IntegratorObject Integrator = integrator;
  public bool CommonFileRules = commonFileRules;
}
