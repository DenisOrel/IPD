// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSystemAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Kernel;

internal abstract class DBSystemAttribute : 
  DBAttribute,
  IDBAttribute,
  IDBSessionable,
  IDBGuid,
  IDBLanguage,
  IDBSubjectArea,
  IDeletable
{
  private ObligatoryObjectAttributes _ObligatoryAttribute;

  public DBSystemAttribute(
    UserSession uSession,
    ObligatoryObjectAttributes attribute,
    IDBAttributeCollection attrs)
    : base(uSession)
  {
    this._ObligatoryAttribute = attribute;
    this._Attributes = attrs;
  }

  public ObligatoryObjectAttributes ObligatoryAttribute => this._ObligatoryAttribute;

  public override int AttributeID => (int) this.ObligatoryAttribute;

  public override bool IsNull => this.Value == null || this.Value == DBNull.Value;

  public override void Clear()
  {
    throw new KernelException(string.Format(sc_12583.ssp_appserver_12584(), (object) this.Name));
  }

  internal override void DirectSetValue(string fieldName, object newValue)
  {
    throw new OperationNotApplicableException();
  }

  public override void DirectSetValues(object[] values)
  {
    throw new OperationNotApplicableException();
  }

  public override string AsString
  {
    get => this.Value.ToString();
    set => this.Value = (object) value;
  }

  public override long AsInteger
  {
    get => Convert.ToInt64(this.Value);
    set => this.Value = (object) value;
  }

  public override double AsDouble
  {
    get => Convert.ToDouble(this.Value);
    set => this.Value = (object) value;
  }

  public override DateTime AsDateTime
  {
    get => Convert.ToDateTime(this.Value);
    set => this.Value = (object) value;
  }

  public override bool AsBoolean
  {
    get => Convert.ToBoolean(this.Value);
    set => this.Value = (object) value;
  }

  public override int ValuesCount => 1;

  public override int Index
  {
    get => 0;
    set
    {
      if (value != 1)
        throw new OperationNotApplicableException();
    }
  }

  public override int AddValue(object newValue) => throw new OperationNotApplicableException();

  public override int DeleteValue() => throw new OperationNotApplicableException();

  internal override void Purge(bool purgeOwner) => throw new OperationNotApplicableException();

  public override FieldTypes DataType => FieldTypes.ftSystem;

  public override bool IsSystem => true;

  public override int Delete(long DeleteMode) => throw new OperationNotApplicableException();

  public override void Assign(IDBAttribute sourceAttribute)
  {
    if (sourceAttribute.DataType == FieldTypes.ftSystem && sourceAttribute.AttributeID != this.AttributeID)
      throw new KernelException(string.Format(sc_12583.ssp_appserver_12585(), (object) this.Name, (object) sourceAttribute.Name));
    this.AttributeType.ValidateAssign(sourceAttribute.AttributeType);
    this.Value = sourceAttribute.Value;
  }

  public override object[] Values
  {
    get => new object[1]{ this.Value };
    set
    {
      if (value.Length == 0)
        return;
      this.Value = value[0];
    }
  }

  public override string[] Descriptions
  {
    get => new string[1]{ this.Description };
  }

  public override DataTable GetPossibleValues() => this.AttributeType.GetPossibleValues();

  public override void ClearValues() => this.Clear();

  public override string GroupName => Consts.SystemAttributesGroupName;

  public override bool Visible => true;

  public override bool IsSystemGUID => true;

  public override string LanguageID
  {
    get => throw new NotImplementedException();
    set => throw new OperationNotApplicableException();
  }

  public override string LanguageName => string.Empty;

  public override bool IsDefaultLanguage => true;

  public override string SubjectAreas
  {
    get => string.Empty;
    set => throw new OperationNotApplicableException();
  }

  public override string SubjectAreasCaption => string.Empty;

  public override bool VisibleByFilters => true;

  public override bool VisibleByAccess => true;
}
