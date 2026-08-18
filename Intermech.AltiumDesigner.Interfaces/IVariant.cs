// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.IVariant
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

public interface IVariant : IParametrable, IValueBagContainer, IIdentification, IDisposable
{
  /// <summary>Описание варианта</summary>
  string Description { get; }

  /// <summary>Подборы</summary>
  List<IVariation> Variations { get; }
}
