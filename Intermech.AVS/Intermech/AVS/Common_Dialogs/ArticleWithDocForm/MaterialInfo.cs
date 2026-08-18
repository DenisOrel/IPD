// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.MaterialInfo
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Информация по материалу</summary>
internal class MaterialInfo
{
  /// <summary>Идентификатор</summary>
  public long ObjectID;
  /// <summary>Заголовок</summary>
  public string Caption;

  public MaterialInfo(long objectID, string caption)
  {
    this.ObjectID = objectID;
    this.Caption = caption;
  }
}
