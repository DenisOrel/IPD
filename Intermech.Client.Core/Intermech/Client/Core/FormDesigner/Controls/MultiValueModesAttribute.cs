
// Type: Intermech.Client.Core.FormDesigner.Controls.MultiValueModesAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Тип множественного значения атрибута.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class MultiValueModesAttribute : Attribute
{
  /// <summary>Сохраненное значение.</summary>
  public MultiValueModes[] MultiValuesModes { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="multipleValues"></param>
  public MultiValueModesAttribute(MultiValueModes multipleValues)
  {
    this.MultiValuesModes = new MultiValueModes[1]
    {
      multipleValues
    };
  }

  /// <summary>Конструктор.</summary>
  /// <param name="multipleValues"></param>
  public MultiValueModesAttribute(MultiValueModes[] multipleValues)
  {
    this.MultiValuesModes = multipleValues;
  }
}
