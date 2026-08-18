// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ExtendedSelectedItemsCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Commands;

/// <summary>
/// Расширенный вариант класса <see cref="T:Intermech.Commands.SelectedItemsCommand" />,
/// предоставляющий дополнительные точки расширения.
/// </summary>
public abstract class ExtendedSelectedItemsCommand(string name) : SelectedItemsCommand(name)
{
  /// <summary>Уведомление перед началом обработки объектов</summary>
  /// <param name="session"></param>
  /// <remarks>В ряде случаев нужно знать есть ли общая сессия для всех объектов</remarks>
  protected virtual void DoBeforeProceedItems(IUserSession session)
  {
  }

  /// <summary>Уведомление после окончания обработки объектов</summary>
  /// <param name="session">В ряде случаев нужно знать есть ли общая сессия для всех объектов</param>
  protected virtual void DoAfterProceedItems(IUserSession session)
  {
  }

  /// <summary>Уведомление перед началом обработки объекта</summary>
  /// <param name="index"></param>
  protected virtual void DoBeforeProceedItem(int index)
  {
  }

  /// <summary>Уведомление после окончания обработки объекта</summary>
  /// <param name="index"></param>
  protected virtual void DoAfterProceedItem(int index)
  {
  }
}
