
// Type: Intermech.Interfaces.CompositionsAutosortRuleEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Аргументы событий от правила отображения и сортировки составов
    /// </summary>
    [Serializable]
    public class CompositionsAutosortRuleEventArgs : EventArgs, IAssignable, ICloneable
    {
      /// <summary>Тип родительского объекта</summary>
      public int ObjectType = -1;
      /// <summary>
      /// Возвращать ли в списке VisibleRelTypes тип связи по умолчанию,
      /// если нет ни одного видимого типа связи в настройках правила
      /// </summary>
      public bool ReturnDefault = true;
      /// <summary>Список видимых типов связей</summary>
      public List<int> VisibleRelTypes;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="objectType">Тип родительского объекта</param>
      /// <param name="returnDefault">Возвращать ли в списке VisibleRelTypes тип связи по умолчанию,
      /// если нет ни одного видимого типа связи в настройках правила</param>
      /// <param name="visibleRelTypes">Список видимых типов связей</param>
      public CompositionsAutosortRuleEventArgs(
        int objectType,
        bool returnDefault,
        List<int> visibleRelTypes)
      {
        this.ObjectType = objectType;
        this.ReturnDefault = returnDefault;
        this.VisibleRelTypes = visibleRelTypes ?? new List<int>();
      }

      /// <summary>
      /// Создать экземпляр класса и заполнить его информацией из объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public CompositionsAutosortRuleEventArgs(object source) => this.Assign(source);

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this.ObjectType = -1;
        this.ReturnDefault = true;
        this.VisibleRelTypes = new List<int>();
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is CompositionsAutosortRuleEventArgs autosortRuleEventArgs))
          return;
        this.ObjectType = autosortRuleEventArgs.ObjectType;
        this.ReturnDefault = autosortRuleEventArgs.ReturnDefault;
        this.VisibleRelTypes = autosortRuleEventArgs.VisibleRelTypes != null ? new List<int>((IEnumerable<int>) autosortRuleEventArgs.VisibleRelTypes) : this.VisibleRelTypes;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns></returns>
      public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);
    }
}
