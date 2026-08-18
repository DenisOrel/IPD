// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsVirtualAttributeColumnsScheme
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Отдельная группа для добавления виртуальных атрибутов AVS
/// </summary>
public class AvsVirtualAttributeColumnsScheme : DocFieldsColumnsScheme
{
  private static Guid defaultSchemeGuid = new Guid("{D201D543-F7C3-4B31-BE58-93ADC0445C85}");

  public AvsVirtualAttributeColumnsScheme() => this._schemeGuid = Guid.NewGuid();

  public AvsVirtualAttributeColumnsScheme(IEnumerable<AttributeInfo> fields)
    : base(fields)
  {
    this._schemeGuid = AvsVirtualAttributeColumnsScheme.defaultSchemeGuid;
  }

  public override string Name => "Атрибуты записи документа";
}
