
// Type: Intermech.RelationConstraintModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Как обрабатывать удаление объектов, связанных этой связью:
    /// 0 - не мешать удалению родителя и дочернего объекта.
    /// 1 - не давать удалять дочерний объект, не удалив связь.
    /// 2 - не давать удалять родительский объект, не удалив связь.
    /// 3 - не давать удалять и дочерний, и родительский объект, не удалив связь.
    /// 4 - удалять дочерний объект при удалении родительского.
    /// 5 - удалять родительский объект при удалении всех дочерних, включенных данной связью.
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_72")]
    [Category("Misc")]
    public enum RelationConstraintModes
    {
      /// <summary>
      /// Не запрещать удаление родительского и дочернего объектов
      /// </summary>
      [CustomDescription("Attribute.Interfaces_73")] None,
      /// <summary>Запрещать удаление дочернего объекта</summary>
      [CustomDescription("Attribute.Interfaces_74")] ChildConstrained,
      /// <summary>Запрещать удаление родительского объекта</summary>
      [CustomDescription("Attribute.Interfaces_75")] ParentConstrained,
      /// <summary>Запрещать удаление родительского и дочернего объектов</summary>
      [CustomDescription("Attribute.Interfaces_76")] ParentChildConstrained,
      /// <summary>
      /// Удалять дочерний объект при удалении родительского объекта
      /// </summary>
      [CustomDescription("Attribute.Interfaces_77")] ChildDelete,
      /// <summary>
      /// Удалять родительский объект при удалении всех дочерних объектов, включенных в его состав связью данного типа
      /// </summary>
      [CustomDescription("Attribute.Interfaces_78")] ParentDelete,
      /// <summary>
      /// Удалять дочерний объект при удалении родительского объекта без проверки применяемости
      /// </summary>
      [CustomDescription("ChildForcedDelete")] ChildForcedDelete,
    }
}
