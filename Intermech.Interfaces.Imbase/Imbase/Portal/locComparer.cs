// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.locComparer
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Portal;

internal class locComparer : IComparer<string>
{
  public int Compare(string x, string y) => y.CompareTo(x);
}
