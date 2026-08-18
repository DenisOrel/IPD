// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ISynchronizationObjService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Сервис синхронизации объекта с Imbase</summary>
public interface ISynchronizationObjService
{
  /// <summary>Синхронизировать объект</summary>
  /// <param name="session"></param>
  /// <param name="objId"></param>
  /// <param name="imbaseObjId"></param>
  /// <param name="recId"></param>
  /// <param name="createVersion"></param>
  /// <param name="message"></param>
  /// <param name="objTypeAttrs"></param>
  /// <returns></returns>
  SynchObjectsStatus Synchronize(
    IUserSession session,
    long objId,
    long imbaseObjId,
    long recId,
    bool createVersion,
    out string message);
}
