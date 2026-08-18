// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.GetCategoryTypeParentEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Аргументы для события "Найти родительскую категорию и тип"
/// </summary>
public class GetCategoryTypeParentEventArgs : EventArgs
{
  /// <summary>Категория</summary>
  private int _category;
  /// <summary>Тип</summary>
  private int _type;
  /// <summary>Дополнительное имя</summary>
  private string _suffix;
  /// <summary>Родительская категория</summary>
  public int ParentCategory;
  /// <summary>Родительский тип</summary>
  public int ParentType;
  /// <summary>Родительское дополнительное имя</summary>
  public string ParentSuffix = string.Empty;
  /// <summary>Выполнена ли обработка аргументов</summary>
  public bool Processed;

  /// <summary>Категория</summary>
  public int Category
  {
    [DebuggerStepThrough] get => this._category;
  }

  /// <summary>Тип</summary>
  public int Type
  {
    [DebuggerStepThrough] get => this._type;
  }

  /// <summary>Дополнительное имя</summary>
  public string Suffix
  {
    [DebuggerStepThrough] get => this._suffix;
  }

  /// <summary>Создать аргументы события</summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  public GetCategoryTypeParentEventArgs(int category, int type, string suffix)
  {
    this._category = category;
    this._type = type;
    this._suffix = suffix;
  }
}
