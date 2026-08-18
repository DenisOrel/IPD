
// Type: Intermech.Interfaces.Attributes.AttributeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Intermech.Interfaces.Attributes
{
    /// <summary>Базовый класс, представляющий информацию об атрибуте</summary>
    [Serializable]
    public class AttributeInfo : ICloneable, INodeColumnSource, ISerializable
    {
      public const int VirtualAttributeFirstID = -50000;
      private Guid _attributeGuid = Guid.Empty;
      private int _attributeId = -1;
      private string _attributeName = "";
      protected FieldTypes? _type;
      private int nodeColumnWidth;

      protected AttributeInfo(SerializationInfo info, StreamingContext context)
      {
        this.SetObjectData(info, context);
      }

      /// <summary>Глобальный идентификатор атрибута</summary>
      public Guid AttributeGuid
      {
        get
        {
          if (this._attributeGuid == Guid.Empty && this._attributeId != -1)
            this._attributeGuid = MetaDataHelper.GetAttributeTypeGuid(this._attributeId);
          return this._attributeGuid;
        }
        set => this._attributeGuid = value;
      }

      /// <summary>Идентификатор атрибута</summary>
      public int AttributeId
      {
        get
        {
          if (this._attributeId == -1 && this._attributeGuid != Guid.Empty)
            this._attributeId = MetaDataHelper.GetAttributeTypeID(this._attributeGuid);
          return this._attributeId;
        }
        set => this._attributeId = value;
      }

      /// <summary>Наименование атрибута</summary>
      public string Name
      {
        get
        {
          if (string.IsNullOrWhiteSpace(this._attributeName))
          {
            if (this.AttributeGuid != Guid.Empty)
              this._attributeName = MetaDataHelper.GetAttributeTypeName(this.AttributeGuid);
            else if (this.AttributeId != -1)
              this._attributeName = MetaDataHelper.GetAttributeTypeName(this.AttributeId);
          }
          return this._attributeName;
        }
        set => this._attributeName = value;
      }

      /// <summary>Источник данных</summary>
      public virtual FieldSource AttrSrc { get; set; }

      /// <summary>Атрибут связи</summary>
      public bool IsRelationAttribute => this.AttrSrc == FieldSource.Relation;

      /// <summary>Атрибут объекта</summary>
      public bool IsObjectAttribute => this.AttrSrc == FieldSource.Object;

      /// <summary>Атрибут является виртуальным и вычисляется программно</summary>
      public bool IsVirtualAttribute => this.AttributeId <= -50000;

      /// <summary>Поле записи в документе</summary>
      public bool IsDocField
      {
        [DebuggerStepThrough] get => this.AttrSrc == FieldSource.DocumentRowField;
      }

      /// <summary>Тип атрибута</summary>
      public FieldTypes FieldType
      {
        get
        {
          if (!this._type.HasValue)
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeId);
            if (attributeType == null)
              return FieldTypes.ftString;
            this._type = new FieldTypes?(attributeType.FieldType);
          }
          return this._type.Value;
        }
      }

      int INodeColumnSource.ColumnWidth
      {
        get => this.nodeColumnWidth;
        set => this.nodeColumnWidth = value;
      }

      /// <summary>Конструктор</summary>
      public AttributeInfo()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="isRelationAttribute">Атрибут связи (если true) или атрибут объекта (если false)</param>
      /// <param name="attributeId">Идентификатор атрибута</param>
      public AttributeInfo(bool isRelationAttribute, int attributeId)
        : this()
      {
        this.AttrSrc = isRelationAttribute ? FieldSource.Relation : FieldSource.Object;
        this.AttributeId = attributeId;
      }

      /// <summary>Конструктор</summary>
      /// <param name="attrSrc">Источник данных поля записи AVS</param>
      /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
      /// <param name="attributeId">Идентификатор атрибута</param>
      /// <param name="attributeName">Имя атрибута</param>
      public AttributeInfo(
        FieldSource attrSrc,
        Guid attributeGuid,
        int attributeId,
        string attributeName)
      {
        this.AttrSrc = attrSrc;
        this.AttributeGuid = attributeGuid;
        this.AttributeId = attributeId;
        this.Name = attributeName;
      }

      /// <summary>Конструктор</summary>
      /// <param name="attrSrc">Источник данных поля записи AVS</param>
      /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
      /// <param name="attributeId">Идентификатор атрибута</param>
      /// <param name="attributeName">Имя атрибута</param>
      /// <param name="fieldType">Тип данных атрибута</param>
      public AttributeInfo(
        FieldSource attrSrc,
        Guid attributeGuid,
        int attributeId,
        string attributeName,
        FieldTypes? fieldType = null)
        : this(attrSrc, attributeGuid, attributeId, attributeName)
      {
        this._type = fieldType;
      }

      public void UpdateName()
      {
        if (this.AttributeId == -1 && this.AttributeGuid != Guid.Empty)
          this.AttributeId = MetaDataHelper.GetAttributeTypeID(this.AttributeGuid);
        if (this.AttributeId == -1)
          return;
        this.Name = MetaDataHelper.GetAttributeTypeName(this.AttributeId);
      }

      public override string ToString()
      {
        string name = this.Name;
        return name.IsEmpty() ? $"[{this.AttributeId}] ({this.AttrSrc})" : $"{name} [{this.AttributeId}] ({this.AttrSrc})";
      }

      /// <summary>Сравнить данные об атрибуте</summary>
      /// <param name="attrInfo">Данные о втором атрибуте</param>
      /// <returns>true, если данные об одном и том же атрибуте</returns>
      public virtual bool Equals(AttributeInfo attrInfo)
      {
        if (attrInfo == null || this.AttrSrc != attrInfo.AttrSrc)
          return false;
        if (this.IsDocField)
          return this.Name == attrInfo.Name;
        if (this.AttributeGuid != Guid.Empty && attrInfo.AttributeGuid != Guid.Empty)
          return this.AttributeGuid == attrInfo.AttributeGuid;
        if (this.AttributeId != -1 && attrInfo.AttributeId != -1)
          return this.AttributeId == attrInfo.AttributeId;
        if (this.AttributeId == -1 && attrInfo.AttributeId == -1 && this.AttributeGuid == Guid.Empty && attrInfo.AttributeGuid == Guid.Empty && !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(attrInfo.Name))
          return this.Name == attrInfo.Name;
        return this.AttributeId == attrInfo.AttributeId && this.AttributeGuid == attrInfo.AttributeGuid && this.Name == attrInfo.Name;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is AttributeInfo attributeInfo) || attributeInfo.AttrSrc != this.AttrSrc)
          return false;
        if (attributeInfo.IsDocField)
          return this.Name == attributeInfo.Name;
        return this.AttributeGuid != Guid.Empty && attributeInfo.AttributeGuid != Guid.Empty ? this.AttributeGuid == attributeInfo.AttributeGuid : this.AttributeId == attributeInfo.AttributeId;
      }

      public override int GetHashCode()
      {
        int num = 1729291762 * -1521134295 + this.AttrSrc.GetHashCode();
        if (this.IsDocField)
          return num * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Name);
        return this.AttributeGuid != Guid.Empty ? num * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.AttributeGuid) : num * -1521134295 + this.AttributeId.GetHashCode();
      }

      /// <summary>Создать копию экземпляра класса</summary>
      public virtual AttributeInfo Clone()
      {
        return new AttributeInfo(this.AttrSrc, this.AttributeGuid, this.AttributeId, this.Name);
      }

      /// <summary>Создать копию экземпляра класса</summary>
      object ICloneable.Clone() => (object) this.Clone();

      public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("AttributeGuid", (object) this.AttributeGuid);
        info.AddValue("Name", (object) this.Name);
        info.AddValue("AttributeId", this.AttributeId);
        info.AddValue("AttrSrc", (int) this.AttrSrc);
        info.AddValue("nodeColumnWidth", this.nodeColumnWidth);
      }

      public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
      {
        this.AttributeGuid = (Guid) info.GetValue("AttributeGuid", typeof (Guid));
        this.Name = info.GetString("Name");
        this.AttributeId = info.GetInt32("AttributeId");
        this.AttrSrc = (FieldSource) info.GetInt32("AttrSrc");
        if (!this.HasValue(info, "nodeColumnWidth"))
          return;
        this.nodeColumnWidth = info.GetInt32("nodeColumnWidth");
      }

      protected bool HasValue(SerializationInfo info, string name)
      {
        foreach (SerializationEntry serializationEntry in info)
        {
          if (serializationEntry.Name == name)
            return true;
        }
        return false;
      }
    }
}
