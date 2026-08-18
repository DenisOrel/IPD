// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NavigatorRelationCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Код выполненной команды "Навигатора"</summary>
[Serializable]
public enum NavigatorRelationCommand
{
  /// <summary>Команда не указана</summary>
  Unknown,
  /// <summary>Выполнена цепочка команд "Копировать" - "Вставить"</summary>
  CopyPaste,
  /// <summary>Выполнена цепочка команд "Вырезать" - "Вставить"</summary>
  CutPaste,
  /// <summary>Выполнена команда "Добавить в состав"</summary>
  InsertIn,
  /// <summary>Выполнена команда "Создать в составе"</summary>
  CreateIn,
}
