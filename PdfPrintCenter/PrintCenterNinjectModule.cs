// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterNinjectModule
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

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
            this.Bind<IEventLogWriter>().ToMethod<EventLogWriterSyncWrapper>((Func<IContext, EventLogWriterSyncWrapper>)(context => EventLogWriters.Synchronized(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, PrintCenterConsts.PrintCenterTitle)))).InSingletonScope();
            this.Bind<PrintCenterStartupService>().ToSelf().InSingletonScope();
            this.Bind<IPrintCenterFormServices>().ToFactory<IPrintCenterFormServices>();
            this.Bind<IPrintCenterSettingsFactory>().ToFactory<IPrintCenterSettingsFactory>();
        }
    }
}
