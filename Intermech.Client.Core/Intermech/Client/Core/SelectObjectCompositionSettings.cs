
// Type: Intermech.Client.Core.SelectObjectCompositionSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Client.Core;

/// <summary>Настройки контрола выбора в составе объектов</summary>
[Serializable]
public class SelectObjectCompositionSettings
{
  private bool _checkAllObjectsOnLoad;

  public SelectObjectCompositionSettings()
  {
    this.BackgroundVisibleObjectsCompositionLoad = true;
    this.CheckAllObjectsOnLoad = true;
    this.AutoLoadComposition = SelectObjectCompositionAutoload.Full;
    this.AutoLoadCompositionDepth = 2;
    this.WarningWhenCheckedNotLoaded = true;
    this.WarningWhenCheckedCountMoreThan = true;
    this.WarningWhenCheckedCountMoreThanCount = 1000;
  }

  public SelectObjectCompositionSettings([NotNull] SelectObjectCompositionSettings copyFrom)
  {
    this.BackgroundVisibleObjectsCompositionLoad = copyFrom.BackgroundVisibleObjectsCompositionLoad;
    this.CheckAllObjectsOnLoad = copyFrom.CheckAllObjectsOnLoad;
    this.AutoLoadComposition = copyFrom.AutoLoadComposition;
    this.AutoLoadCompositionDepth = copyFrom.AutoLoadCompositionDepth;
    this.WarningWhenCheckedNotLoaded = copyFrom.WarningWhenCheckedNotLoaded;
    this.WarningWhenCheckedCountMoreThan = copyFrom.WarningWhenCheckedCountMoreThan;
    this.WarningWhenCheckedCountMoreThanCount = copyFrom.WarningWhenCheckedCountMoreThanCount;
  }

  public SelectObjectCompositionSettings(
    bool backgroundVisibleObjectsCompositionLoad,
    bool checkAllObjectsOnLoad,
    SelectObjectCompositionAutoload autoLoadComposition,
    int autoLoadCompositionDepth,
    bool warningWhenCheckedNotLoaded,
    bool warningWhenCheckedCountMoreThan,
    int warningWhenCheckedCountMoreThanCount)
  {
    this.BackgroundVisibleObjectsCompositionLoad = backgroundVisibleObjectsCompositionLoad;
    this.CheckAllObjectsOnLoad = checkAllObjectsOnLoad;
    this.AutoLoadComposition = autoLoadComposition;
    this.AutoLoadCompositionDepth = autoLoadCompositionDepth;
    this.WarningWhenCheckedNotLoaded = warningWhenCheckedNotLoaded;
    this.WarningWhenCheckedCountMoreThan = warningWhenCheckedCountMoreThan;
    this.WarningWhenCheckedCountMoreThanCount = warningWhenCheckedCountMoreThanCount;
  }

  /// <summary>Загружать ли автоматически состав видимых узлов (убирать "плюсики" у объектов без состава)</summary>
  public bool BackgroundVisibleObjectsCompositionLoad { get; protected set; }

  /// <summary>Отмечать ли все объекты при открытии формы</summary>
  public virtual bool CheckAllObjectsOnLoad
  {
    [DebuggerStepThrough] get => this._checkAllObjectsOnLoad;
    [DebuggerStepThrough] protected set => this._checkAllObjectsOnLoad = value;
  }

  /// <summary>На какую глубину загружать состав объектов при открытии формы</summary>
  public virtual SelectObjectCompositionAutoload AutoLoadComposition { get; protected set; }

  /// <summary>Если AutoLoadComposition == SelectObjectCompositionAutoload.Depth то определяет глубину загрузки состава объектов при
  /// открытии формы</summary>
  public int AutoLoadCompositionDepth { get; protected set; }

  /// <summary>Предупреждать ли пользователя в том случае, если есть отмеченные объекты с незагруженным составом</summary>
  public bool WarningWhenCheckedNotLoaded { get; protected set; }

  /// <summary>Предупреждать ли пользователя в том случае, если число отмеченных объектов превышает WarningWhenCheckedCountMoreThanCount</summary>
  public bool WarningWhenCheckedCountMoreThan { get; protected set; }

  /// <summary>Число отмеченных объектов, после превышения которого пользователю будет выдаваться предупреждение</summary>
  public int WarningWhenCheckedCountMoreThanCount { get; protected set; }

  public virtual void SaveToDictionary([NotNull] Dictionary<string, object> dic)
  {
    dic["BackgroundVisibleObjectsCompositionLoad"] = (object) this.BackgroundVisibleObjectsCompositionLoad;
    dic["CheckAllObjectsOnLoad"] = (object) this.CheckAllObjectsOnLoad;
    dic["AutoLoadComposition"] = (object) this.AutoLoadComposition;
    dic["AutoLoadCompositionDepth"] = (object) this.AutoLoadCompositionDepth;
    dic["WarningWhenCheckedNotLoaded"] = (object) this.WarningWhenCheckedNotLoaded;
    dic["WarningWhenCheckedCountMoreThan"] = (object) this.WarningWhenCheckedCountMoreThan;
    dic["WarningWhenCheckedCountMoreThanCount"] = (object) this.WarningWhenCheckedCountMoreThanCount;
  }

  public virtual void LoadFromDictionary([NotNull] Dictionary<string, object> dic)
  {
    object obj;
    if (dic.TryGetValue("BackgroundVisibleObjectsCompositionLoad", out obj))
      this.BackgroundVisibleObjectsCompositionLoad = (bool) obj;
    if (dic.TryGetValue("CheckAllObjectsOnLoad", out obj))
      this.CheckAllObjectsOnLoad = (bool) obj;
    if (dic.TryGetValue("AutoLoadComposition", out obj))
      this.AutoLoadComposition = (SelectObjectCompositionAutoload) obj;
    if (dic.TryGetValue("AutoLoadCompositionDepth", out obj))
      this.AutoLoadCompositionDepth = (int) obj;
    if (dic.TryGetValue("WarningWhenCheckedNotLoaded", out obj))
      this.WarningWhenCheckedNotLoaded = (bool) obj;
    if (dic.TryGetValue("WarningWhenCheckedCountMoreThan", out obj))
      this.WarningWhenCheckedCountMoreThan = (bool) obj;
    if (!dic.TryGetValue("WarningWhenCheckedCountMoreThanCount", out obj))
      return;
    this.WarningWhenCheckedCountMoreThanCount = (int) obj;
  }
}
