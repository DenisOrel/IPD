// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionCreator.AutoSelectionDBObjectCreator
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.AutoSelection.Server.AutoSelectionCreator;

public class AutoSelectionDBObjectCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return (IDBObject) new AutoSelectionDBObject((UserSession) uSession, objectParams);
  }
}
