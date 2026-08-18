
// Type: Intermech.Client.Core.FormDesigner.Controls.IMultipleAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Интерфейс для работы с объектом/связью "напрямую"</summary>
public interface IMultipleAttributeEditor : IBaseDesForm, IAttributeEditorModified
{
  /// <summary>
  /// Загрузка данных в контрол данные об объектах/связях брать из формы.
  /// </summary>
  void Load();

  /// <summary>
  /// Сохранение данных из контрола данные об объектах/связях брать из формы.
  /// </summary>
  void Save();
}
