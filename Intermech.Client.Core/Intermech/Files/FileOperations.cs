
// Type: Intermech.Files.FileOperations
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Files;

public static class FileOperations
{
  private static readonly TraceSwitch traceFileAttributeActions = new TraceSwitch("FileVault.FileAttributeActions", "", "0");

  public static void BatchReadFiles(long objectId, ICollection<IFileAttributeAction> actions)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (actions == null)
      throw new ArgumentNullException();
    if (FileOperations.traceFileAttributeActions.Level != TraceLevel.Off)
      FileOperations.TraceActions(objectId, actions);
    using (RemoteLock remoteLock = new RemoteLock())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objToLock = sessionKeeper.Session.GetObject(objectId);
        remoteLock.Add((object) objToLock);
        IDBAttribute attributeById = objToLock.GetAttributeByID(sessionKeeper.Session.IdentHelper.FileAttributeID);
        if (attributeById == null)
          return;
        remoteLock.Add((object) attributeById);
        List<string> initialFileNames = new List<string>((IEnumerable<string>) attributeById.Descriptions);
        foreach (IFileAttributeAction action in (IEnumerable<IFileAttributeAction>) actions)
          action.Perform(attributeById, initialFileNames);
      }
    }
  }

  public static void BatchUpdateFiles(long objectId, IList<IFileAttributeAction> actions)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (actions == null)
      throw new ArgumentNullException();
    if (FileOperations.traceFileAttributeActions.Level != TraceLevel.Off)
      FileOperations.TraceActions(objectId, (ICollection<IFileAttributeAction>) actions);
    using (RemoteLock remoteLock = new RemoteLock())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objToLock = sessionKeeper.Session.GetObject(objectId);
        remoteLock.Add((object) objToLock);
        IDBAttribute attributeById = objToLock.GetAttributeByID(sessionKeeper.Session.IdentHelper.FileAttributeID);
        if (attributeById == null)
          return;
        remoteLock.Add((object) attributeById);
        List<string> initialFileNames = new List<string>((IEnumerable<string>) attributeById.Descriptions);
        IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, true);
        remoteLock.Add((object) service);
        service.StartTransaction();
        try
        {
          foreach (IFileAttributeAction action in (IEnumerable<IFileAttributeAction>) actions)
            action.Perform(attributeById, initialFileNames);
          service.Commit();
        }
        catch
        {
          service.Rollback();
          throw;
        }
      }
    }
  }

  private static void TraceActions(long objectId, ICollection<IFileAttributeAction> actions)
  {
    List<string> stringList = new List<string>(actions.Count);
    foreach (IFileAttributeAction action in (IEnumerable<IFileAttributeAction>) actions)
    {
      if (action is IFileAttributeActionInfo attributeActionInfo)
        stringList.Add(attributeActionInfo.GetInfo());
    }
    if (stringList.Count == 0)
      stringList.Add("* no actions neeeded");
    Trace.WriteLine($"File vault: the file attribute actions for the ObjectID={objectId}");
    Trace.Indent();
    foreach (string message in stringList)
      Trace.WriteLine(message);
    Trace.Unindent();
  }
}
