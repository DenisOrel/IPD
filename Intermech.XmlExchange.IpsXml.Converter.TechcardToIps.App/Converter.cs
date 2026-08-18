// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Converter
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using Intermech.Client.Specialized;
using Intermech.Interfaces;
using Intermech.Interfaces.Caches.Metadata;
using Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Resources;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

internal class Converter : ApplicationContext
{
  private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
  private ProgressForm _progressForm = new ProgressForm();

  private void OnInitProgress(int stepCount, string initMessage)
  {
    this._progressForm.Invoke((Delegate) (() => this._progressForm.InitProgress(stepCount, initMessage)));
  }

  private void OnProgress(int step, string message)
  {
    this._progressForm.Invoke((Delegate) (() => this._progressForm.DoProgress(step, message)));
  }

  private void Convert()
  {
    bool flag = ((int) this.InputParams.ShowProgress ?? 0) != 0;
    ClientApplicationLifecycleHandler lifecycleHandler = (ClientApplicationLifecycleHandler) null;
    try
    {
      this._progressForm.CancellationToken = this._cancellationToken;
      if (flag)
        this.MainForm.Invoke((Delegate) (() => this._progressForm.Show()));
      Environment.SetEnvironmentVariable("IPS_ROSLYNSCRIPTCOMPILER", this.InputParams.RolsynScriptCompiler);
      lifecycleHandler = IpsConnector.Connect(this.InputParams.UserName, this.InputParams.UserPassword, this.InputParams.UserRole);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        ((IMetaDataHelperCache) MetaDataHelperService.Instance).LoadMetadata((session as IUserSessionCacheDataSet).CacheDataSet);
        new TechToIpsXmlService().Convert(Path.GetFullPath(this.InputParams.InputFile), Path.GetFullPath(this.InputParams.ConfigFile), this.InputParams.WorkDir, session, this._cancellationToken.Token, new Action<int, string>(this.OnInitProgress), new Action<int, string>(this.OnProgress));
      }
      if (flag)
        this._progressForm.Invoke((Delegate) (() => this._progressForm.Hide()));
    }
    catch (Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(LocalizationHolder.rm.GetString("msgCriticalErrorDuringConvertation"));
      stringBuilder.AppendLine(ex.Message);
      File.WriteAllText(Path.Combine(this.InputParams.WorkDir, "critical_error.log"), stringBuilder.ToString());
      this.ExitThread();
    }
    finally
    {
      lifecycleHandler?.Shutdown();
    }
    this.ExitThread();
  }

  public Converter(InputParams inputParams)
  {
    if (inputParams != null)
      this.InputParams.Assign((object) inputParams);
    if (((int) this.InputParams.ShowProgress ?? 0) != 0)
    {
      this.MainForm = (Form) this._progressForm;
      this._progressForm.Show();
    }
    Task.Run((Action) (() => this.Convert()), this._cancellationToken.Token);
  }

  public InputParams InputParams { get; set; } = new InputParams();
}
