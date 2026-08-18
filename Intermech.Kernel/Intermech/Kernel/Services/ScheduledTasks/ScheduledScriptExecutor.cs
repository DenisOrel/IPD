// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.ScheduledScriptExecutor
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class ScheduledScriptExecutor
{
  private readonly ScheduledScriptInfo _scriptInfo;

  public ScheduledScriptExecutor([NotNull] ScheduledScriptInfo scriptInfo)
  {
    this._scriptInfo = scriptInfo;
  }

  public bool Execute([NotNull] IUserSession session)
  {
    IDBObject dbObject = session.GetObject(this._scriptInfo.ScriptGuid, false);
    if (dbObject == null)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_14187.ssp_appserver_14188()), (object) this._scriptInfo.ScriptName, (object) this._scriptInfo.ScriptGuid));
    string code = string.Empty;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00366-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null)
      code = Convert.ToString(attributeByGuid.Value).Trim();
    if (string.IsNullOrEmpty(code))
      return true;
    try
    {
      string message = ScriptExecHelper.IsolatedExecScript(code, CSharpScriptInvocationOptions.WithOptimizations, (object) session, null);
      if (!string.IsNullOrEmpty(message))
        throw new Exception(message);
    }
    catch (Exception ex)
    {
      IEventLogHelper service = ServiceUtils.GetService<IEventLogHelper>((object) ApplicationServices.Container, false);
      if (ex is ISimpleMessageException)
      {
        if (service != null)
        {
          string str = string.Format(LocalizationHolder.rm.GetString(sc_14187.ssp_appserver_14189()), (object) dbObject.Caption);
          service.AddToTrace(string.Format("{0}{1}{2}{1}{3}", (object) "------------------------------------", (object) Environment.NewLine, (object) str, (object) ex.Message), Consts.traceAlways, "scheduled_scripts.log");
        }
        throw;
      }
      string str1 = string.Format(LocalizationHolder.rm.GetString(sc_14187.ssp_appserver_14190()), (object) dbObject.Caption);
      service?.AddToTrace(string.Format("{0}{1}{2}{1}{3}{1}{4}", (object) "------------------------------------", (object) Environment.NewLine, (object) str1, (object) ex.Message, (object) ExceptionServices.GetExtendedStackTrace(ex)), Consts.traceAlways, "scheduled_scripts.log");
      throw new KernelException(str1 + ex.Message);
    }
    return true;
  }
}
