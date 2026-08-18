
// Type: Intermech.ObjectModifyModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Способы модификации объектов (применяется на этапах ЖЦ):
    /// 0 - объекты модифицируются в базе без взятия их на изменение,
    /// 1 - объекты должны быть предварительно взяты на изменение,
    /// 2 - нужно выпустить новую версию объекта (текущую изменять нельзя),
    /// 3 - объект модифицировать никак нельзя (можно только перевести на следующи уровень
    /// продвижения или добавить связи).
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_67")]
    [Category("Misc")]
    public enum ObjectModifyModes
    {
      /// <summary>Непосредственно в базе данных</summary>
      [CustomDescription("Attribute.Interfaces_68")] InBase,
      /// <summary>Через рабочую копию объекта</summary>
      [CustomDescription("Attribute.Interfaces_69")] Checkout,
      /// <summary>Выпуском новой версии объекта</summary>
      [CustomDescription("Attribute.Interfaces_70")] CreateVersion,
      /// <summary>Нельзя модифицировать объект</summary>
      [CustomDescription("Attribute.Interfaces_71")] CantModify,
    }
}
