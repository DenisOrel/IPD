
// Type: Intermech.Client.Core.FormDesigner.Controls.IIMControlEnabled
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Создавался для того, чтобы контролировать доступность дочерних контролов, при Enabled == false родительского контрола.
/// </summary>
/// <remarks>В частноти С.Тавстуха захотел, чтобы у ссылочных контролов, кнопка "карточка объекта" была доступна всегда</remarks>
public interface IIMControlEnabled
{
  /// <summary>Запретить редактирование данных.</summary>
  bool DisabledInDesign { get; set; }

  /// <summary>
  /// Устанавливает и возвращает доступность элемента управления.
  /// </summary>
  bool EnabledCtrl { get; set; }
}
