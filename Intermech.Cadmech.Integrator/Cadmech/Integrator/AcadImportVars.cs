// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadImportVars
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ControlFlow;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class AcadImportVars
{
  public static readonly DynamicVariable<bool> MechanicalOnly = new DynamicVariable<bool>("AcadImportVars.MechanicalOnly", false);
  public static readonly DynamicVariable<bool> ConstructionalOnly = new DynamicVariable<bool>("AcadImportVars.ConstructionalOnly", false);
  public static readonly DynamicVariable<Guid> RootDocumentTypes = new DynamicVariable<Guid>("AcadImportVars.RootDocumentTypes", Guid.Empty);
}
