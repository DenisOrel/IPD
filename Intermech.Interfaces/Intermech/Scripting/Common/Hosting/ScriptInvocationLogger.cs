
// Type: Intermech.Scripting.Common.Hosting.ScriptInvocationLogger
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Text;


namespace Intermech.Scripting.Common.Hosting
{
    public sealed class ScriptInvocationLogger : ServiceExtender
    {
      private IScriptExecutorEvents scriptExecutor;
      private IServerEventLogService serverEventLog;
      private bool logAll;

      public ScriptInvocationLogger(
        IScriptExecutorEvents scriptExecutor,
        IServerEventLogService serverEventLog)
      {
        if (scriptExecutor == null)
          throw new ArgumentNullException(nameof (scriptExecutor));
        if (serverEventLog == null)
          throw new ArgumentNullException(nameof (serverEventLog));
        this.scriptExecutor = scriptExecutor;
        this.serverEventLog = serverEventLog;
        this.logAll = false;
      }

      public bool LogAll
      {
        [DebuggerStepThrough] get => this.logAll;
        [DebuggerStepThrough] set
        {
          if (this.logAll == value)
            return;
          if (this.Enabled)
            throw new InvalidOperationException("Невозможно изменить значение свойства.");
          this.logAll = value;
        }
      }

      protected override void DoEnable()
      {
        base.DoEnable();
        this.scriptExecutor.ScriptInvocationFailed += new EventHandler<ScriptInvocationFailedEventArgs>(this.ReportExceptionToLogFile);
        if (!this.LogAll)
          return;
        this.scriptExecutor.ScriptInvocationCompleted += new EventHandler<ScriptInvocationEventArgs>(this.ReportInvocationToLogFile);
      }

      protected override void DoDisable()
      {
        this.scriptExecutor.ScriptInvocationFailed -= new EventHandler<ScriptInvocationFailedEventArgs>(this.ReportExceptionToLogFile);
        this.scriptExecutor.ScriptInvocationCompleted -= new EventHandler<ScriptInvocationEventArgs>(this.ReportInvocationToLogFile);
        base.DoDisable();
      }

      private void ReportExceptionToLogFile(object sender, ScriptInvocationFailedEventArgs e)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(4096 /*0x1000*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("Exception info:");
          stringBuilder.AppendLine(ExceptionServices.GetExtendedExceptionText((Exception) e.Exception));
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("Script method: Script.Execute");
          if (e.Arguments.Length == 0)
          {
            stringBuilder.AppendLine("Script arguments: <none>");
          }
          else
          {
            stringBuilder.AppendLine("Script arguments:");
            for (int index = 0; index < e.Arguments.Length; ++index)
              stringBuilder.AppendLine($"  {index + 1}) {this.ConvertScriptArgumentToString(e.Arguments[index])}");
          }
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("Script code:");
          stringBuilder.AppendLine(e.ScriptCode);
          stringBuilder.AppendLine("------------------------------------");
          try
          {
            this.serverEventLog.AddToTrace(stringBuilder.ToString(), "script_errors.log");
          }
          catch (Exception ex)
          {
            SuppressedExceptions.TraceException(ex, "ScriptInvocationLogger.ReportExceptionToLogFile()");
          }
        }
      }

      private void ReportInvocationToLogFile(object sender, ScriptInvocationEventArgs e)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(4096 /*0x1000*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("Сценарий был успешно выполнен.");
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("Script method: Script.Execute");
          if (e.Arguments.Length == 0)
          {
            stringBuilder.AppendLine("Script arguments: <none>");
          }
          else
          {
            stringBuilder.AppendLine("Script arguments:");
            for (int index = 0; index < e.Arguments.Length; ++index)
              stringBuilder.AppendLine($"  {index + 1}) {this.ConvertScriptArgumentToString(e.Arguments[index])}");
          }
          stringBuilder.AppendLine();
          stringBuilder.AppendLine("Script code:");
          stringBuilder.AppendLine(e.ScriptCode);
          stringBuilder.AppendLine("------------------------------------");
          try
          {
            this.serverEventLog.AddToTrace(stringBuilder.ToString(), "script_invocations.log");
          }
          catch (Exception ex)
          {
            SuppressedExceptions.TraceException(ex, "ScriptInvocationLogger.ReportInvocationToLogFile()");
          }
        }
      }

      private string ConvertScriptArgumentToString(object argument)
      {
        if (argument == null)
          return "<null>";
        return RemotingServices.IsTransparentProxy(argument) ? "<transparent proxy>" : Convert.ToString(argument);
      }
    }
}
