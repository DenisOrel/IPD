// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTreeNodeTypeEnum
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AVS;

/// <summary>Режимы вида спецификации</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum DocumentTreeNodeTypeEnum
{
  /// <summary>Правила вывода на шаблон: имя_таблицы</summary>
  [Description("Правила вывода на шаблон")] Main,
  /// <summary>Заголовок раздела</summary>
  [Description("Раздел")] Section,
  /// <summary>Строка данных спецификации</summary>
  [Description("Строка данных")] Record,
  /// <summary>Спросить у пользователя</summary>
  [Description("Графа")] Cell,
}
