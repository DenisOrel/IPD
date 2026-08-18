// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.CrossTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.IO;

internal class CrossTable
{
  private Dictionary<long, PdfStream> m_archives = new Dictionary<long, PdfStream>();
  private PdfCrossTable m_crossTable;
  private PdfReferenceHolder m_documentCatalog;
  private PdfEncryptor m_encryptor;
  private const int m_generationNumber = 65535 /*0xFFFF*/;
  internal long m_initialNumberOfSubsection;
  private bool m_isStructureAltered;
  internal Dictionary<long, CrossTable.ObjectInformation> m_objects;
  private PdfParser m_parser;
  private PdfReader m_reader;
  private Dictionary<PdfStream, PdfParser> m_readersTable = new Dictionary<PdfStream, PdfParser>();
  private long m_startXRef;
  private Stream m_stream;
  internal long m_totalNumberOfSubsection;
  private PdfDictionary m_trailer;

  public CrossTable(Stream docStream, PdfCrossTable crossTable)
  {
    if (docStream == null)
      throw new ArgumentNullException(nameof (docStream));
    if (!docStream.CanSeek || !docStream.CanRead)
      throw new PdfDocumentException("Ivalid stream.");
    if (crossTable == null)
      throw new ArgumentNullException(nameof (crossTable));
    this.m_stream = docStream;
    int num1 = this.CheckJunk();
    this.m_crossTable = crossTable;
    this.m_objects = new Dictionary<long, CrossTable.ObjectInformation>();
    PdfReader reader = this.Reader;
    PdfParser pdfParser = this.Parser;
    reader.Position = (long) num1;
    reader.SkipWS();
    long position = reader.Position;
    long num2 = reader.Seek(0L, SeekOrigin.End);
    long num3 = reader.SearchBack("%%EOF");
    if (num2 != num3 + 5L)
    {
      reader.Position = num3 + 5L;
      string nextToken = reader.GetNextToken();
      if (nextToken != string.Empty && nextToken[0] != char.MinValue)
      {
        MemoryStream memoryStream = new MemoryStream();
        this.m_stream.Position = 0L;
        byte[] buffer = new byte[num3 + 5L];
        this.m_stream.Read(buffer, 0, buffer.Length);
        memoryStream.Write(buffer, 0, buffer.Length);
        reader = new PdfReader((Stream) memoryStream);
        pdfParser = new PdfParser(this, reader, this.m_crossTable);
      }
    }
    long offset1 = reader.SearchBack("startxref");
    pdfParser.SetOffset(offset1);
    long offset2 = pdfParser.StartXRef();
    this.m_startXRef = offset2;
    pdfParser.SetOffset(offset2);
    if (position != 0L)
    {
      offset2 = reader.SearchForward("xref");
      pdfParser.SetOffset(offset2);
    }
    string str = reader.ReadLine();
    if (!str.Contains("xref") && !str.Contains("obj"))
    {
      long num4 = reader.SearchBack("xref");
      if (num4 != -1L)
        offset2 = num4;
      pdfParser.SetOffset(offset2);
    }
    reader.Position = offset2;
    this.m_trailer = pdfParser.ParseXRefTable(this.m_objects, this) as PdfDictionary;
    PdfDictionary pdfDictionary = this.m_trailer;
    while (pdfDictionary.ContainsKey("Prev"))
    {
      long intValue = (long) (pdfDictionary["Prev"] as PdfNumber).IntValue;
      PdfReader pdfReader = new PdfReader(this.m_reader.Stream);
      pdfReader.Position = intValue;
      if (!pdfReader.GetNextToken().Equals("xref"))
      {
        if (pdfReader.GetNextToken().Equals("0") && pdfReader.GetNextToken().Equals("obj"))
        {
          pdfParser.SetOffset(intValue);
          pdfDictionary = pdfParser.ParseXRefTable(this.m_objects, this) as PdfDictionary;
        }
        else
        {
          pdfParser.RebuildXrefTable(this.m_objects, this);
          break;
        }
      }
      else
      {
        pdfParser.SetOffset(intValue);
        pdfDictionary = pdfParser.ParseXRefTable(this.m_objects, this) as PdfDictionary;
      }
    }
    if (position == 0L)
      return;
    for (int key = 1; key <= this.m_objects.Count; ++key)
    {
      if (this.m_objects.ContainsKey((long) key))
      {
        CrossTable.ObjectInformation objectInformation = this.m_objects[(long) key];
        this.m_objects[(long) key] = new CrossTable.ObjectInformation(CrossTable.ObjectType.Normal, objectInformation.Offset + position, (CrossTable.ArchiveInformation) null, this);
      }
    }
    this.m_isStructureAltered = true;
  }

  private int CheckJunk()
  {
    byte[] numArray = new byte[this.m_stream.Length];
    this.m_stream.Position = 0L;
    this.m_stream.Read(numArray, 0, (int) this.m_stream.Length);
    int num = Encoding.Default.GetString(numArray).IndexOf("%PDF-");
    this.m_stream.Position = 0L;
    return num;
  }

  public IPdfPrimitive GetObject(IPdfPrimitive pointer)
  {
    if (pointer == null)
      throw new ArgumentNullException(nameof (pointer));
    if ((object) (pointer as PdfReference) == null)
      return pointer;
    PdfReference pdfReference = pointer as PdfReference;
    CrossTable.ObjectInformation objectInformation = this[pdfReference.ObjNum];
    if (objectInformation == null)
      return (IPdfPrimitive) new PdfNull();
    if (this.m_crossTable.Encrypted)
      objectInformation.Parser.Encrypted = true;
    PdfParser parser = objectInformation.Parser;
    long offset = objectInformation.Offset;
    IPdfPrimitive pdfPrimitive;
    if (objectInformation.Obj != null)
      pdfPrimitive = objectInformation.Obj;
    else if (objectInformation.Archive == null)
    {
      pdfPrimitive = parser.Parse(offset);
    }
    else
    {
      pdfPrimitive = this.GetObject(parser, offset);
      if (this.Encryptor != null)
      {
        if (pdfPrimitive is PdfDictionary)
        {
          PdfDictionary pdfDictionary = pdfPrimitive as PdfDictionary;
          pdfDictionary.IsDecrypted = true;
          foreach (object obj in pdfDictionary.Items.Values)
          {
            if (obj is PdfString)
              (obj as PdfString).IsParentDecrypted = true;
          }
        }
        if (pdfPrimitive is PdfArray)
        {
          foreach (object obj in pdfPrimitive as PdfArray)
          {
            if (obj is PdfString && objectInformation.Type == CrossTable.ObjectType.Packed)
              (obj as PdfString).IsPacked = true;
          }
        }
        if (pdfPrimitive is IPdfDecryptable pdfDecryptable)
          pdfDecryptable.Decrypt(this.Encryptor, pdfReference.ObjNum);
      }
    }
    objectInformation.Obj = pdfPrimitive;
    return pdfPrimitive;
  }

  private IPdfPrimitive GetObject(PdfParser parser, long position)
  {
    parser.StartFrom(position);
    return parser.Simple();
  }

  private List<CrossTable.SubSection> GetSections(PdfStream stream)
  {
    List<CrossTable.SubSection> sections = new List<CrossTable.SubSection>();
    int intValue1 = (stream["Size"] as PdfNumber).IntValue;
    if (intValue1 == 0)
      throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
    IPdfPrimitive pointer = stream["Index"];
    if (pointer == null)
    {
      sections.Add(new CrossTable.SubSection(intValue1));
      return sections;
    }
    if (!(this.GetObject(pointer) is PdfArray pdfArray))
      throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
    if ((pdfArray.Count & 1) != 0)
      throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
    int index1;
    for (int index2 = 0; index2 < pdfArray.Count; index2 = index1 + 1)
    {
      int intValue2 = (pdfArray[index2] as PdfNumber).IntValue;
      index1 = index2 + 1;
      int intValue3 = (pdfArray[index1] as PdfNumber).IntValue;
      sections.Add(new CrossTable.SubSection(intValue2, intValue3));
    }
    return sections;
  }

  public byte[] GetStream(IPdfPrimitive streamRef)
  {
    if (streamRef == null)
      throw new ArgumentNullException(nameof (streamRef));
    return this.GetObject(streamRef) is PdfStream pdfStream ? pdfStream.Data : (byte[]) null;
  }

  internal void ParseNewTable(
    PdfStream stream,
    Dictionary<long, CrossTable.ObjectInformation> hashTable)
  {
    if (stream == null)
      throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
    stream.Decompress();
    List<CrossTable.SubSection> sections = this.GetSections(stream);
    int startIndex = 0;
    foreach (CrossTable.SubSection subsection in sections)
      startIndex = this.ParseSubsection(stream, subsection, hashTable, startIndex);
  }

  internal void ParseSubsection(
    PdfParser parser,
    Dictionary<long, CrossTable.ObjectInformation> table)
  {
    this.m_initialNumberOfSubsection = (long) (parser.Simple() as PdfNumber).IntValue;
    this.m_totalNumberOfSubsection = (long) (parser.Simple() as PdfNumber).IntValue;
    for (int index = 0; (long) index < this.m_totalNumberOfSubsection; ++index)
    {
      long intValue1 = (long) (parser.Simple() as PdfNumber).IntValue;
      int intValue2 = (parser.Simple() as PdfNumber).IntValue;
      if (parser.GetObjectFlag() == 'n')
      {
        CrossTable.ObjectInformation objectInformation = new CrossTable.ObjectInformation(CrossTable.ObjectType.Normal, intValue1, (CrossTable.ArchiveInformation) null, this);
        long key = this.m_initialNumberOfSubsection + (long) index;
        if (!table.ContainsKey(key))
          table[key] = objectInformation;
      }
      else if (this.m_initialNumberOfSubsection != 0L && intValue1 == 0L && intValue2 == (int) ushort.MaxValue)
        --this.m_initialNumberOfSubsection;
    }
  }

  private int ParseSubsection(
    PdfStream stream,
    CrossTable.SubSection subsection,
    Dictionary<long, CrossTable.ObjectInformation> table,
    int startIndex)
  {
    int subsection1 = startIndex;
    PdfArray pdfArray = this.GetObject(stream["W"]) as PdfArray;
    int count1 = pdfArray.Count;
    int[] numArray1 = new int[count1];
    for (int index = 0; index < count1; ++index)
      numArray1[index] = (pdfArray[index] as PdfNumber).IntValue;
    long[] numArray2 = new long[count1];
    byte[] data = stream.Data;
    int num1 = 0;
    for (int count2 = subsection.Count; num1 < count2; ++num1)
    {
      for (int index1 = 0; index1 < count1; ++index1)
      {
        int num2 = 0;
        for (int index2 = 0; index2 < numArray1[index1]; ++index2)
          num2 = (num2 << 8) + (int) data[subsection1++];
        numArray2[index1] = (long) num2;
      }
      long offset = 0;
      CrossTable.ArchiveInformation arciveInfo = (CrossTable.ArchiveInformation) null;
      if (numArray2[0] == 1L)
        offset = numArray2[1];
      else if (numArray2[0] == 2L)
      {
        arciveInfo = new CrossTable.ArchiveInformation(numArray2[1], numArray2[2], new CrossTable.GetArchive(this.RetrieveArchive));
      }
      else
      {
        PdfReader reader = this.Reader;
        reader.Position = offset;
        string str = reader.ReadLine();
        if (!str.Contains("%") && !str.Contains("obj"))
        {
          reader.Position = 0L;
          offset = reader.SearchForward(num1.ToString() + " 0 obj");
          if (offset != -1L)
            numArray2[0] = 1L;
        }
      }
      CrossTable.ObjectInformation objectInformation = (CrossTable.ObjectInformation) null;
      if (numArray2[0] != 0L)
        objectInformation = new CrossTable.ObjectInformation((CrossTable.ObjectType) numArray2[0], offset, arciveInfo, this);
      if (objectInformation != null)
      {
        long key = (long) (subsection.StartNumber + num1);
        if (!table.ContainsKey(key))
          table[key] = objectInformation;
      }
    }
    return subsection1;
  }

  private PdfStream RetrieveArchive(long archiveNumber)
  {
    PdfStream pdfStream = (PdfStream) null;
    if (this.m_archives.ContainsKey(archiveNumber))
      pdfStream = this.m_archives[archiveNumber];
    if (pdfStream == null)
    {
      CrossTable.ObjectInformation objectInformation = this[archiveNumber];
      pdfStream = objectInformation.Parser.Parse(objectInformation.Offset) as PdfStream;
      pdfStream.Decrypt(this.Encryptor, archiveNumber);
      pdfStream.Decompress();
      this.m_archives[archiveNumber] = pdfStream;
    }
    return pdfStream;
  }

  private PdfParser RetrieveParser(CrossTable.ArchiveInformation archive)
  {
    if (archive == null)
      return this.m_parser;
    PdfStream archive1 = archive.Archive;
    PdfParser pdfParser = (PdfParser) null;
    if (this.m_readersTable.ContainsKey(archive1))
      pdfParser = this.m_readersTable[archive1];
    if (pdfParser == null)
    {
      pdfParser = new PdfParser(this, new PdfReader((Stream) new MemoryStream(archive1.Data, false)), this.m_crossTable);
      this.m_readersTable[archive1] = pdfParser;
    }
    return pdfParser;
  }

  public long Count => (long) this.m_objects.Count;

  public PdfReferenceHolder DocumentCatalog
  {
    get
    {
      if (this.m_documentCatalog == (PdfReferenceHolder) null)
      {
        IPdfPrimitive pdfPrimitive = this.Trailer["Root"];
        this.m_documentCatalog = (object) (pdfPrimitive as PdfReferenceHolder) != null ? pdfPrimitive as PdfReferenceHolder : throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
      }
      return this.m_documentCatalog;
    }
  }

  internal PdfEncryptor Encryptor
  {
    get => this.m_encryptor;
    set
    {
      this.m_encryptor = value != null ? value : throw new ArgumentNullException("m_encryptor");
    }
  }

  internal bool IsStructureAltered => this.m_isStructureAltered;

  internal CrossTable.ObjectInformation this[long index]
  {
    get
    {
      object obj = this.m_objects.ContainsKey(index) ? (object) this.m_objects[index] : (object) (CrossTable.ObjectInformation) null;
      return obj == null ? (CrossTable.ObjectInformation) null : obj as CrossTable.ObjectInformation;
    }
  }

  public PdfParser Parser
  {
    get
    {
      if (this.m_parser == null)
        this.m_parser = new PdfParser(this, this.Reader, this.m_crossTable);
      return this.m_parser;
    }
  }

  public PdfReader Reader
  {
    get
    {
      if (this.m_reader == null)
        this.m_reader = new PdfReader(this.m_stream);
      return this.m_reader;
    }
  }

  internal Stream Stream => this.m_stream;

  internal PdfDictionary Trailer => this.m_trailer;

  internal long XRefOffset => this.m_startXRef;

  internal class ArchiveInformation
  {
    private PdfStream m_archive;
    private long m_archiveNumber;
    private CrossTable.GetArchive m_getArchive;
    private long m_index;

    public ArchiveInformation(long arcNum, long index, CrossTable.GetArchive getArchive)
    {
      this.m_archiveNumber = arcNum;
      this.m_index = index;
      this.m_getArchive = getArchive;
    }

    public PdfStream Archive
    {
      get
      {
        if (this.m_archive == null)
          this.m_archive = this.m_getArchive(this.m_archiveNumber);
        return this.m_archive;
      }
    }

    internal long ArchiveNumber => this.m_archiveNumber;

    public long Index => this.m_index;
  }

  internal delegate PdfStream GetArchive(long archiveNumber);

  internal class ObjectInformation
  {
    private CrossTable.ArchiveInformation m_archive;
    private CrossTable m_crossTable;
    private long m_offset;
    private PdfParser m_parser;
    private CrossTable.ObjectType m_type;
    public IPdfPrimitive Obj;

    public ObjectInformation(
      CrossTable.ObjectType type,
      long offset,
      CrossTable.ArchiveInformation arciveInfo,
      CrossTable crossTable)
    {
      this.m_type = type;
      this.m_offset = offset;
      this.m_archive = arciveInfo;
      this.m_crossTable = crossTable;
    }

    public static implicit operator long(CrossTable.ObjectInformation oi) => oi.Offset;

    public CrossTable.ArchiveInformation Archive => this.m_archive;

    public long Offset
    {
      get
      {
        if (this.m_offset == 0L)
        {
          PdfParser parser = this.Parser;
          parser.StartFrom(0L);
          if (this.Archive != null)
          {
            int intValue = (this.Archive.Archive["N"] as PdfNumber).IntValue;
            int[] numArray = new int[intValue * 2];
            for (int index = 0; index < intValue; ++index)
            {
              IPdfPrimitive pdfPrimitive1 = parser.Simple();
              numArray[index * 2] = (pdfPrimitive1 as PdfNumber).IntValue;
              IPdfPrimitive pdfPrimitive2 = parser.Simple();
              numArray[index * 2 + 1] = (pdfPrimitive2 as PdfNumber).IntValue;
            }
            long index1 = this.Archive.Index;
            if (index1 * 2L >= (long) numArray.Length)
              throw new PdfDocumentException("Missing indexes in archive #" + (object) this.Archive.ArchiveNumber);
            this.m_offset = (long) numArray[(int) (IntPtr) (index1 * 2L + 1L)];
            this.m_offset += (long) (this.Archive.Archive["First"] as PdfNumber).IntValue;
          }
        }
        return this.m_offset;
      }
    }

    public PdfParser Parser
    {
      get
      {
        if (this.m_parser == null)
          this.m_parser = this.m_crossTable.RetrieveParser(this.m_archive);
        return this.m_parser;
      }
    }

    public CrossTable.ObjectType Type => this.m_type;
  }

  internal enum ObjectType
  {
    Free,
    Normal,
    Packed,
  }

  private struct SubSection
  {
    public int StartNumber;
    public int Count;

    public SubSection(int start, int count)
    {
      this.StartNumber = start;
      this.Count = count;
    }

    public SubSection(int count)
    {
      this.StartNumber = 0;
      this.Count = count;
    }
  }
}
