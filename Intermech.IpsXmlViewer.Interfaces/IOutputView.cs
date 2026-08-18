// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IOutputView
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Служба позволяет отобразить текстовую информацию в окне вывода Навигатора
/// </summary>
public interface IOutputView
{
  /// <summary>Переключает окно вывода для заданной категории</summary>
  /// <param name="category">Категория</param>
  void Activate(string category);

  /// <summary>Очищает окно вывода для указанной категории</summary>
  /// <param name="category">Категория</param>
  void ClearText(string category);

  /// <summary>Показывает окно вывода</summary>
  void ShowView();

  /// <summary>Выводит текст в окно вывода для заданной категории</summary>
  /// <param name="category">Категория</param>
  /// <param name="text">Выводимый текст</param>
  void WriteString(string category, string text);
}
