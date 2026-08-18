// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.UEditDocument
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Загрузчик-конвертер для документов UEdit</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Группа владелец</param>
/// <param name="origin">Шаблон</param>
public class UEditDocument(GroupClone owner, RectPrimitive origin) : GroupClone(owner, origin)
{
  /// <summary>BinaryReader</summary>
  public BinaryReader Reader;
  /// <summary>Сигнатура документа</summary>
  public static string DocSign = "TDOC";
  /// <summary>Сигнатура элемента документа</summary>
  public static string CloneSign = "CLON";
  /// <summary>Сигнатура файла</summary>
  public static string File_Sign = "IDOC";
  /// <summary>Сигнатура</summary>
  public static int MagicSign = 858993459 /*0x33333333*/;
  /// <summary>Версия текущего формата файла</summary>
  public int version = 318;
  /// <summary>Имя загружаемого файла</summary>
  public string LoadingFile;
  /// <summary>Версия формата загружаемого файла</summary>
  public int loadingVersion;
  internal int CurrentCloneSize;
  internal long CurrentCloneStartPosition;
  /// <summary>Флаг означает, что это документ Каталога и он может содержать точки перехода (гиперссылки)</summary>
  public bool jumpPoints;
  /// <summary>Бланк в файле документа</summary>
  public bool blankInDoc;
  /// <summary>Имя бланка</summary>
  public string blankName;
  /// <summary>Загрузчик бланка</summary>
  public BlankLoader blank;
  /// <summary>Рабочая область документа</summary>
  public AreaClone PageArea;
  /// <summary>Идентификатор документа</summary>
  public int docId = -1;
  /// <summary>add to the first list num</summary>
  public int listNumPlus;
  /// <summary>Имя документа</summary>
  public string docName;
  /// <summary>Комментарий документа</summary>
  public string docComment;
  /// <summary>Загрузчик библиотеки примитивов</summary>
  [NonSerialized]
  private PrimLibraryLoader primLib;

  /// <summary>Версия текущего формата файла</summary>
  public int Version
  {
    [DebuggerStepThrough] get => this.version;
  }

  /// <summary>Версия формата загружаемого файла</summary>
  public int LoadingVersion
  {
    [DebuggerStepThrough] get => this.loadingVersion;
  }

  internal long CurrentCloneEndPosition
  {
    get => this.CurrentCloneStartPosition + (long) this.CurrentCloneSize;
  }

  internal bool CurrentCloneIsLoaded
  {
    get => this.Reader.BaseStream.Position >= this.CurrentCloneEndPosition;
  }

  /// <summary>Бланк в файле документа</summary>
  public bool BlankInDoc
  {
    [DebuggerStepThrough] get => this.blankInDoc;
  }

  /// <summary>Имя бланка</summary>
  public string BlankName => this.blankName;

  /// <summary>Идентификатор документа</summary>
  public int DocId => this.docId;

  /// <summary>add to the first list num</summary>
  public int ListNumPlus => this.listNumPlus;

  /// <summary>Имя документа</summary>
  public string DocName => this.docName;

  /// <summary>Комментарий документа</summary>
  public string DocComment => this.docComment;

  /// <summary>Загрузчик библиотеки примитивов</summary>
  public PrimLibraryLoader PrimLib
  {
    [DebuggerStepThrough] get
    {
      if (this.primLib == null)
      {
        this.primLib = new PrimLibraryLoader();
        this.primLib.FindAndLoad(Path.GetDirectoryName(this.LoadingFile));
      }
      return this.primLib;
    }
  }

  /// <summary>Загрузить из файла</summary>
  /// <param name="fileName">Имя файла</param>
  public void LoadFromFile(string fileName)
  {
    this.LoadingFile = fileName;
    this.Load((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read), Path.GetFullPath(fileName), (string) null);
  }

  /// <summary>Загрузить из файла</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="defaultPathForBlank">Путь по умолчанию для поиска бланка</param>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public void Load(Stream stream, string defaultPathForBlank, string preReadedHeaderSignature)
  {
    Stream stream1;
    if (stream.CanSeek)
    {
      stream1 = stream;
    }
    else
    {
      stream1 = (Stream) new ImChunkedStream();
      stream.CopyTo(stream1);
      stream1.Position = 0L;
    }
    this.Reader = new BinaryReader(stream1, Encoding.GetEncoding(1251));
    try
    {
      DocHeader docHeader = new DocHeader(4);
      long position = this.Reader.BaseStream.Position;
      if (string.IsNullOrEmpty(preReadedHeaderSignature))
      {
        this.Reader.Read(docHeader.Signature, 0, 4);
      }
      else
      {
        position -= (long) docHeader.Signature.Length;
        docHeader.Signature = preReadedHeaderSignature.ToCharArray();
      }
      if (docHeader.SignatureStr != UEditDocument.File_Sign)
        throw new Exception(LocalizationHolder.rm.GetString("Document.Model_512"));
      docHeader.HeaderLen = this.Reader.ReadUInt16();
      docHeader.VersionNum = this.Reader.ReadUInt16();
      if (this.Reader.BaseStream.Position < (long) docHeader.HeaderLen)
        docHeader.TechData = this.Reader.ReadInt32();
      this.jumpPoints = ((uint) docHeader.VersionNum & 32768U /*0x8000*/) > 0U;
      this.loadingVersion = (int) docHeader.VersionNum & (int) short.MaxValue;
      PrimitiveLoader.GotoEndDataBlock(position, (long) docHeader.HeaderLen, this.Reader);
      this.blankName = this.ReadString();
      this.blankInDoc = this.LoadingVersion >= 120 && this.Reader.ReadBoolean();
      this.blank = new BlankLoader();
      if (this.blankInDoc)
        this.blank.LoadFromStream(this.Reader, (string) null);
      else
        this.blank.FindAndLoad(this.blankName, defaultPathForBlank);
      bool flag = false;
      if (this.PageArea == null)
      {
        for (int index = 0; index < this.blank.PrimitiveList.Count; ++index)
        {
          if (this.blank.PrimitiveList[index] is BlankList primitive && primitive.HasWorkspace)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          this.PageArea = new AreaClone((GroupClone) null, (RectPrimitive) this.blank.WorkSpace);
          this.PageArea.ownerDoc = this;
          this.CreateChildren((GroupClone) this.PageArea, (GroupPrimitive) this.blank.WorkSpace);
        }
      }
      this.docId = this.Reader.ReadInt32();
      this.LoadDoc((string) null);
    }
    finally
    {
      this.Reader.Close();
    }
  }

  /// <summary>Загрузить документ</summary>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public void LoadDoc(string preReadedHeaderSignature)
  {
    long position = this.Reader.BaseStream.Position;
    DocHeader docHeader = new DocHeader(4);
    if (string.IsNullOrEmpty(preReadedHeaderSignature))
    {
      this.Reader.Read(docHeader.Signature, 0, 4);
    }
    else
    {
      position -= (long) docHeader.Signature.Length;
      docHeader.Signature = preReadedHeaderSignature.ToCharArray();
    }
    if (docHeader.SignatureStr != UEditDocument.DocSign)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_513"));
    docHeader.HeaderLen = this.Reader.ReadUInt16();
    docHeader.VersionNum = this.Reader.ReadUInt16();
    if (this.Reader.BaseStream.Position < position + (long) docHeader.HeaderLen)
      docHeader.TechData = this.Reader.ReadInt32();
    PrimitiveLoader.GotoEndDataBlock(position, (long) docHeader.HeaderLen, this.Reader);
    this.docName = this.ReadString();
    this.docComment = this.ReadString();
    this.listNumPlus = this.LoadingVersion < 112 /*0x70*/ ? 0 : this.Reader.ReadInt32();
    this.Reader.ReadInt32();
    this.Load(this);
    if (this.PageArea == null)
      return;
    this.PageArea.Load(this);
  }

  /// <summary>Прочитать строку</summary>
  /// <returns>Прочитанную строку</returns>
  public string ReadString()
  {
    byte count = this.Reader.ReadByte();
    char[] buffer = new char[(int) count];
    this.Reader.Read(buffer, 0, (int) count);
    return new string(buffer);
  }

  /// <summary>Дочерние копии по шаблону</summary>
  /// <param name="clone">Группа</param>
  /// <param name="orig">Шаблон</param>
  public void CreateChildren(GroupClone clone, GroupPrimitive orig)
  {
  }

  /// <summary>Создать дочерний элемент по шаблону</summary>
  /// <param name="parent">Родительский элемент</param>
  /// <param name="prim">Примитив</param>
  /// <returns>Дочерний элемент</returns>
  public CloneBase CreateChild(GroupClone parent, RectPrimitive prim) => (CloneBase) null;

  /// <summary>Проверить дочерние элементы</summary>
  /// <param name="cl">Группа клон</param>
  /// <param name="orig">Группа примитивов</param>
  public void CheckChildren(GroupClone cl, GroupPrimitive orig)
  {
  }

  /// <summary>?</summary>
  /// <param name="parent">?</param>
  /// <param name="prim">?</param>
  public void CheckChild(GroupClone parent, RectPrimitive prim)
  {
  }

  /// <summary>?</summary>
  /// <param name="p">?</param>
  /// <returns>?</returns>
  public bool NeedClone(PrimitiveBase p)
  {
    return (!(p is PictPrimitive) || !(p as PictPrimitive).IsConstant) && p.Id != null && p.Id != "";
  }

  /// <summary>Найти примитив в библиотеке</summary>
  /// <param name="prim">Имя примитива</param>
  /// <returns>Примитив</returns>
  internal RectPrimitive FindStrictUserPrimitive(UserPrimitive prim)
  {
    RectPrimitive strictUserPrimitive = this.PrimLib.GetByName(prim.Name);
    if (strictUserPrimitive != null && prim.Id != strictUserPrimitive.Id)
      strictUserPrimitive = (RectPrimitive) null;
    return strictUserPrimitive;
  }

  /// <summary>?</summary>
  /// <param name="cl">?</param>
  /// <param name="prim">?</param>
  /// <returns>?</returns>
  public CloneBase AddUserPrimitive(GroupClone cl, UserPrimitive prim)
  {
    return this.CreateChild(cl, this.FindStrictUserPrimitive(prim) ?? throw new Exception($"Cant Find Variant {prim.Name} ({prim.Id})"));
  }

  /// <summary>?</summary>
  /// <param name="p">?</param>
  /// <returns>?</returns>
  public Type StandardRef(RectPrimitive p)
  {
    Type type = p.GetType();
    if (type == typeof (AutoText) || type == typeof (TextField))
      return typeof (TextClone);
    if (type == typeof (TablePrimitive))
      return typeof (TableClone);
    if (type == typeof (PictPrimitive))
      return typeof (PictClone);
    if (type == typeof (ContainerPrimitive))
      return typeof (ContainerClone);
    if (type == typeof (Area))
      return typeof (AreaClone);
    if (type == typeof (BlankList))
      return typeof (BlankListClone);
    return type == typeof (OlePrimitive) ? typeof (OLEClone) : (Type) null;
  }

  /// <summary>?</summary>
  /// <param name="p">?</param>
  /// <returns>?</returns>
  public Type GetCloneRef(RectPrimitive p) => this.StandardRef(p);

  /// <summary>?</summary>
  /// <param name="classId">?</param>
  /// <returns>?</returns>
  public Type GetCloneBaseRef(CloneClassId classId)
  {
    switch (classId)
    {
      case CloneClassId.BASE_CLONE:
        return typeof (CloneBase);
      case CloneClassId.GROUP_CLONE:
        return typeof (GroupClone);
      case CloneClassId.TEXT_CLONE:
        return typeof (TextClone);
      case CloneClassId.PICT_CLONE:
        return typeof (PictClone);
      case CloneClassId.TABLE_CLONE:
        return typeof (TableClone);
      case CloneClassId.CONT_CLONE:
        return typeof (ContainerClone);
      case CloneClassId.AREA_CLONE:
        return typeof (AreaClone);
      case CloneClassId.BLIST_CLONE:
        return typeof (BlankListClone);
      case CloneClassId.OLE_CLONE:
        return typeof (OLEClone);
      case CloneClassId.DOCUMENT:
        return typeof (UEditDocument);
      default:
        return (Type) null;
    }
  }

  /// <summary>Загрузить клон</summary>
  /// <param name="cloneOwner">Владелец клона</param>
  /// <returns>Клон</returns>
  public CloneBase LoadClone(GroupClone cloneOwner)
  {
    char[] buffer = new char[4];
    this.Reader.Read(buffer, 0, 4);
    string str = new string(buffer);
    if (new string(buffer) != UEditDocument.CloneSign)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_514"));
    int classId = this.Reader.ReadInt32();
    byte[] numArray = new byte[4];
    this.Reader.Read(numArray, 0, 4);
    string primId = PrimitiveBase.MakeIdStr(numArray);
    PrimitiveBase primitiveBase = !(cloneOwner is UEditDocument) ? (cloneOwner.origin == null ? (PrimitiveBase) null : cloneOwner.origin.FindById(primId)) : (cloneOwner as UEditDocument).blank.PrimWithId(primId);
    RectPrimitive prim = primitiveBase != null ? primitiveBase as RectPrimitive : (RectPrimitive) null;
    int currentCloneSize = this.CurrentCloneSize;
    long cloneStartPosition = this.CurrentCloneStartPosition;
    this.CurrentCloneSize = this.Reader.ReadInt32();
    this.CurrentCloneStartPosition = this.Reader.BaseStream.Position;
    if (prim == null)
    {
      PrimitiveLoader.GotoEndDataBlock(this.CurrentCloneStartPosition, (long) this.CurrentCloneSize, this.Reader);
      return (CloneBase) null;
    }
    if (prim is UserPrimitive)
      prim = this.blank.FindStrictUserPrimitive(prim as UserPrimitive);
    if (classId > 10)
      classId = classId >> 24 & (int) byte.MaxValue;
    Type cloneBaseRef = this.GetCloneBaseRef((CloneClassId) classId);
    CloneBase cloneBase = (CloneBase) null;
    if (cloneBaseRef != (Type) null)
    {
      cloneBase = (CloneBase) Activator.CreateInstance(cloneBaseRef, (object) cloneOwner, (object) prim);
      cloneBase.Load(this);
    }
    PrimitiveLoader.GotoEndDataBlock(this.CurrentCloneStartPosition, (long) this.CurrentCloneSize, this.Reader);
    this.CurrentCloneSize = currentCloneSize;
    this.CurrentCloneStartPosition = cloneStartPosition;
    return cloneBase;
  }

  /// <summary>Создать новый узел документа</summary>
  /// <returns>Узел документа</returns>
  public override DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    ImDocument newDocumentNode = new ImDocument(false);
    if (parentDocNode != null)
    {
      if (this.Id != null && this.Id != "")
        newDocumentNode.Id = this.Id;
      parentDocNode.AddChildNode((DocumentTreeNode) newDocumentNode, false, false);
    }
    this.DocumentNode = (DocumentTreeNode) newDocumentNode;
    this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode);
    return (DocumentTreeNode) newDocumentNode;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    ImDocument imDocument = node as ImDocument;
    TableElement tableElement1 = (TableElement) null;
    imDocument.AssignDocumentTemplate((ImDocumentData) this.blank.GeneateDocument(), false, false, false);
    imDocument.ApplyTemplateProperties(false, false);
    base.InitNewDocumentNode(node);
    Page page = (Page) null;
    bool flag = true;
    TableElement tableElement2 = (TableElement) null;
    for (int index1 = 0; index1 < this.ChildList.Count; ++index1)
    {
      FlowID flow = (FlowID) null;
      if (imDocument.DocumentFlows != null && imDocument.DocumentFlows.Count != 0)
        flow = imDocument.DocumentFlows[0];
      if (this.ChildList[index1].DocumentNode is Page documentNode && this.ChildList[index1] is BlankListClone child && ((BlankList) child.origin).HasWorkspace)
      {
        if (flag)
        {
          flag = false;
          if (flow != null && documentNode.GetFirstFlowElement(flow) is TableElement firstFlowElement)
          {
            int index2 = firstFlowElement.Index;
            DocumentTreeNode parent = firstFlowElement.Parent;
            if (this.blank.WorkSpace != null)
            {
              if (!(this.PageArea.DocumentNode is TableElement tableElement3))
                tableElement3 = firstFlowElement;
              tableElement1 = tableElement3;
              this.PageArea.InitNewDocumentNode((DocumentTreeNode) tableElement1);
            }
            if (tableElement1 != null)
            {
              parent.InsertChildNode(index2, (DocumentTreeNode) tableElement1, false, true, false, false);
              tableElement2 = tableElement1;
            }
          }
        }
        else
        {
          if (documentNode.GetFirstFlowElement(flow) is TableElement firstFlowElement)
          {
            firstFlowElement.SetPrevCell((RectangleElement) tableElement2);
            tableElement2 = firstFlowElement;
          }
          if (page != null)
          {
            page.NextPageTemplateId = child.Id;
            page.NextPage = (PageData) documentNode;
          }
        }
        page = documentNode;
      }
    }
    imDocument.SetAttributeValue(LocalizationHolder.rm.GetString("Document.Model_515"), this.DocComment, false, false, false);
    imDocument.SetName(this.DocName, false, false);
    imDocument.Id = this.DocId.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    imDocument.SetStartPageNumber(this.ListNumPlus + 1, false, false);
    imDocument.UpdateTemplateLinks(false, true, false, false);
    imDocument.UpdateNodeAttributeLinks(true, false, false);
    imDocument.SetPropertiesChangedFlag(false, true, false, false, false);
    imDocument.AssignTreeStructureChangedFlag(false, true);
    imDocument.UpdateLayout(0, false, true);
  }
}
