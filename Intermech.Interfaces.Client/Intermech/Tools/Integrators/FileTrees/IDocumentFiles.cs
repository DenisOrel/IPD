// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileTrees.IDocumentFiles
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.FileTrees;

public interface IDocumentFiles
{
  string Path { get; }

  ICollection<string> Satellites { get; }

  ICollection<string> Dependencies { get; }
}
