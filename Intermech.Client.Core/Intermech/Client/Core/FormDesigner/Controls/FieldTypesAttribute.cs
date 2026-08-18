
// Type: Intermech.Client.Core.FormDesigner.Controls.FieldTypesAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Тип значения атрибута.</summary>
[AttributeUsage(AttributeTargets.Property)]
public class FieldTypesAttribute : Attribute
{
  /// <summary>Сохраненное значение.</summary>
  public Intermech.FieldTypes[] FieldTypes { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="fieldType"></param>
  public FieldTypesAttribute(Intermech.FieldTypes[] fieldType) => this.FieldTypes = fieldType;
}
