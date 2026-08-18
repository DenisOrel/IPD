
// Type: Intermech.Interfaces.Compositions.RelationPair
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
    /// Класс-ключ для хранения значений [HANDLE x TOP_OBJECT_ID x TOP_OBJECT_TYPE] x [USER_ID x F_PROJ_ID x F_RELATION_TYPE] либо
    /// значений [HANDLE x TOP_OBJECT_ID x TOP_OBJECT_TYPE] x [F_PRJLINK_ID x F_OBJECT_TYPE].
    /// При сравнении, хэшировании учитывается либо [HANDLE x TOP_OBJECT_ID x TOP_OBJECT_TYPE] x [F_PRJLINK_ID], либо значения
    /// [HANDLE x TOP_OBJECT_ID x TOP_OBJECT_TYPE] x [USER_ID x F_PROJ_ID x F_RELATION_TYPE].
    /// Приоритетным считается значение [HANDLE x TOP_OBJECT_ID x TOP_OBJECT_TYPE] x [F_PRJLINK_ID].
    /// </summary>
    [Serializable]
    public sealed class RelationPair : IAssignable, ICloneable, IComparable, IComparable<RelationPair>
    {
      /// <summary>
      /// Учитывать знак у идентификаторов версий объектов/связей при сравнениях
      /// </summary>
      private bool _signSensitive = true;
      /// <summary>
      /// Некое уникальное число, которое генерируется для текущего сеанса работы
      /// клиента IPS с сервером приложений. Используется для того, чтобы можно было
      /// работать с кэшами конфигуратора составов из-под одной и той же учётной записи
      /// одновременно, без конфликтов
      /// </summary>
      private long _Handle;
      /// <summary>
      /// Идентификатор версии объекта, который расположен на самом верхнем уровне состава
      /// </summary>
      private long _TOP_OBJECT_ID;
      /// <summary>
      /// Идентификатор типа объекта, который расположен на самом верхнем уровне состава
      /// </summary>
      private int _TOP_OBJECT_TYPE = -1;
      /// <summary>Идентификатор связи</summary>
      private long _F_PRJLINK_ID;
      /// <summary>Идентификатор пользователя</summary>
      private long _USER_ID;
      /// <summary>Идентификатор версии родительского объекта</summary>
      private long _F_PROJ_ID;
      /// <summary>Идентификатор типа связи</summary>
      private int _F_RELATION_TYPE = -1;
      /// <summary>Идентификатор типа объекта</summary>
      private int _F_OBJECT_TYPE = -1;

      /// <summary>
      /// Учитывать знак у идентификаторов версий объектов/связей при сравнениях
      /// </summary>
      public bool SignSensitive
      {
        [DebuggerStepThrough] get => this._signSensitive;
        set => this._signSensitive = value;
      }

      /// <summary>
      /// Некое уникальное число, которое генерируется для текущего сеанса работы
      /// клиента IPS с сервером приложений. Используется для того, чтобы можно было
      /// работать с кэшами конфигуратора составов из-под одной и той же учётной записи
      /// одновременно, без конфликтов
      /// </summary>
      public long Handle
      {
        [DebuggerStepThrough] get => this._Handle;
      }

      /// <summary>
      /// Идентификатор версии объекта, который расположен на самом верхнем уровне состава
      /// </summary>
      public long TOP_OBJECT_ID
      {
        [DebuggerStepThrough] get => this._TOP_OBJECT_ID;
      }

      /// <summary>
      /// Идентификатор типа объекта, который расположен на самом верхнем уровне состава
      /// </summary>
      public int TOP_OBJECT_TYPE
      {
        [DebuggerStepThrough] get => this._TOP_OBJECT_TYPE;
      }

      /// <summary>Идентификатор связи</summary>
      public long F_PRJLINK_ID
      {
        [DebuggerStepThrough] get => this._F_PRJLINK_ID;
      }

      /// <summary>Идентификатор пользователя</summary>
      public long USER_ID
      {
        [DebuggerStepThrough] get => this._USER_ID;
      }

      /// <summary>Идентификатор версии родительского объекта</summary>
      public long F_PROJ_ID
      {
        [DebuggerStepThrough] get => this._F_PROJ_ID;
      }

      /// <summary>Идентификатор типа связи</summary>
      public int F_RELATION_TYPE
      {
        [DebuggerStepThrough] get => this._F_RELATION_TYPE;
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
          return this.F_PRJLINK_ID == 0L && this.TOP_OBJECT_ID == 0L && this.TOP_OBJECT_TYPE == -1 && this.USER_ID == 0L && this.F_PROJ_ID == 0L && this.F_RELATION_TYPE == -1 && this.F_OBJECT_TYPE == -1;
        }
      }

      /// <summary>Создать пустой экземпляр класса</summary>
      public RelationPair()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public RelationPair(object source) => this.Assign(source);

      /// <summary>Создать частично заполненный экземпляр класса</summary>
      /// <param name="handle">Уникальное число текущего сеанса связи клиента IPS с сервером приложений</param>
      /// <param name="topObjectID">Идентификатор версии корневого объекта состава</param>
      /// <param name="topObjectType">Идентификатор типа корневого объекта состава</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      public RelationPair(long handle, long topObjectID, int topObjectType, long prjLinkID)
        : this(handle, topObjectID, topObjectType, prjLinkID, 0L, 0L, -1, -1, true)
      {
      }

      /// <summary>Создать частично заполненный экземпляр класса</summary>
      /// <param name="handle">Уникальное число текущего сеанса связи клиента IPS с сервером приложений</param>
      /// <param name="topObjectID">Идентификатор версии корневого объекта состава</param>
      /// <param name="topObjectType">Идентификатор типа объекта, который расположен на самом верхнем уровне состава</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="userID">Идентификатор пользователя</param>
      public RelationPair(
        long handle,
        long topObjectID,
        int topObjectType,
        long prjLinkID,
        long userID)
        : this(handle, topObjectID, topObjectType, prjLinkID, userID, 0L, -1, -1)
      {
      }

      /// <summary>Создать частично заполненный экземпляр класса</summary>
      /// <param name="handle">Уникальное число текущего сеанса связи клиента IPS с сервером приложений</param>
      /// <param name="topObjectID">Идентификатор версии корневого объекта состава</param>
      /// <param name="topObjectType">Идентификатор типа объекта, который расположен на самом верхнем уровне состава</param>
      /// <param name="projID">Идентификатор версии родительского объекта</param>
      /// <param name="relType">Идентификатор типа связи</param>
      public RelationPair(long handle, long topObjectID, int topObjectType, long projID, int relType)
        : this(handle, topObjectID, topObjectType, projID, (long) relType, -1L, -1, -1, true)
      {
      }

      /// <summary>Создать частично заполненный экземпляр класса</summary>
      /// <param name="handle">Уникальное число текущего сеанса связи клиента IPS с сервером приложений</param>
      /// <param name="topObjectID">Идентификатор версии корневого объекта состава</param>
      /// <param name="topObjectType">Идентификатор типа объекта, который расположен на самом верхнем уровне состава</param>
      /// <param name="projID">Идентификатор версии родительского объекта</param>
      /// <param name="relType">Идентификатор типа связи</param>
      /// <param name="objType">Идентификатор типа объекта</param>
      public RelationPair(
        long handle,
        long topObjectID,
        int topObjectType,
        long projID,
        int relType,
        int objType)
        : this(handle, topObjectID, topObjectType, 0L, 0L, projID, relType, objType, true)
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="handle">Уникальное число текущего сеанса связи клиента IPS с сервером приложений</param>
      /// <param name="topObjectID">Идентификатор версии корневого объекта состава</param>
      /// <param name="topObjectType">Идентификатор типа объекта, который расположен на самом верхнем уровне состава</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="userID">Идентификатор пользователя</param>
      /// <param name="projID">Идентификатор версии родительского объекта</param>
      /// <param name="relType">Идентификатор типа связи</param>
      /// <param name="objType">Идентификатор типа объекта</param>
      public RelationPair(
        long handle,
        long topObjectID,
        int topObjectType,
        long prjLinkID,
        long userID,
        long projID,
        int relType,
        int objType)
        : this(handle, topObjectID, topObjectType, prjLinkID, userID, projID, relType, objType, true)
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="handle">Уникальное число текущего сеанса связи клиента IPS с сервером приложений</param>
      /// <param name="topObjectID">Идентификатор версии корневого объекта состава</param>
      /// <param name="topObjectType">Идентификатор типа объекта, который расположен на самом верхнем уровне состава</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="userID">Идентификатор пользователя</param>
      /// <param name="projID">Идентификатор версии родительского объекта</param>
      /// <param name="relType">Идентификатор типа связи</param>
      /// <param name="objType">Идентификатор типа объекта</param>
      /// <param name="signSensitive">Учитывать знак у идентификаторов версий объектов/связей при сравнениях</param>
      public RelationPair(
        long handle,
        long topObjectID,
        int topObjectType,
        long prjLinkID,
        long userID,
        long projID,
        int relType,
        int objType,
        bool signSensitive)
      {
        this._signSensitive = signSensitive;
        this._Handle = handle;
        this._TOP_OBJECT_ID = topObjectID;
        this._TOP_OBJECT_TYPE = topObjectType;
        this._F_PRJLINK_ID = prjLinkID;
        this._USER_ID = Math.Abs(userID);
        this._F_PROJ_ID = projID;
        this._F_RELATION_TYPE = relType;
        this._F_OBJECT_TYPE = objType;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        if (!(obj is RelationPair relationPair))
          return false;
        return this.F_PRJLINK_ID != 0L || relationPair.F_PRJLINK_ID != 0L ? (!this.SignSensitive ? this.Handle == relationPair.Handle && Math.Abs(this.TOP_OBJECT_ID) == Math.Abs(relationPair.TOP_OBJECT_ID) && this.TOP_OBJECT_TYPE == relationPair.TOP_OBJECT_TYPE && Math.Abs(this.F_PRJLINK_ID) == Math.Abs(relationPair.F_PRJLINK_ID) : this.Handle == relationPair.Handle && this.TOP_OBJECT_ID == relationPair.TOP_OBJECT_ID && this.TOP_OBJECT_TYPE == relationPair.TOP_OBJECT_TYPE && this.F_PRJLINK_ID == relationPair.F_PRJLINK_ID) : (!this.SignSensitive ? this.Handle == relationPair.Handle && Math.Abs(this.TOP_OBJECT_ID) == Math.Abs(relationPair.TOP_OBJECT_ID) && this.TOP_OBJECT_TYPE == relationPair.TOP_OBJECT_TYPE && this.USER_ID == relationPair.USER_ID && Math.Abs(this.F_PROJ_ID) == Math.Abs(relationPair.F_PROJ_ID) && this.F_RELATION_TYPE == relationPair.F_RELATION_TYPE && this.F_OBJECT_TYPE == relationPair.F_OBJECT_TYPE : this.Handle == relationPair.Handle && this.TOP_OBJECT_ID == relationPair.TOP_OBJECT_ID && this.TOP_OBJECT_TYPE == relationPair.TOP_OBJECT_TYPE && this.USER_ID == relationPair.USER_ID && this.F_PROJ_ID == relationPair.F_PROJ_ID && this.F_RELATION_TYPE == relationPair.F_RELATION_TYPE && this.F_OBJECT_TYPE == relationPair.F_OBJECT_TYPE);
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        if (this.F_PRJLINK_ID != 0L)
          return !this.SignSensitive ? this.Handle.GetHashCode() << 24 ^ Math.Abs(this.F_PRJLINK_ID).GetHashCode() : this.Handle.GetHashCode() << 24 ^ this.F_PRJLINK_ID.GetHashCode();
        if (!this.SignSensitive)
        {
          int num1 = this.Handle.GetHashCode() << 28;
          long num2 = this.USER_ID;
          int num3 = num2.GetHashCode() << 16 /*0x10*/;
          int num4 = num1 ^ num3;
          num2 = Math.Abs(this.F_PROJ_ID);
          int num5 = num2.GetHashCode() << 8;
          int num6 = num4 ^ num5;
          int num7 = this.F_RELATION_TYPE;
          int num8 = num7.GetHashCode() << 4;
          int num9 = num6 ^ num8;
          num7 = this.F_OBJECT_TYPE;
          int hashCode = num7.GetHashCode();
          return num9 ^ hashCode;
        }
        int num10 = this.Handle.GetHashCode() << 28;
        long num11 = this.USER_ID;
        int num12 = num11.GetHashCode() << 16 /*0x10*/;
        int num13 = num10 ^ num12;
        num11 = this.F_PROJ_ID;
        int num14 = num11.GetHashCode() << 8;
        return num13 ^ num14 ^ this.F_RELATION_TYPE.GetHashCode() << 4 ^ this.F_OBJECT_TYPE.GetHashCode();
      }

      /// <summary>Описание экземпляра класса в виде строки</summary>
      /// <returns>Описание экземпляра класса в виде строки</returns>
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.F_PRJLINK_ID != 0L)
        {
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_705"), (object) (this.SignSensitive ? this.F_PRJLINK_ID : Math.Abs(this.F_PRJLINK_ID))));
          if (this.F_RELATION_TYPE != -1)
          {
            string relationTypeName = MetaDataHelper.GetRelationTypeName(this.F_RELATION_TYPE);
            if (!string.IsNullOrEmpty(relationTypeName))
              stringBuilder.Append(string.Format(":{1} (\"{0}\"))", (object) relationTypeName, (object) this.F_RELATION_TYPE));
            else
              stringBuilder.Append(")");
          }
        }
        if (this.F_PROJ_ID != 0L)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_706"), (object) (this.SignSensitive ? this.F_PROJ_ID : Math.Abs(this.F_PROJ_ID))));
          if (this.F_OBJECT_TYPE != -1)
          {
            string objectTypeName = MetaDataHelper.GetObjectTypeName(this.F_OBJECT_TYPE);
            if (!string.IsNullOrEmpty(objectTypeName))
              stringBuilder.Append(string.Format(":{1} (\"{0}\"))", (object) objectTypeName, (object) this.F_OBJECT_TYPE));
            else
              stringBuilder.Append(")");
          }
        }
        stringBuilder.Insert(0, $"[{this.Handle}x{this.USER_ID}@{(this.SignSensitive ? this.TOP_OBJECT_ID : Math.Abs(this.TOP_OBJECT_ID))}:{this.TOP_OBJECT_TYPE}] ");
        return stringBuilder.ToString();
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this._signSensitive = true;
        this._Handle = 0L;
        this._TOP_OBJECT_ID = 0L;
        this._TOP_OBJECT_TYPE = -1;
        this._F_PRJLINK_ID = 0L;
        this._USER_ID = 0L;
        this._F_PROJ_ID = 0L;
        this._F_RELATION_TYPE = -1;
        this._F_OBJECT_TYPE = -1;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        this.Clear();
        if (!(source is RelationPair relationPair))
          return;
        this._signSensitive = relationPair.SignSensitive;
        this._Handle = relationPair.Handle;
        this._TOP_OBJECT_ID = relationPair.TOP_OBJECT_ID;
        this._TOP_OBJECT_TYPE = relationPair.TOP_OBJECT_TYPE;
        this._F_PRJLINK_ID = relationPair.F_PRJLINK_ID;
        this._USER_ID = relationPair.USER_ID;
        this._F_PROJ_ID = relationPair.F_PROJ_ID;
        this._F_RELATION_TYPE = relationPair.F_RELATION_TYPE;
        this._F_OBJECT_TYPE = relationPair.F_OBJECT_TYPE;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new RelationPair((object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as RelationPair);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(RelationPair other)
      {
        if (other == null)
          return 1;
        if (this == other)
          return 0;
        int num1 = this._Handle.CompareTo(other._Handle);
        if (num1 != 0)
          return num1;
        if (this.F_PRJLINK_ID != 0L && other.F_PRJLINK_ID != 0L)
          return !this.SignSensitive ? Math.Abs(this.F_PRJLINK_ID).CompareTo(Math.Abs(other.F_PRJLINK_ID)) : this.F_PRJLINK_ID.CompareTo(other.F_PRJLINK_ID);
        int num2 = Math.Abs(this.USER_ID).CompareTo(Math.Abs(other.USER_ID));
        if (num2 == 0)
          num2 = Math.Abs(this.F_PROJ_ID).CompareTo(Math.Abs(other.F_PROJ_ID));
        int num3;
        if (num2 == 0)
        {
          num3 = this.F_RELATION_TYPE;
          num2 = num3.CompareTo(other.F_RELATION_TYPE);
        }
        if (num2 == 0)
        {
          num3 = this.F_OBJECT_TYPE;
          num2 = num3.CompareTo(other.F_OBJECT_TYPE);
        }
        return num2;
      }
    }
}
