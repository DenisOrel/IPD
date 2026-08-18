
// Type: Intermech.Interfaces.Sets.ISet
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Sets
{
    /// <summary>Интерфейс диапазона множеств</summary>
    public interface ISet : IAssignable, ICloneable, IDisplayable, ICodedStringFrom, IEditableString
    {
      /// <summary>Является ли множество пустым</summary>
      bool IsEmpty { get; }

      /// <summary>Является ли множество открытым</summary>
      bool IsOpen { get; }

      /// <summary>Является ли множество открытым слева</summary>
      bool IsLeftOpen { get; }

      /// <summary>Является ли множество открытым справа</summary>
      bool IsRightOpen { get; }

      /// <summary>Проверить на пересечение с указанным множеством</summary>
      /// <param name="set">Проверяемое множество</param>
      /// <returns>true - есть пересечение в каком-либо диапазоне</returns>
      bool IsIntersectsWith(ISet set);

      /// <summary>
      /// Проверить, можно ли добавить указанное множество во множество.
      /// Условие - диапазоны не должны пересекаться ни с одним диапазоном во множестве
      /// </summary>
      /// <param name="set">Проверяемое множество</param>
      /// <returns>true - множество можно добавлять во множество</returns>
      bool CanAdd(ISet set);

      /// <summary>
      /// Добавить указанное множество
      /// При ошибке будет выдано исключение ArithmeticException.
      /// </summary>
      /// <param name="set">Добавляемое множество</param>
      void Add(ISet set);

      /// <summary>Количество диапазонов во множестве</summary>
      int Count { get; }
    }
}
