// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ExplicitModelConfigurationProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class ExplicitModelConfigurationProvider : 
  CADInterfaceObjectProxy,
  IModelConfigurationProvider
{
  private IModelConfiguration rawModelConfiguration;

  public ExplicitModelConfigurationProvider(IModelConfiguration rawModelConfiguration)
  {
    this.rawModelConfiguration = rawModelConfiguration != null ? rawModelConfiguration : throw new ArgumentNullException(nameof (rawModelConfiguration));
  }

  /// <summary>
  /// Находит и возвращает COM-объект конфигурации документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  public IModelConfiguration RawConfiguration
  {
    [DebuggerStepThrough] get => this.rawModelConfiguration;
  }
}
