// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeColumnRenameEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Аргументы события, позволяющего переименовывать колонки в гридах Навигатора
/// </summary>
[Serializable]
public class NodeColumnRenameEventArgs : EventArgs, IAssignable, ICloneable
{
  /// <summary>Колонка Навигатора</summary>
  public NodeColumn Column;
  /// <summary>
  /// Новое имя колонки. Значение String.Empty - имя колонки останется без изменений
  /// </summary>
  public string NewName = string.Empty;

  /// <summary>Создать аргументы события</summary>
  /// <param name="column">Колонка Навигатора</param>
  /// <param name="newName">Новое имя колонки. Значение String.Empty - имя колонки останется без изменений</param>
  public NodeColumnRenameEventArgs(NodeColumn column, string newName)
  {
    this.Column = column;
    this.NewName = newName;
  }

  /// <summary>
  /// Создать экземпляр класса и заполнить его информацией из объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public NodeColumnRenameEventArgs(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.Column = (NodeColumn) null;
    this.NewName = string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is NodeColumnRenameEventArgs columnRenameEventArgs))
      return;
    this.Column = columnRenameEventArgs.Column;
    this.NewName = columnRenameEventArgs.NewName;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns></returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);
}
