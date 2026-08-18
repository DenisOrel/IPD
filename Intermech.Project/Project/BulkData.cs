// Decompiled with JetBrains decompiler
// Type: Intermech.Project.BulkData
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System.Data;

#nullable disable
namespace Intermech.Project;

internal class BulkData
{
  [CanBeNull]
  public DataTable Tasks;
  [CanBeNull]
  public DataTable Assignments;
  [CanBeNull]
  public DataTable Dependences;
}
