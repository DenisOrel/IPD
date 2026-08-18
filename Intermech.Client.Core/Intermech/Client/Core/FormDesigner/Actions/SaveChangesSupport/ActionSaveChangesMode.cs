
// Type: Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport.ActionSaveChangesMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport;

/// <summary>
/// Режимы сохранения изменений на форме редактирования перед вызовом действия
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum ActionSaveChangesMode
{
  /// <summary>Игнорировать</summary>
  [CustomDescription("Attribute.Client.Core_322")] Ignore,
  /// <summary>Отменить</summary>
  [CustomDescription("Attribute.Client.Core_215")] Discard,
  /// <summary>Применить</summary>
  [CustomDescription("Attribute.Client.Core_323")] Apply,
  /// <summary>Запрос у пользователя</summary>
  [CustomDescription("Attribute.Client.Core_324")] Confirm,
}
