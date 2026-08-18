// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Cadmech3DServices
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.LaunchActions;
using Interop.Cadmech;
using System;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class Cadmech3DServices : ICadmech3DServices
{
  private readonly IIntegratorRegistry integrators;

  public Cadmech3DServices() => this.integrators = ClientContext.Integrators;

  public void UseAttInterface(long documentId, Action<IAttInterface> method)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    if (method == null)
      throw new ArgumentNullException();
    int objectType = DBHelper.GetObjectType(documentId);
    IntegratorObject iobj = IntegratorServices.Find(objectType);
    IIntegrator integrator = iobj != null ? this.integrators.GetIntegrator(iobj, true) : throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_109"), (object) DBHelper.GetObjectCaption(documentId)));
    if (!ServiceUtils.IsServiceAvailable((object) integrator, typeof (ILaunchActionSupport)) || !ServiceUtils.IsServiceAvailable((object) integrator, typeof (ICADInterfaceService)))
      throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_110"), (object) DBHelper.GetObjectCaption(documentId), (object) iobj));
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    try
    {
      ClientContext.LaunchActions.Launch(new LaunchParams(LaunchType.Edit, documentId, objectType, editorRule, false));
      using (CADApiSession cadApiSession = new CADApiSession(integrator))
      {
        IAttInterface attInterface = cadApiSession.Application.GetAttInterface();
        method(attInterface);
      }
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_111"), (object) DBHelper.GetObjectCaption(documentId)), ex);
    }
  }

  public T UseAttInterface<T>(long documentId, Func<IAttInterface, T> method)
  {
    T result = default (T);
    this.UseAttInterface(documentId, (Action<IAttInterface>) (attInterface => result = method(attInterface)));
    return result;
  }
}
