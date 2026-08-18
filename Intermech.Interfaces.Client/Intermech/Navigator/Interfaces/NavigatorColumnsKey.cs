// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NavigatorColumnsKey
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Ключ для описания настроек вида</summary>
[Serializable]
public class NavigatorColumnsKey : IAssignable, ICloneable, IComparable<NavigatorColumnsKey>
{
  /// <summary>Категория</summary>
  private int _category;
  /// <summary>Тип</summary>
  private int _type;
  /// <summary>Дополнение к названию схемы</summary>
  private string _suffix;

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

  /// <summary>Дополнение к названию схемы</summary>
  public string Suffix
  {
    [DebuggerStepThrough] get => this._suffix;
  }

  /// <summary>Создать ключ для описания настроек вида</summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнение к названию схемы</param>
  public NavigatorColumnsKey(int category, int type, string suffix)
  {
    this._category = category;
    this._type = type;
    this._suffix = suffix;
  }

  /// <summary>
  /// Создать ключ для описания настроек вида на основе объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public NavigatorColumnsKey(object source) => this.Assign(source);

  public override bool Equals(object obj) => this.CompareTo(obj as NavigatorColumnsKey) == 0;

  public override int GetHashCode()
  {
    return this._category.GetHashCode() << 24 | this._type.GetHashCode() << 8 | (this._suffix != null ? this._suffix.GetHashCode() : 0);
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._category = 0;
    this._type = 0;
    this._suffix = string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    switch (source)
    {
      case NavigatorColumnsKey navigatorColumnsKey:
        this._category = navigatorColumnsKey._category;
        this._type = navigatorColumnsKey._type;
        this._suffix = navigatorColumnsKey._suffix;
        break;
      case NavigatorColumns navigatorColumns:
        this._category = navigatorColumns.Category;
        this._type = navigatorColumns.Type;
        this._suffix = navigatorColumns.Suffix;
        break;
    }
  }

  /// <summary>Вернуть точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new NavigatorColumnsKey((object) this);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(NavigatorColumnsKey other)
  {
    if (other == null)
      return 1;
    int num1 = this._category.CompareTo(other._category);
    if (num1 != 0)
      return num1;
    int num2 = this._type.CompareTo(other._type);
    return num2 != 0 ? num2 : Comparer<string>.Default.Compare(this._suffix, other._suffix);
  }
}
