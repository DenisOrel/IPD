// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.SpecialFiles.LockFileServices
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.SpecialFiles;

internal static class LockFileServices
{
  private static readonly BooleanSwitch traceSwitch = new BooleanSwitch("EnterpriseArchive.LockFile", string.Empty, "0");
  private const string EmptyFileContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?><LockedFiles></LockedFiles>";

  public static PathCollection GetLockedFiles(long userId)
  {
    if (userId == 0L)
      throw new ArgumentException();
    string lockFilePath = LockFileServices.GetLockFilePath(SpecialFileServices.LocateServiceDirectory(false));
    if (!File.Exists(lockFilePath))
      return new PathCollection(0);
    XmlDocument doc = new XmlDocument();
    doc.PreserveWhitespace = true;
    using (Stream inStream = SpecialFileServices.OpenFile(lockFilePath, FileShare.Read))
      doc.Load(inStream);
    List<LockFileServices.LockEntry> lockEntries = LockFileServices.Decode(doc);
    lockEntries.RemoveAll((Predicate<LockFileServices.LockEntry>) (entry => entry.UserId != userId));
    return LockFileServices.ToPathCollection(lockEntries);
  }

  public static PathCollection GetLockedFiles()
  {
    string lockFilePath = LockFileServices.GetLockFilePath(SpecialFileServices.LocateServiceDirectory(false));
    if (!File.Exists(lockFilePath))
      return new PathCollection(0);
    XmlDocument doc = new XmlDocument();
    doc.PreserveWhitespace = true;
    using (Stream inStream = SpecialFileServices.OpenFile(lockFilePath, FileShare.Read))
      doc.Load(inStream);
    return LockFileServices.ToPathCollection(LockFileServices.Decode(doc));
  }

  private static PathCollection ToPathCollection(List<LockFileServices.LockEntry> lockEntries)
  {
    PathCollection pathCollection = new PathCollection(lockEntries.Count);
    foreach (LockFileServices.LockEntry lockEntry in lockEntries)
      pathCollection.Add(lockEntry.Path);
    return pathCollection;
  }

  public static void Unlock(ICollection<string> fileNames, long userId)
  {
    if (fileNames == null)
      throw new ArgumentNullException(nameof (fileNames));
    if (userId == 0L)
      throw new ArgumentException();
    if (fileNames.Count == 0)
      return;
    string lockFilePath = LockFileServices.GetLockFilePath(SpecialFileServices.LocateServiceDirectory(false));
    if (!File.Exists(lockFilePath))
      return;
    using (Stream stream = SpecialFileServices.OpenFile(lockFilePath, FileShare.None))
    {
      XmlDocument doc = new XmlDocument();
      doc.PreserveWhitespace = true;
      doc.Load(stream);
      List<LockFileServices.LockEntry> newEntries = LockFileServices.Decode(doc);
      if (newEntries.RemoveAll((Predicate<LockFileServices.LockEntry>) (entry => entry.UserId == userId && CollectionUtils.Exists<string>((IEnumerable<string>) fileNames, (Predicate<string>) (fileName => PathUtils.IsSamePath(fileName, entry.Path))))) <= 0)
        return;
      doc.DocumentElement.RemoveAll();
      LockFileServices.Append(doc, newEntries);
      stream.Position = 0L;
      stream.SetLength(0L);
      doc.Save(stream);
    }
  }

  public static void UnlockAll(long userId)
  {
    if (userId == 0L)
      throw new ArgumentException();
    string lockFilePath = LockFileServices.GetLockFilePath(SpecialFileServices.LocateServiceDirectory(false));
    if (!File.Exists(lockFilePath))
      return;
    using (Stream stream = SpecialFileServices.OpenFile(lockFilePath, FileShare.None))
    {
      XmlDocument doc = new XmlDocument();
      doc.PreserveWhitespace = true;
      doc.Load(stream);
      List<LockFileServices.LockEntry> newEntries = LockFileServices.Decode(doc);
      if (newEntries.RemoveAll((Predicate<LockFileServices.LockEntry>) (entry => entry.UserId == userId)) <= 0)
        return;
      doc.DocumentElement.RemoveAll();
      LockFileServices.Append(doc, newEntries);
      stream.Position = 0L;
      stream.SetLength(0L);
      doc.Save(stream);
    }
  }

  public static void FilterAndLock(List<string> fileNames, long userId, string userName)
  {
    if (fileNames == null)
      throw new ArgumentNullException(nameof (fileNames));
    if (userId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(userName))
      throw new ArgumentException();
    using (Stream stream = SpecialFileServices.OpenFile(LockFileServices.GetLockFilePath(SpecialFileServices.LocateServiceDirectory(true)), FileShare.None))
    {
      if (LockFileServices.traceSwitch.Enabled)
        Trace.WriteLine($"LockFileServices: lock file size = {stream.Length}");
      if (stream.Length == 0L)
        SpecialFileServices.FillEmptyFile(stream, "<?xml version=\"1.0\" encoding=\"utf-8\"?><LockedFiles></LockedFiles>");
      XmlDocument doc = new XmlDocument();
      doc.PreserveWhitespace = true;
      doc.Load(stream);
      List<LockFileServices.LockEntry> lockEntries = LockFileServices.Decode(doc);
      if (LockFileServices.traceSwitch.Enabled)
        LockFileServices.TraceLockFileNames($"LockFileServices: BeforeLock stage, file count = {fileNames.Count}", fileNames);
      fileNames.RemoveAll((Predicate<string>) (fn => lockEntries.Exists((Predicate<LockFileServices.LockEntry>) (entry => entry.UserId != userId && PathUtils.IsSamePath(entry.Path, fn)))));
      if (fileNames.Count > 0)
      {
        List<string> all = fileNames.FindAll((Predicate<string>) (fileName => !lockEntries.Exists((Predicate<LockFileServices.LockEntry>) (entry => PathUtils.IsSamePath(entry.Path, fileName)))));
        if (all.Count > 0)
        {
          List<LockFileServices.LockEntry> newEntries = all.ConvertAll<LockFileServices.LockEntry>((Converter<string, LockFileServices.LockEntry>) (fn => new LockFileServices.LockEntry(fn, userId, userName)));
          LockFileServices.Append(doc, newEntries);
          stream.Position = 0L;
          stream.SetLength(0L);
          doc.Save(stream);
        }
      }
      if (!LockFileServices.traceSwitch.Enabled)
        return;
      LockFileServices.TraceLockFileNames($"LockFileServices: AfterLock stage, file count = {fileNames.Count}", fileNames);
    }
  }

  public static LinkedList<FileBucket> FilterAndLock(
    LinkedList<FileBucket> fileBuckets,
    int maxFileCount,
    long userId,
    string userName,
    bool returnFilteredOut)
  {
    if (fileBuckets == null)
      throw new ArgumentNullException("fileNames");
    if (maxFileCount <= 0)
      throw new ArgumentOutOfRangeException(nameof (maxFileCount));
    if (userId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(userName))
      throw new ArgumentException();
    LinkedList<FileBucket> linkedList = returnFilteredOut ? new LinkedList<FileBucket>() : (LinkedList<FileBucket>) null;
    if (fileBuckets.Count == 0)
      return linkedList;
    using (Stream stream = SpecialFileServices.OpenFile(LockFileServices.GetLockFilePath(SpecialFileServices.LocateServiceDirectory(true)), FileShare.None))
    {
      if (LockFileServices.traceSwitch.Enabled)
        Trace.WriteLine($"LockFileServices: lock file size = {stream.Length}");
      if (stream.Length == 0L)
        SpecialFileServices.FillEmptyFile(stream, "<?xml version=\"1.0\" encoding=\"utf-8\"?><LockedFiles></LockedFiles>");
      XmlDocument doc = new XmlDocument();
      doc.PreserveWhitespace = true;
      doc.Load(stream);
      List<LockFileServices.LockEntry> lockEntries = LockFileServices.Decode(doc);
      if (LockFileServices.traceSwitch.Enabled)
        LockFileServices.TraceLockFileNames($"LockFileServices: BeforeLock stage, buckets count = {fileBuckets.Count}", fileBuckets);
      int num1 = 0;
      LinkedListNode<FileBucket> next;
      for (LinkedListNode<FileBucket> node = fileBuckets.First; node != null; node = next)
      {
        next = node.Next;
        if (num1 >= maxFileCount)
          fileBuckets.Remove(node);
        else if (node.Value.Exists((Predicate<string>) (bucketFile => lockEntries.Exists((Predicate<LockFileServices.LockEntry>) (entry => entry.UserId != userId && PathUtils.IsSamePath(entry.Path, bucketFile))))))
        {
          fileBuckets.Remove(node);
          if (returnFilteredOut)
            linkedList.AddLast(node);
        }
        else
          num1 += node.Value.Count;
      }
      if (fileBuckets.Count > 0)
      {
        int num2 = 0;
        foreach (List<string> fileBucket in fileBuckets)
        {
          List<string> all = fileBucket.FindAll((Predicate<string>) (bucketFile => !lockEntries.Exists((Predicate<LockFileServices.LockEntry>) (entry => PathUtils.IsSamePath(entry.Path, bucketFile)))));
          if (all.Count > 0)
          {
            List<LockFileServices.LockEntry> newEntries = all.ConvertAll<LockFileServices.LockEntry>((Converter<string, LockFileServices.LockEntry>) (fn => new LockFileServices.LockEntry(fn, userId, userName)));
            LockFileServices.Append(doc, newEntries);
            num2 += newEntries.Count;
          }
        }
        if (num2 > 0)
        {
          stream.Position = 0L;
          stream.SetLength(0L);
          doc.Save(stream);
        }
      }
      if (LockFileServices.traceSwitch.Enabled)
        LockFileServices.TraceLockFileNames($"LockFileServices: AfterLock stage, buckets count = {fileBuckets.Count}", fileBuckets);
    }
    return linkedList;
  }

  private static void TraceLockFileNames(string preludeMessage, List<string> fileNames)
  {
    Trace.WriteLine(preludeMessage);
    Trace.Indent();
    foreach (string fileName in fileNames)
      Trace.WriteLine(fileName);
    Trace.Unindent();
  }

  private static void TraceLockFileNames(string preludeMessage, LinkedList<FileBucket> buckets)
  {
    Trace.WriteLine(preludeMessage);
    Trace.Indent();
    foreach (List<string> bucket in buckets)
    {
      foreach (string message in bucket)
        Trace.WriteLine(message);
    }
    Trace.Unindent();
  }

  private static List<LockFileServices.LockEntry> Decode(XmlDocument doc)
  {
    XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("File[@path and @userName and @userId]");
    List<LockFileServices.LockEntry> lockEntryList = new List<LockFileServices.LockEntry>(xmlNodeList.Count);
    foreach (XmlNode xmlNode in xmlNodeList)
    {
      string path = xmlNode.Attributes["path"].Value.Trim();
      string userName = xmlNode.Attributes["userName"].Value.Trim();
      long int64 = Convert.ToInt64(xmlNode.Attributes["userId"].Value.Trim());
      lockEntryList.Add(new LockFileServices.LockEntry(path, int64, userName));
    }
    return lockEntryList;
  }

  private static void Append(XmlDocument doc, List<LockFileServices.LockEntry> newEntries)
  {
    foreach (LockFileServices.LockEntry newEntry in newEntries)
    {
      XmlElement element = doc.CreateElement("File");
      XmlAttribute attribute1 = doc.CreateAttribute("path");
      attribute1.Value = newEntry.Path;
      element.Attributes.Append(attribute1);
      XmlAttribute attribute2 = doc.CreateAttribute("userName");
      attribute2.Value = newEntry.UserName;
      element.Attributes.Append(attribute2);
      XmlAttribute attribute3 = doc.CreateAttribute("userId");
      attribute3.Value = newEntry.UserId.ToString();
      element.Attributes.Append(attribute3);
      doc.DocumentElement.AppendChild((XmlNode) element);
    }
  }

  private static string GetLockFilePath(string serviceDir)
  {
    return Path.Combine(serviceDir, "lockfile.xml");
  }

  private sealed class LockEntry
  {
    public readonly string Path;
    public readonly long UserId;
    public readonly string UserName;

    public LockEntry(string path, long userId, string userName)
    {
      this.Path = path;
      this.UserId = userId;
      this.UserName = userName;
    }
  }
}
