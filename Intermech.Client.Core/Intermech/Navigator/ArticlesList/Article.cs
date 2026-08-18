
// Type: Intermech.Navigator.ArticlesList.Article
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ArticlesList;

public sealed class Article
{
  public long ArticleID;
  public bool BaseArticle;
  public int ArticleType;
  public string Caption;

  public Article(long articleID, bool baseArticle, int articleType, string caption)
  {
    this.ArticleID = articleID;
    this.BaseArticle = baseArticle;
    this.ArticleType = articleType;
    this.Caption = caption != null ? caption : $"< {articleID} >";
  }
}
