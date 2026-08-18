// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.BasicCaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.UI;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

public abstract class BasicCaptureChangesDriver : CaptureChangesDriver
{
  private CaptureChangesDriverContext driverContext;

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.driverContext = (CaptureChangesDriverContext) null;
  }

  protected sealed override void DoInvoke(
    CaptureChangesContext ctx,
    IPercentageProgressSink progressSink)
  {
    this.driverContext = new CaptureChangesDriverContext((ICaptureChangesDriver) this, ctx);
    this.InitializeDriverContextServices();
    IEnumerable<SectionEntity> rootDocuments = this.DriverContext.Database.GetRootDocuments();
    try
    {
      this.BeginAnalyzeDocuments(rootDocuments);
      if (this.PrepareRootDocuments(rootDocuments) == 0)
        return;
      this.AnalyzeDocuments(progressSink);
    }
    finally
    {
      this.EndAnalyzeDocuments(rootDocuments);
    }
  }

  /// <summary>
  /// Инициализирует сервисы драйвера, которым требуется контекст текущего вызова драйвера. В момент вызова этого метода свойство <see cref="P:DriverContext" /> уже заполнено.
  /// </summary>
  protected virtual void InitializeDriverContextServices()
  {
  }

  protected CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  private int PrepareRootDocuments(IEnumerable<SectionEntity> rootDocuments)
  {
    int num = 0;
    foreach (SectionEntity rootDocument in rootDocuments)
    {
      RootItemSection rootItemSection = rootDocument.Sections.Get<RootItemSection>();
      if (!rootItemSection.Handled)
      {
        rootItemSection.Handled = this.PrepareRootDocument(rootDocument);
        if (rootItemSection.Handled)
          ++num;
      }
    }
    return num;
  }

  protected virtual void BeginAnalyzeDocuments(IEnumerable<SectionEntity> rootDocuments)
  {
  }

  protected virtual void EndAnalyzeDocuments(IEnumerable<SectionEntity> rootDocuments)
  {
  }

  protected abstract bool PrepareRootDocument(SectionEntity rootDocument);

  private void AnalyzeDocuments(IPercentageProgressSink progressSink)
  {
    if (this.DriverContext.Scheduler.Run(progressSink) == CooperativeSchedulerResult.Cancelled)
      throw new AbortException("Пользователь прервал выполнение операции.");
  }
}
