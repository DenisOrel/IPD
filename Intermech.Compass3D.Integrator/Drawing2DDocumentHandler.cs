// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DDocumentHandler
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DDocumentHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : DocumentWithArticlesHandler(driver, ctx, docItem)
{
}
