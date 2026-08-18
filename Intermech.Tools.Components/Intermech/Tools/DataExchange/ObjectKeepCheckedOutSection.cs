// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ObjectKeepCheckedOutSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Секция для объектов IPS типа <see cref="T:Intermech.Data.SectionEntities.SectionEntity" />, позволяющая управлять завершением редактирования на уровне отдельных объектов.
/// </summary>
internal class ObjectKeepCheckedOutSection
{
  /// <summary>
  /// Возвращает или устанавливает флаг, управляющий взятием на изменение нового объекта IPS.
  /// Если значение флага равно true, то объект IPS после фиксации заготовки будет взят на изменение.
  /// Если значение флага равно false, то объект IPS после фиксации заготовки останется невзятым на редактирование.
  /// </summary>
  public bool KeepCheckedOut { get; set; }
}
