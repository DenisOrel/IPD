// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileTrees.ReadOnlyFileTreeNode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Integrators.FileTrees;

public sealed class ReadOnlyFileTreeNode : IDocumentFiles
{
  private readonly string path;
  private readonly ReadOnlyCollection<string> satellitesWrapper;
  private readonly ReadOnlyCollection<string> dependenciesWrapper;

  public ReadOnlyFileTreeNode(string path, List<string> satellites, List<string> dependencies)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    if (satellites == null)
      throw new ArgumentNullException(nameof (satellites));
    if (dependencies == null)
      throw new ArgumentNullException(nameof (dependencies));
    this.path = path;
    this.satellitesWrapper = new ReadOnlyCollection<string>((IList<string>) satellites);
    this.dependenciesWrapper = new ReadOnlyCollection<string>((IList<string>) dependencies);
  }

  public ReadOnlyFileTreeNode(IDocumentFiles node)
  {
    this.path = node != null ? node.Path : throw new ArgumentNullException(nameof (node));
    this.satellitesWrapper = new ReadOnlyCollection<string>((IList<string>) new List<string>((IEnumerable<string>) node.Satellites));
    this.dependenciesWrapper = new ReadOnlyCollection<string>((IList<string>) new List<string>((IEnumerable<string>) node.Dependencies));
  }

  public string Path => this.path;

  public ICollection<string> Satellites => (ICollection<string>) this.satellitesWrapper;

  public ICollection<string> Dependencies => (ICollection<string>) this.dependenciesWrapper;
}
