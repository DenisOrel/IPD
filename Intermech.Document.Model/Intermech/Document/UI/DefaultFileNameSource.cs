// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.DefaultFileNameSource
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.UI;

[TypeConverter(typeof (EnumCustomConverter))]
[Serializable]
public enum DefaultFileNameSource
{
  [CustomDescription("Attribute.Document.Model_311")] ObjectCaption = 1,
  [CustomDescription("Attribute.Document.Model_312")] ObjectVersionID = 2,
}
