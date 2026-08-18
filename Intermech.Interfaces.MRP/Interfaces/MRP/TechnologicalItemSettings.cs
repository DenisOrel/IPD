// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.TechnologicalItemSettings
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Настройки, связаннные с технологическими объектами.
/// Сохраняется в настройках по полному пути к дочернему объекту состава.
/// </summary>
[Serializable]
public sealed class TechnologicalItemSettings : BaseOrderItemSetting
{
  /// <summary>Идентификатор версии объекта с маршрутом обработки</summary>
  public long RouteObjID;
  /// <summary>Идентификатор связи с маршрутом обработки</summary>
  public long RouteLinkID;

  /// <summary>Создать пустой экземпляр класса</summary>
  public TechnologicalItemSettings()
  {
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public TechnologicalItemSettings(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    lock (this.syncRoot)
    {
      this.RouteLinkID = 0L;
      this.RouteObjID = 0L;
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is TechnologicalItemSettings technologicalItemSettings))
      return;
    lock (this.syncRoot)
    {
      this.RouteLinkID = technologicalItemSettings.RouteLinkID;
      this.RouteObjID = technologicalItemSettings.RouteObjID;
    }
  }

  /// <summary>
  /// Редактируемые данные (возвращаем ссылку на самого себя)
  /// </summary>
  public override object Data
  {
    [DebuggerStepThrough] get => (object) this;
  }
}
