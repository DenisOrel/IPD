// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDBEditingContextsServerService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDBEditingContextsServerService : IDBEditingContextsService
{
  bool ReleaseContextObjects(object usrSession, long contextID, bool exceptIfFail);

  bool ClearContext(object usrSession, long contextID, bool exceptIfFail);

  long GetUserContextID(Guid id);

  bool SetUserContextID(Guid id, long contextID, long linkedContextNumber);

  EditingContextMode GetUserContextMode(Guid id);

  long GetModificationID(Guid id);

  bool SetUserContextMode(Guid id, EditingContextMode mode);

  CurrentEditingContext GetUserContext(Guid id);

  CurrentEditingContext SetUserContext(
    Guid id,
    long contextID,
    long linkedContextNumber,
    EditingContextMode mode);

  CurrentEditingContext SetUserContext(Guid id, CurrentEditingContext context);

  void RemoveUsersContext(long contextID);

  bool HasUserContextSourceInfo(long userID, long roleID);

  EditingContextSource GetUserContextSource(long userID, long roleID);

  void UpdateModificationInCache(long contextID, long newModificationID);

  void SetUserContextSource(long userID, long roleID, EditingContextSource value);

  void RemoveUserContextSource(long userID, long roleID);

  List<EditingContextsObjectVersion> SelectContextInfo(
    long contextID,
    long linkedContextNumber,
    IUserSession serverSession);

  List<ObjectVersionDescription> SelectContextDescriptions(
    long contextID,
    long linkedContextNumber,
    IUserSession serverSession);

  List<EditingContextsObjectVersion> SelectContextsInfo(
    long contextID,
    long linkedContextNumber,
    IUserSession serverSession);

  List<ObjectVersionDescription> SelectContextsDescriptions(
    long contextID,
    IUserSession serverSession);

  void ResetCache();

  void RemoveFromCache(long modificationID);

  void RemoveVersionFromCache(long versionID, IList<long> fromContexts);

  void RemoveVersionsFromCache(IList<long> versionIDs, IList<long> fromContexts);

  void RemoveObjectFromCache(long fID, IList<long> fromContexts);

  void RemoveObjectsFromCache(IList<long> fIDs, IList<long> fromContexts);

  bool DeleteFromIMS_VERSIONS_CONTEXT(object usrSession, long versionID, bool exceptIfFail);

  bool Replace_ModificationID_IMS_VERSIONS_CONTEXT(
    object usrSession,
    long contextID,
    long newModificationID,
    bool exceptIfFail);
}
