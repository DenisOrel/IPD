// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADIntegratorSettings
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADIntegratorSettings : ECADIntegratorSettings, ISettingsObject
{
  public List<GlobalId<int>> PCBDocumentTypes { get; set; }

  public List<GlobalId<int>> SchemaDocumentTypes { get; set; }

  public GlobalId<int> ProjectType { get; set; }

  public string GerberFiles { get; set; }

  public ComponentsFilterSettings<ADComponentsCompositionVariants> ComponentsFilter { get; set; }

  public List<Tuple<StringKey, StringKey, bool>> ProjectAttributes { get; set; }

  public string AdditionalFilesExt { get; set; }

  public string PartTypeParameter { get; set; }

  public List<Tuple<StringKey, StringKey>> VariantsFilter { get; set; }

  public string QuantityParameter { get; set; }
}
