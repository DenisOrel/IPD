// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionItemAttributeValue
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Значение сравниваемого атрибута</summary>
public sealed class CompositionItemAttributeValue
{
  /// <summary>Индекс в коллекции значений</summary>
  public int Index { get; private set; }

  /// <summary>Значение</summary>
  public object Value { get; private set; }

  /// <summary>Строковое представление значения</summary>
  public string Description { get; set; }

  /// <summary>Ссылка на сравниваемый атрибут</summary>
  public CompositionItemAttribute Parent { get; private set; }

  /// <summary>Конструктоа</summary>
  /// <param name="parent">Сравниваемый атрибут</param>
  /// <param name="index">Индекс в коллекции значений</param>
  /// <param name="value">Значение</param>
  public CompositionItemAttributeValue(CompositionItemAttribute parent, int index, object value)
  {
    this.Index = index;
    this.Value = value;
    this.Parent = parent;
  }

  /// <summary>Создание значения-пустышки</summary>
  /// <param name="parent">Сравниваемый атрибут</param>
  public static CompositionItemAttributeValue CreateDummy(CompositionItemAttribute parent)
  {
    return new CompositionItemAttributeValue(parent, -1, (object) null);
  }

  /// <summary>Признак значения-пустышки</summary>
  public bool IsDummy => this.Index == -1;
}
