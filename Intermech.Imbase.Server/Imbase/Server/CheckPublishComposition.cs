// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.CheckPublishComposition
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server;

internal static class CheckPublishComposition
{
  public static void CheckPublishCompositionEvent(object sender, CheckPublishCompositionEventArgs e)
  {
    if (e.Composition != null && e.Composition.Objects != null && e.Options.EnableTypes != null && !e.Options.EnableTypes.Contains(Intermech.Imbase.Consts.ImbaseTableRefTypeID) && e.Options.EnableTypes.Contains(Intermech.Imbase.Consts.ImbaseTableTypeID) && e.Composition.Objects.First<PublishCompositionObject>((Func<PublishCompositionObject, bool>) (x => x.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID)) != null)
      throw new Exception("Нельзя публиковать таблицы Imbase без ярлыков!");
  }
}
