// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfCrossTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.IO
{
    internal class PdfCrossTable : IDisposable
    {
      private PdfArchiveStream m_archive;
      private List<PdfCrossTable.ArchiveInfo> m_archives;
      private bool m_bDisposed;
      private bool m_bEncrypt;
      private bool m_bForceNew;
      private int m_count;
      private CrossTable m_crossTable;
      private PdfDocumentBase m_document;
      private PdfDictionary m_documentCatalog;
      private PdfDictionary m_encryptorDictionary;
      private bool m_isColorSpace;
      private bool m_isMerging;
      private PdfMainObjectCollection m_items;
      private Dictionary<PdfReference, PdfReference> m_mappedReferences;
      private long m_maxGenNumIndex;
      private Dictionary<long, PdfCrossTable.RegisteredObject> m_objects;
      private Stack<PdfReference> m_objNumbers;
      private Dictionary<IPdfPrimitive, object> m_pageCorrespondance;
      private List<PdfReference> m_preReference;
      private int m_storedCount;
      private Stream m_stream;
      private IPdfPrimitive m_trailer;

      public PdfCrossTable()
      {
        this.m_objects = new Dictionary<long, PdfCrossTable.RegisteredObject>();
        this.m_objNumbers = new Stack<PdfReference>();
      }

      public PdfCrossTable(Stream docStream)
      {
        this.m_objects = new Dictionary<long, PdfCrossTable.RegisteredObject>();
        this.m_objNumbers = new Stack<PdfReference>();
        this.m_stream = docStream != null ? docStream : throw new ArgumentNullException("stream");
        this.m_crossTable = new CrossTable(docStream, this);
      }

      internal PdfCrossTable(int count, PdfDictionary encryptionDictionary)
        : this()
      {
        this.m_storedCount = count;
        this.m_bForceNew = true;
        this.m_encryptorDictionary = encryptionDictionary;
      }

      internal void Close(bool completely)
      {
        if (completely)
        {
          if (this.m_archives != null)
          {
            this.m_archives.Clear();
            this.m_archives = (List<PdfCrossTable.ArchiveInfo>) null;
          }
          if (this.m_archive != null)
          {
            this.m_archive.Clear();
            this.m_archive = (PdfArchiveStream) null;
          }
          if (((this.m_items == null ? 0 : (this.m_items.Count > 0 ? 1 : 0)) & (completely ? 1 : 0)) != 0)
          {
            for (int index = this.m_items.Count - 1; index >= 0; --index)
            {
              PdfMainObjectCollection.ObjectInfo objectInfo = this.m_items[index];
              this.m_items.Remove(index);
              if (objectInfo.Object is PdfStream)
                (objectInfo.Object as PdfStream).Clear();
              else if (objectInfo.Object is PdfCatalog)
                (objectInfo.Object as PdfCatalog).Clear();
              else if (objectInfo.Object is PdfArray)
                (objectInfo.Object as PdfArray).Clear();
            }
          }
          this.m_preReference = (List<PdfReference>) null;
          if (this.m_pageCorrespondance != null)
          {
            this.m_pageCorrespondance.Clear();
            this.m_pageCorrespondance = (Dictionary<IPdfPrimitive, object>) null;
          }
        }
        this.Dispose();
      }

      private void Decrypt(IPdfPrimitive obj)
      {
        PdfDictionary pdfDictionary = obj as PdfDictionary;
        PdfArray pdfArray = obj as PdfArray;
        if (pdfDictionary != null && !pdfDictionary.IsDecrypted)
        {
          foreach (IPdfPrimitive pdfPrimitive in (IEnumerable) pdfDictionary.Values)
            this.Decrypt(pdfPrimitive);
          this.Decrypt(pdfDictionary as IPdfDecryptable);
        }
        else if (pdfArray != null)
        {
          foreach (IPdfPrimitive pdfPrimitive in pdfArray)
          {
            PdfName pdfName = pdfPrimitive as PdfName;
            if (pdfName != (PdfName) null && pdfName.Value.Equals("Indexed"))
              this.m_isColorSpace = true;
            this.Decrypt(pdfPrimitive);
          }
          this.m_isColorSpace = false;
        }
        else if (obj is PdfString)
        {
          PdfString pdfString = obj as PdfString;
          if ((pdfString.Decrypted || pdfString.Hex) && (pdfString.Decrypted || !this.m_isColorSpace || pdfString.IsPacked))
            return;
          this.Decrypt(obj as IPdfDecryptable);
        }
        else
          this.Decrypt(obj as IPdfDecryptable);
      }

      private void Decrypt(IPdfDecryptable obj)
      {
        if (!this.Document.WasEncrypted || obj == null || obj.Decrypted || this.m_objNumbers.Count <= 0 || this.Encryptor == null)
          return;
        PdfEncryptor encryptor = this.Encryptor;
        long objNum = this.m_objNumbers.Peek().ObjNum;
        obj.Decrypt(encryptor, objNum);
      }

      public static IPdfPrimitive Dereference(IPdfPrimitive obj)
      {
        PdfReferenceHolder pdfReferenceHolder = obj as PdfReferenceHolder;
        if (pdfReferenceHolder != (PdfReferenceHolder) null)
          obj = pdfReferenceHolder.Object;
        return obj;
      }

      public void Dispose() => this.Dispose(true);

      public void Dispose(bool completely)
      {
        if (this.m_bDisposed)
          return;
        if (this.m_stream != null)
        {
          this.m_stream.Dispose();
          this.m_stream = (Stream) null;
        }
        if (this.m_objects != null)
        {
          this.m_objects.Clear();
          this.m_objects = (Dictionary<long, PdfCrossTable.RegisteredObject>) null;
        }
        this.m_crossTable = (CrossTable) null;
        this.m_documentCatalog = (PdfDictionary) null;
        this.m_trailer = (IPdfPrimitive) null;
        this.m_document = (PdfDocumentBase) null;
        this.m_bDisposed = true;
        this.m_items = (PdfMainObjectCollection) null;
      }

      private void DoArchiveObject(IPdfPrimitive obj, PdfReference reference, PdfWriter writer)
      {
        if (this.m_archive == null)
        {
          this.m_archive = new PdfArchiveStream(this.m_document);
          this.SaveArchive(writer);
        }
        int objCount = this.m_archive.ObjCount;
        this.RegisterObject(this.m_archive, reference);
        this.m_archive.SaveObject(obj, reference);
        if (this.m_archive.ObjCount < 100)
          return;
        this.m_archive = (PdfArchiveStream) null;
      }

      private void DoSaveObject(IPdfPrimitive obj, PdfReference reference, PdfWriter writer)
      {
        long length = writer.Length;
        if (writer.GetStream().CanSeek && writer.Position != length)
          writer.Position = length;
        writer.Write(reference.ObjNum.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        writer.Write(" ");
        writer.Write(reference.GenNum.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        writer.Write(" ");
        writer.Write(nameof (obj));
        writer.Write("\r\n");
        lock (PdfDocument.Cache)
          obj.Save((IPdfWriter) writer);
        if ((object) (obj as PdfName) != null || obj is PdfNumber || obj is PdfNull)
          writer.Write("\r\n");
        if (writer.GetStream().CanRead)
        {
          Stream stream = writer.GetStream();
          BinaryReader binaryReader = new BinaryReader(stream);
          if (binaryReader.BaseStream.CanRead)
          {
            binaryReader.BaseStream.Position = stream.Length - 1L;
            if (binaryReader.ReadChar() != '\n')
              writer.Write("\r\n");
          }
        }
        writer.Write("endobj");
        writer.Write("\r\n");
      }

      ~PdfCrossTable() => this.Dispose(false);

      private PdfReference FindArchiveReference(PdfArchiveStream archive)
      {
        int index = 0;
        PdfCrossTable.ArchiveInfo archiveInfo = (PdfCrossTable.ArchiveInfo) null;
        for (int count = this.m_archives.Count; index < count; ++index)
        {
          archiveInfo = this.m_archives[index];
          if (archiveInfo.Archive == archive)
            break;
        }
        PdfReference archiveReference = archiveInfo.Reference;
        if (archiveReference == (PdfReference) null)
          archiveReference = new PdfReference((long) this.NextObjNumber, 0);
        archiveInfo.Reference = archiveReference;
        return archiveReference;
      }

      private void ForceIDHex(PdfDictionary trailer)
      {
        if (!(PdfCrossTable.Dereference(trailer["ID"]) is PdfArray pdfArray))
          return;
        foreach (PdfString pdfString in pdfArray)
        {
          pdfString.Encode = PdfString.ForceEncoding.ASCII;
          pdfString.ToHex();
        }
      }

      internal void ForceNew()
      {
        this.m_crossTable.Trailer.Remove("Size");
        this.m_crossTable.Trailer.Remove("Prev");
        if (this.m_count > 0)
          this.m_storedCount = this.m_count;
        this.m_count = 0;
        this.m_bForceNew = true;
      }

      private string GenerateFileVersion(PdfDocumentBase document)
      {
        if (document == null)
          throw new ArgumentNullException(nameof (document));
        return "1." + ((int) document.FileStructure.Version).ToString();
      }

      private PdfDictionary GeneratePagesRoot()
      {
        IPdfPrimitive pdfPrimitive = this.DocumentCatalog["Pages"];
        if (pdfPrimitive == null)
          throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
        return pdfPrimitive is PdfDictionary pdfDictionary ? pdfDictionary : throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
      }

      internal static string GetItem(long offset, long genNumber, bool isFree)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(offset.ToString("0000000000 "));
        stringBuilder.Append(((ushort) genNumber).ToString("00000 "));
        stringBuilder.Append(isFree ? "f" : "n");
        stringBuilder.Append("\r\n");
        return stringBuilder.ToString();
      }

      private PdfReference GetMappedReference(PdfReference reference)
      {
        if (reference == (PdfReference) null)
          return (PdfReference) null;
        if (this.m_mappedReferences == null)
          this.m_mappedReferences = new Dictionary<PdfReference, PdfReference>(100);
        PdfReference mappedReference = this.m_mappedReferences.ContainsKey(reference) ? this.m_mappedReferences[reference] : (PdfReference) null;
        if (mappedReference == (PdfReference) null)
        {
          mappedReference = new PdfReference((long) this.NextObjNumber, 0);
          this.m_mappedReferences[reference] = mappedReference;
        }
        return mappedReference;
      }

      public IPdfPrimitive GetObject(IPdfPrimitive pointer)
      {
        IPdfPrimitive pdfPrimitive = pointer;
        if ((object) (pointer as PdfReferenceHolder) != null)
          pdfPrimitive = (pointer as PdfReferenceHolder).Object;
        else if ((object) (pointer as PdfReference) != null)
        {
          PdfReference reference = pointer as PdfReference;
          this.m_objNumbers.Push(pointer as PdfReference);
          IPdfPrimitive element = this.PageProceed(this.m_crossTable == null ? this.PdfObjects.GetObject(this.PdfObjects.GetObjectIndex(reference)) : this.m_crossTable.GetObject(pointer));
          PdfMainObjectCollection pdfObjects = this.PdfObjects;
          if (element != null && !pdfObjects.Contains(element))
          {
            if (pdfObjects.ContainsReference(reference))
            {
              int objectIndex = pdfObjects.GetObjectIndex(reference);
              element = pdfObjects.GetObject(objectIndex);
            }
            else
            {
              pdfObjects.Add(element, reference);
              if (!this.m_isMerging)
              {
                element.Position = -1;
                reference.Position = -1;
              }
            }
          }
          pdfPrimitive = element;
          if (this.Document.WasEncrypted)
            this.Decrypt(pdfPrimitive);
        }
        if (this.Document.WasEncrypted)
          this.Decrypt(pdfPrimitive as IPdfDecryptable);
        if ((object) (pointer as PdfReference) != null)
          this.m_objNumbers.Pop();
        return pdfPrimitive;
      }

      internal PdfReference GetReference(IPdfPrimitive obj) => this.GetReference(obj, out bool _);

      internal PdfReference GetReference(IPdfPrimitive obj, out bool bNew)
      {
        bool flag = false;
        if (obj is PdfArchiveStream)
        {
          PdfReference archiveReference = this.FindArchiveReference(obj as PdfArchiveStream);
          bNew = flag;
          return archiveReference;
        }
        if ((object) (obj as PdfReferenceHolder) != null)
        {
          obj = (obj as PdfReferenceHolder).Object;
          if (this.m_document is PdfDocument)
            obj.IsSaving = true;
        }
        if (obj is IPdfWrapper)
          obj = (obj as IPdfWrapper).Element;
        PdfReference reference = (PdfReference) null;
        bool isNew;
        if (obj.IsSaving)
        {
          if (this.m_items.Count > 0 && obj.ObjectCollectionIndex > 0 && this.m_items.Count > obj.ObjectCollectionIndex - 1)
            reference = !this.m_items[obj.ObjectCollectionIndex - 1].Equals((object) obj) ? this.Document.PdfObjects.GetReference(obj, out isNew) : this.Document.PdfObjects.GetReference(obj.ObjectCollectionIndex - 1);
        }
        else
          reference = this.Document.PdfObjects.GetReference(obj, out isNew);
        isNew = reference == (PdfReference) null && obj.Status != ObjectStatus.Registered;
        if (this.m_bForceNew)
        {
          if (reference == (PdfReference) null)
          {
            long objNum = this.m_storedCount > 0 ? (long) this.m_storedCount++ : (long) this.Document.PdfObjects.Count;
            if (objNum <= 0L)
            {
              objNum = 1L;
              this.m_storedCount = 2;
            }
            reference = new PdfReference(objNum, 0);
            if (isNew)
            {
              this.Document.PdfObjects.Add(obj, reference);
              if (!this.m_isMerging)
              {
                obj.Position = -1;
                reference.Position = -1;
              }
            }
            else
              this.Document.PdfObjects.TrySetReference(obj, reference, out bool _);
          }
          reference = this.GetMappedReference(reference);
        }
        if (reference == (PdfReference) null)
        {
          reference = new PdfReference((long) this.NextObjNumber, 0);
          bool found;
          if (isNew)
          {
            this.Document.PdfObjects.Add(obj);
            this.Document.PdfObjects.TrySetReference(obj, reference, out found);
            if (!this.m_isMerging)
              obj.Position = -1;
          }
          else
            this.Document.PdfObjects.TrySetReference(obj, reference, out found);
          obj.ObjectCollectionIndex = (int) reference.ObjNum;
          obj.Status = ObjectStatus.None;
          flag = true;
        }
        bNew = flag || this.m_bForceNew;
        return reference;
      }

      private int GetSize(ulong number)
      {
        if (number >= (ulong) uint.MaxValue)
          return 8;
        return number < (ulong) ushort.MaxValue ? (number < (ulong) byte.MaxValue ? 1 : 2) : (number < 16777215UL /*0xFFFFFF*/ ? 3 : 4);
      }

      public byte[] GetStream(IPdfPrimitive streamRef)
      {
        return streamRef != null ? this.m_crossTable.GetStream(streamRef) : throw new ArgumentNullException(nameof (streamRef));
      }

      private bool IsCrossReferenceStream(PdfDocumentBase document)
      {
        if (document == null)
          throw new ArgumentNullException(nameof (document));
        return this.m_crossTable != null ? this.m_crossTable.Trailer is PdfStream : document.FileStructure.CrossReferenceType == PdfCrossReferenceType.CrossReferenceStream;
      }

      private void MarkTrailerReferences()
      {
        foreach (IPdfPrimitive pdfPrimitive in (IEnumerable) this.Trailer.Values)
        {
          PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
          if (pdfReferenceHolder != (PdfReferenceHolder) null && !this.Document.PdfObjects.Contains(pdfReferenceHolder.Object))
          {
            this.Document.PdfObjects.Add(pdfReferenceHolder.Object);
            if (!this.m_isMerging)
              pdfReferenceHolder.Object.Position = -1;
          }
        }
      }

      private IPdfPrimitive PageProceed(IPdfPrimitive obj)
      {
        switch (obj)
        {
          case PdfDictionary pdfDictionary:
            if (!(obj is PdfPage) && pdfDictionary.ContainsKey("Type"))
            {
              IPdfPrimitive pointer = pdfDictionary["Type"];
              if (pointer.GetType().Name == "PdfName" && (this.GetObject(pointer) as PdfName).Value == "Page" && !pdfDictionary.ContainsKey("Kids"))
              {
                obj = ((IPdfWrapper) (this.Document as PdfLoadedDocument).Pages.GetPage(pdfDictionary)).Element;
                PdfMainObjectCollection pdfObjects = this.Document.PdfObjects;
                int oldObjIndex = pdfObjects.IndexOf((IPdfPrimitive) pdfDictionary);
                if (oldObjIndex >= 0)
                {
                  pdfObjects.ReregisterReference(oldObjIndex, obj);
                  if (!this.m_isMerging)
                  {
                    obj.Position = -1;
                    break;
                  }
                  break;
                }
                break;
              }
              break;
            }
            break;
        }
        return obj;
      }

      private long PrepareSubsection(ref long objectNum)
      {
        long num1 = 0;
        int num2 = this.Count;
        if (num2 <= 0)
          num2 = this.Document.PdfObjects.Count + 1;
        if (objectNum < (long) num2)
        {
          long key = objectNum;
          while (key < (long) num2 && !this.m_objects.ContainsKey(key))
            ++key;
          objectNum = key;
          for (; key < (long) num2 && this.m_objects.ContainsKey(key); ++key)
            ++num1;
        }
        return num1;
      }

      private PdfStream PrepareXRefStream(long prevXRef, long position, out PdfReference reference)
      {
        if (!(this.Trailer is PdfStream trailer1))
        {
          trailer1 = new PdfStream();
        }
        else
        {
          trailer1.Remove("Filter");
          trailer1.Remove("DecodeParms");
        }
        PdfArray pdfArray = new PdfArray();
        reference = new PdfReference((long) this.NextObjNumber, 0);
        this.RegisterObject(position, reference);
        long objectNum = 0;
        int[] numArray = new int[3]
        {
          1,
          Math.Max(this.GetSize((ulong) position), this.GetSize((ulong) this.Count)),
          this.GetSize((ulong) this.m_maxGenNumIndex)
        };
        using (MemoryStream output = new MemoryStream(100))
        {
          using (BinaryWriter xRefStream = new BinaryWriter((Stream) output))
          {
            long count;
            for (; (count = this.PrepareSubsection(ref objectNum)) > 0L; objectNum += count)
            {
              pdfArray.Add((IPdfPrimitive) new PdfNumber(objectNum));
              pdfArray.Add((IPdfPrimitive) new PdfNumber(count));
              this.SaveSubsection(xRefStream, objectNum, count, numArray);
            }
            xRefStream.Flush();
            trailer1.Data = output.ToArray();
          }
        }
        trailer1["Index"] = (IPdfPrimitive) pdfArray;
        trailer1["Size"] = (IPdfPrimitive) new PdfNumber(this.Count);
        if (prevXRef != 0L)
          trailer1["Prev"] = (IPdfPrimitive) new PdfNumber(prevXRef);
        trailer1["Type"] = (IPdfPrimitive) new PdfName("XRef");
        trailer1["W"] = (IPdfPrimitive) new PdfArray(numArray);
        if (this.m_crossTable != null)
        {
          PdfDictionary trailer2 = this.m_crossTable.Trailer;
          foreach (PdfName key in (IEnumerable) trailer2.Keys)
          {
            if (!trailer1.ContainsKey(key) && key.Value != "DecodeParms" && key.Value != "Filter")
              trailer1[key] = trailer2[key];
          }
        }
        this.ForceIDHex((PdfDictionary) trailer1);
        trailer1.Encrypt = false;
        return trailer1;
      }

      public void RegisterObject(PdfArchiveStream archive, PdfReference reference)
      {
        this.m_objects[reference.ObjNum] = new PdfCrossTable.RegisteredObject(this, archive, reference);
        this.m_maxGenNumIndex = Math.Max(this.m_maxGenNumIndex, (long) archive.Count);
      }

      public void RegisterObject(long offset, PdfReference reference)
      {
        this.m_objects[reference.ObjNum] = !(reference == (PdfReference) null) ? new PdfCrossTable.RegisteredObject(offset, reference) : throw new ArgumentNullException(nameof (reference));
        this.m_maxGenNumIndex = Math.Max(this.m_maxGenNumIndex, (long) reference.GenNum);
      }

      public void RegisterObject(long offset, PdfReference reference, bool free)
      {
        if (reference == (PdfReference) null)
          throw new ArgumentNullException(nameof (reference));
        this.m_objects[reference.ObjNum] = new PdfCrossTable.RegisteredObject(offset, reference, free);
        this.m_maxGenNumIndex = Math.Max(this.m_maxGenNumIndex, (long) reference.GenNum);
      }

      public void Save(PdfWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        this.SaveHead(writer);
        bool flag = false;
        PdfSecurity security = this.m_document.Security;
        this.m_mappedReferences = (Dictionary<PdfReference, PdfReference>) null;
        if (this.m_archives != null)
          this.m_archives.Clear();
        this.m_archive = (PdfArchiveStream) null;
        if (this.m_objects != null)
          this.m_objects.Clear();
        this.MarkTrailerReferences();
        if (this.m_document.FileStructure.CrossReferenceType == PdfCrossReferenceType.CrossReferenceTable && security != null && security.Enabled && security.Encryptor.Encrypt && this.m_document is PdfDocument && security.Encryptor.UserPassword.Length == 0 && security.Encryptor.OwnerPassword.Length == 0)
        {
          flag = security.Enabled;
          security.Enabled = false;
        }
        this.SaveObjects(writer);
        if (this.m_document.FileStructure.CrossReferenceType == PdfCrossReferenceType.CrossReferenceTable && security != null && security.Enabled && security.Encryptor.Encrypt && this.m_document is PdfDocument && security.Encryptor.UserPassword.Length == 0 && security.Encryptor.OwnerPassword.Length == 0)
          security.Enabled = flag;
        int count = this.Count;
        this.SaveArchives(writer);
        if (writer.GetStream().CanSeek)
          writer.Position = writer.Length;
        long position = writer.Position;
        this.RegisterObject(0L, new PdfReference(0L, -1), true);
        long xrefOffset = this.m_crossTable == null ? 0L : this.m_crossTable.XRefOffset;
        long prevXRef = this.m_bForceNew ? 0L : xrefOffset;
        if (this.IsCrossReferenceStream(writer.Document))
        {
          PdfReference reference;
          PdfStream pdfStream = this.PrepareXRefStream(prevXRef, position, out reference);
          pdfStream.BlockEncryption();
          this.DoSaveObject((IPdfPrimitive) pdfStream, reference, writer);
        }
        else
        {
          writer.Write("xref");
          writer.Write("\r\n");
          this.SaveSections(writer);
          this.SaveTrailer(writer, (long) this.Count, prevXRef);
        }
        this.SaveTheEndess(writer, position);
        this.Count = count;
        for (int index = 0; index < this.ObjectCollection.Count; ++index)
          this.ObjectCollection[index].Object.IsSaving = false;
      }

      private void SaveArchive(PdfWriter writer)
      {
        PdfCrossTable.ArchiveInfo archiveInfo = new PdfCrossTable.ArchiveInfo((PdfReference) null, this.m_archive);
        if (this.m_archives == null)
          this.m_archives = new List<PdfCrossTable.ArchiveInfo>(10);
        this.m_archives.Add(archiveInfo);
      }

      private void SaveArchives(PdfWriter writer)
      {
        if (this.m_archives == null)
          return;
        foreach (PdfCrossTable.ArchiveInfo archive in this.m_archives)
        {
          PdfReference reference = archive.Reference;
          if (reference == (PdfReference) null)
          {
            reference = new PdfReference((long) this.NextObjNumber, 0);
            archive.Reference = reference;
          }
          this.m_document.CurrentSavingObj = reference;
          this.RegisterObject(writer.Position, reference);
          this.DoSaveObject((IPdfPrimitive) archive.Archive, reference, writer);
        }
      }

      private void SaveHead(PdfWriter writer)
      {
        byte[] data = new byte[5]
        {
          (byte) 37,
          (byte) 131,
          (byte) 146,
          (byte) 250,
          (byte) 254
        };
        writer.Write("%PDF-");
        string fileVersion = this.GenerateFileVersion(writer.Document);
        writer.Write(fileVersion);
        writer.Write("\r\n");
        writer.Write(data);
        writer.Write("\r\n");
      }

      internal void SaveIndirectObject(IPdfPrimitive obj, PdfWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        if (obj == null)
          throw new ArgumentNullException(nameof (obj));
        if (obj is PdfEncryptor && !(obj as PdfEncryptor).Encrypt)
          return;
        PdfReference reference = this.GetReference(obj);
        if (obj is PdfCatalog)
        {
          this.Trailer["Root"] = (IPdfPrimitive) reference;
          if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B || PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_X1A2001)
            this.Trailer["ID"] = (IPdfPrimitive) this.m_document.Security.Encryptor.FileID;
        }
        this.m_document.CurrentSavingObj = reference;
        bool flag = !(obj is PdfDictionary) || (obj as PdfDictionary).Archive;
        if (!(obj is PdfStream) & flag && !(obj is PdfCatalog) && !(obj is Pdf3DStream) && this.IsCrossReferenceStream(writer.Document) && reference.GenNum == 0)
        {
          this.DoArchiveObject(obj, reference, writer);
        }
        else
        {
          this.RegisterObject(writer.Position, reference);
          this.DoSaveObject(obj, reference, writer);
          if (obj != this.m_archive)
            return;
          this.m_archive = (PdfArchiveStream) null;
        }
      }

      private void SaveLong(BinaryWriter xRefStream, long number, int count)
      {
        for (int index = count - 1; index >= 0; --index)
        {
          byte num = (byte) ((ulong) (number >> (index << 3)) & (ulong) byte.MaxValue);
          xRefStream.Write(num);
        }
      }

      private void SaveObjects(PdfWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        PdfMainObjectCollection objectCollection = this.ObjectCollection;
        if (this.m_bForceNew)
        {
          this.Count = 1;
          this.m_mappedReferences = (Dictionary<PdfReference, PdfReference>) null;
        }
        this.SetSecurity();
        for (int index = 0; index < objectCollection.Count; ++index)
        {
          PdfMainObjectCollection.ObjectInfo objectInfo = objectCollection[index];
          if (objectInfo.Modified || this.m_bForceNew)
          {
            IPdfPrimitive pdfPrimitive = objectInfo.Object;
            if (this.Document is PdfDocument)
              pdfPrimitive.IsSaving = true;
            if (pdfPrimitive != this.Trailer)
              this.SaveIndirectObject(pdfPrimitive, writer);
          }
        }
      }

      private void SaveSections(PdfWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        long objectNum = 0;
        long count;
        do
        {
          count = this.PrepareSubsection(ref objectNum);
          this.SaveSubsection(writer, objectNum, count);
          objectNum += count;
        }
        while (count != 0L);
      }

      private void SaveSubsection(PdfWriter writer, long objectNum, long count)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        if (count <= 0L || objectNum >= (long) this.Count)
          return;
        writer.Write($"{objectNum} {count}{"\r\n"}");
        for (long key = objectNum; key < objectNum + count; ++key)
        {
          PdfCrossTable.RegisteredObject registeredObject = this.m_objects[key];
          string text = PdfCrossTable.GetItem(registeredObject.Offset, (long) registeredObject.GenerationNumber, registeredObject.Type == CrossTable.ObjectType.Free);
          writer.Write(text);
        }
      }

      private void SaveSubsection(BinaryWriter xRefStream, long objectNum, long count, int[] format)
      {
        for (long key = objectNum; key < objectNum + count; ++key)
        {
          PdfCrossTable.RegisteredObject registeredObject = this.m_objects[key];
          xRefStream.Write((byte) registeredObject.Type);
          switch (registeredObject.Type)
          {
            case CrossTable.ObjectType.Free:
              this.SaveLong(xRefStream, registeredObject.ObjectNumber, format[1]);
              this.SaveLong(xRefStream, (long) registeredObject.GenerationNumber, format[2]);
              break;
            case CrossTable.ObjectType.Normal:
              this.SaveLong(xRefStream, registeredObject.Offset, format[1]);
              this.SaveLong(xRefStream, (long) registeredObject.GenerationNumber, format[2]);
              break;
            case CrossTable.ObjectType.Packed:
              this.SaveLong(xRefStream, registeredObject.ObjectNumber, format[1]);
              this.SaveLong(xRefStream, registeredObject.Offset, format[2]);
              break;
            default:
              throw new PdfDocumentException("Internal error: Undefined object type.");
          }
        }
      }

      private void SaveTheEndess(PdfWriter writer, long xrefPos)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        writer.Write("\r\nstartxref\r\n");
        writer.Write(xrefPos.ToString() + "\r\n");
        writer.Write("%%EOF\r\n");
      }

      private void SaveTrailer(PdfWriter writer, long count, long prevXRef)
      {
        if (writer == null)
          throw new ArgumentNullException(nameof (writer));
        writer.Write("trailer\r\n");
        PdfDictionary trailer = this.Trailer;
        if (prevXRef != 0L)
          trailer["Prev"] = (IPdfPrimitive) new PdfNumber(prevXRef);
        this.ForceIDHex(trailer);
        trailer["Size"] = (IPdfPrimitive) new PdfNumber(this.m_count);
        new PdfDictionary(trailer) { Encrypt = false }.Save((IPdfWriter) writer);
      }

      private void SetSecurity()
      {
        PdfSecurity security = this.m_document.Security;
        this.Trailer.Encrypt = false;
        if (!security.Encryptor.Encrypt)
          return;
        PdfDictionary pdfDictionary = this.EncryptorDictionary;
        if (pdfDictionary == null)
        {
          pdfDictionary = new PdfDictionary();
          pdfDictionary.Encrypt = false;
          this.m_document.PdfObjects.Add((IPdfPrimitive) pdfDictionary);
          if (!this.m_isMerging)
            pdfDictionary.Position = -1;
          this.Trailer["Encrypt"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary);
        }
        security.Encryptor.SaveToDictionary(pdfDictionary);
        this.Trailer["ID"] = (IPdfPrimitive) security.Encryptor.FileID;
        this.Trailer["Encrypt"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary);
      }

      internal int Count
      {
        get
        {
          if (this.m_count == 0)
          {
            IPdfPrimitive pdfPrimitive = (IPdfPrimitive) null;
            if (this.m_crossTable != null)
              pdfPrimitive = this.m_crossTable.Trailer["Size"];
            this.m_count = (pdfPrimitive == null ? new PdfNumber(0) : PdfCrossTable.Dereference(pdfPrimitive) as PdfNumber).IntValue;
          }
          return this.m_count;
        }
        set
        {
          this.m_count = value != 0 ? value : throw new ArgumentException("The value can't be 0.", nameof (Count));
        }
      }

      internal CrossTable CrossTable => this.m_crossTable;

      internal PdfDocumentBase Document
      {
        get => this.m_document;
        set
        {
          this.m_document = value != null ? value : throw new ArgumentNullException(nameof (Document));
          this.m_items = this.m_document.PdfObjects;
        }
      }

      public PdfDictionary DocumentCatalog
      {
        get
        {
          if (this.m_documentCatalog == null && this.m_crossTable != null)
            this.m_documentCatalog = PdfCrossTable.Dereference((IPdfPrimitive) this.m_crossTable.DocumentCatalog) as PdfDictionary;
          return this.m_documentCatalog;
        }
      }

      internal bool Encrypted
      {
        get => this.m_bEncrypt;
        set => this.m_bEncrypt = value;
      }

      internal PdfEncryptor Encryptor
      {
        get => this.m_crossTable.Encryptor;
        set
        {
          this.m_crossTable.Encryptor = value != null ? value.Clone() : throw new ArgumentNullException(nameof (Encryptor));
        }
      }

      internal PdfDictionary EncryptorDictionary
      {
        get
        {
          if (this.m_encryptorDictionary == null)
          {
            this.m_bEncrypt = true;
            this.m_encryptorDictionary = PdfCrossTable.Dereference(this.Trailer["Encrypt"]) as PdfDictionary;
          }
          this.m_bEncrypt = false;
          return this.m_encryptorDictionary;
        }
      }

      internal bool IsMerging
      {
        get => this.m_isMerging;
        set => this.m_isMerging = value;
      }

      internal int NextObjNumber
      {
        get
        {
          if (this.Count == 0)
            ++this.Count;
          int count;
          this.Count = (count = this.Count) + 1;
          return count;
        }
      }

      private PdfMainObjectCollection ObjectCollection => this.m_document.PdfObjects;

      internal Dictionary<IPdfPrimitive, object> PageCorrespondance
      {
        get
        {
          if (this.m_pageCorrespondance == null)
            this.m_pageCorrespondance = new Dictionary<IPdfPrimitive, object>();
          return this.m_pageCorrespondance;
        }
        set => this.m_pageCorrespondance = value;
      }

      internal PdfMainObjectCollection PdfObjects => this.m_items;

      internal List<PdfReference> PrevReference
      {
        get
        {
          if (this.m_preReference == null)
            this.m_preReference = new List<PdfReference>();
          return this.m_preReference;
        }
        set => this.m_preReference = value;
      }

      internal Stream Stream => this.m_crossTable.Stream;

      internal bool StructureAltered => this.m_crossTable.IsStructureAltered;

      internal PdfDictionary Trailer
      {
        get
        {
          if (this.m_trailer == null)
            this.m_trailer = this.m_crossTable == null ? (IPdfPrimitive) new PdfStream() : (IPdfPrimitive) this.m_crossTable.Trailer;
          if ((this.m_trailer as PdfDictionary).ContainsKey("XRefStm"))
            (this.m_trailer as PdfDictionary).Remove(new PdfName("XRefStm"));
          return this.m_trailer as PdfDictionary;
        }
      }

      internal class ArchiveInfo
      {
        public PdfArchiveStream Archive;
        public PdfReference Reference;

        public ArchiveInfo(PdfReference reference, PdfArchiveStream archive)
        {
          this.Reference = reference;
          this.Archive = archive;
        }
      }

      public class RegisteredObject
      {
        public int GenerationNumber;
        private PdfArchiveStream m_archive;
        private long m_objectNumber;
        private long m_offset;
        private PdfCrossTable m_xrefTable;
        public CrossTable.ObjectType Type;

        public RegisteredObject(long offset, PdfReference reference)
        {
          if (reference == (PdfReference) null)
            throw new ArgumentNullException(nameof (reference));
          this.m_offset = offset;
          this.GenerationNumber = reference.GenNum;
          this.m_objectNumber = reference.ObjNum;
          this.Type = CrossTable.ObjectType.Normal;
        }

        public RegisteredObject(
          PdfCrossTable xrefTable,
          PdfArchiveStream archive,
          PdfReference reference)
        {
          this.m_xrefTable = xrefTable;
          this.m_archive = archive;
          this.m_offset = reference.ObjNum;
          this.Type = CrossTable.ObjectType.Packed;
        }

        public RegisteredObject(long offset, PdfReference reference, bool free)
          : this(offset, reference)
        {
          if (reference == (PdfReference) null)
            throw new ArgumentNullException(nameof (reference));
          this.Type = free ? CrossTable.ObjectType.Free : CrossTable.ObjectType.Normal;
        }

        internal long ObjectNumber
        {
          get
          {
            if (this.m_objectNumber == 0L && this.m_archive != null)
              this.m_objectNumber = this.m_xrefTable.GetReference((IPdfPrimitive) this.m_archive).ObjNum;
            return this.m_objectNumber;
          }
        }

        internal long Offset
        {
          get => this.m_archive != null ? (long) this.m_archive.GetIndex(this.m_offset) : this.m_offset;
        }
      }
    }
}
