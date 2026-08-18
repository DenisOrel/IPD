// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.SystemVariable
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Workflow
{
    [Serializable]
    public class SystemVariable : Variable, ISystemVariable
    {
      private IDBObject _object;
      private long _objectID;
      protected bool _loaded;

      public SystemVariable(VarList owner, IDBObject obj, int typeID)
        : this(owner, obj, typeID, "")
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="owner"></param>
      /// <param name="obj"></param>
      /// <param name="typeID"></param>
      /// <param name="defValue"></param>
      public SystemVariable(VarList owner, IDBObject obj, int typeID, string defValue)
        : base(owner)
      {
        this.Kind = VarKind.System;
        this._object = (IDBObject) null;
        if (obj != null)
          this._objectID = obj.ObjectID;
        this.AttrTypeID = typeID;
        this._value = defValue;
      }

      protected IDBObject GetObject()
      {
        if (this._objectID != 0L)
          this._object = this._owner.Session.GetObject(this._objectID, false) ?? this._owner.Session.GetObject(-this._objectID);
        return this._object;
      }

      protected void ReleaseObject()
      {
        if (this._objectID == 0L)
          return;
        this._object = (IDBObject) null;
      }

      protected override string GetValue()
      {
        if (!this._loaded)
        {
          IDBObject dbObject = this.GetObject();
          try
          {
            if (dbObject == null)
              return (string) null;
            IDBAttribute attributeById = dbObject.GetAttributeByID(this.AttrTypeID);
            if (attributeById != null)
              this._value = DBNull.Value.Equals(attributeById.Value) || attributeById.Value == null ? "" : attributeById.Value.ToString();
            this._loaded = true;
          }
          finally
          {
            this.ReleaseObject();
          }
        }
        return this._value;
      }

      protected override void AfterSetValue() => this._loaded = true;

      public void Save()
      {
        IDBObject dbObject = this.GetObject();
        try
        {
          if (dbObject == null)
            return;
          IDBAttribute byId = dbObject.Attributes.FindByID(this.AttrTypeID);
          if (byId != null && byId.TemporaryAttribute)
            byId.Delete(0L);
          dbObject.Attributes.AddAttribute(this.AttrTypeID, false, new object[1]
          {
            this.TypedValue
          });
        }
        finally
        {
          this.ReleaseObject();
        }
      }
    }
}
