
// Type: Intermech.Kernel.Search.RelationalOperators
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>Операторы отношений</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_336")]
    [Category("SQL")]
    public enum RelationalOperators
    {
      /// <summary>Нет оператора</summary>
      [Description("")] None = -1, // 0xFFFFFFFF
      /// <summary>Пустое значение</summary>
      [CustomDescription("Attribute.Interfaces_337"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.NoneValue)] Empty = 0,
      /// <summary>Не пустое значение</summary>
      [CustomDescription("Attribute.Interfaces_338"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.NoneValue)] NotEmpty = 1,
      /// <summary>Равно</summary>
      [CustomDescription("Attribute.Interfaces_339"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] Equal = 2,
      /// <summary>Не равно</summary>
      [CustomDescription("Attribute.Interfaces_340"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotEqual = 3,
      /// <summary>Больше</summary>
      [CustomDescription("Attribute.Interfaces_341"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] Greater = 4,
      /// <summary>Больше или равно</summary>
      [CustomDescription("Attribute.Interfaces_342"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] GreaterOrEqual = 5,
      /// <summary>Меньше</summary>
      [CustomDescription("Attribute.Interfaces_343"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] Less = 6,
      /// <summary>Меньше или равно</summary>
      [CustomDescription("Attribute.Interfaces_344"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] LessOrEqual = 7,
      /// <summary>Содержит строку</summary>
      [CustomDescription("Attribute.Interfaces_345"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] Substring = 8,
      /// <summary>Начинается со строки</summary>
      [CustomDescription("Attribute.Interfaces_346"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] StartString = 9,
      /// <summary>Заканчивается строкой</summary>
      [CustomDescription("Attribute.Interfaces_347"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] EndString = 10, // 0x0000000A
      /// <summary>Находится в диапазоне</summary>
      [CustomDescription("Attribute.Interfaces_348"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] Between = 11, // 0x0000000B
      /// <summary>Не содержит строку</summary>
      [CustomDescription("Attribute.Interfaces_349"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotSubstring = 12, // 0x0000000C
      /// <summary>Не начинается со строки</summary>
      [CustomDescription("Attribute.Interfaces_350"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotStartString = 13, // 0x0000000D
      /// <summary>Не заканчивается строкой</summary>
      [CustomDescription("Attribute.Interfaces_351"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotEndString = 14, // 0x0000000E
      /// <summary>Не находится в диапазоне</summary>
      [CustomDescription("Attribute.Interfaces_352"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotBetween = 15, // 0x0000000F
      /// <summary>Входит в</summary>
      [CustomDescription("Attribute.Interfaces_353"), SelectionInfo(UsedInSelection.Base, RelationOperatorOptions.InRelation)] EntersIn = 16, // 0x00000010
      /// <summary>Состоит из</summary>
      [CustomDescription("Attribute.Interfaces_354"), SelectionInfo(UsedInSelection.Base, RelationOperatorOptions.InRelation)] ConsistFrom = 17, // 0x00000011
      /// <summary>Не входит в объекты типа</summary>
      [CustomDescription("Attribute.Interfaces_355"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.InRelation)] NotEntersInType = 18, // 0x00000012
      /// <summary>Входит в объекты типа</summary>
      [CustomDescription("Attribute.Interfaces_356"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.InRelation)] EntersInType = 19, // 0x00000013
      /// <summary>Равно одному из значений</summary>
      [CustomDescription("Attribute.Interfaces_357"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] In = 20, // 0x00000014
      /// <summary>Включено в выборку (классификатор)</summary>
      [CustomDescription("Attribute.Interfaces_358"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] InSelection = 21, // 0x00000015
      /// <summary>Нет операции (заглушка)</summary>
      [CustomDescription("Attribute.Interfaces_359"), SelectionInfo(UsedInSelection.None, RelationOperatorOptions.NoneValue)] NOP = 22, // 0x00000016
      /// <summary>Состоит из объектов типа</summary>
      [CustomDescription("Attribute.Interfaces_360"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.InRelation)] ConsistFromType = 23, // 0x00000017
      /// <summary>Не состоит из объектов типа</summary>
      [CustomDescription("Attribute.Interfaces_361"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.InRelation)] NotConsistFromType = 24, // 0x00000018
      /// <summary>Создана на основе версии</summary>
      [CustomDescription("Attribute.Interfaces_362"), SelectionInfo(UsedInSelection.Base, RelationOperatorOptions.None)] ParentVersionID = 25, // 0x00000019
      /// <summary>Искать среди объектов типа</summary>
      [CustomDescription("Attribute.Interfaces_363"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] ObjectTypeFilter = 26, // 0x0000001A
      /// <summary>Содержит атрибут</summary>
      [CustomDescription("Attribute.Interfaces_364"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.NoneValue)] AttributeExists = 27, // 0x0000001B
      /// <summary>За последние N дней</summary>
      [CustomDescription("Attribute.Interfaces_365"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] LastNDays = 28, // 0x0000001C
      /// <summary>Не равно ни одному из значений</summary>
      [CustomDescription("Attribute.Interfaces_366"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotIn = 29, // 0x0000001D
      /// <summary>Не содержит атрибут или значение</summary>
      [CustomDescription("Attribute.NotExistsOrEmpty"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.NoneValue)] NotExistsOrEmpty = 30, // 0x0000001E
      /// <summary>Находится в контексте редактирования</summary>
      [CustomDescription("Attribute.ExistsInVersionContext"), SelectionInfo(UsedInSelection.Base, RelationOperatorOptions.InRelation)] ExistsInVersionContext = 31, // 0x0000001F
      /// <summary>Содержит шаблон строки</summary>
      [CustomDescription("Attribute.StringTemplate"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] StringTemplate = 32, // 0x00000020
      /// <summary>Поиск в общем индексе</summary>
      [CustomDescription("Attribute.InGlobalIndex"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] InGlobalIndex = 33, // 0x00000021
      /// <summary>Поиск в фильтрующей таблице IMS_ATTRFILTER_VALUE</summary>
      [CustomDescription("Attribute.InFiltrationTable"), SelectionInfo(UsedInSelection.None, RelationOperatorOptions.None)] InFiltrationTable = 34, // 0x00000022
      /// <summary>За следующие N дней</summary>
      [CustomDescription("Attribute.NextNDays"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NextNDays = 35, // 0x00000023
      /// <summary>Поиск в истории переходов по шагам жизненного цикла</summary>
      [CustomDescription("Attribute.InLCHistory"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] InLCHistory = 36, // 0x00000024
      /// <summary>На версии объектов есть ссылки</summary>
      [CustomDescription("Attribute.Linked"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] Linked = 37, // 0x00000025
      /// <summary>На версии объектов нет ссылок</summary>
      [CustomDescription("Attribute.NotLinked"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] NotLinked = 38, // 0x00000026
      /// <summary>Поиск среди объектов локальных типов</summary>
      [CustomDescription("Attribute.LocalObjectTypes"), SelectionInfo(UsedInSelection.All, RelationOperatorOptions.None)] LocalObjectTypes = 39, // 0x00000027
    }
}
