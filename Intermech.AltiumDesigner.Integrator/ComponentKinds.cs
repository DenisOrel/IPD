// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ComponentKinds
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal enum ComponentKinds
{
  [Description("Standard")] Standard,
  [Description("Mechanical")] Mechanical,
  [Description("Graphical")] Graphical,
  [Description("Net tie (in BOM)")] NetTie_BOM,
  [Description("Net tie (no BOM)")] NetTie_NoBOM,
  [Description("Standard (no BOM)")] Standard_NoBOM,
}
