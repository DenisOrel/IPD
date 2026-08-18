// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.IExpertServerEx
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces.Expert;
using System;

#nullable disable
namespace Intermech.Expert.Server;

public interface IExpertServerEx
{
  IExpertServerTask GetServerTask(int taskId);

  IExpertServerTask GetServerTask(Guid sessionGuid);

  ExpertResult Calculate(
    IExpertServerTask ti,
    int objTypeId,
    int attrTypeId,
    long objId,
    out object value,
    long contObjId = -1,
    long[] moreObjIds = null);

  ExpertResult CalculateQuiet(
    IExpertServerTask ti,
    int objTypeId,
    int attrTypeId,
    long objId,
    out object value,
    long contObjId = -1,
    long[] moreObjIds = null);
}
