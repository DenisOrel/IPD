// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.IExpertTableColorsService
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Интерфесы для службы</summary>
public interface IExpertTableColorsService
{
  /// <summary>Получить текущие значения цветов</summary>
  IExpertTableColors Current { get; set; }

  /// <summary>Сохранить данные в конфигурацию пользователя</summary>
  /// <param name="value">Данные о цветах</param>
  void SaveToBase(IExpertTableColors value);

  /// <summary>Загрузить цвета из конфигурации пользователя</summary>
  /// <returns>Данные о цветах</returns>
  IExpertTableColors LoadFromBase();
}
