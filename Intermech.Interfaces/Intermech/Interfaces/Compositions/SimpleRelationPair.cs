
// Type: Intermech.Interfaces.Compositions.SimpleRelationPair
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс-ключ для хранения значений [F_PART_ID x F_OBJECT_TYPE] либо значений [F_PRJLINK_ID x F_RELATION_TYPE].
    /// При сравнении, хэшировании учитывается либо [F_PRJLINK_ID], либо значения [F_PART_ID x F_OBJECT_TYPE].
    /// Приоритетным считается значение [F_PRJLINK_ID].
    /// </summary>
    [Serializable]
    public sealed class SimpleRelationPair : 
      IAssignable,
      ICloneable,
      IComparable,
      IComparable<SimpleRelationPair>
    {
      /// <summary>
      /// Учитывать знак у идентификаторов версий объектов/связей при сравнениях
      /// </summary>
      private bool _signSensitive = true;
      /// <summary>Идентификатор связи</summary>
      private long _F_PRJLINK_ID;
      /// <summary>Идентификатор типа связи</summary>
      private int _F_RELATION_TYPE = -1;
      /// <summary>Идентификатор версии объекта состава</summary>
      private long _F_PART_ID;
      /// <summary>Идентификатор типа объекта состава</summary>
      private int _F_OBJECT_TYPE = -1;

      /// <summary>
      /// Учитывать знак у идентификаторов версий объектов/связей при сравнениях
      /// </summary>
      public bool SignSensitive
      {
        [DebuggerStepThrough] get => this._signSensitive;
        set => this._signSensitive = value;
      }

      /// <summary>Идентификатор связи</summary>
      public long F_PRJLINK_ID
      {
        [DebuggerStepThrough] get => this._F_PRJLINK_ID;
      }

      /// <summary>Идентификатор типа связи</summary>
      public int F_RELATION_TYPE
      {
        [DebuggerStepThrough] get => this._F_RELATION_TYPE;
      }

      /// <summary>Идентификатор версии объекта состава</summary>
      public long F_PART_ID
      {
        [DebuggerStepThrough] get => this._F_PART_ID;
      }

      /// <summary>Идентификатор типа объекта</summary>
      public int F_OBJECT_TYPE
      {
        [DebuggerStepThrough] get => this._F_OBJECT_TYPE;
      }

      /// <summary>Является ли ключ пустым</summary>
      public bool Empty
      {
        get
        {
          return this.F_PRJLINK_ID == 0L && this.F_PART_ID == 0L && this.F_RELATION_TYPE == -1 && this.F_OBJECT_TYPE == -1;
        }
      }

      /// <summary>Создать пустой экземпляр класса</summary>
      public SimpleRelationPair()
      {
      }

      /// <summary>
      /// Создать пустой экземпляр класса, указать чувствительность к знаку
      /// </summary>
      /// <param name="signSensitive">Учитывать знак у идентификаторов версий объектов/связей при сравнениях</param>
      public SimpleRelationPair(bool signSensitive) => this._signSensitive = signSensitive;

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public SimpleRelationPair(object source) => this.Assign(source);

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="relType">Идентификатор типа связи</param>
      /// <param name="partID">Идентификатор версии объекта состава</param>
      /// <param name="objType">Идентификатор типа объекта состава</param>
      public SimpleRelationPair(long prjLinkID, int relType, long partID, int objType)
        : this(prjLinkID, relType, partID, objType, true)
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="relType">Идентификатор типа связи</param>
      /// <param name="partID">Идентификатор версии объекта состава</param>
      /// <param name="objType">Идентификатор типа объекта состава</param>
      /// <param name="signSensitive">TODO</param>
      public SimpleRelationPair(
        long prjLinkID,
        int relType,
        long partID,
        int objType,
        bool signSensitive)
      {
        this._signSensitive = signSensitive;
        this._F_PRJLINK_ID = prjLinkID;
        this._F_RELATION_TYPE = relType;
        this._F_PART_ID = partID;
        this._F_OBJECT_TYPE = objType;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        if (!(obj is SimpleRelationPair simpleRelationPair))
          return false;
        return this.F_PRJLINK_ID != 0L && simpleRelationPair.F_PRJLINK_ID != 0L ? (!this.SignSensitive ? Math.Abs(this.F_PRJLINK_ID) == Math.Abs(simpleRelationPair.F_PRJLINK_ID) : this.F_PRJLINK_ID == simpleRelationPair.F_PRJLINK_ID) : (!this.SignSensitive ? Math.Abs(this.F_PART_ID) == Math.Abs(simpleRelationPair.F_PART_ID) && this.F_RELATION_TYPE == simpleRelationPair.F_RELATION_TYPE && this.F_OBJECT_TYPE == simpleRelationPair.F_OBJECT_TYPE : this.F_PART_ID == simpleRelationPair.F_PART_ID && this.F_RELATION_TYPE == simpleRelationPair.F_RELATION_TYPE && this.F_OBJECT_TYPE == simpleRelationPair.F_OBJECT_TYPE);
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.F_PRJLINK_ID != 0L ? (!this.SignSensitive ? Math.Abs(this.F_PRJLINK_ID).GetHashCode() : this.F_PRJLINK_ID.GetHashCode()) : (!this.SignSensitive ? Math.Abs(this.F_PART_ID).GetHashCode() << 8 ^ this.F_OBJECT_TYPE.GetHashCode() : this.F_PART_ID.GetHashCode() << 8 ^ this.F_OBJECT_TYPE.GetHashCode());
      }

      /// <summary>Описание экземпляра класса в виде строки</summary>
      /// <returns>Описание экземпляра класса в виде строки</returns>
      public override string ToString() => this.ToString(true);

      /// <summary>Описание экземпляра класса в виде строки</summary>
      /// <param name="withDescriptions">true - добавлять расшифровки типов объектов и связей</param>
      /// <returns>Описание экземпляра класса в виде строки</returns>
      public string ToString(bool withDescriptions)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.F_PRJLINK_ID != 0L)
        {
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_705"), (object) (this.SignSensitive ? this.F_PRJLINK_ID : Math.Abs(this.F_PRJLINK_ID))));
          if (this.F_RELATION_TYPE != -1)
          {
            string relationTypeName = MetaDataHelper.GetRelationTypeName(this.F_RELATION_TYPE);
            if (!string.IsNullOrEmpty(relationTypeName))
            {
              if (withDescriptions)
                stringBuilder.Append(string.Format(":{1} (\"{0}\"))", (object) relationTypeName, (object) this.F_RELATION_TYPE));
              else
                stringBuilder.Append($":{this.F_RELATION_TYPE})");
            }
            else
              stringBuilder.Append(")");
          }
        }
        if (this.F_PART_ID != 0L)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_706"), (object) (this.SignSensitive ? this.F_PART_ID : Math.Abs(this.F_PART_ID))));
          if (this.F_OBJECT_TYPE != -1)
          {
            string objectTypeName = MetaDataHelper.GetObjectTypeName(this.F_OBJECT_TYPE);
            if (!string.IsNullOrEmpty(objectTypeName))
            {
              if (withDescriptions)
                stringBuilder.Append(string.Format(":{1} (\"{0}\"))", (object) objectTypeName, (object) this.F_OBJECT_TYPE));
              else
                stringBuilder.Append($":{this.F_OBJECT_TYPE})");
            }
            else
              stringBuilder.Append(")");
          }
        }
        return stringBuilder.ToString();
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this._signSensitive = true;
        this._F_PRJLINK_ID = 0L;
        this._F_RELATION_TYPE = -1;
        this._F_PART_ID = 0L;
        this._F_OBJECT_TYPE = -1;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is SimpleRelationPair simpleRelationPair))
          return;
        this._signSensitive = simpleRelationPair.SignSensitive;
        this._F_PRJLINK_ID = simpleRelationPair.F_PRJLINK_ID;
        this._F_RELATION_TYPE = simpleRelationPair.F_RELATION_TYPE;
        this._F_PART_ID = simpleRelationPair._F_PART_ID;
        this._F_OBJECT_TYPE = simpleRelationPair.F_OBJECT_TYPE;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new SimpleRelationPair((object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as SimpleRelationPair);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(SimpleRelationPair other)
      {
        if (other == null)
          return 1;
        if (this == other)
          return 0;
        if (this.F_PRJLINK_ID != 0L && other.F_PRJLINK_ID != 0L)
          return !this.SignSensitive ? Math.Abs(this.F_PRJLINK_ID).CompareTo(Math.Abs(other.F_PRJLINK_ID)) : this.F_PRJLINK_ID.CompareTo(other.F_PRJLINK_ID);
        int num = Math.Abs(this.F_PART_ID).CompareTo(Math.Abs(other.F_PART_ID));
        if (num == 0)
          num = this.F_OBJECT_TYPE.CompareTo(other.F_OBJECT_TYPE);
        return num;
      }
    }
}
