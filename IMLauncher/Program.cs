// Decompiled with JetBrains decompiler
// Type: IMLauncher.Program
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using Intermech.Globalization;
using Intermech.Interfaces;
using Intermech.Services;
using Intermech.UI;
using Intermech.UI.Winforms;
using System;
using System.Windows.Forms;

#nullable disable
namespace IMLauncher;

internal static class Program
{
  private static UIExceptionHandler uiExceptionHandler;

  [STAThread]
  private static void Main()
  {
    UICultureHelper.ApplySettingsFromConfigurationFile();
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
    try
    {
      AppServiceContext appServices = Program.CreateAppServices();
      Program.uiExceptionHandler = new UIExceptionHandler((Action<Exception>) (uiException => appServices.ExceptionService.ShowException(uiException)));
      Program.uiExceptionHandler.Activate();
      Application.Run((Form) new ApplicationListForm()
      {
        AppServices = appServices
      });
    }
    finally
    {
      if (Program.uiExceptionHandler != null)
      {
        Program.uiExceptionHandler.Deactivate();
        Program.uiExceptionHandler = (UIExceptionHandler) null;
      }
    }
  }

  private static AppServiceContext CreateAppServices()
  {
    return new AppServiceContext()
    {
      ExceptionService = (IExceptionHandlerService) new ExceptionHandlerService((IUIDispatcherService) UIDispatcherService.FromCurrentUIThread(), new Func<Exception, DialogResult>(Program.ShowUnhandledExceptionDialog))
    };
  }

  private static DialogResult ShowUnhandledExceptionDialog(Exception exception)
  {
    using (ExceptionForm exceptionForm = new ExceptionForm())
      return exceptionForm.ShowException(exception);
  }
}
