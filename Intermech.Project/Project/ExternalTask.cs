// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ExternalTask
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Project;

public class ExternalTask(long objectID) : StandaloneTask(objectID)
{
  [CanBeNull]
  private string _projectName;

  [CanBeNull]
  public new static Task Get([NotNull] ISessionProvider sessionProvider, [NotEmpty] long objectID)
  {
    return StandaloneTask.Get(sessionProvider, objectID, typeof (ExternalTask));
  }

  public override string IndexString => string.Empty;

  public override string ProjectName
  {
    get
    {
      if (this._projectName == null)
      {
        IUserSession session = this.GetSession();
        try
        {
          this._projectName = session.GetObject(this.ProjectID, false)?.Caption ?? string.Empty;
        }
        finally
        {
          this.ReleaseSession();
        }
      }
      return this._projectName ?? string.Empty;
    }
  }
}
