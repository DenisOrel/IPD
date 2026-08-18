
// Type: Intermech.Search.Relation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Search
{
    /// <summary>Связь</summary>
    [Serializable]
    public sealed class Relation : AttributeHolderBase
    {
      private long? _prjLinkId;
      private long? _explicitPartVersionId;

      public Relation() => this.PartVersionID = 0L;

      /// <summary>Конструктор</summary>
      /// <param name="attributes">Коллекция атрибутов</param>
      public Relation(IAttributeCollection attributes)
        : base(attributes)
      {
        this.PartVersionID = 0L;
      }

      /// <summary>Идентификатор связи</summary>
      public long ID
      {
        get
        {
          return !this._prjLinkId.HasValue ? (this._prjLinkId = new long?(this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_PRJLINK_ID, 0L))).Value : this._prjLinkId.Value;
        }
        set
        {
          this._prjLinkId = new long?(value);
          this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_PRJLINK_ID, value);
        }
      }

      /// <summary>Глобальный идентификатор связи</summary>
      public Guid Guid
      {
        get => this.GetAttributeValue<Guid>(ObligatoryObjectAttributes.F_PRJ_GUID, Guid.Empty);
      }

      /// <summary>Идентификатор версии родительского объекта</summary>
      public long ProjectVersionID
      {
        get => this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_PROJ_ID, 0L);
        set => this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_PROJ_ID, value);
      }

      /// <summary>Идентификатор версии дочернего объекта</summary>
      public long PartID
      {
        get => this.GetAttributeValue<long>(ObligatoryObjectAttributes.F_PART_ID, 0L);
        set => this.SetAttributeValue<long>(ObligatoryObjectAttributes.F_PART_ID, value);
      }

      /// <summary>Статусы</summary>
      public byte[] Statuses
      {
        get
        {
          return this.GetAttributeValue<byte[]>(ObligatoryObjectAttributes.F_ELEMENT_STATUSES, (byte[]) null);
        }
      }

      /// <summary>Значение атрибута "Идентификатор версии в составе"</summary>
      public long ExplicitPartVersionID
      {
        get
        {
          return !this._explicitPartVersionId.HasValue ? (this._explicitPartVersionId = new long?(this.GetAttributeValue<long>(Constants.ExplicitPartVersionIDAttributeTypeID, 0L))).Value : this._explicitPartVersionId.Value;
        }
      }

      /// <summary>Идентификатор типа связи</summary>
      public int TypeID
      {
        get => this.GetAttributeValue<int>(ObligatoryObjectAttributes.F_RELATION_TYPE, -1);
        set => this.SetAttributeValue<int>(ObligatoryObjectAttributes.F_RELATION_TYPE, value);
      }

      public long PartVersionID { get; set; }
    }
}
