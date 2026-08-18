
// Type: Intermech.Client.Core.FormDesigner.ComponentTypeProducer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using System;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>
/// Статический класс определяющий тип компонента по атрибуту.
/// </summary>
public class ComponentTypeProducer
{
  /// <summary>
  /// Опрделяет тип компонента для изменения значений атрибутов.
  /// </summary>
  /// <param name="mode">Множественность атрибута</param>
  /// <param name="field">Тип атрибута</param>
  /// <param name="compute">Расчитываемость атрибута</param>
  /// <returns>Тип контрола</returns>
  public static Type GetComponentType(
    MultiValueModes mode,
    FieldTypes field,
    ComputeValueModes compute)
  {
    return ComponentTypeProducer.GetComponentType(mode, field, false);
  }

  /// <summary>
  /// Опрделяет тип компонента для изменения значений атрибутов.
  /// </summary>
  /// <param name="mode">Множественность атрибута</param>
  /// <param name="field">Тип атрибута</param>
  /// <param name="bMasked">Наличие маски у текстового атрибута</param>
  /// <returns>Тип контрола</returns>
  public static Type GetComponentType(MultiValueModes mode, FieldTypes field, bool bMasked)
  {
    Type componentType = (Type) null;
    switch (mode)
    {
      case MultiValueModes.SingleValue:
        switch (field)
        {
          case FieldTypes.ftString:
            componentType = bMasked ? typeof (AttrMaskedTextEdit) : typeof (AttrTextEdit);
            break;
          case FieldTypes.ftInteger:
          case FieldTypes.ftDouble:
          case FieldTypes.ftGuid:
            componentType = typeof (AttrTextEdit);
            break;
          case FieldTypes.ftDateTime:
            componentType = typeof (AttrDateEdit);
            break;
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            componentType = typeof (AttrTextBtn);
            break;
          case FieldTypes.ftPassword:
            componentType = typeof (AttrPassword);
            break;
          case FieldTypes.ftMemo:
            componentType = typeof (AttrMemoEdit);
            break;
          case FieldTypes.ftBoolean:
            componentType = typeof (AttrCheckBox);
            break;
          case FieldTypes.ftMeasured:
            componentType = typeof (AttrMeasuredEdit);
            break;
          case FieldTypes.ftSystem:
            componentType = typeof (AttrTextBtn);
            break;
        }
        break;
      case MultiValueModes.MultiValues:
        switch (field)
        {
          case FieldTypes.ftString:
          case FieldTypes.ftInteger:
          case FieldTypes.ftDouble:
          case FieldTypes.ftGuid:
            componentType = typeof (AttrListBox);
            break;
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            componentType = typeof (AttrListBoxBtn);
            break;
          case FieldTypes.ftMeasured:
            componentType = typeof (AttrMeasuredListBox);
            break;
          case FieldTypes.ftSystem:
            componentType = typeof (AttrLabel);
            break;
        }
        break;
      case MultiValueModes.SingleValueFromList:
        switch (field)
        {
          case FieldTypes.ftString:
          case FieldTypes.ftInteger:
          case FieldTypes.ftDouble:
          case FieldTypes.ftDateTime:
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftMeasured:
          case FieldTypes.ftObjectLinkByID:
            componentType = typeof (AttrComboBox);
            break;
        }
        break;
      case MultiValueModes.MultiValuesFromList:
        switch (field)
        {
          case FieldTypes.ftString:
          case FieldTypes.ftInteger:
          case FieldTypes.ftDouble:
            componentType = typeof (AttrCheckedListBox);
            break;
          case FieldTypes.ftObjectLink:
          case FieldTypes.ftObjectLinkByID:
            componentType = typeof (AttrListBoxBtn);
            break;
        }
        break;
    }
    return componentType;
  }
}
