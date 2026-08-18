// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.ImbaseParamsHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Params;

#nullable disable
namespace Intermech.Imbase.Params;

public static class ImbaseParamsHelper
{
  private static ImbaseCommonParams _commonParams;
  private static ImbaseUserParams _userParams;

  public static ImbaseCommonParams CommonParams
  {
    get
    {
      return ImbaseParamsHelper._commonParams ?? (ImbaseParamsHelper._commonParams = ImbaseParamsHelper.LoadCommonParams());
    }
  }

  public static ImbaseUserParams UserParams
  {
    get
    {
      return ImbaseParamsHelper._userParams ?? (ImbaseParamsHelper._userParams = ImbaseParamsHelper.LoadUserParams());
    }
  }

  private static ImbaseCommonParams LoadCommonParams()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams;
  }

  private static ImbaseUserParams LoadUserParams()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).GetUserParams(sessionKeeper.Session.SessionGUID);
  }

  private static void SaveCommonParams()
  {
    if (ImbaseParamsHelper._commonParams == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).SetCommonParams(sessionKeeper.Session.SessionGUID, ImbaseParamsHelper._commonParams);
  }

  private static void SaveUserParams()
  {
    if (ImbaseParamsHelper._userParams == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).SetUserParams(sessionKeeper.Session.SessionGUID, ImbaseParamsHelper._userParams);
  }

  public static void LoadParams()
  {
    ImbaseParamsHelper._commonParams = ImbaseParamsHelper.LoadCommonParams();
    ImbaseParamsHelper._userParams = ImbaseParamsHelper.LoadUserParams();
  }

  public static void SaveParams()
  {
    ImbaseParamsHelper.SaveCommonParams();
    ImbaseParamsHelper.SaveUserParams();
  }
}
