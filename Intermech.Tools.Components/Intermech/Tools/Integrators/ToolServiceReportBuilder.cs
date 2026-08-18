// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ToolServiceReportBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

public class ToolServiceReportBuilder : UIReportBuilder
{
  public void ReportFileImportStart(string fullPath)
  {
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException();
    this.ReportStart(string.Format(LocalizationHolder.rm.GetString("Tools.Components_371"), (object) Path.GetFileName(fullPath)));
  }

  public void ReportSaveChangesStart(long objectId)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    this.ReportStart(string.Format(LocalizationHolder.rm.GetString("Tools.Components_389"), (object) DBHelper.GetObjectCaption(objectId)));
  }
}
