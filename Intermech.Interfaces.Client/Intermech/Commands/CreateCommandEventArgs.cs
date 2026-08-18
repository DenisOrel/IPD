// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.CreateCommandEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Commands;

/// <summary>Аргументы события создания команды.</summary>
public sealed class CreateCommandEventArgs : EventArgs
{
  private Type commandType;
  private string commandName;
  private Command command;

  /// <summary>Создает объект.</summary>
  /// <param name="commandType">Тип создаваемой команды, который должен быть унаследован от класса <see cref="T:Command" /></param>
  /// <param name="commandName">Имя создаваемой команды</param>
  /// <exception cref="T:ArgumentNullException">Параметры <paramref name="commandType" />, <paramref name="commandName" /> не должны быть равны null</exception>
  /// <exception cref="T:ArgumentException">Параметр <paramref name="commandType" /> унаследован не от типа <see cref="T:Command" />; параметр <paramref name="commandName" /> не должен быть пуст</exception>
  public CreateCommandEventArgs(Type commandType, string commandName)
  {
    CommandHelper.CheckCommandType(commandType);
    CommandHelper.CheckCommandName(commandName, nameof (commandName));
    this.commandType = commandType;
    this.commandName = commandName;
  }

  /// <summary>Возвращает тип создаваемой команды.</summary>
  public Type CommandType
  {
    [DebuggerStepThrough] get => this.commandType;
  }

  /// <summary>Возвращает имя создаваемой команды.</summary>
  public string CommandName
  {
    [DebuggerStepThrough] get => this.commandName;
  }

  /// <summary>Возвращает или задает созданную команду.</summary>
  /// <exception cref="T:ArgumentException">Тип созданной команды унаследован не от требуемого типа</exception>
  public Command Command
  {
    [DebuggerStepThrough] get => this.command;
    set
    {
      this.command = value == null || this.commandType.IsAssignableFrom(value.GetType()) ? value : throw new ArgumentException($"Тип команды '{value.GetType()}' не соответствует ожидаемому типу '{this.commandType}'.", nameof (value));
    }
  }
}
