
// Type: Intermech.Client.Core.MultiValueEditorMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary>Режим изменения значений для многозначного атрибута</summary>
internal enum MultiValueEditorMode
{
  [Description("Задать новые значения")] SetValue,
  [Description("Добавить выбранные значения")] AddValue,
  [Description("Удалить выбранные значения")] DelValue,
  [Description("Заменяемое значение")] ReplaceValue,
}
