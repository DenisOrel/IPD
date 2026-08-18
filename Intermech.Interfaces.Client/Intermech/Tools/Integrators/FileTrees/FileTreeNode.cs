// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileTrees.FileTreeNode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.FileTrees;

public sealed class FileTreeNode : IDocumentFiles
{
  private readonly string path;
  private readonly List<string> satellites;
  private readonly List<string> dependencies;

  public FileTreeNode(string path, List<string> satellites, List<string> dependencies)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    if (satellites == null)
      throw new ArgumentNullException(nameof (satellites));
    if (dependencies == null)
      throw new ArgumentNullException(nameof (dependencies));
    this.path = path;
    this.satellites = satellites;
    this.dependencies = dependencies;
  }

  public FileTreeNode(
    string path,
    IEnumerable<string> satellites,
    IEnumerable<string> dependencies)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException();
    if (satellites == null)
      throw new ArgumentNullException(nameof (satellites));
    if (dependencies == null)
      throw new ArgumentNullException(nameof (dependencies));
    this.path = path;
    this.satellites = new List<string>(satellites);
    this.dependencies = new List<string>(dependencies);
  }

  public string Path => this.path;

  public ICollection<string> Satellites => (ICollection<string>) this.satellites;

  public ICollection<string> Dependencies => (ICollection<string>) this.dependencies;
}
