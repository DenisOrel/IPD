
// Type: Intermech.Interfaces.Sets.IRange`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Sets
{
    /// <summary>Интерфейс множества значений</summary>
    public interface IRange<T> : IAssignable, ICloneable, IDisplayable, ICodedStringFrom, IEditableString
    {
      /// <summary>Является ли диапазон пустым</summary>
      bool IsEmpty { get; }

      /// <summary>Является ли диапазон бесконечным с обеих сторон</summary>
      bool IsOpen { get; }

      /// <summary>
      /// Является ли диапазон открытым слева
      /// (диапазон не должен быть пустым)
      /// </summary>
      bool IsLeftOpen { get; }

      /// <summary>
      /// Является ли диапазон открытым справа
      /// (диапазон не должен быть пустым)
      /// </summary>
      bool IsRightOpen { get; }

      /// <summary>
      /// Начальная граница диапазона (исключение ArithmeticException, если диапазон пустой)
      /// </summary>
      T MinValue { get; set; }

      /// <summary>
      /// Конечная граница диапазона (исключение ArithmeticException, если диапазон пустой, либо ArgumentOutOfRangeException)
      /// </summary>
      T MaxValue { get; set; }

      /// <summary>Упорядочить дочерние элементы</summary>
      void Normalize();

      /// <summary>Проверить, входит ли указанное значение в диапазон</summary>
      /// <param name="value">Проверяемое значение</param>
      /// <returns>true - значение входит в диапазон</returns>
      bool Contains(T value);

      /// <summary>
      /// Проверить, содержит ли текущий диапазон все значения из указанного диапазона
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - проверяемый диапазон целиком входит в текущий диапазон, исключение ArgumentNullException - при ошибке</returns>
      bool Contains(IRange<T> range);

      /// <summary>
      /// Проверить, пересекает ли проверяемый диапазон текущий любой из своих границ
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - проверяемый диапазон одной из границ пересекает текущий диапазон, исключение ArgumentNullException - при ошибке</returns>
      bool IsIntersect(IRange<T> range);

      /// <summary>
      /// Проверить, прилегает ли проверяемый диапазон к текущему
      /// </summary>
      /// <param name="range">Проверяемый диапазон</param>
      /// <returns>true - проверяемый диапазон прилегает к текущему диапазону одной из границ, исключение ArgumentNullException - при ошибке</returns>
      bool IsAdjoining(IRange<T> range);

      /// <summary>
      /// Проверить, возможно ли объединение с указанным диапазоном
      /// </summary>
      /// <param name="range">Диапазон, с которым требуется проверить возможность объединения</param>
      /// <returns>true - данные диапазоны можно объединять</returns>
      bool CanMerge(IRange<T> range);

      /// <summary>
      /// Проверить, возможно ли объединение с указанными диапазонами
      /// </summary>
      /// <param name="ranges">Диапазоны, с которыми требуется проверить возможность объединения</param>
      /// <returns>true - данные диапазоны можно объединять</returns>
      bool CanMerge(IList<IRange<T>> ranges);

      /// <summary>
      /// Выполнить объединение с указанным диапазоном.
      /// Ограничение: диапазоны должны быть закрытыми, либо открыты с одной стороны
      /// </summary>
      /// <param name="range">Диапазон, с которым требуется выполнить объединение</param>
      /// <returns>Объединённый диапазон (исключения ArgumentNullException, ArithmeticException при ошибках)</returns>
      IRange<T> Merge(IRange<T> range);

      /// <summary>
      /// Выполнить объединение с указанными диапазонами.
      /// Ограничение: диапазоны должны быть закрытыми, либо открыты с одной стороны
      /// </summary>
      /// <param name="ranges">Диапазоны, с которыми требуется выполнить объединение</param>
      /// <returns>Объединённый диапазон (исключение ArgumentNullException при ошибках)</returns>
      IRange<T> Merge(IList<IRange<T>> ranges);

      /// <summary>
      /// Проверить, можно ли разделить диапазон на два диапазона с помощью указанного значения
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <param name="toLeftRange">true - сделать точку-разделитель максимальной границей левого диапазона-результата,
      /// false - минимальной границей правого диапазона-результата</param>
      /// <returns>true - разделение диапазона допустимо</returns>
      bool CanBreak(T value, bool toLeftRange);

      /// <summary>
      /// Разделить диапазон на два диапазона с помощью указанного значения.
      /// Значение может стать границей левого или правого диапазонов-результатов
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <param name="toLeftRange">true - сделать точку-разделитель максимальной границей левого диапазона-результата,
      /// false - минимальной границей правого диапазона-результата</param>
      /// <returns>Два диапазона-результата (левый, затем правый диапазоны), или исключение ArithmeticException при ошибке</returns>
      List<IRange<T>> Break(T value, bool toLeftRange);

      /// <summary>
      /// Проверить, можно ли разделить диапазон на несколько диапазонов с помощью указанного значения.
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <returns>true - есть возможность разделить диапазон</returns>
      bool CanSplit(T value);

      /// <summary>
      /// Разделить диапазон на несколько диапазонов с помощью указанного значения.
      /// Данное значение будет представлено в виде отдельного диапазона.
      /// </summary>
      /// <param name="value">Разделитель диапазона</param>
      /// <returns>Один или несколько диапазонов, которые в сумме дают исходный диапазон и указанное значение,
      /// или исключение ArithmeticException при ошибке</returns>
      List<IRange<T>> Split(T value);

      /// <summary>
      /// Проверить, можно ли удалить указанный диапазон из текущего диапазона.
      /// Ограничения - диапазон должен пересекаться с текущим диапазоном
      /// </summary>
      /// <param name="range">Удаляемый диапазон</param>
      /// <returns>true - удаление разрешено</returns>
      bool CanDelete(IRange<T> range);

      /// <summary>
      /// Удалить указанный диапазон из текущего диапазона.
      /// Ограничения - диапазон должен пересекаться с текущим диапазоном
      /// При ошибках будет выдано одно из исключений - ArithmeticException или ArgumentNullException
      /// </summary>
      /// <param name="range">Удаляемый диапазон</param>
      /// <returns>Один или несколько диапазонов, получившихся в результате удаления</returns>
      List<IRange<T>> Delete(IRange<T> range);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      int CompareTo(IRange<T> other);

      /// <summary>Отыскать предыдущее значение</summary>
      /// <param name="value">Изменяемое значение</param>
      /// <returns>Предыдущее значение</returns>
      T Prev(T value);

      /// <summary>Отыскать следующее значение</summary>
      /// <param name="value">Изменяемое значение</param>
      /// <returns>Следующее значение</returns>
      T Next(T value);

      /// <summary>Найти минимальное значение</summary>
      /// <param name="value1">Первое значение</param>
      /// <param name="value2">Второе значение</param>
      /// <returns></returns>
      T Min(T value1, T value2);

      /// <summary>Найти максимальное значение</summary>
      /// <param name="value1">Первое значение</param>
      /// <param name="value2">Второе значение</param>
      /// <returns></returns>
      T Max(T value1, T value2);
    }
}
