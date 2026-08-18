// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadStateProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Прокси-объект для COM-объекта состояния CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
public class CadStateProxy : CadObjectProxy
{
  private object rawState;
  private CadProxy cadProxy;

  /// <summary>Создает объект.</summary>
  /// <param name="rawState">Исходный необернутый COM-объект состояния CAD-системы</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="rawState" /> содержит null</exception>
  public CadStateProxy(object rawState, CadProxy cadProxy)
  {
    if (rawState == null)
      throw new ArgumentNullException(nameof (rawState));
    if (cadProxy == null)
      throw new ArgumentNullException(nameof (cadProxy));
    this.rawState = rawState;
    this.cadProxy = cadProxy;
  }

  /// <summary>
  /// Возвращает состояние CAD-системы: занята она или готова к использованию
  /// </summary>
  public bool IsReady
  {
    get
    {
      try
      {
        if (CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
          CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (CadStateProxy)));
        Func<CallSite, object, bool> target = CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, bool>> p1 = CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__1;
        if (CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
          CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsQuiescent", typeof (CadStateProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj = CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) CadStateProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, this.rawState);
        return target((CallSite) p1, obj);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.cadProxy.ApplicationName, "IAcadState.IsQuiescent");
      }
    }
  }
}
