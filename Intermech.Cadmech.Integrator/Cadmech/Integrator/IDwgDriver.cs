// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.IDwgDriver
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal interface IDwgDriver : ICaptureChangesDriver
{
  IIntegrator Integrator { get; }

  AcadIntegratorSettings IntegratorSettings { get; }

  IDrawingTypesInfo DrawingTypes { get; }

  void CheckDocumentTypeSupported(int documentType);
}
