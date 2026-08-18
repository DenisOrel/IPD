// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.DocRecord
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Включает всю информацию, кроме самого документа и отладочной информации
/// </summary>
[Serializable]
public class DocRecord
{
  /// <summary>Имя документа</summary>
  public string docName = "";
  /// <summary>
  /// Текст ошибки при генерации документа - или пустая строка если все ОК
  /// </summary>
  public string errorMsg = "";
  /// <summary>
  /// Идентификатор объекта документа/комплекта (обычно заполняется только после разбиения)
  /// </summary>
  public long docObjectID = -1;
  /// <summary>
  /// Идентификатор объекта СТАРОГО документа/комплекта (может не быть)
  /// </summary>
  public long oldObjectID = -1;
  /// <summary>
  /// Идентификатор скрипта (есть для всех документов, но не для комплектов)
  /// Для оператора ссылки на объект - это не скрипт, а ИД исходного объекта
  /// </summary>
  public long scriptID = -1;
  /// <summary>
  /// Чисто номер документа - ПО НЕМУ ОБРАЩАТЬСЯ ЗА ДОКУМЕНТАМИ
  /// </summary>
  public int docNumber = -1;
  /// <summary>
  /// // Идентификатор объекта, на который выпускается документ (есть всегда)
  /// </summary>
  public long objID = -1;
  /// <summary>
  /// Есть только для документов, которые на группу объектов (например, операций)
  /// </summary>
  private List<long> _objIDList;
  /// <summary>
  /// Номер записи с комплектом, в который входит этот документ/комплект
  /// </summary>
  public int parentIndex = -1;
  /// <summary>
  /// "Y" - сценарий C#, "N" - мой скрипт, "Т" - документ Техкард
  /// </summary>
  public string docType = "N";
  /// <summary>
  /// 
  /// </summary>
  public DocState state;
  /// <summary>
  /// Набор флагов, при которых эту DocRecord надо игнорировать как документ
  /// </summary>
  public static readonly DocState ignoreDoc = DocState.CondFalse | DocState.Empty | DocState.Complect | DocState.Delayed;

  public List<long> objIDList
  {
    get => this._objIDList;
    set => this._objIDList = value;
  }

  /// <summary>// Документ готов (создан)?</summary>
  /// <returns>true, если документ уже готов</returns>
  public bool Ready() => (this.state & DocState.Ready) != 0;

  /// <summary>Игнорировать этот документ?</summary>
  /// <returns>true, если документ надо игнорировать</returns>
  public bool IgnoreDoc() => (this.state & DocRecord.ignoreDoc) != 0;

  public bool IsComplect() => (this.state & DocState.Complect) != 0;

  public bool IsDocGenerating() => (this.state & DocState.NotGenerating) == DocState.NoFlags;

  public bool IsDocLink() => (this.state & DocState.DocLink) != 0;

  public DocRecord(string dName, long scriptID, long objectID)
  {
    this.docName = dName;
    this.scriptID = scriptID;
    this.objID = objectID;
  }

  public DocRecord(string dName, long scriptID, long objectID, long docObjID)
  {
    this.docName = dName;
    this.scriptID = scriptID;
    this.objID = objectID;
    this.docObjectID = docObjID;
  }

  public override string ToString()
  {
    return $"ObjId={this.objID} DocName='{this.docName}' State={(int) this.state:X} DocType={this.docType}";
  }
}
