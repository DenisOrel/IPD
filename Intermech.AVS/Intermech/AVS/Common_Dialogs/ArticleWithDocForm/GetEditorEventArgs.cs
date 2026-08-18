// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.GetEditorEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

internal class GetEditorEventArgs
{
  /// <summary>Идентификатор атрибута</summary>
  public int AttributeID;
  /// <summary>Значение</summary>
  public object Value;
  /// <summary>Флаг того, что событие было обработано</summary>
  public bool Handled;

  public GetEditorEventArgs(int attrID, object val)
  {
    this.AttributeID = attrID;
    this.Value = val;
    this.Handled = false;
  }
}
