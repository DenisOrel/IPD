// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleFiles
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class ArticleFiles
{
  private string mainArticleFile;

  /// <summary>
  /// Возвращает или задает файл документа, описывающий изделие. Открытие этого файла в приложении позволяет напрямую редактировать определение изделия.
  /// Если данная возможность не поддерживается приложением, то значение свойства может быть не задано.
  /// </summary>
  public string MainArticleFile
  {
    get => this.mainArticleFile;
    set => this.mainArticleFile = value;
  }
}
