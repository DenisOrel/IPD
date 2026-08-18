// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.SetVirtualAttributeResult
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Результат выполнения метода SetVirtualAttribute</summary>
/// <summary>Конструктор</summary>
/// <param name="found">Виртуальный атрибут с заданным именем найден</param>
/// <param name="cancel">Установка значения атрибута невозможна</param>
public struct SetVirtualAttributeResult(bool found, bool cancel)
{
  /// <summary>Виртуальный атрибут с заданным именем найден</summary>
  public bool Found = found;
  /// <summary>Установка значения атрибута невозможна</summary>
  public bool Cancel = cancel;
}
