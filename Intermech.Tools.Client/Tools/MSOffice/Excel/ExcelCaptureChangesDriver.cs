// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelCaptureChangesDriver
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using System;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelCaptureChangesDriver(IServiceProvider integrator) : 
  SingleFileCaptureChangesDriver(integrator)
{
  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    return (IAction) new ExcelDocumentHandler((DocumentCaptureChangesDriver) this, this.DriverContext, docItem);
  }
}
