// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.PDF.PDFCache
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.PDF;

internal static class PDFCache
{
  private const int CACHE_SIZE = 128 /*0x80*/;
  private static readonly object syncRoot = new object();
  private static Dictionary<long, IPdfDocument> _documentsList = new Dictionary<long, IPdfDocument>(128 /*0x80*/);

  static PDFCache()
  {
    ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true)?.Subscribe("ObjectsChanged", new NotificationEventHandler(PDFCache.OnObjectChanged));
    PdfiumResolver.Resolve += new PdfiumResolveEventHandler(PDFCache.PdfiumResolver_Resolve);
  }

  internal static IPdfDocument GetOrLoadPdfDocument(long objectId)
  {
    if (!PDFCache._documentsList.ContainsKey(objectId))
    {
      Monitor.Enter(PDFCache.syncRoot);
      try
      {
        if (!PDFCache._documentsList.ContainsKey(objectId))
        {
          IPdfDocument pdfDocument = PDFCache.LoadPdfDocument(objectId);
          if (pdfDocument == null)
            return (IPdfDocument) null;
          PDFCache.EnsureForSpace();
          PDFCache._documentsList.Add(objectId, pdfDocument);
        }
      }
      finally
      {
        Monitor.Exit(PDFCache.syncRoot);
      }
    }
    return PDFCache._documentsList[objectId];
  }

  internal static void Clear()
  {
    Monitor.Enter(PDFCache.syncRoot);
    try
    {
      PDFCache._documentsList.Clear();
    }
    finally
    {
      Monitor.Exit(PDFCache.syncRoot);
    }
  }

  private static void EnsureForSpace()
  {
  }

  private static Stream ExtractIntoStream(IBlobReader br, BlobInformation blobInfo)
  {
    Stream outStream = (Stream) null;
    if (br != null && blobInfo.RealFileSize != 0L)
    {
      byte[] buffer = br.ReadDataBlock();
      br.CloseBlob();
      if (buffer != null && buffer.Length != 0)
      {
        using (MemoryStream inStream = new MemoryStream(buffer))
        {
          inStream.Position = 0L;
          IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
          if (service != null)
          {
            outStream = (Stream) new MemoryStream((int) blobInfo.RealFileSize);
            service.UnpackStream(outStream, (Stream) inStream);
            outStream.Position = 0L;
          }
        }
      }
    }
    return outStream;
  }

  private static IPdfDocument LoadPdfDocument(long objectId)
  {
    IPdfDocument pdfDocument = (IPdfDocument) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectId, false);
      if (objectActualCopy != null)
      {
        IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
        if (attributeByGuid != null)
        {
          if (attributeByGuid is IBlobReader br)
          {
            BlobInformation blobInfo = br.OpenBlob(0);
            pdfDocument = (IPdfDocument) PdfDocument.Load(PDFCache.ExtractIntoStream(br, blobInfo));
          }
        }
      }
    }
    return pdfDocument;
  }

  private static void OnObjectChanged(object sender, NotificationEventArgs ne)
  {
    if (!(ne is DBObjectsEventArgs objectsEventArgs) || !(objectsEventArgs.EventName == "ObjectsChanged"))
      return;
    Monitor.Enter(PDFCache.syncRoot);
    try
    {
      foreach (long objectId in (IEnumerable<long>) objectsEventArgs.ObjectIDs)
      {
        if (PDFCache._documentsList.ContainsKey(objectId))
          PDFCache._documentsList.Remove(objectId);
      }
    }
    finally
    {
      Monitor.Exit(PDFCache.syncRoot);
    }
  }

  private static void PdfiumResolver_Resolve(object sender, PdfiumResolveEventArgs e)
  {
    string path1 = Path.Combine(Path.Combine(Path.GetDirectoryName(typeof (PDFCache).Assembly.Location), "PdfPrintCenter"), IntPtr.Size == 4 ? "x86" : "x64");
    e.PdfiumFileName = Path.Combine(path1, "Pdfium.dll");
  }
}
