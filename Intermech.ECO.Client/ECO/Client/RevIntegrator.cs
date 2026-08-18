// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevIntegrator
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ECO.Client;

public sealed class RevIntegrator : NonConfigurableIntegrator
{
  internal static readonly string IntegratorName = "Интегратор с редактором извещений";
  internal static readonly string ApplicationName = "Редактор извещений";
  internal static readonly Guid IntegratorId = new Guid("bede0392-cb90-4f55-adf6-6390c9d6fe98");

  public override Guid Id => RevIntegrator.IntegratorId;

  public override string DisplayName => RevIntegrator.IntegratorName;

  protected override bool HasSpecialFileManagement() => true;

  protected override ICollection<Guid> GetDocumentTypes()
  {
    ICollection<Guid> documentTypes = base.GetDocumentTypes();
    foreach (Guid revObjType in RevIntegrator.GetRevObjTypes())
      documentTypes.Add(revObjType);
    return documentTypes;
  }

  public static List<Guid> GetRevObjTypes()
  {
    return new List<Guid>()
    {
      new Guid(RevHelper.guidObj_II),
      new Guid(RevHelper.guidObj_PI),
      new Guid(RevHelper.guidObj_PR),
      new Guid(RevHelper.guidObj_DI),
      new Guid(RevHelper.guidObj_DPI),
      new Guid(RevHelper.guidChangeJournal),
      new Guid(RevHelper.guidObjTypeServiceNote)
    };
  }
}
