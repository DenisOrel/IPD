// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.IAttributeEditorControl
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
///  Интерфейс общения с контролами редактирования атрибутов
/// </summary>
public interface IAttributeEditorControl
{
  /// <summary>Идентификатор атрибута</summary>
  int AttributeId { get; }

  /// <summary>
  /// Intermech.PropertyEditors.AttrProcessor.AttributeProcessor
  /// </summary>
  object AttributeProcessor { get; }

  /// <summary>
  /// Индекс значения.
  /// null для многозначных.
  /// </summary>
  int? Index { get; }

  /// <summary>
  /// Инициализация контрола, для инициализации значения требуется вызов Refresh()
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attributeProcessor"></param>
  /// <param name="index">может быть null если неизвестно чем инициализировать</param>
  void InitControl(int attributeId, object attributeProcessor, int? index);

  /// <summary>
  /// указывает, что контрол находится в контейнере (форме-панели) и что самостоятельные действия по назначению данных не требуются.
  /// по умолчанию должен быть false.
  /// инициализируется до редактирования.
  /// </summary>
  bool InContainer { get; set; }

  /// <summary>
  /// Загрузить значение атрибута по инициализированным данным (через InitControl) и принять меры к отражению значения в контроле
  /// </summary>
  void RefreshControl();

  /// <summary>
  /// применить извне или изнутри, производя сохранение в AttributeProcessor.
  /// вызвать AttributeValueChanged.
  /// null -&gt; apply не произошел
  /// </summary>
  /// <returns></returns>
  bool Apply();

  /// <summary>
  /// срабатывает на изменении значений атрибута, редактируемого в контроле, значение еще не попадает в списки AttributeProcessor'а.
  /// (при Apply происходит вызов AttributeValuesChanged от AttributeProcessor
  /// из-за назначения значений атрибутов через соответствующих функции AttributeProcessor )
  /// </summary>
  event AttributeValuesChangedHandler OnAttributeValueChanged;

  /// <summary>
  /// срабатывает, когда контролу требуется закрытие.
  /// имеет смысл для DropDown контролов, чтобы для контрола была вызвана команда CloseDropdown
  /// </summary>
  event CloseDemandHandler OnCloseDemand;

  /// <summary>
  /// есть ли непримененные изменения в контроле (форма дергает за это контрол при закрытии и вызывает Apply у контрола)
  /// </summary>
  bool WasChanged { get; }

  /// <summary>отменить изменения</summary>
  void Cancel();

  bool IsDropDownResizable { get; }

  UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context);

  bool GetPaintValueSupported(ITypeDescriptorContext context);

  void PaintValue(PaintValueEventArgs e);
}
