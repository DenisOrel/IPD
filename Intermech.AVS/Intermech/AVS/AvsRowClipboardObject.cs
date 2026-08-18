// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsRowClipboardObject
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.AVS;

public class AvsRowClipboardObject : ClipboardObject
{
  public TableData DocRow;
  public bool IsFormB;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="iDBTypedObjectID"> Интерфейс, описывающий пару тип объекта + его идентификатор </param>
  /// <param name="iDBRelationID"> Интерфейс, описывающий связь, входящей в объект </param>
  /// <param name="docRow">Запись документа</param>
  /// <param name="isFormB">Запись документа формы Б</param>
  public AvsRowClipboardObject(
    IDBTypedObjectID iDBTypedObjectID,
    IDBRelationID iDBRelationID,
    TableData docRow,
    bool isFormB)
    : base(iDBTypedObjectID, iDBRelationID)
  {
    this.DocRow = docRow;
    docRow?.RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
    this.IsFormB = isFormB;
  }

  public override string ToString() => this.DocRow != null ? this.DocRow.ToString() : this.Caption;
}
