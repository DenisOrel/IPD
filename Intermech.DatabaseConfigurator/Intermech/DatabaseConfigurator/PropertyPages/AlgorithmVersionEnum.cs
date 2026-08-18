// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.AlgorithmVersionEnum
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum AlgorithmVersionEnum
{
  [CustomDescription("NotPortableSign")] NotPortable,
  [CustomDescription("PortableSign")] Portable,
}
