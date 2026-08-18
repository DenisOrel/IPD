// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.TraceUtils
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.EnterpriseArchive.UI;
using Intermech.Tools.Integrators.FileTrees;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal static class TraceUtils
{
  internal static void TraceFileList(string traceMessage, ICollection<string> files)
  {
    Trace.WriteLine(traceMessage);
    TraceUtils.TraceFileList(files, true);
  }

  internal static void TraceFileList(ICollection<string> files, bool traceWithIndent)
  {
    if (traceWithIndent)
      Trace.Indent();
    Trace.WriteLine($"(II) List content, total items = {files.Count}");
    foreach (object file in (IEnumerable<string>) files)
      Trace.WriteLine(file.ToString());
    if (!traceWithIndent)
      return;
    Trace.Unindent();
  }

  internal static void TraceFileTree(string traceMessage, FileTree fileTree)
  {
    Trace.WriteLine(traceMessage);
    Trace.Indent();
    Trace.WriteLine($"(II) FileTree #{fileTree.GetHashCode()}, total nodes = {fileTree.Nodes.Count}");
    foreach (FileTreeNode node in fileTree.Nodes)
    {
      Trace.WriteLine(node.Path);
      if (node.Satellites.Count > 0)
        TraceUtils.TraceFileList("Satellites:", node.Satellites);
      if (node.Dependencies.Count > 0)
        TraceUtils.TraceFileList("Dependencies:", node.Dependencies);
    }
    if (fileTree.BadFiles.Count > 0)
      TraceUtils.TraceFileList("FileTree errors:", (ICollection<string>) fileTree.BadFiles);
    Trace.WriteLine($"(II) FileTree #{fileTree.GetHashCode()} end.");
    Trace.Unindent();
  }

  internal static void TraceBucketList(string traceMessage, ICollection<FileBucket> fileBuckets)
  {
    Trace.WriteLine(traceMessage);
    Trace.Indent();
    Trace.WriteLine($"(II) List content, total buckets = {fileBuckets.Count}");
    int num = 1;
    foreach (FileBucket fileBucket in (IEnumerable<FileBucket>) fileBuckets)
    {
      Trace.WriteLine($"Bucket #{num++}");
      TraceUtils.TraceFileList((ICollection<string>) fileBucket, true);
    }
    Trace.Unindent();
  }

  internal static void TraceFileErrors(string traceMessage, ICollection<FileError> errors)
  {
    Trace.WriteLine(traceMessage);
    Trace.Indent();
    Trace.WriteLine($"(II) List content, total errors = {errors.Count}");
    foreach (FileError error in (IEnumerable<FileError>) errors)
      Trace.WriteLine($"{error.FileName}: {error.Error}");
    Trace.Unindent();
  }
}
