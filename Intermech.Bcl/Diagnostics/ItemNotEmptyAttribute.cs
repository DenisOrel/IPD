
// Type: Intermech.Diagnostics.ItemNotEmptyAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Признак того, что все элементы описываемой коллекции/перечислении/списка/массива не могут иметь пустое значение.
    ///   идентификаторы: значение не может иметь неизвестное или неопределённое значение, например идентификатор объекта не может равняться Consts.UnknownObjectId, Consts.NoObject или Consts.NavigatorUndefinedObjectID
    ///   строки: строка быть отличной от string.Empty
    ///   коллекция/перечисление/список/массив: в нём должен быть хотя бы один элемент.
    ///   Object: не может быть равен DBNull
    ///   Guid: не может быть равен Guid.Empty
    ///   ObligatoryObjectAttribute: не может равняться ObligatoryObjectAttributes.Zero или ObligatoryObjectAttributes.None.
    ///   и так далее по аналогии для всех других типов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
    public sealed class ItemNotEmptyAttribute : Attribute
    {
    }
}
