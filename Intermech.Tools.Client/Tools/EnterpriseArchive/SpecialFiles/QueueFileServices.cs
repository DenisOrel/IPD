// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.SpecialFiles.QueueFileServices
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.SpecialFiles;

internal static class QueueFileServices
{
  public static QueueFile ReadQueue()
  {
    string queueFilePath = QueueFileServices.GetQueueFilePath(SpecialFileServices.LocateServiceDirectory(false));
    if (!File.Exists(queueFilePath))
      return new QueueFile();
    XmlDocument document = new XmlDocument();
    document.PreserveWhitespace = true;
    using (Stream inStream = SpecialFileServices.OpenFile(queueFilePath, FileShare.Read))
      document.Load(inStream);
    QueueFile queueFile = new QueueFile();
    queueFile.FromXml(document);
    return queueFile;
  }

  public static void ReplaceQueue(QueueFile queueFile)
  {
    if (queueFile == null)
      throw new ArgumentNullException(nameof (queueFile));
    string queueFilePath = QueueFileServices.GetQueueFilePath(SpecialFileServices.LocateServiceDirectory(true));
    XmlDocument xml = queueFile.ToXml();
    using (Stream outStream = SpecialFileServices.OpenFile(queueFilePath, FileShare.None))
    {
      outStream.SetLength(0L);
      outStream.Position = 0L;
      xml.Save(outStream);
    }
  }

  private static string GetQueueFilePath(string serviceDir)
  {
    return Path.Combine(serviceDir, "queuefile.xml");
  }
}
