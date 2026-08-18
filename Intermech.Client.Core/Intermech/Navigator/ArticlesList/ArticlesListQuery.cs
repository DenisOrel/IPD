
// Type: Intermech.Navigator.ArticlesList.ArticlesListQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.ArticlesList;

internal sealed class ArticlesListQuery : ObjectsQuery
{
  public ArticlesListQuery(
    INodeQuerySupport support,
    int objTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
    : base(support, objTypeID, conditions, services)
  {
    this.enableFiltration = false;
  }
}
