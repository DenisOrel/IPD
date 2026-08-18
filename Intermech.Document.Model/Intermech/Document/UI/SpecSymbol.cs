// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.SpecSymbol
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.UI;

/// <summary>
/// Структура для хранения идентификационных данных о спецсимволе
/// </summary>
/// <summary>Конструктор</summary>
public struct SpecSymbol(string id)
{
  /// <summary>Идентификатор спецсимвола</summary>
  private string id = id;

  public string Id => this.id;

  public static bool operator ==(SpecSymbol x, SpecSymbol y) => string.Equals(x.id, y.id);

  public static bool operator !=(SpecSymbol x, SpecSymbol y) => !string.Equals(x.id, y.id);

  public bool Equals(SpecSymbol other) => string.Equals(this.id, other.id);

  public override bool Equals(object obj)
  {
    return obj != null && obj is SpecSymbol other && this.Equals(other);
  }
}
