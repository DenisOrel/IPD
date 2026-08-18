
// Type: Intermech.Client.Core.FormDesigner.Controls.IAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Интерфейс для определения является ли контрол редактором значений атрибута.
/// Организовывает поведение контрола.
/// </summary>
public interface IAttributeEditor : IBaseDesForm, IAttributeEditorModified
{
  /// <summary>
  /// Устанавливает и возвращает Guid атрибута и типа объекта/связи.
  /// </summary>
  AttributeInfo AttributeInfo { get; set; }

  /// <summary>
  /// Устанавливает и возвращает возможность добавления атрибута к объекту.
  /// </summary>
  bool CanAddAttribute { get; set; }

  /// <summary>Значение атрибута.</summary>
  AttributeValues Values { get; set; }

  /// <summary>Установка допустимых значений.</summary>
  /// <param name="data">DataTable со значениями</param>
  void SetPossibleValues(
    DataTable data,
    string possibleValueFieldName,
    string descriptionFieldName);
}
