// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.BackgroundState
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Состояние потока запроса состава/применяемости</summary>
public enum BackgroundState
{
  /// <summary>Пусто</summary>
  Empty,
  /// <summary>Ошибка</summary>
  Error,
  /// <summary>Выполнение запроса</summary>
  Reading,
  /// <summary>Результаты готовы</summary>
  Fill,
  /// <summary>Изменение процента выполнения</summary>
  SetPersent,
  /// <summary>Часть закончена</summary>
  PartComplete,
}
