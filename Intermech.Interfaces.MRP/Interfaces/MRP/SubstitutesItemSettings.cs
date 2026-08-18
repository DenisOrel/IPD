// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.SubstitutesItemSettings
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Настройки, связаннные с допустимыми заменами.
/// Сохраняется в настройках по идентификатору связи.
/// </summary>
[Serializable]
public sealed class SubstitutesItemSettings : BaseOrderItemSetting
{
  /// <summary>
  /// Номера групп заменителей и номера актуальных заменителей в этих группах
  /// </summary>
  public Dictionary<long, long> ActualSubstitutes = new Dictionary<long, long>();

  /// <summary>Создать пустой экземпляр класса</summary>
  public SubstitutesItemSettings()
  {
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public SubstitutesItemSettings(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    lock (this.syncRoot)
      this.ActualSubstitutes.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case SubstitutesItemSettings substitutesItemSettings:
        lock (this.syncRoot)
        {
          this.ActualSubstitutes = new Dictionary<long, long>((IDictionary<long, long>) substitutesItemSettings.ActualSubstitutes);
          break;
        }
      case SubstituteObjects substituteObjects:
        lock (this.syncRoot)
        {
          List<long> groups = substituteObjects.Groups;
          for (int index = 0; index < groups.Count; ++index)
            this.ActualSubstitutes[groups[index]] = 0L;
          break;
        }
    }
  }

  /// <summary>Редактируемые данные</summary>
  public override object Data
  {
    [DebuggerStepThrough] get => (object) this.ActualSubstitutes;
  }
}
