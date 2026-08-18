// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.ICompositionConfiguratorServerService
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public interface ICompositionConfiguratorServerService
{
  void CopyApplicationConditionsToAllInstances(
    Guid userSessionGuid,
    Tuple<long, long, long>[] compositionParts);
}
