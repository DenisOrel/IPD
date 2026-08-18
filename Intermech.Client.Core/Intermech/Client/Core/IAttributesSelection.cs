
// Type: Intermech.Client.Core.IAttributesSelection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary>
/// Интерфейс который должны поддерживать контролы, которые позваляют выбрать некоторый набор атрибутов.
/// Позволяет определить, список атрибутов доступных для выбора, Получить список выбранных атрибутов и т.п.
/// </summary>
public interface IAttributesSelection
{
  /// <summary> Добавить в список атрибуты, которые могут принадлежать некоторым типам связей </summary>
  /// <param name="relationTypeIDs"> Идентификаторы типов связей, атрибуты которых должны быть добавлены в список </param>
  void AddRelationAttributes(int[] relationTypeIDs);

  /// <summary> Добавить в список атрибуты, которые могут принадлежать некоторым типам объектов </summary>
  /// <param name="objectTypeIDs"> Идентификаторы типов объектов, атрибуты которых должны быть добавлены в список </param>
  void AddObjectAttributes(int[] objectTypeIDs);

  /// <summary>
  /// Добавить в список атрибуты.
  /// Все добавленые атрибуты считаются принадлежащими связи
  /// (всё равно, в том случае, если атрибут относиться связи при чтении значения
  /// необходимо проверять его принадлежность связи, и, если он связи не принадлежит,
  /// пытаться прочитать его из объекта)
  /// </summary>
  /// <param name="attributeIDs"> Идентификаторы атрибутов которые должны быть добавлены в список </param>
  /// <returns> Список дескрипторов добавленных атрибутов </returns>
  AttributeDescriptorList AddAttributes(int[] attributeIDs);

  /// <summary> Добавить в список атрибуты </summary>
  /// <param name="attributeIDs"> Идентификаторы атрибутов которые должны быть добавлены в список </param>
  /// <param name="isRelationAttributes"> Признак того, что дабавляемые атрибуты относятся к связи </param>
  /// <returns> Список дескрипторов добавленных атрибутов </returns>
  AttributeDescriptorList AddAttributes(int[] attributeIDs, bool isRelationAttributes);

  /// <summary> Добавить в список атрибуты </summary>
  /// <param name="attributeDescriptorList"> Список дескрипторов атрибутов, которые должны быть добавлены в список </param>
  void AddAttributes(AttributeDescriptorList attributeDescriptorList);

  /// <summary> Выставить Checked = true у атрибутов с переданными идентификаторами </summary>
  /// <param name="attributeIDs"> Массив идентификаторов атрибутов, у которых свойство Checked должно стать = true </param>
  void SetCheckedAttributes(int[] attributeIDs);

  /// <summary> Выставить Checked = true у атрибутов с переданными идентификаторами </summary>
  /// <param name="attributeIDs"> Массив идентификаторов атрибутов, у которых свойство Checked должно стать = true </param>
  /// <param name="moveToTop"> Переместить ли данные атрибуты на самый верх списка выбора </param>
  void SetCheckedAttributes(int[] attributeIDs, bool moveToTop);

  /// <summary> Получить список дескрипторов отмеченых атрибутов </summary>
  /// <returns> Список дескрипторов отмеченых атрибутов </returns>
  AttributeDescriptorList GetCheckedAttributesList();

  /// <summary> Очистить список атрибутов </summary>
  void ClearAttributesList();

  /// <summary> Отметить все атрибуты, доступные для выбора как отмеченые </summary>
  void CheckAllAttributes();

  /// <summary> Снять отметки со всех отмеченых атрибутов </summary>
  void UncheckAllAttributes();

  /// <summary> Список загруженных атрибутов </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  AttributeDescriptorList LoadedAttributes { get; set; }

  /// <summary> Вызывается перед началом редактирования списка атрибутов (ускоряет работу, блокируя обновление визуальных контролов) </summary>
  void BeginUpdate();

  /// <summary> Вызывается по окончании редактирования списка атрибутов (разблокирует обновление визуальных контролов, обновляет их содержимое) </summary>
  void EndUpdate();

  /// <summary> Показывать ли кнопку "Все атрибуты" </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  bool ShowButtonAllAttributes { get; set; }
}
