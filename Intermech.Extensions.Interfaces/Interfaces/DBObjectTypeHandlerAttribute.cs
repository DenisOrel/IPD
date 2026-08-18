// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.DBObjectTypeHandlerAttribute
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
public sealed class DBObjectTypeHandlerAttribute : Attribute
{
  [CanBeEmpty]
  private int _objectTypeID = -1;
  [CanBeNull]
  private string _objectTypeName;

  [NotEmpty]
  public int ObjectTypeID
  {
    get
    {
      if (this._objectTypeID == -1)
        this._objectTypeID = MetaDataHelperService.Instance.GetObjectTypeID(this.ObjectTypeGuid);
      return this._objectTypeID;
    }
  }

  [NotEmpty]
  public Guid ObjectTypeGuid { get; }

  [NotNull]
  [NotWhitespace]
  public string ObjectTypeName
  {
    get
    {
      if (this._objectTypeName == null)
        this._objectTypeName = MetaDataHelperService.Instance.GetObjectTypeName(this.ObjectTypeGuid);
      return this._objectTypeName;
    }
  }

  public bool RecursiveHandle { get; }

  public DBObjectTypeHandlerAttribute([NotEmpty] Guid dbTypeGuid, bool recursiveHandle = true)
  {
    this.ObjectTypeGuid = dbTypeGuid;
    this.RecursiveHandle = recursiveHandle;
  }

  public DBObjectTypeHandlerAttribute([NotNull, NotWhitespace] string dbTypeGuid, bool recursiveHandle = true)
    : this(new Guid(dbTypeGuid), recursiveHandle)
  {
  }
}
