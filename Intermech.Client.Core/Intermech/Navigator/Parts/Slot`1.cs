
// Type: Intermech.Navigator.Parts.Slot`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Diagnostics;


namespace Intermech.Navigator.Parts;

/// <summary>
/// Является частью внутренней реализации механизма частей и не предназначен использования где-либо еще.
/// </summary>
/// <typeparam name="T">Тип данных, размещаемых в слоте</typeparam>
public class Slot<T>
{
  /// <summary>Уникальный идентификатор</summary>
  protected int uniqueId;
  /// <summary>Объект</summary>
  protected T obj;

  /// <summary>Создать пустой слот</summary>
  protected Slot()
  {
  }

  /// <summary>Создать типизированный слот</summary>
  /// <param name="uniqueId">Уникальный идентификатор</param>
  /// <param name="obj">Объект</param>
  protected Slot(int uniqueId, T obj)
  {
    this.uniqueId = uniqueId;
    this.obj = obj;
  }

  /// <summary>Уникальный идентификатор</summary>
  public int UniqueId
  {
    [DebuggerStepThrough] get => this.uniqueId;
    set => this.uniqueId = value;
  }

  /// <summary>Объект</summary>
  public T Object
  {
    [DebuggerStepThrough] get => this.obj;
  }
}
