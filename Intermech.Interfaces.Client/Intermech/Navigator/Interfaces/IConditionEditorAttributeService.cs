// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IConditionEditorAttributeService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс на службу обработки условий для специальных атрибутов
/// </summary>
public interface IConditionEditorAttributeService
{
  /// <summary>Регистрация обработчика на специальный атрибут</summary>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <param name="handler">Ссылка на обработчик</param>
  void Register(Guid attributeGuid, IConditionEditorAttribute handler);

  /// <summary>Получить обработчик для атрибута</summary>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <returns>Интерфейс на обработчик спец. атрибута или null если обработчик для атрибута не зарегистрирован</returns>
  IConditionEditorAttribute GetHandler(Guid attributeGuid);
}
