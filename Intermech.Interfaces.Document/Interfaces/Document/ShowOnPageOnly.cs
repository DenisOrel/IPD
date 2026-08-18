// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ShowOnPageOnly
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

[Flags]
[TypeConverter(typeof (EnumCustomConverter))]
public enum ShowOnPageOnly
{
  [CustomDescription("Attribute.Interfaces.Document_623")] None = 0,
  [CustomDescription("Attribute.Interfaces.Document_622")] All = 7,
  [CustomDescription("Attribute.Interfaces.Document_616")] FirstDataPage = 1,
  [CustomDescription("Attribute.Interfaces.Document_618")] NextDataPage = 2,
  [CustomDescription("Attribute.Interfaces.Document_620")] LastDataPage = 4,
}
