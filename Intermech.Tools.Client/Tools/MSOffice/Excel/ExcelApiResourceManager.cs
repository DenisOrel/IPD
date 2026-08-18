// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelApiResourceManager
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelApiResourceManager : CompositeApiResourceManager, IMsoApiResourceTracker
{
  private object applicationObject;
  private OpenFilesApiResourceManager openFilesManager;

  public ExcelApiResourceManager(object applicationObject)
  {
    this.applicationObject = applicationObject != null ? applicationObject : throw new ArgumentNullException(nameof (applicationObject));
    this.openFilesManager = (OpenFilesApiResourceManager) new ExcelOpenFilesManager(applicationObject);
  }

  protected override ICollection<ApplicationApiResourceManager> GetSubManagers()
  {
    return (ICollection<ApplicationApiResourceManager>) new ApplicationApiResourceManager[1]
    {
      (ApplicationApiResourceManager) this.openFilesManager
    };
  }

  protected override void ReportErrors()
  {
    base.ReportErrors();
    ExcelApiHelper.EnsureApplicationWindowIsAvailableToUser(this.applicationObject);
  }

  public void TrackOpenFile(string fullPath) => this.openFilesManager.TrackOpenFile(fullPath);
}
