// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.DocIDCache
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Expert;
using Intermech.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.DBCore;

public class DocIDCache
{
  public static Guid FirstPageNumberInDocumentComplect_Guid = new Guid("cad014b1-306c-11d8-b4e9-00304f19f545");
  public static Guid ComplectPageCount_Guid = new Guid("cadd9978-306c-11d8-b4e9-00304f19f545");
  public static Guid DocumentPageCount_Guid = new Guid("cad014b0-306c-11d8-b4e9-00304f19f545");
  public static bool Cached = false;
  private static readonly Guid AttrNeedUpdateDoc_Guid = new Guid("cadd93f8-306c-11d8-b4e9-00304f19f545");
  private static int attr_NeedUpdateDoc = -1;

  public static int ObjType_Specification
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_DocumentComplect
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00199-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_Document
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_ImDocument
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00136-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_ECO
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_ImDocTemplate
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00134-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_ConstructorDocumentsTemplate
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00269-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int ObjType_FormulaLib
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetObjectTypeID("cad00251-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Relation_Project
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Relation_Document
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_File
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_DocumentFile
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cadd9620-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_ScanDocument
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cadd9644-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_SourceLink
  {
    [DebuggerStepThrough] get => MetaDataHelper.GetAttributeTypeID(ExpertAttrGUIDs.attrSourceLink);
  }

  public static int Attr_Name
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_Designation
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_CheckSum
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad014af-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_Format
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad00255-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_Caption
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad00047-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_SortIndex
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_ContentModifyDate
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID("cad0013a-306c-11d8-b4e9-00304f19f545");
    }
  }

  public static int Attr_NeedUpdateDoc
  {
    [DebuggerStepThrough] get
    {
      if (DocIDCache.attr_NeedUpdateDoc == -1)
        DocIDCache.attr_NeedUpdateDoc = MetaDataHelper.GetAttributeTypeID(DocIDCache.AttrNeedUpdateDoc_Guid);
      return DocIDCache.attr_NeedUpdateDoc;
    }
  }

  public static int Attr_ObjectInECO
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_ChangeNo
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_NList1
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00771-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_NList2
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00772-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_NList3
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00773-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_NList4
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00774-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_NList5
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00775-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_Pages
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad003a7-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_SoprovDoc
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00776-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_DocNo
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00777-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_Date
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00778-306c-11d8-b4e9-00304f19f545"));
    }
  }

  public static int Attr_LRI_Podpis
  {
    [DebuggerStepThrough] get
    {
      return MetaDataHelper.GetAttributeTypeID(new Guid("cad00779-306c-11d8-b4e9-00304f19f545"));
    }
  }
}
