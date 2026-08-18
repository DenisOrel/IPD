// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.CadApiService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.AutoCAD.Proxies.COM;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools.Integrators;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal abstract class CadApiService : ApplicationApiService<ICadProxy>
{
  protected const int RejectedCallRetryDelay = 250;
  private readonly ComObjectProvider comObjectProvider;
  private readonly AcadApiOperations apiOperations;
  private AcadIntegratorSettingsService settingsService;
  private bool reconfigureCadSystemOnNextSession;

  public CadApiService(
    IIntegrator owner,
    string applicationName,
    ComObjectProvider comObjectProvider)
    : base(owner, applicationName)
  {
    this.comObjectProvider = comObjectProvider != null ? comObjectProvider : throw new ArgumentNullException(nameof (comObjectProvider));
    this.apiOperations = new AcadApiOperations(owner, (IApplicationApiService) this);
  }

  public AcadIntegratorSettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsService = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
  }

  protected override bool IsInstalled() => this.comObjectProvider.IsRegistered();

  protected override bool IsRunning()
  {
    // ISSUE: reference to a compiler-generated field
    if (CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (CadApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) CadApiService.\u003C\u003Eo__6.\u003C\u003Ep__0, this.comObjectProvider.TryGetRunningInstance(), (object) null);
    return target((CallSite) p1, obj);
  }

  protected override void DoTestApplicationObject(ICadProxy applicationObject)
  {
    applicationObject.KnockKnock();
  }

  protected override ICadProxy DoCreateApplicationObject()
  {
    ICadProxy cadSystemProxy = this.CreateCADSystemProxy();
    if (!this.WaitReady(cadSystemProxy, 30000, 250))
    {
      int num = (int) MessageBox.Show($"Взаимодействие с {this.ApplicationName} в данный момент невозможно, т.к. он занят (например, в нем открыто диалоговое окно). Попробуйте повторить операцию позже.", $"{this.ApplicationName} занят", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      throw new AbortException();
    }
    return cadSystemProxy;
  }

  private bool WaitReady(ICadProxy cadSystemProxy, int totalTime, int interval)
  {
    int num = totalTime / interval;
    if (totalTime % interval != 0)
      ++num;
    while (!cadSystemProxy.IsReady() && num > 0)
    {
      --num;
      Thread.Sleep(interval);
    }
    return cadSystemProxy.IsReady();
  }

  private ICadProxy CreateCADSystemProxy()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, ICadProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ICadProxy), typeof (CadApiService)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, ICadProxy> target = CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, ICadProxy>> p1 = CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadApiService, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateCADSystemProxy", (IEnumerable<System.Type>) null, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) CadApiService.\u003C\u003Eo__10.\u003C\u003Ep__0, this, this.GetOrCreateRawCADSystem());
      return target((CallSite) p1, obj);
    }
    catch (COMException ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("При подключении к {0} произошла ошибка.", (object) this.ApplicationName);
      stringBuilder.Append(' ');
      stringBuilder.Append(ex.Message);
      throw new ApplicationProxyException(stringBuilder.ToString());
    }
  }

  private object GetOrCreateRawCADSystem()
  {
    object rawCadSystem = this.comObjectProvider.TryGetRunningInstance();
    // ISSUE: reference to a compiler-generated field
    if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__0, rawCadSystem, (object) null);
    if (target1((CallSite) p1, obj1))
    {
      try
      {
        rawCadSystem = this.comObjectProvider.CreateInstance();
      }
      catch (COMException ex)
      {
        if (ex.ErrorCode != -2147418111 /*0x80010001*/)
          throw;
        int millisecondsTimeout = 250;
        int num = (int) TimeSpan.FromMinutes(1.0).TotalMilliseconds / millisecondsTimeout;
        while (true)
        {
          // ISSUE: reference to a compiler-generated field
          if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target2 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__5.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p5 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__5;
          // ISSUE: reference to a compiler-generated field
          if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__2.Target((CallSite) CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__2, rawCadSystem, (object) null);
          // ISSUE: reference to a compiler-generated field
          if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          object obj3;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (!CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__4.Target((CallSite) CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__4, obj2))
          {
            // ISSUE: reference to a compiler-generated field
            if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__3 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            obj3 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__3.Target((CallSite) CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__3, obj2, num > 0);
          }
          else
            obj3 = obj2;
          if (target2((CallSite) p5, obj3))
          {
            --num;
            Thread.Sleep(millisecondsTimeout);
            rawCadSystem = this.comObjectProvider.TryGetRunningInstance();
          }
          else
            break;
        }
        // ISSUE: reference to a compiler-generated field
        if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target3 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p7 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__7;
        // ISSUE: reference to a compiler-generated field
        if (CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__6.Target((CallSite) CadApiService.\u003C\u003Eo__11.\u003C\u003Ep__6, rawCadSystem, (object) null);
        if (target3((CallSite) p7, obj4))
          throw;
      }
    }
    return rawCadSystem;
  }

  protected virtual CadProxy DoCreateCADSystemProxy(object rawCADSystem)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadApiService.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadApiService.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, System.Type, object, string, AcadProxy>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (CadProxy) CadApiService.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) CadApiService.\u003C\u003Eo__12.\u003C\u003Ep__0, typeof (AcadProxy), rawCADSystem, this.ApplicationName);
  }

  protected override void DoOpenApiSession(bool topLevelSession)
  {
    base.DoOpenApiSession(topLevelSession);
  }

  protected override void DoCloseApiSession(bool topLevelSession)
  {
    base.DoCloseApiSession(topLevelSession);
  }

  protected override void DoAttachApplicationToApiSession(
    ICadProxy applicationObject,
    bool newApplicationObject)
  {
    base.DoAttachApplicationToApiSession(applicationObject, newApplicationObject);
    if (!applicationObject.IsReady())
      return;
    this.PrepareApplication(applicationObject, newApplicationObject);
  }

  private void PrepareApplication(ICadProxy cadSystem, bool isNewInstance)
  {
    if (IntegratorVars.NakedApiSessions.Value)
    {
      this.reconfigureCadSystemOnNextSession = true;
    }
    else
    {
      if (isNewInstance && !this.reconfigureCadSystemOnNextSession)
        this.reconfigureCadSystemOnNextSession = true;
      if (!this.reconfigureCadSystemOnNextSession)
        return;
      AcadSetupSettings appSetupSettings = this.settingsService.GetAppSetupSettings();
      this.apiOperations.ReconfigureApplication(cadSystem, appSetupSettings);
      this.reconfigureCadSystemOnNextSession = false;
    }
  }
}
