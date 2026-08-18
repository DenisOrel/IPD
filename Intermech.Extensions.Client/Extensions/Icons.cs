// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Icons
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public abstract class Icons
{
  private static int? _userImageIndex;
  private static int? _groupImageIndex;
  private static int? _rankImageIndex;

  [NotNull]
  public static ICategoryTypeIconService Service
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Client.Services.IconService;
  }

  public static int UserImageIndex
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Icons._userImageIndex ?? (Icons._userImageIndex = new int?(Icons.Service.IndexOf(4, (int) (IpsMetadataEntityBase<int>) ObjectTypes.User))).Value;
    }
  }

  public static int GroupImageIndex
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Icons._groupImageIndex ?? (Icons._groupImageIndex = new int?(Icons.Service.IndexOf(4, (int) (IpsMetadataEntityBase<int>) ObjectTypes.UserGroup))).Value;
    }
  }

  public static int RankImageIndex
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Icons._rankImageIndex ?? (Icons._rankImageIndex = new int?(Icons.Service.IndexOf(4, (int) (IpsMetadataEntityBase<int>) ObjectTypes.Rank))).Value;
    }
  }
}
