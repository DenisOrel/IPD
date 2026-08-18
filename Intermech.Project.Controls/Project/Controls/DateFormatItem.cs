// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.DateFormatItem
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project.Controls;

public class DateFormatItem
{
  [NotNull]
  public readonly string Format;

  public DateFormatItem([NotNull] string format) => this.Format = format;

  public override string ToString() => StringFuncs.UCFirst(DateTime.Now.ToString(this.Format));

  public override int GetHashCode() => this.Format.GetHashCode();

  public override bool Equals(object obj) => obj != null && this.GetHashCode() == obj.GetHashCode();
}
