// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public abstract class DBAttribute : 
  DBSessionable,
  IDBAttribute,
  IDBSessionable,
  IDBGuid,
  IDBLanguage,
  IDBSubjectArea,
  IDeletable
{
  private IDBAttributeType _AttributeType;
  internal IDBAttributeCollection _Attributes;
  protected DBAttributable _ParentObject;
  protected long _DBObjectID;
  protected long _DB_ID;
  protected long _DBRelationID;
  internal bool _TemporaryAttribute;
  protected int _Index;
  protected int _ValuesCount;
  public bool ValidatingOn = true;
  internal ColumnContents _ValueContentMode = ColumnContents.Value;
  internal int _TypeID = -1;
  internal static List<int> _DontUpdateContentDateInBlanks = new List<int>();

  public DBAttribute(UserSession uSession)
    : base(uSession)
  {
  }

  public static void RegisterAttribute4DisableUpdateContentDate(int attrID)
  {
    DBAttribute._DontUpdateContentDateInBlanks.Add(attrID);
  }

  public abstract bool IsObjectAttribute { get; }

  public abstract int TypeID { get; }

  public virtual long DBObjectID => this.IsObjectAttribute ? this._DBObjectID : this._DBRelationID;

  public abstract string GroupName { get; }

  public abstract bool Visible { get; }

  public abstract bool IsSystem { get; }

  public abstract DataTable GetPossibleValues();

  public abstract bool VisibleByFilters { get; }

  public abstract bool VisibleByAccess { get; }

  public virtual long DB_ID
  {
    get
    {
      if (!this.IsObjectAttribute)
        throw new KernelException($"Атрибут '{this.Name}' не является атрибутом объекта");
      if (this._DB_ID == 0L)
        this._DB_ID = (this.ParentObject as IDBObject).ID;
      return this._DB_ID;
    }
  }

  public IDBAttributeType AttributeType
  {
    get
    {
      if (this._AttributeType == null)
      {
        this._AttributeType = !this.IsObjectAttribute ? (IDBAttributeType) this.UserSession.GetRelationType(this.TypeID).Attributes.GetAttributeByID(this.AttributeID, false) : (IDBAttributeType) this.UserSession.GetObjectType(this.TypeID).Attributes.GetAttributeByID(this.AttributeID, false);
        if (this._AttributeType == null)
          this._AttributeType = this.UserSession.GetAttributeType(this.AttributeID);
      }
      return this._AttributeType;
    }
  }

  public string Name => this.AttributeType.Name;

  public virtual FieldTypes DataType
  {
    get
    {
      return this._AttributeType != null ? this._AttributeType.AttributeType : this.UserSession.GetAttributeType(this.AttributeID).AttributeType;
    }
  }

  public abstract int AttributeID { get; }

  public Guid GUID => (this.AttributeType as IDBGuid).GUID;

  public virtual bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public bool TemporaryAttribute => this._TemporaryAttribute;

  public abstract void Assign(IDBAttribute sourceAttribute);

  public abstract int Delete(long DeleteMode);

  public IDBAttributeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = (this.ParentObject as IDBAttributable).Attributes;
      return this._Attributes;
    }
    set
    {
      if (this._Attributes != null)
        return;
      this._Attributes = value;
    }
  }

  public virtual int ValuesCount => this._ValuesCount;

  public virtual int Index
  {
    get => this._Index;
    set
    {
      this._Index = value < this._ValuesCount && value >= 0 ? value : throw new IndexOutOfRangeException();
    }
  }

  public abstract int DeleteValue();

  public abstract int AddValue(object newValue);

  internal virtual void InsertIntoView(int sign, bool writeNulls = false)
  {
  }

  internal abstract void Purge(bool purgeOwner);

  public abstract bool ReadOnly { get; }

  public DBAttributable ParentObject
  {
    get
    {
      if (this._ParentObject == null)
        this._ParentObject = !this.IsObjectAttribute ? this.UserSession.GetRelation(this._DBRelationID) as DBAttributable : this.UserSession.GetObject(this._DBObjectID) as DBAttributable;
      return this._ParentObject;
    }
  }

  public abstract string AsString { get; set; }

  public abstract long AsInteger { get; set; }

  public abstract double AsDouble { get; set; }

  public abstract DateTime AsDateTime { get; set; }

  public abstract bool AsBoolean { get; set; }

  public virtual object Value
  {
    get => (object) null;
    set => throw new OperationNotApplicableException();
  }

  public abstract object[] Values { get; set; }

  public abstract bool IsNull { get; }

  public abstract string[] Descriptions { get; }

  public abstract string Description { get; }

  public abstract void Clear();

  public abstract void ClearValues();

  internal abstract void DirectSetValue(string fieldName, object newValue);

  public abstract void DirectSetValues(object[] values);

  internal virtual void ChangeComputedValues(bool postedWrite)
  {
  }

  internal virtual object GetCalculatedValue(DBAttribute changedAttribute) => (object) null;

  internal virtual void SetCalculatedValue(object newValue, bool postedWrite)
  {
  }

  internal virtual void Compute(bool SilentMode)
  {
  }

  internal virtual void CheckNotNullValue(object newValue)
  {
  }

  public virtual void CheckUniqueValue(object[] newValues, bool excludeThis)
  {
  }

  internal virtual void ValidateRule(int attributeID, object newValue)
  {
  }

  public void SetValidatingMode(int mode)
  {
    if (!this.UserSession.IsSystemSession)
      throw new KernelExceptionID(sc_12385.ssp_appserver_12386(1830197707));
    if (mode == 1)
      this.ValidatingOn = false;
    else
      this.ValidatingOn = true;
  }

  public virtual void DoAfterCreate()
  {
  }

  public abstract string LanguageID { get; set; }

  public abstract string LanguageName { get; }

  public abstract bool IsDefaultLanguage { get; }

  public abstract string SubjectAreas { get; set; }

  public abstract string SubjectAreasCaption { get; }
}
