// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.CreatedPair
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Созданная пара объектов</summary>
internal class CreatedPair
{
  private long articleID;
  /// <summary>Идентификатор документа</summary>
  public long DocumentID;
  /// <summary>Идентификаторы связей</summary>
  public List<long> RelationIDs;
  /// <summary>Тип связей</summary>
  public int RelationType;
  /// <summary>В диалоге были созданы новые связи</summary>
  public bool NewRelations;
  /// <summary>Формат</summary>
  public string Format = string.Empty;
  /// <summary>Зона</summary>
  public string Zona = string.Empty;
  /// <summary>Примечание</summary>
  public string Note = string.Empty;
  /// <summary>Позиция</summary>
  public string Position = string.Empty;
  /// <summary>Смотри</summary>
  public string Smotri = string.Empty;
  /// <summary>Подбор</summary>
  public bool Podbor;
  /// <summary>Количество</summary>
  public MeasuredValue Count;

  /// <summary>Идентификатор изделия </summary>
  public long ArticleID
  {
    get => this.articleID;
    set => this.articleID = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="articleID">Изделие</param>
  /// <param name="documentID">Документ на изделие</param>
  public CreatedPair(long articleID, long documentID)
  {
    this.ArticleID = articleID;
    this.DocumentID = documentID;
  }

  /// <summary>Конструктор</summary>
  public CreatedPair()
  {
    this.ArticleID = 0L;
    this.DocumentID = 0L;
    this.RelationIDs = new List<long>(0);
  }
}
