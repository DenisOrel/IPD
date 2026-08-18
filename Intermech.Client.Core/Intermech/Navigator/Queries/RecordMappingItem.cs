
// Type: Intermech.Navigator.Queries.RecordMappingItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Описывает отображение виртуальной колонки навигатора в поле источника данных.
/// </summary>
public sealed class RecordMappingItem
{
  private readonly NodeColumn column;
  private readonly object field;
  private INodeColumnTransform transform;

  /// <summary>
  /// Создает отображение виртуальной колонки в поле источника данных.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <param name="field">Поле источника данных</param>
  /// <param name="transform">Преобразование, используемое для расшифровки значений поля источника данных</param>
  public RecordMappingItem(NodeColumn column, object field, INodeColumnTransform transform)
  {
    this.column = column;
    this.field = field;
    this.transform = transform;
  }

  /// <summary>
  /// Возвращает отображаемую виртуальную колонку навигатора.
  /// </summary>
  public NodeColumn Column => this.column;

  /// <summary>
  /// Возвращает идентификатор поля источника данных, в которое
  /// отображается виртуальная колонка навигатора.
  /// </summary>
  public object Field => this.field;

  /// <summary>
  /// Возвращает преобразование, используемое для расшифровки значений поля источника данных
  /// </summary>
  public INodeColumnTransform Transform
  {
    get => this.transform;
    set => this.transform = value;
  }

  /// <summary>
  /// Возвращает true, если данный объект эквивалентен указанному.
  /// </summary>
  /// <param name="obj">Объект, с которым производится сравнение</param>
  /// <returns>True, в случае эквивалентности</returns>
  public override bool Equals(object obj)
  {
    return !(obj is RecordMappingItem recordMappingItem) ? base.Equals(obj) : this.column.Equals((object) recordMappingItem.Column);
  }

  /// <summary>Возвращает хэш-код объекта.</summary>
  /// <returns>Значение хэш-кода</returns>
  public override int GetHashCode() => this.column.GetHashCode();
}
