
// Type: Intermech.Client.Core.FormDesigner.Controls.IParent4Control
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Интерфейс для установки родителя для контрола.</summary>
public interface IParent4Control
{
  /// <summary>Устанавливает родителя для атрибута.</summary>
  IElementInfo ParentInfo { get; set; }

  /// <summary>Для чего нужен контрол.</summary>
  AttributeDestinationPoint ParentPoint { get; set; }
}
