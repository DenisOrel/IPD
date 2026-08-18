
// Type: Intermech.Interfaces.Compositions.RelationPath
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Вспомогательный класс, позволяющий описать полный путь к любому дочернему объекту в составе
    /// </summary>
    [Serializable]
    public sealed class RelationPath : IAssignable, ICloneable, IComparable, IComparable<RelationPath>
    {
      /// <summary>
      /// Уникальный ключ, позволяющий сохранять путь в дополнительных параметрах запросов
      /// </summary>
      public static Guid RelationPathGuid = new Guid("{7DE0DFDF-EB51-472B-B17E-A211C238C820}");
      /// <summary>
      /// Учитывать знак у идентификаторов версий объектов/связей при сравнениях
      /// </summary>
      private bool _signSensitive = true;
      /// <summary>Список элементов пути</summary>
      private List<SimpleRelationPair> _items = new List<SimpleRelationPair>();
      /// <summary>Пользовательские данные</summary>
      private object _tag;

      /// <summary>
      /// Учитывать знак у идентификаторов версий объектов/связей при сравнениях
      /// </summary>
      public bool SignSensitive
      {
        [DebuggerStepThrough] get => this._signSensitive;
        set
        {
          this._signSensitive = value;
          this.SetSignSensitive();
        }
      }

      /// <summary>Является ли путь пустым</summary>
      public bool Empty
      {
        [DebuggerStepThrough] get => this.Items.Count == 0 && this.Tag == null;
      }

      /// <summary>Список элементов пути</summary>
      public List<SimpleRelationPair> Items
      {
        [DebuggerStepThrough] get => this._items;
      }

      /// <summary>Пользовательские данные</summary>
      public object Tag
      {
        [DebuggerStepThrough] get => this._tag;
        set => this._tag = value;
      }

      /// <summary>Создать пустой путь</summary>
      public RelationPath()
      {
      }

      /// <summary>
      /// Создать пустой путь, указать чувствительность к знакам
      /// </summary>
      /// <param name="signSensitive">Учитывать знак у идентификаторов версий объектов/связей при сравнениях</param>
      public RelationPath(bool signSensitive) => this._signSensitive = signSensitive;

      /// <summary>
      /// Создать пустой путь, заполнить его информацией из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public RelationPath(object source) => this.Assign(source);

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this._signSensitive = true;
        this.Items.Clear();
        this.Tag = (object) null;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is RelationPath relationPath))
          return;
        this._signSensitive = relationPath.SignSensitive;
        if (!(CloneHelper.Clone((object) relationPath._items) is List<SimpleRelationPair> simpleRelationPairList))
          simpleRelationPairList = new List<SimpleRelationPair>();
        this._items = simpleRelationPairList;
        this.Tag = CloneHelper.Clone(relationPath.Tag);
        this.SetSignSensitive();
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new RelationPath((object) this);

      /// <summary>
      /// Создать точную копию пути, задав чувствительность к знакам
      /// </summary>
      /// <param name="signSensitive">Учитывать знак у идентификаторов версий объектов/связей при сравнениях</param>
      /// <returns>Точная копия пути с заданной чувствительностью к знакам</returns>
      public RelationPath SignedClone(bool signSensitive)
      {
        return new RelationPath((object) this)
        {
          SignSensitive = signSensitive
        };
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as RelationPath);

      /// <summary>Сравнить с другим объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(RelationPath other)
      {
        if (this == other)
          return 0;
        if (other == null)
          return 1;
        int num = this.Items.Count.CompareTo(other.Items.Count);
        if (num != 0)
          return num;
        for (int index = 0; index < this.Items.Count; ++index)
        {
          num = this.Items[index].CompareTo(other.Items[index]);
          if (num != 0)
            return num;
        }
        return num;
      }

      /// <summary>Проверить на равенство с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj) => this.CompareTo(obj as RelationPath) == 0;

      public override int GetHashCode()
      {
        int hashCode = this.Items.Count.GetHashCode();
        for (int index = 0; index < this.Items.Count; ++index)
          hashCode ^= this.Items[index].GetHashCode();
        return hashCode;
      }

      /// <summary>Описание экземпляра класса в виде строки</summary>
      /// <returns>Описание экземпляра класса в виде строки</returns>
      public override string ToString()
      {
        if (this.Empty)
          return "";
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < this.Items.Count; ++index)
        {
          stringBuilder.Append(this.Items[index].ToString(false));
          if (index < this.Items.Count - 1)
            stringBuilder.Append(" \\ ");
        }
        return stringBuilder.ToString();
      }

      /// <summary>Метод позволяет скомбинировать элементы в единый путь</summary>
      /// <param name="items">Элементы пути</param>
      /// <returns>Суммарный путь из нескольких элементов</returns>
      public static RelationPath Combine(params RelationPath[] items)
      {
        RelationPath relationPath = new RelationPath();
        if (items == null || items.Length == 0)
          return relationPath;
        for (int index = 0; index < items.Length; ++index)
          relationPath.Items.AddRange((IEnumerable<SimpleRelationPair>) items[index].Items);
        relationPath.SetSignSensitive();
        return relationPath;
      }

      /// <summary>Установить чувствительность к знаку у элементов пути</summary>
      private void SetSignSensitive()
      {
        this._items.ForEach((Action<SimpleRelationPair>) (item => item.SignSensitive = this.SignSensitive));
      }
    }
}
