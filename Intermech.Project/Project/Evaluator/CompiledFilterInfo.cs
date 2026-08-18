// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.CompiledFilterInfo
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System.CodeDom.Compiler;

#nullable disable
namespace Intermech.Project.Evaluator;

internal class CompiledFilterInfo
{
  [CanBeNull]
  public CompilerResults CompilerResults;
  public int CachedExpressionsHash;
}
