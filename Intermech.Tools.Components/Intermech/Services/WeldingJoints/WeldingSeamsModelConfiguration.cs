// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingSeamsModelConfiguration
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Kernel.Entities;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Конфигурация доменной модели сварных швов. Реализация является thread safe.
/// </summary>
internal sealed class WeldingSeamsModelConfiguration : DBModelConfiguration
{
  protected override void DoBuildModel(DBModelBuilder modelBuilder)
  {
    base.DoBuildModel(modelBuilder);
    modelBuilder.Entity<MechanicalArticleEntity>();
    modelBuilder.Entity<MechanicalDocumentEntity>();
    modelBuilder.Entity<WeldingSeamEntity>();
    modelBuilder.ChildOccurence<MechanicalDocumentOccurence>();
    modelBuilder.ChildOccurence<WeldingSeamOccurence>();
    modelBuilder.ChildOccurence<WeldingSeamComponentOccurence>();
  }
}
