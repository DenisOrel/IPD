// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileTrees.FileTree
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.FileTrees;

public sealed class FileTree
{
  public readonly LinkedList<FileTreeNode> Nodes;
  public readonly List<string> BadFiles;

  public FileTree(LinkedList<FileTreeNode> nodes, List<string> badFiles)
  {
    if (nodes == null)
      throw new ArgumentNullException("documents");
    if (badFiles == null)
      throw new ArgumentNullException(nameof (badFiles));
    this.Nodes = nodes;
    this.BadFiles = badFiles;
  }

  public FileTree()
    : this(new LinkedList<FileTreeNode>(), new List<string>())
  {
  }
}
