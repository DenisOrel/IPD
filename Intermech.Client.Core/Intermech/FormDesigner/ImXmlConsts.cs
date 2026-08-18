
// Type: Intermech.FormDesigner.ImXmlConsts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.FormDesigner;

/// <summary>Константы генерации.</summary>
public static class ImXmlConsts
{
  /// <summary>Версия документа</summary>
  public const string Version = "2.0";
  /// <summary>Корень документа</summary>
  public const string dRoot = "FormDesignerXMLRoot";
  /// <summary>Элемент объект</summary>
  public const string eObject = "Object";
  /// <summary>Элемент компонент</summary>
  public const string eComponent = "Component";
  /// <summary>Элемент управления</summary>
  public const string eControl = "Control";
  /// <summary>Набор свойств</summary>
  public const string nProperties = "Properties";
  /// <summary>Одно свойство</summary>
  public const string nProperty = "Property";
  /// <summary>Значение набора IList</summary>
  public const string nItem = "Item";
  /// <summary>Атрибут тип</summary>
  public const string aType = "Type";
  /// <summary>Атрибут сборка</summary>
  public const string aAssembly = "Assembly";
  /// <summary>Атрибут имя</summary>
  public const string aName = "Name";
  /// <summary>Атрибут версия</summary>
  public const string aVersion = "Version";
  /// <summary>Атрибут тип данных</summary>
  public const string aPropertyFormat = "PropertyFormat";
  /// <summary>Пропустить запись следующих свойств</summary>
  public static readonly List<string> SkipProperties = new List<string>((IEnumerable<string>) new string[2]
  {
    "Controls",
    "Name"
  });
  /// <summary>Атрибуты для фильтрации PropertyDescription</summary>
  public static readonly Attribute[] FilterAttributes = new Attribute[2]
  {
    (Attribute) ReadOnlyAttribute.No,
    (Attribute) DesignOnlyAttribute.No
  };
  /// <summary>
  /// Типы, которые будут принудительно сохраняться в строки
  /// </summary>
  public static readonly List<Type> Types2String = new List<Type>((IEnumerable<Type>) new Type[6]
  {
    typeof (Point),
    typeof (Size),
    typeof (Guid),
    typeof (AttributeInfo),
    typeof (NodeColumnCollection),
    typeof (Dictionary<Guid, string>)
  });
  /// <summary>
  /// Кэш на типы контролов, для ускорения загрузки и сохранения
  /// </summary>
  public static Dictionary<Type, PropertyDescriptorCollection> PDCCache = new Dictionary<Type, PropertyDescriptorCollection>();

  /// <summary>Проверка двухсторонней возможности конвертации.</summary>
  /// <param name="converter">Конвертер типа</param>
  /// <param name="conversionType">Тип в который необходимо конвертировать</param>
  /// <returns>True - если возможна двусторонняя конверсия</returns>
  public static bool GetConversionSupported(TypeConverter converter, Type conversionType)
  {
    return converter.CanConvertFrom(conversionType) && converter.CanConvertTo(conversionType);
  }

  /// <summary>Тип записываемого объекта.</summary>
  public enum ObjectClass
  {
    /// <summary>Обычные объект.</summary>
    Object,
    /// <summary>Компонент.</summary>
    Component,
    /// <summary>Элемент управления.</summary>
    Control,
  }

  /// <summary>Класс для описания PropertyFormat-ов.</summary>
  public enum PropertyFormat
  {
    /// <summary>
    /// Записываемое значение в Propery - это значение в нормальном виде (в строке).
    /// </summary>
    Value,
    /// <summary>Значение в бинарном формате.</summary>
    Binary,
    /// <summary>Значение коллекция.</summary>
    Collection,
    /// <summary>Значение сериализованный объект.</summary>
    Serialized,
    /// <summary>Значение - это объект.</summary>
    Object,
  }
}
