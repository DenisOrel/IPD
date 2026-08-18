// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.ServiceHolder
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

public class ServiceHolder
{
  private static ICategoryTypeIconService _CategoryTypeIconService;
  private static IBackgroundTaskView _BackgroundTaskView;
  private static IOutputView _OutputView;
  private static IInvokeService _IInvokeService;
  private static IImbaseServer _ImbaseServer;

  public static ICategoryTypeIconService CategoryTypeIconService
  {
    get => ServiceHolder._CategoryTypeIconService;
  }

  public static IBackgroundTaskView BackgroundTaskView => ServiceHolder._BackgroundTaskView;

  public static IOutputView OutputView => ServiceHolder._OutputView;

  public static IInvokeService IInvokeService => ServiceHolder._IInvokeService;

  public static IImbaseServer ImbaseServer => ServiceHolder._ImbaseServer;

  public static void Initialize(IServiceProvider serviceProvider, IUserSession session)
  {
    ServiceHolder._CategoryTypeIconService = serviceProvider.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    ServiceHolder._BackgroundTaskView = serviceProvider.GetService(typeof (IBackgroundTaskView)) as IBackgroundTaskView;
    ServiceHolder._OutputView = serviceProvider.GetService(typeof (IOutputView)) as IOutputView;
    ServiceHolder._IInvokeService = serviceProvider.GetService(typeof (IInvokeService)) as IInvokeService;
    ServiceHolder._ImbaseServer = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
  }
}
