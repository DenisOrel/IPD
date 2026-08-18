
// Type: Intermech.Interfaces.UpdateHandlerEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для аргументов изменения форм редактирования объектов и связей.
    /// </summary>
    public class UpdateHandlerEventArgs : EventArgs
    {
      private readonly Dictionary<FormInformation, Tuple<FormOrderPriority, int>> _oldFormsOrderedList;
      private Dictionary<FormInformation, Tuple<FormOrderPriority, int>> _newFormsOrderedList;
      private List<FormInformation> _newList;

      /// <summary>Продолжать обработку вызовов обновлений форм.</summary>
      public bool ContinueProcessing { get; set; }

      /// <summary>Вид - объект/связь.</summary>
      public AttributableElements Kind { get; private set; }

      /// <summary>Обновленный список ID объектов форм.</summary>
      public List<FormInformation> NewList
      {
        get => this._newList;
        set
        {
          this._newList = new List<FormInformation>();
          this.UpdateNewFormsOrderedList();
          if (value == null)
          {
            this._newFormsOrderedList.Clear();
          }
          else
          {
            int num = 0;
            foreach (FormInformation key in value)
            {
              if (!this._newFormsOrderedList.ContainsKey(key))
              {
                num += 100;
                this._newFormsOrderedList.Add(key, Tuple.Create<FormOrderPriority, int>(FormOrderPriority.Medium, num));
              }
            }
          }
        }
      }

      /// <summary>Список ID объектов форм.</summary>
      public List<FormInformation> OldList
      {
        get
        {
          Dictionary<FormInformation, Tuple<FormOrderPriority, int>> formsOrderedList = this._oldFormsOrderedList;
          return formsOrderedList == null ? (List<FormInformation>) null : formsOrderedList.Keys.ToList<FormInformation>();
        }
      }

      /// <summary>Экземпляр объекта/связи.</summary>
      public IDBAttributable Parent { get; private set; }

      /// <summary>Связь к родителю от объекта (может и не быть).</summary>
      public IDBRelation ParentRelation { get; private set; }

      /// <summary>
      /// Возвращает идентификатор типа объекта(связи) если есть объект(связь).
      /// </summary>
      public int ParentType { get; private set; }

      /// <summary>Сохранять данные в кэше на ТИП объекта/связи.</summary>
      public bool StoreInTypesCache { get; set; }

      /// <summary>Сохранять данные в кэше на ВЕРСИЮ объекта/связи.</summary>
      public bool StoreInVersionCache { get; set; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="kind"></param>
      /// <param name="dict"></param>
      public UpdateHandlerEventArgs(
        AttributableElements kind,
        Dictionary<FormInformation, Tuple<FormOrderPriority, int>> dict)
      {
        this.ContinueProcessing = true;
        this.StoreInTypesCache = true;
        this.StoreInVersionCache = true;
        this.Kind = kind;
        this._oldFormsOrderedList = dict;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="parent">Экземпляр объекта/связи</param>
      /// <param name="parentRelation">Связь к родителю</param>
      /// <param name="kind">Вид - объект/связь</param>
      /// <param name="dict">Список ID объектов форм</param>
      public UpdateHandlerEventArgs(
        IDBAttributable parent,
        IDBRelation parentRelation,
        AttributableElements kind,
        Dictionary<FormInformation, Tuple<FormOrderPriority, int>> dict)
        : this(kind, dict)
      {
        this.Parent = parent;
        this.ParentRelation = parentRelation;
        IDBAttributable parent1 = this.Parent;
        this.ParentType = parent1 != null ? parent1.TypeID : -1;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="parentType">Идентификатор типа объекта/связи</param>
      /// <param name="kind">Вид - объект/связь</param>
      /// <param name="dict">Список ID объектов форм</param>
      public UpdateHandlerEventArgs(
        int parentType,
        AttributableElements kind,
        Dictionary<FormInformation, Tuple<FormOrderPriority, int>> dict)
        : this(kind, dict)
      {
        this.ParentType = parentType;
      }

      /// <summary>
      /// 
      /// </summary>
      private void UpdateNewFormsOrderedList()
      {
        this._newFormsOrderedList = this._newFormsOrderedList ?? new Dictionary<FormInformation, Tuple<FormOrderPriority, int>>();
      }

      public FormOrderPriority GetFormInformationPriorityFromOldList(FormInformation fi)
      {
        return this._oldFormsOrderedList[fi].Item1;
      }

      public int GetFormInformationIndexFromOldList(FormInformation fi)
      {
        return this._oldFormsOrderedList[fi].Item2;
      }

      public void AddOrChangeFormInformationInNewList(
        FormInformation fi,
        FormOrderPriority priority,
        int index)
      {
        this.UpdateNewFormsOrderedList();
        if (this._newFormsOrderedList.ContainsKey(fi))
          this._newFormsOrderedList[fi] = Tuple.Create<FormOrderPriority, int>(priority, index);
        else
          this._newFormsOrderedList.Add(fi, Tuple.Create<FormOrderPriority, int>(priority, index));
      }

      public Dictionary<FormInformation, Tuple<FormOrderPriority, int>> GetNewFormInformation
      {
        get
        {
          if (this._newList != null && this._newList.Count > 0)
          {
            this.UpdateNewFormsOrderedList();
            int num = this._newFormsOrderedList.Count > 0 ? this._newFormsOrderedList.Select<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, int>((Func<KeyValuePair<FormInformation, Tuple<FormOrderPriority, int>>, int>) (x => x.Value.Item2)).Max() : 0;
            foreach (FormInformation key in this._newList)
            {
              if (!this._newFormsOrderedList.ContainsKey(key))
              {
                num += 100;
                this._newFormsOrderedList.Add(key, Tuple.Create<FormOrderPriority, int>(FormOrderPriority.Medium, num));
              }
            }
          }
          return this._newFormsOrderedList;
        }
      }
    }
}
