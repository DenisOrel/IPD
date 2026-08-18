// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionUtils
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;

#nullable disable
namespace Intermech.AutoSelection.Server;

internal sealed class AutoSelectionUtils
{
  public static IImbaseServer GetImbaseServerService(IUserSession session, bool throwException)
  {
    return ServiceUtils.GetService<IImbaseServer>((object) session, throwException);
  }
}
