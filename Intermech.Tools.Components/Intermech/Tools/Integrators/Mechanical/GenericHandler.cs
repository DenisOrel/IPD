// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.GenericHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class GenericHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : DocumentHandler(driver, ctx, docItem)
{
}
