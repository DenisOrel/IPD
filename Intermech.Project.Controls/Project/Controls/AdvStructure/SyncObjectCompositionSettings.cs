// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.AdvStructure.SyncObjectCompositionSettings
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Client.Core;
using Intermech.Diagnostics;
using System.Diagnostics;

#nullable disable
namespace Intermech.Project.Controls.AdvStructure;

/// <summary>Настройки импорта </summary>
internal class SyncObjectCompositionSettings : SelectObjectCompositionSettings
{
  public SyncObjectCompositionSettings()
  {
  }

  public SyncObjectCompositionSettings([NotNull] SelectObjectCompositionSettings copyFrom)
    : base(copyFrom)
  {
  }

  public SyncObjectCompositionSettings(
    bool backgroundVisibleObjectsCompositionLoad,
    int autoLoadCompositionDepth,
    bool warningWhenCheckedNotLoaded,
    bool warningWhenCheckedCountMoreThan,
    int warningWhenCheckedCountMoreThanCount)
    : base(backgroundVisibleObjectsCompositionLoad, false, SelectObjectCompositionAutoload.None, autoLoadCompositionDepth, warningWhenCheckedNotLoaded, warningWhenCheckedCountMoreThan, warningWhenCheckedCountMoreThanCount)
  {
  }

  /// <summary>Отмечать ли все объекты при открытии формы</summary>
  public override bool CheckAllObjectsOnLoad
  {
    [DebuggerStepThrough] get => false;
    [DebuggerStepThrough] protected set
    {
    }
  }

  /// <summary>На какую глубину загружать состав объектов при открытии формы</summary>
  public override SelectObjectCompositionAutoload AutoLoadComposition
  {
    [DebuggerStepThrough] get => SelectObjectCompositionAutoload.None;
    [DebuggerStepThrough] protected set
    {
    }
  }
}
