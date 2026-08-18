// Decompiled with JetBrains decompiler
// Type: Intermech.Project.INotifier
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project;

public interface INotifier
{
  void Notify([CanBeNull] object sender, Task.EventKind kind, long objectID, long newObjectID);

  void Start();

  void Commit();

  void Rollback();
}
