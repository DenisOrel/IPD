
// Type: Intermech.VisualCategories
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Категории для отображения в PropertyGrid</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_169")]
    [Category("Misc")]
    [Flags]
    public enum VisualCategories
    {
      /// <summary>Разное</summary>
      [CustomDescription("Attribute.Interfaces_170")] Misc = 0,
      /// <summary>Идентификация</summary>
      [CustomDescription("Attribute.Interfaces_171")] Identification = 1,
      /// <summary>Фильтрация</summary>
      [CustomDescription("Attribute.Interfaces_172")] Filtration = 2,
      /// <summary>Тип хранимой информации</summary>
      [CustomDescription("Attribute.Interfaces_173")] InformationType = Filtration | Identification, // 0x00000003
      /// <summary>Контроль ввода информации</summary>
      [CustomDescription("Attribute.Interfaces_174")] InputControl = 4,
      /// <summary>История</summary>
      [CustomDescription("Attribute.Interfaces_175")] History = InputControl | Identification, // 0x00000005
      /// <summary>Источники данных</summary>
      [CustomDescription("Attribute.Interfaces_176")] DataSources = InputControl | Filtration, // 0x00000006
      /// <summary>Работа с файлами</summary>
      [CustomDescription("Attribute.Interfaces_177")] FileWork = DataSources | Identification, // 0x00000007
      /// <summary>Оформление</summary>
      [CustomDescription("Attribute.Interfaces_178")] Illustration = 8,
    }
}
