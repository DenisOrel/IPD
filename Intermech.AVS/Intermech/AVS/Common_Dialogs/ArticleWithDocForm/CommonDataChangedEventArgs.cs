// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.CommonDataChangedEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>
/// Аргументы для передачи сообщения об изменении общих данных
/// </summary>
internal class CommonDataChangedEventArgs
{
  public CommonDataType Type;

  public CommonDataChangedEventArgs(CommonDataType type) => this.Type = type;
}
