// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSRowClipboardCollection
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections;

#nullable disable
namespace Intermech.AVS;

/// <summary>Коллекция данных о записях AVS для буфера IPS, с поддержкой DBObjectTypedID для других плагинов</summary>
public class AVSRowClipboardCollection : ITypedIDCollection, IEnumerator, IDBObjectTypedIDCollection
{
  /// <summary>Список записей</summary>
  public ArrayList RowList;
  /// <summary>Тип документа из которого были скопированы записи</summary>
  public AVSDocumentType DocType;
  /// <summary>Форма конструкторского документа</summary>
  public AVSDocumentForm DocForm;
  private ArrayList dbTypedObjectIDs;
  private IEnumerator _baseEnumerator;
  private long specificationId = -1;

  public long SpecificationId
  {
    get => this.specificationId;
    set => this.specificationId = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="rowsList">Список записей AVS</param>
  /// <param name="docType">Тип документа из которого были скопированы записи</param>
  /// <param name="specForm">Форма конструкторского документа</param>
  public AVSRowClipboardCollection(
    ArrayList rowsList,
    AVSDocumentType docType,
    AVSDocumentForm docForm,
    long specificationId)
  {
    this.RowList = rowsList != null ? rowsList : throw new ArgumentNullException(nameof (rowsList));
    for (int index = 0; index < this.RowList.Count; ++index)
    {
      if (this.RowList[index] is DocumentTreeNode row)
        row.RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
    }
    this.DocType = docType;
    this.DocForm = docForm;
    this.SpecificationId = specificationId;
    this.dbTypedObjectIDs = new ArrayList();
    for (int index = 0; index < this.RowList.Count; ++index)
    {
      if (this.RowList[index] is IDBTypedObjectID)
        this.dbTypedObjectIDs.Add(this.RowList[index]);
      else if (this.RowList[index] is IDBRelationID)
        this.dbTypedObjectIDs.Add(this.RowList[index]);
    }
    this._baseEnumerator = this.dbTypedObjectIDs.GetEnumerator();
  }

  IDBTypedObjectID IDBObjectTypedIDCollection.GetTypedObjectID(int index)
  {
    return this.dbTypedObjectIDs[index] as IDBTypedObjectID;
  }

  IDBTypedObjectID[] IDBObjectTypedIDCollection.GetTypedObjects()
  {
    return (IDBTypedObjectID[]) this.dbTypedObjectIDs.ToArray(typeof (IDBTypedObjectID));
  }

  IDBRelationID IDBObjectTypedIDCollection.GetRelationID(int index)
  {
    return this.dbTypedObjectIDs[index] as IDBRelationID;
  }

  IDBRelationID[] IDBObjectTypedIDCollection.GetRelations()
  {
    return (IDBRelationID[]) this.dbTypedObjectIDs.ToArray(typeof (IDBRelationID));
  }

  object ITypedIDCollection.this[int index] => this.dbTypedObjectIDs[index];

  int ITypedIDCollection.Count => this.dbTypedObjectIDs.Count;

  object IEnumerator.Current => this._baseEnumerator.Current;

  bool IEnumerator.MoveNext() => this._baseEnumerator.MoveNext();

  void IEnumerator.Reset() => this._baseEnumerator.Reset();

  public override string ToString()
  {
    if (this.RowList != null && this.RowList.Count > 0)
    {
      object row = this.RowList[0];
      if (row != null)
      {
        ClipboardObject clipboardObject = row as ClipboardObject;
        if (row is AvsRowClipboardObject rowClipboardObject)
          return rowClipboardObject.ToString();
        return clipboardObject != null ? clipboardObject.ToString() : row.ToString();
      }
    }
    return base.ToString();
  }
}
