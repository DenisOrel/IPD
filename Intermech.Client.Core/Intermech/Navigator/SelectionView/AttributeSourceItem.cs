
// Type: Intermech.Navigator.SelectionView.AttributeSourceItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для принадлежности атрибутов (для реализации элементов в ComboBox)
/// </summary>
internal sealed class AttributeSourceItem
{
  public readonly AttributeSourceTypes AttributeSourceType;

  public AttributeSourceItem(AttributeSourceTypes attributeSourceType)
  {
    this.AttributeSourceType = attributeSourceType;
  }

  public override string ToString() => EnumTypeHelper.GetCaption((Enum) this.AttributeSourceType);
}
