// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NewSpecObjectParams
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Параметры, с которыми начинает работать форма "Создание нового объекта/связи"
/// </summary>
public class NewSpecObjectParams
{
  /// <summary>Идентификатор нового объекта</summary>
  public long NewObjectID;
  private bool sameSpecification = true;
  /// <summary>
  /// Является ли этот объект заготовкой, либо он существует в базе данных
  /// </summary>
  public bool IsBlank;
  /// <summary>Классификатор</summary>
  public long ClassifierID;
  /// <summary>Спецификация</summary>
  public AVSDocument AVSDocument;
  /// <summary>Существующий объект, его связь</summary>
  public IDBSpecificationObjectID OldPart;
  /// <summary>Исполнения, в которые вставляется изделие</summary>
  public List<long> DestinationProducts;
  /// <summary>Вновь созданный объект или null</summary>
  public IDBSpecificationObjectID NewPart;
  /// <summary>Идентификаторы новых связей</summary>
  public List<long> NewRelations;
  /// <summary>Раздел СП в который вставляется объект</summary>
  public long ContextSectionID = -1;
  public bool AlwaysCreateRelations = true;

  /// <summary>Создается в той же спецификации</summary>
  public bool SameSpecification
  {
    get => this.sameSpecification;
    set => this.sameSpecification = value;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="oldPart">Существующий объект, его связь</param>
  /// <param name="dstProducts">Исполнения в которые нужно добавить связь</param>
  /// <param name="contextSectionID">Текущий раздел спецификации</param>
  /// <param name="alwaysCreateRelations">Создавать связи даже если количество не задано</param>
  public NewSpecObjectParams(
    AVSDocument avsDocument,
    IDBSpecificationObjectID oldPart,
    List<long> dstProducts,
    long contextSectionID,
    bool alwaysCreateRelations)
  {
    this.AVSDocument = avsDocument;
    this.OldPart = oldPart;
    this.ContextSectionID = contextSectionID;
    this.DestinationProducts = dstProducts;
    this.AlwaysCreateRelations = alwaysCreateRelations;
  }
}
