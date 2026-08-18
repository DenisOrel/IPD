// Decompiled with JetBrains decompiler
// Type: Intermech.Client.Services
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Client;

public abstract class Services : Intermech.Extensions.Services
{
  [CanBeNull]
  private static ICategoryTypeIconService _iconService;
  [CanBeNull]
  private static IGuidMapper _guidMapper;
  [CanBeNull]
  private static IFactory _factory;
  [CanBeNull]
  private static INotificationService _notificationService;
  [CanBeNull]
  private static INamedImageList _namedList;
  [CanBeNull]
  private static IPopupMenuHost _popupHost;
  [CanBeNull]
  private static IHotKeysManager _hotKeysManager;
  [CanBeNull]
  private static ICommandManager _commandManager;
  [CanBeNull]
  private static IAttributePropertyDescriberService _attributePropertyDescriber;
  [CanBeNull]
  private static IFiltrationService _filtrationService;
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  [NotNull]
  public static ICategoryTypeIconService IconService
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._iconService.CheckInitializedIn<ICategoryTypeIconService>(typeof (Services));
    }
  }

  [NotNull]
  public static IGuidMapper GuidMapper
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._guidMapper.CheckInitializedIn<IGuidMapper>(typeof (Services));
    }
  }

  [NotNull]
  public static IFactory Factory
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._factory.CheckInitializedIn<IFactory>(typeof (Services));
    }
  }

  [NotNull]
  public static INotificationService NotificationService
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._notificationService.CheckInitializedIn<INotificationService>(typeof (Services));
    }
  }

  [NotNull]
  public static INamedImageList NamedList
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._namedList.CheckInitializedIn<INamedImageList>(typeof (Services));
    }
  }

  [NotNull]
  public static IPopupMenuHost PopupHost
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._popupHost.CheckInitializedIn<IPopupMenuHost>(typeof (Services));
    }
  }

  [NotNull]
  public static IHotKeysManager HotKeysManager
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._hotKeysManager.CheckInitializedIn<IHotKeysManager>(typeof (Helper));
    }
  }

  [NotNull]
  public static ICommandManager CommandManager
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._commandManager.CheckInitializedIn<ICommandManager>(typeof (Services));
    }
  }

  [NotNull]
  public static IAttributePropertyDescriberService AttributePropertyDescriber
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._attributePropertyDescriber.CheckInitializedIn<IAttributePropertyDescriberService>(typeof (Services));
    }
  }

  [NotNull]
  public static IFiltrationService FiltrationService
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services._filtrationService.CheckInitializedIn<IFiltrationService>(typeof (Services));
    }
  }

  internal new static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Services._initOnce.Invoke(ref session, (Action) (() =>
    {
      Services._iconService = serviceProvider.GetService<ICategoryTypeIconService>();
      Services._guidMapper = serviceProvider.GetService<IGuidMapper>();
      Services._factory = serviceProvider.GetService<IFactory>();
      Services._notificationService = serviceProvider.GetService<INotificationService>();
      Services._namedList = serviceProvider.GetService<INamedImageList>();
      Services._popupHost = serviceProvider.GetService<IPopupMenuHost>();
      Services._hotKeysManager = serviceProvider.GetService<IHotKeysManager>();
      Services._commandManager = serviceProvider.GetService<ICommandManager>();
      Services._attributePropertyDescriber = serviceProvider.GetService<IAttributePropertyDescriberService>();
      Services._filtrationService = serviceProvider.GetService<IFiltrationService>();
    }));
  }
}
