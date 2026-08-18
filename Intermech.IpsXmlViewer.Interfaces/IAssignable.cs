// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IAssignable
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Интерфейс позволяет копировать в поля объекта содержимое полей другого объекта
/// </summary>
public interface IAssignable
{
  /// <summary>Очистить поля класса</summary>
  void Clear();

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  void Assign(object source);
}
