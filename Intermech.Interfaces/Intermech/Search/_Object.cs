
// Type: Intermech.Search._Object
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Search
{
    /// <summary>Объект</summary>
    [TypeConverter(typeof (AttributeHolderConverter))]
    [Serializable]
    public class _Object : AttributeHolderBase, ICloneable
    {
      private long? _id;
      private long? _objectId;
      private int? _objectTypeId;
      private int? _levelId;
      /// <summary>Константа "Признак базовой версии"</summary>
      private const long BaseVersionSign = 1;
      /// <summary>Константа "Признак не базовой версии"</summary>
      private const long NotBaseVersionSign = 0;

      public _Object() => this.Composition = new CompositionPartCollection(this);

      /// <summary>Конструктор</summary>
      /// <param name="attributes">Коллекция атрибутов</param>
      public _Object(IAttributeCollection attributes)
        : base(attributes)
      {
        this.Composition = new CompositionPartCollection(this);
      }

      /// <summary>Идентификатор объекта</summary>
      public long ID
      {
        get
        {
          return !this._id.HasValue ? (this._id = new long?(this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_ID, 0L))).Value : this._id.Value;
        }
        set
        {
          this._id = new long?(value);
          this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_ID, value);
        }
      }

      /// <summary>Идентификатор версии объекта</summary>
      public long VersionID
      {
        get
        {
          return !this._objectId.HasValue ? (this._objectId = new long?(this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_OBJECT_ID, 0L))).Value : this._objectId.Value;
        }
        set
        {
          this._objectId = new long?(value);
          this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_OBJECT_ID, value);
        }
      }

      /// <summary>Статусы</summary>
      public byte[] Statuses
      {
        get
        {
          return this.GetAttributeValue<byte[]>(ObligatoryObjectAttributes.F_ELEMENT_STATUSES, (byte[]) null);
        }
        set => this.SetAttributeValue<byte[]>(ObligatoryObjectAttributes.F_ELEMENT_STATUSES, value);
      }

      /// <summary>Признак базовой версии</summary>
      public bool IsBaseVersion
      {
        get => this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_BASE_VERSION, 0L) == 1L;
        set => this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_BASE_VERSION, 1L);
      }

      /// <summary>Идентификатор группы изменений</summary>
      public long ModificationID
      {
        get => this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_MODIFICATION_ID, 0L);
      }

      /// <summary>Идентификатор версии владельца</summary>
      public long OwnerVersionID
      {
        get => this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_OWNER_ID, 0L);
      }

      /// <summary>Уровень продвижения</summary>
      public int LifecycleLevelID
      {
        get
        {
          return !this._levelId.HasValue ? (this._levelId = new int?(this.GetAttributeValue<int>(ObligatoryObjectAttributes.F_LEVEL_ID, 0))).Value : this._levelId.Value;
        }
        set
        {
          this._levelId = new int?(value);
          this.SetAttributeValue<int>(ObligatoryObjectAttributes.F_LEVEL_ID, value);
        }
      }

      /// <summary>Шаг жизненного цикла</summary>
      public int LifecycleStepID
      {
        get => this.GetAttributeValue<int>(ObligatoryObjectAttributes.F_LC_STEP, -1);
        set => this.SetAttributeValue<int>(ObligatoryObjectAttributes.F_LC_STEP, value);
      }

      /// <summary>Идентификатор типа объекта</summary>
      public int TypeID
      {
        get
        {
          return !this._objectTypeId.HasValue ? (this._objectTypeId = new int?(this.GetAttributeValue<int>(ObligatoryObjectAttributes.F_OBJECT_TYPE, -1))).Value : this._objectTypeId.Value;
        }
        set
        {
          this._objectTypeId = new int?(value);
          this.SetAttributeValue<int>(ObligatoryObjectAttributes.F_OBJECT_TYPE, value);
        }
      }

      /// <summary>Номер версии объекта</summary>
      public int VersionNumber
      {
        get => this.GetAttributeValue<int>(ObligatoryObjectAttributes.F_VERSION_ID, 0);
        set => this.SetAttributeValue<int>(ObligatoryObjectAttributes.F_VERSION_ID, value);
      }

      /// <summary>
      /// Идентификатор версии пользователя, взявшего объект на изменение
      /// </summary>
      public long CheckOutByVersionID
      {
        get => this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_CHKOUT_BY, 0L);
        set => this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_CHKOUT_BY, value);
      }

      /// <summary>Заголовок объекта</summary>
      public string Caption
      {
        get => this.GetAttributeValue<string>(ObligatoryObjectAttributes.CAPTION, (string) null);
        set => this.SetAttributeValue<string>(ObligatoryObjectAttributes.CAPTION, value);
      }

      public DateTime ModifyDate
      {
        get
        {
          return this.GetAttributeValue<DateTime>(ObligatoryObjectAttributes.F_MODIFY_DATE, DateTime.MinValue);
        }
        set => this.SetAttributeValue<DateTime>(ObligatoryObjectAttributes.F_MODIFY_DATE, value);
      }

      public DateTime CreateDate
      {
        get
        {
          return this.GetAttributeValue<DateTime>(ObligatoryObjectAttributes.F_OBJ_CREATE, DateTime.MinValue);
        }
        set => this.SetAttributeValue<DateTime>(ObligatoryObjectAttributes.F_OBJ_CREATE, value);
      }

      public CompositionPartCollection Composition { get; private set; }

      public _Object Clone()
      {
        _Object @object = new _Object();
        foreach (_Attribute attribute in (IEnumerable<_Attribute>) this.Attributes)
          @object.Attributes.Add(attribute.Clone());
        return @object;
      }

      object ICloneable.Clone() => (object) this.Clone();
    }
}
