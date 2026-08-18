// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADCommandsProvider
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADCommandsProvider(IIntegrator integrator) : ECADCommandsProvider(integrator, AltiumObjectTypeGuids.Project)
{
  protected override ElementList elementList => (ElementList) new ADElementList();

  protected override string elementListCommandName => "AD.CreateElementList";
}
