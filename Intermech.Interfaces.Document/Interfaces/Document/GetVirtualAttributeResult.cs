// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.GetVirtualAttributeResult
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Результат выполнения метода GetVirtualAttribute</summary>
/// <summary>Конструктор</summary>
/// <param name="found">Виртуальный атрибут с заданным именем найден</param>
/// <param name="value">Значение атрибута</param>
public struct GetVirtualAttributeResult(bool found, string value)
{
  /// <summary>Виртуальный атрибут с заданным именем найден</summary>
  public bool Found = found;
  /// <summary>Значение атрибута</summary>
  public string Value = value;
}
