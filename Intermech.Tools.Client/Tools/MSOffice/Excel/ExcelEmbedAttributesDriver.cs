// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelEmbedAttributesDriver
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelEmbedAttributesDriver(IIntegrator integrator) : 
  DocumentEmbedAttributesDriver(integrator)
{
  protected override void DoSaveModifiedDocument(IOpenDocument document)
  {
    this.RecalculateMacroFields((OpenComDocument) document);
    base.DoSaveModifiedDocument(document);
  }

  private void RecalculateMacroFields(OpenComDocument document)
  {
    if (!((ExcelIntegratorSettings) this.SettingsService.GetSettingsObject()).RunAutoOpenMacro)
      return;
    // ISSUE: reference to a compiler-generated field
    if (ExcelEmbedAttributesDriver.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelEmbedAttributesDriver.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RunAutoMacros", (IEnumerable<Type>) null, typeof (ExcelEmbedAttributesDriver), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ExcelEmbedAttributesDriver.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) ExcelEmbedAttributesDriver.\u003C\u003Eo__2.\u003C\u003Ep__0, document.ComObject, 1);
  }
}
