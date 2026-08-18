// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.Interfaces.IDataValidator
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces.Interfaces;

/// <summary>
/// Интерфейс для валидации структуры / данных перед импортом
/// </summary>
public interface IDataValidator
{
  /// <summary>Валидация метаданых перед импортом</summary>
  /// <param name="throwIfError"></param>
  /// <returns></returns>
  bool ValidateMetaData(bool throwIfError);

  /// <summary>Валидация данных перед импортом</summary>
  /// <param name="throwIfError"></param>
  /// <returns></returns>
  bool ValidateData(bool throwIfError);
}
