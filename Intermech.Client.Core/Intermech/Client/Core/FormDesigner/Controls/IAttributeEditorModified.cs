
// Type: Intermech.Client.Core.FormDesigner.Controls.IAttributeEditorModified
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Интерфейс обработки изменений в контроле.</summary>
public interface IAttributeEditorModified
{
  /// <summary>
  /// Устанавливает и возвращает произошло ли изменение данных.
  /// </summary>
  bool Modified { get; set; }

  /// <summary>Событие на изменение данных в контроле.</summary>
  event EventHandler ModifiedEvent;
}
