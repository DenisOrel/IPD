// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ExceptionHandlersModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools.Integrators;
using System;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class ExceptionHandlersModule : InitializerModule
{
  private readonly IExceptionHandlerService exceptionService;

  public ExceptionHandlersModule(IExceptionHandlerService exceptionService)
  {
    this.exceptionService = exceptionService != null ? exceptionService : throw new ArgumentNullException(nameof (exceptionService));
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.exceptionService.HandleException += new Intermech.Interfaces.ExceptionHandler(this.OnShowIntegratorNotInstalledException);
    this.exceptionService.HandleException += new Intermech.Interfaces.ExceptionHandler(this.OnShowBadIntegratorSettingsException);
    this.exceptionService.HandleException += new Intermech.Interfaces.ExceptionHandler(this.OnShowAppNotInstalledException);
    this.exceptionService.HandleException += new Intermech.Interfaces.ExceptionHandler(this.OnShowBadAppSettingsException);
    this.exceptionService.HandleException += new Intermech.Interfaces.ExceptionHandler(this.OnShowAppProxyException);
  }

  protected override void DoShutdown()
  {
    this.exceptionService.HandleException -= new Intermech.Interfaces.ExceptionHandler(this.OnShowIntegratorNotInstalledException);
    this.exceptionService.HandleException -= new Intermech.Interfaces.ExceptionHandler(this.OnShowBadIntegratorSettingsException);
    this.exceptionService.HandleException -= new Intermech.Interfaces.ExceptionHandler(this.OnShowAppNotInstalledException);
    this.exceptionService.HandleException -= new Intermech.Interfaces.ExceptionHandler(this.OnShowBadAppSettingsException);
    this.exceptionService.HandleException -= new Intermech.Interfaces.ExceptionHandler(this.OnShowAppProxyException);
    base.DoShutdown();
  }

  private void OnShowIntegratorNotInstalledException(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is IntegratorNotInstalledException))
      return;
    IntegratorNotInstalledException exception = (IntegratorNotInstalledException) e.Exception;
    int num = (int) MessageBox.Show(e.Exception.Message, exception.IntegratorName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    e.Handled = true;
  }

  private void OnShowBadIntegratorSettingsException(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is BadIntegratorSettingsException))
      return;
    BadIntegratorSettingsException exception = (BadIntegratorSettingsException) e.Exception;
    int num = (int) MessageBox.Show(exception.Message, exception.IntegratorName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    e.Handled = true;
  }

  private void OnShowAppNotInstalledException(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is ApplicationNotInstalledException))
      return;
    ApplicationNotInstalledException exception = (ApplicationNotInstalledException) e.Exception;
    int num = (int) MessageBox.Show(e.Exception.Message, exception.IntegratorName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    e.Handled = true;
  }

  private void OnShowBadAppSettingsException(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is BadApplicationSettingsException))
      return;
    BadApplicationSettingsException exception = (BadApplicationSettingsException) e.Exception;
    StringBuilder stringBuilder = new StringBuilder(128 /*0x80*/);
    stringBuilder.AppendFormat("Не удалось настроить приложение {0} на взаимодействие с IPS.", (object) exception.ApplicationName);
    stringBuilder.Append(' ');
    stringBuilder.Append(exception.Message);
    int num = (int) MessageBox.Show(stringBuilder.ToString(), exception.IntegratorName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    e.Handled = true;
  }

  private void OnShowAppProxyException(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is ApplicationProxyException))
      return;
    int num = (int) MessageBox.Show(e.Exception.Message, LocalizationHolder.rm.GetString("Tools.Client_209"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    e.Handled = true;
  }
}
