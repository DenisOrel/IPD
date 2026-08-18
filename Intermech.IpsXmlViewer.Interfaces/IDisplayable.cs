// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IDisplayable
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Интерфейс, реализуемый элементами, способными вернуть строку для отображения на экране
/// </summary>
public interface IDisplayable
{
  /// <summary>Строка для отображения на экране</summary>
  string Text { get; }
}
