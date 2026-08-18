// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ComInterop.ComApplicationApiService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.Integrators.ComInterop;

public abstract class ComApplicationApiService : ApplicationApiService<object>
{
  private readonly ProgIdProvider comObjectProvider;

  public ComApplicationApiService(IIntegrator owner, string applicationName, string progId)
    : base(owner, applicationName)
  {
    this.comObjectProvider = !string.IsNullOrEmpty(progId) ? new ProgIdProvider(progId, false) : throw new ArgumentException();
  }

  protected override bool IsInstalled() => this.comObjectProvider.IsRegistered();

  protected override bool IsRunning()
  {
    // ISSUE: reference to a compiler-generated field
    if (ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (ComApplicationApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ComApplicationApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) ComApplicationApiService.\u003C\u003Eo__2.\u003C\u003Ep__0, this.comObjectProvider.TryGetRunningInstance(), (object) null);
    return target((CallSite) p1, obj);
  }

  /// <summary>
  /// Выполняет подключение к интегрируемому приложению. Метод возвращает API-объект приложения, через который осуществляется всё взаимодействие с приложением.
  /// </summary>
  /// <returns>API-объект приложения</returns>
  protected override object DoCreateApplicationObject()
  {
    object applicationObject = this.comObjectProvider.TryGetRunningInstance();
    if (applicationObject != null)
    {
      try
      {
        this.DoTestApplicationObject(applicationObject);
      }
      catch
      {
        this.DoReleaseApplicationObject(applicationObject);
        applicationObject = (object) null;
      }
    }
    if (applicationObject == null)
      applicationObject = this.comObjectProvider.CreateInstance();
    return applicationObject;
  }
}
