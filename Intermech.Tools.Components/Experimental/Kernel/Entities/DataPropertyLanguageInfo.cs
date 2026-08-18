// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DataPropertyLanguageInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Расширенные сведения о языковом определении свойства доменного объекта.
/// </summary>
internal sealed class DataPropertyLanguageInfo
{
  /// <summary>Создает объект.</summary>
  /// <param name="isNullable">Признак, может ли свойство доменного объекта принимать значение null</param>
  /// <param name="hasEmptyValue">Признак, что тип свойства доменного объекта содержит пустое значение, не равное null</param>
  /// <param name="emptyValue">Пустое значение свойства доменного объекта</param>
  public DataPropertyLanguageInfo(bool isNullable, bool hasEmptyValue, object emptyValue)
  {
    this.IsNullable = isNullable;
    this.HasEmptyValue = hasEmptyValue;
    this.EmptyValue = emptyValue;
  }

  /// <summary>
  /// Возвращает признак, может ли свойство доменного объекта принимать значение null.
  /// Для этого свойство должно быть либо ссылочного, либо nullable типа.
  /// </summary>
  public bool IsNullable { get; private set; }

  /// <summary>
  /// Возвращает признак, что тип свойства доменного объекта содержит пустое значение, не равное null.
  /// Например, у строкового типа таким значением является пустая строка.
  /// </summary>
  public bool HasEmptyValue { get; private set; }

  /// <summary>
  /// Возвращает пустое значение свойства доменного объекта.
  /// </summary>
  public object EmptyValue { get; private set; }
}
