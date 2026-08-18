// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.FType
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[AttributeUsage(AttributeTargets.All)]
public class FType : Attribute
{
  private FieldTypes _fieldType;
  private MultiValueModes _multiValueModes;
  private Guid _linkedObjectType;
  public readonly string ResourceImageName;

  public FType(FieldTypes ft) => this._fieldType = ft;

  public FType(FieldTypes ft, string resourceImageName)
    : this(ft)
  {
    this.ResourceImageName = resourceImageName;
  }

  public FType(FieldTypes ft, string linkedObjectType, string resourceImageName)
    : this(ft)
  {
    this._linkedObjectType = new Guid(linkedObjectType);
    this.ResourceImageName = resourceImageName;
  }

  public FieldTypes FieldType => this._fieldType;

  public MultiValueModes MultiValueModes
  {
    get => this._multiValueModes;
    set => this._multiValueModes = value;
  }

  public Guid LinkedObjectType => this._linkedObjectType;
}
