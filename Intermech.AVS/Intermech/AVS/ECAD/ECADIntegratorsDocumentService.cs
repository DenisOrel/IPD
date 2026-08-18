// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ECAD.ECADIntegratorsDocumentService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.AVS.ECAD;

internal sealed class ECADIntegratorsDocumentService : IECADIntegratorsDocumentService
{
  public void CreateSpecificationWindow(long assemblyID)
  {
    ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true).InvokeFunc<AVSWindow>(-1, (Func<AVSWindow>) (() => AVSPlugin.Instance.OpenAVSWindow(assemblyID)));
  }
}
