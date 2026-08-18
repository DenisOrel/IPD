// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IDefaultCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс команды по умолчанию для типа объекта</summary>
public interface IDefaultCommand
{
  /// <summary>
  /// Тип объекта, для которого назначается команда по умолчанию
  /// </summary>
  int ObjectTypeID { get; set; }

  /// <summary>Имя команды по умолчанию</summary>
  string DefaultCommandName { get; set; }

  /// <summary>Чья команда - контекстного меню или ICommandManager</summary>
  DefaultCommandHandler CommandHandler { get; set; }
}
