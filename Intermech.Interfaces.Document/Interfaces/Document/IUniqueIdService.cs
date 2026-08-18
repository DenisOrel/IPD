// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IUniqueIdService
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Сервис для обеспечения уникальности идентификаторов</summary>
public interface IUniqueIdService
{
  /// <summary>Сгеренировать уникальный идентификатор (сам факт генерации не резервирует ид)</summary>
  /// <returns>Уникальный идентификатор</returns>
  object GenerateUniqueId();

  /// <summary>Сгеренировать уникальный идентификатор (сам факт генерации не резервирует ид)</summary>
  /// <param name="prototypeID">Заготовка идентификатора</param>
  /// <returns>Уникальный идентификатор</returns>
  object GenerateUniqueId(object prototypeID);

  /// <summary>Используется ли заданный идентификатор</summary>
  /// <param name="id">Идентификатор</param>
  /// <returns>Идентификатор уже используется</returns>
  bool ContainsId(object id);

  /// <summary>Добавить (зарезервировать) объект с идентификатором</summary>
  /// <param name="id">Идентификатор</param>
  /// <param name="value">Объект, которому принадлежит идентификатор</param>
  void AddId(object id, object value);

  /// <summary>Удалить (освободить) идентификатор</summary>
  /// <param name="id">Идентификатор</param>
  void RemoveId(object id);

  /// <summary>Возвращает объект по идентификатору</summary>
  object this[object id] { get; }
}
