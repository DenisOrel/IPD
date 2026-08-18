// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.IExpertTablePropertiesService
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Интерфейс сервиса настройки отображения таблиц</summary>
public interface IExpertTablePropertiesService
{
  /// <summary>Получить текущие настройки</summary>
  IExpertTableProperties Current { get; set; }

  /// <summary>Сохранить настройки в базу</summary>
  /// <param name="properties">настройки</param>
  void SaveToBase(IExpertTableProperties properties);

  /// <summary>Загрузить настройки из базы</summary>
  /// <returns>настройки</returns>
  IExpertTableProperties LoadFromBase();
}
