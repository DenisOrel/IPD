
// Type: Intermech.Diagnostics.CanBeEmptyAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Признак того, что описываемая сущность может иметь пустое значение.
    ///   идентификаторы: значение может иметь неизвестное или неопределённое значение, например идентификатор объекта может равняться Consts.UnknownObjectId, Consts.NoObject или Consts.NavigatorUndefinedObjectID
    ///   строки: строка может быть равна string.Empty
    ///   коллекция/перечисление/список/массив: в нём может не быть элементов.
    ///   Object: может быть равен DBNull
    ///   Guid: может быть равен Guid.Empty
    ///   ObligatoryObjectAttribute: может равняться ObligatoryObjectAttributes.Zero или ObligatoryObjectAttributes.None.
    ///   и так далее по аналогии для всех других типов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CanBeEmptyAttribute : Attribute
    {
    }
}
