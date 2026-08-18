
// Type: Intermech.Tools.Data.Sync.AttributeSyncOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Содержит параметры округления значений переносимых атрибутов в операциях сравнения.
/// </summary>
public sealed class AttributeSyncOptions
{
  private int significantDigits;
  private bool truncTimeToSeconds;

  /// <summary>Создает объект.</summary>
  public AttributeSyncOptions()
  {
    this.significantDigits = 6;
    this.truncTimeToSeconds = true;
  }

  /// <summary>
  /// Заполняет все поля данных текущего объекта, копируя их у указанного объекта.
  /// </summary>
  /// <param name="sourceObject">Объект, чьи поля следует скопировать</param>
  public void Assign(AttributeSyncOptions sourceObject)
  {
    if (sourceObject == null)
      throw new ArgumentNullException(nameof (sourceObject));
    if (this == sourceObject)
      return;
    this.DoAssign(sourceObject);
  }

  /// <summary>
  /// Реализует заполнение всех полей данных текущего объекта, копируя их у указанного объекта.
  /// </summary>
  /// <param name="sourceObject">Объект, чьи поля следует скопировать</param>
  private void DoAssign(AttributeSyncOptions sourceObject)
  {
    this.SignificantDigits = sourceObject.SignificantDigits;
    this.TruncTimeToSeconds = sourceObject.TruncTimeToSeconds;
  }

  /// <summary>Проверяет корректность значений свойств объекта.</summary>
  /// <exception cref="T:System.InvalidOperationException">Значения свойств объекта заданы неверно</exception>
  public void Validate()
  {
    if (this.SignificantDigits < 0)
      throw new InvalidOperationException("Количество значащих цифр после запятой, используемое при сравнении вещественных чисел, не может быть отрицательным.");
  }

  /// <summary>
  /// Возвращает или задает количество значащих цифр после запятой при сравнении вещественных чисел.
  /// </summary>
  public int SignificantDigits
  {
    get => this.significantDigits;
    set => this.significantDigits = value;
  }

  /// <summary>
  /// Включает и выключает откругление до секунд при сравнении единиц времени.
  /// </summary>
  public bool TruncTimeToSeconds
  {
    get => this.truncTimeToSeconds;
    set => this.truncTimeToSeconds = value;
  }
}
