
// Type: Intermech.PdfPrintCenter.PrintCenterNinjectModule




using Intermech.Diagnostics;
using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings;
using Intermech.PdfPrintCenter.Services;
using Intermech.PdfPrintCenter.Utils;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;


namespace Intermech.PdfPrintCenter
{
    internal sealed class PrintCenterNinjectModule : NinjectModule
    {
      public override void Load()
      {
        this.Bind<PrintCenterSystem>().ToSelf().InSingletonScope();
        this.Bind<PrintCenterForm>().ToSelf().InSingletonScope();
        this.Bind<LayoutEditor>().ToSelf();
        this.Bind<PrintersSettingsForm>().ToSelf();
        this.Bind<WatermarkForm>().ToSelf();
        this.Bind<ExceptionForm>().ToSelf();
        this.Bind<ILayoutsAnalyzerService>().To<LayoutsAnalyzerService>().InSingletonScope();
        this.Bind<ILayoutSettingsService>().To<LayoutSettingsService>().InSingletonScope();
        this.Bind<IPrintersSettingsService>().To<PrintersSettingsService>().InSingletonScope();
        this.Bind<IWatermarkSettingsService>().To<WatermarkSettingsService>().InSingletonScope();
        this.Bind<IWindowSettingsService>().To<WindowSettingsService>().InSingletonScope();
        this.Bind<IPDMSystemService>().To<PDMSystemService>().InSingletonScope();
        this.Bind<IEventLogWriter>().ToMethod<EventLogWriterSyncWrapper>((Func<IContext, EventLogWriterSyncWrapper>) (context => EventLogWriters.Synchronized(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, PrintCenterConsts.PrintCenterTitle)))).InSingletonScope();
        this.Bind<PrintCenterStartupService>().ToSelf().InSingletonScope();
        this.Bind<IPrintCenterFormServices>().ToFactory<IPrintCenterFormServices>();
        this.Bind<IPrintCenterSettingsFactory>().ToFactory<IPrintCenterSettingsFactory>();
      }
    }
}
