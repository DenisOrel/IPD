
// Type: Intermech.Interfaces.Contexts.CurrentEditingContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>Текущий контекст редактирования, режим его работы</summary>
    [Serializable]
    public sealed class CurrentEditingContext : 
      ICloneable,
      IComparable,
      IComparable<CurrentEditingContext>
    {
      /// <summary>
      /// Идентификатор текущего контекста
      /// (Intermech.Consts.UnknownObjectId - контекст отключен)
      /// </summary>
      private readonly long contextID;
      /// <summary>Номер взаимосвязанного контекста</summary>
      private readonly long modificationID;
      /// <summary>Режим работы контекста, если он включен</summary>
      private readonly EditingContextMode contextMode;
      /// <summary>
      /// Признак специального значения для имитации фиксации контекста редактирования в контексте вызова.
      /// </summary>
      private readonly bool isDummy;
      /// <summary>
      /// Специальное значение для отключения контекста редактирования.
      /// </summary>
      private static readonly CurrentEditingContext emptyInstance = new CurrentEditingContext(0L, 0L, EditingContextMode.Default, false);
      /// <summary>
      /// Специальное значение для имитации фиксации контекста редактирования в контексте вызова.
      /// </summary>
      private static readonly CurrentEditingContext dummyInstance = new CurrentEditingContext(0L, 0L, EditingContextMode.Default, true);

      /// <summary>Создать экземпляр класса, заполнить его поля</summary>
      /// <param name="contextID">Идентификатор текущего контекста
      /// (Intermech.Consts.UnknownObjectId - контекст отключен)</param>
      /// <param name="modificationID">Номер взаимосвязанного контекста</param>
      /// <param name="contextMode">Режим работы контекста, если он включен</param>
      /// <param name="isDummy">Признак специального значения</param>
      private CurrentEditingContext(
        long contextID,
        long modificationID,
        EditingContextMode contextMode,
        bool isDummy)
      {
        this.contextID = contextID;
        this.contextMode = contextMode;
        this.modificationID = modificationID;
        this.isDummy = isDummy;
      }

      /// <summary>Создать экземпляр класса, заполнить его поля</summary>
      /// <param name="contextID">Идентификатор текущего контекста
      /// (Intermech.Consts.UnknownObjectId - контекст отключен)</param>
      /// <param name="modificationID">Номер взаимосвязанного контекста</param>
      /// <param name="contextMode">Режим работы контекста, если он включен</param>
      public CurrentEditingContext(long contextID, long modificationID, EditingContextMode contextMode)
        : this(contextID, modificationID, contextMode, false)
      {
      }

      /// <summary>Проверить равенство двух объектов</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is CurrentEditingContext currentEditingContext && this.ContextID == currentEditingContext.ContextID && this.ContextMode == currentEditingContext.ContextMode && this.IsDummy == currentEditingContext.IsDummy;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.ContextID.GetHashCode() << 2 ^ this.ContextMode.GetHashCode();
      }

      /// <summary>
      /// Возвращает специальное значение, предназначеннное для отключения контекста редактирования.
      /// </summary>
      public static CurrentEditingContext Empty
      {
        [DebuggerStepThrough] get => CurrentEditingContext.emptyInstance;
      }

      /// <summary>
      /// Возвращает специальное значение, предназначенное для имитации фиксации контекста редактирования в контексте вызова.
      /// </summary>
      public static CurrentEditingContext Dummy
      {
        [DebuggerStepThrough] get => CurrentEditingContext.dummyInstance;
      }

      /// <summary>Создать точную копию объекта</summary>
      /// <returns>Точная копия объекта</returns>
      public CurrentEditingContext Clone()
      {
        return new CurrentEditingContext(this.ContextID, this.ModificationID, this.ContextMode, this.IsDummy);
      }

      /// <summary>Создать точную копию объекта</summary>
      /// <returns>Точная копия объекта</returns>
      object ICloneable.Clone() => (object) this.Clone();

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as CurrentEditingContext);

      /// <summary>
      /// Идентификатор текущего контекста
      /// (Intermech.Consts.UnknownObjectId - контекст отключен)
      /// </summary>
      public long ContextID
      {
        [DebuggerStepThrough] get => this.contextID;
      }

      /// <summary>Режим работы контекста, если он включен</summary>
      public EditingContextMode ContextMode
      {
        [DebuggerStepThrough] get => this.contextMode;
      }

      /// <summary>Номер взаимосвязанного контекста</summary>
      public long ModificationID
      {
        [DebuggerStepThrough] get => this.modificationID;
      }

      /// <summary>
      /// Является ли текущий объект специальным значением, которое означает, что контекст редактирования отключен.
      /// </summary>
      public bool IsEmpty
      {
        [DebuggerStepThrough] get => !this.isDummy && this.ContextID == 0L && this.ModificationID == 0L;
      }

      /// <summary>
      /// Является ли текущий объект специальным значением, которое требуется проигнорировать
      /// (используется для имитации фиксации контекста редактирования)
      /// </summary>
      public bool IsDummy
      {
        [DebuggerStepThrough] get => this.isDummy;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(CurrentEditingContext other)
      {
        if (other == null)
          return 1;
        int num = Math.Abs(this.ContextID).CompareTo(Math.Abs(other.ContextID));
        if (num == 0)
          num = this.ContextMode.CompareTo((object) other.ContextMode);
        if (num == 0)
          num = this.IsDummy.CompareTo(other.IsDummy);
        return num;
      }
    }
}
