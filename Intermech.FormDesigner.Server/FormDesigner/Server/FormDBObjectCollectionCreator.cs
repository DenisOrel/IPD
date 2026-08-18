// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.FormDBObjectCollectionCreator
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.FormDesigner.Server;

public class FormDBObjectCollectionCreator : IDBObjectCollectionCreator
{
  public IDBObjectCollection CreateObjectCollection(
    IUserSession uSession,
    Guid guid,
    int objectTypeId)
  {
    return (IDBObjectCollection) new FormDBObjectCollection(uSession as UserSession, objectTypeId);
  }
}
