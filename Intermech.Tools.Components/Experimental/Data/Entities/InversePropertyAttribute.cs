// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.InversePropertyAttribute
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public class InversePropertyAttribute : Attribute
{
  private string propertyName;

  public InversePropertyAttribute(string propertyName)
  {
    this.propertyName = !string.IsNullOrEmpty(propertyName) ? propertyName : throw new ArgumentException("Не задано имя свойства доменного объекта.", nameof (propertyName));
  }

  public string PropertyName
  {
    [DebuggerStepThrough] get => this.propertyName;
  }
}
