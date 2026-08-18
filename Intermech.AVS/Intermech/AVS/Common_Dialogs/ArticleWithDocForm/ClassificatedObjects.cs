// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ClassificatedObjects
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Структура с информацией по объектам для классификации</summary>
internal class ClassificatedObjects
{
  /// <summary>Идентификатор изделия</summary>
  public long articleID;
  /// <summary>Тип изделия</summary>
  public int articleType = -1;
  /// <summary>Флаг того, что изделие только для чтения</summary>
  public bool articleReadOnly;
  /// <summary>Идентификатор документа</summary>
  public long documentID;
  /// <summary>Тип документа</summary>
  public int documentType = -1;
  /// <summary>Флаг того, что документ только для чтения</summary>
  public bool documentReadOnly;

  public bool EnableClassif
  {
    get
    {
      bool enableClassif = true;
      if (this.articleID != 0L)
        enableClassif = !this.articleReadOnly;
      if (enableClassif && this.documentID != 0L)
        enableClassif = !this.documentReadOnly;
      return enableClassif;
    }
  }
}
