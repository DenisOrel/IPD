// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.OverrideFlags2
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Для внутреннего использования. Сохраняемые в XML Флаги переопределения наследуемых свойств.</summary>
[Flags]
[Serializable]
public enum OverrideFlags2
{
  /// <summary>Нет флагов</summary>
  None = 0,
  /// <summary>Ширина ячейки наследуется не от столбца, а зависит от Width в шаблоне</summary>
  ColumnWidth = 1,
  /// <summary>Высота ячейки наследутеся не от параметра DefaultRowSize, а в зависимости от Height от шаблона</summary>
  RowHeight = 2,
  /// <summary>Высота строки defaultRowSize наследуется не от родительской таблицы, а в зависимости от RowSize от шаблона</summary>
  ParentDefaultRowSize = 4,
  /// <summary>Левая граница столбца перекрыта</summary>
  ColumnLeftBorder = 16, // 0x00000010
  /// <summary>Правая граница столбца перекрыта</summary>
  ColumnRightBorder = 32, // 0x00000020
  /// <summary>Настройки столбцов сетки родительской таблицы перекрыты</summary>
  ParentGrid = 64, // 0x00000040
  /// <summary>NextPageTemplateId перекрыта</summary>
  NextPageTemplateId = 128, // 0x00000080
  /// <summary>LastPageTemplateId перекрыта</summary>
  LastPageTemplateId = 256, // 0x00000100
  /// <summary>"Пропуск перед" устанавливается плагином</summary>
  SkipBeforeForPlugin = 512, // 0x00000200
  /// <summary>"Пропуск после" устанавливается плагином</summary>
  SkipAfterForPlugin = 1024, // 0x00000400
  /// <summary>Поле Name не наследуется из шаблона</summary>
  Name = 2048, // 0x00000800
  /// <summary>Не пропускать строки перед таблицей в начале страницы</summary>
  NonSkipBeforeAtStartPage = 4096, // 0x00001000
  /// <summary>Ссылка не наследуется</summary>
  Reference = 8192, // 0x00002000
  /// <summary>Флаг NonSkipBeforeAtStartPage был назначен в AVS</summary>
  AvsNonSkipBeforeAtStartPage = 16384, // 0x00004000
  /// <summary>Перекрыто наследование внутренних горизонтальных линий от родителя</summary>
  ParentInnerHorizontalLine = 32768, // 0x00008000
}
