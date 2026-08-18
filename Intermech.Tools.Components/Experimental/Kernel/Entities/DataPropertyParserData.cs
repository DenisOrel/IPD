// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DataPropertyParserData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DataPropertyParserData
{
  public DataPropertyParserData(ExtendedEntityPropertyInfo propertyInfo)
  {
    this.ReflectionInfo = propertyInfo;
    this.Name = propertyInfo.Name;
  }

  public ExtendedEntityPropertyInfo ReflectionInfo { get; private set; }

  public string Name { get; private set; }

  public Guid DBAttributeGuid { get; set; }

  public DataPropertyDescriptor Descriptor { get; set; }

  public DataPropertyLanguageInfo LanguageInfo { get; set; }
}
