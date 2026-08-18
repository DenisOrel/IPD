// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IPrepareNewObjectsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Необязательный сервис интегратора, предназначенный для подготовки к использованию новых объектов, создаваемых внутри IPS.
/// </summary>
public interface IPrepareNewObjectsService
{
  /// <summary>
  /// Позволяет обработать и настроить новый объект (а также его файловую копию, если это документ) сразу после создания.
  /// Данный метод вызывается для всех объектов, обрабатываемых интегратором.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  void PrepareNewObject(long objectId);

  /// <summary>
  /// Позволяет обработать и настроить файлы объекта при создании по прототипу. Метод вызывается сразу после создания заготовки нового объекта.
  /// Как правило, обработка заключается в удалении из нового объекта идентифицирующих сведений, относящихся к объекту-прототипу.
  /// К таким сведениям относятся значения атрибутов "Обозначение", "Код ОКП", "Наименование" и др.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="prototypeId">Идентификатор прототипа объекта</param>
  void PreparePrototypedObjectFiles(long objectId, long prototypeId);
}
