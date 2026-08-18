// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.LastViewedDocumentsService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Redline;

internal sealed class LastViewedDocumentsService
{
  private Guid categoryGuid;
  private Lazy<int> categoryId;

  public LastViewedDocumentsService(IGuidMapper navigatorMapper, IFactory navigatorFactory)
  {
    LastViewedDocumentsService documentsService = this;
    this.categoryGuid = new Guid("2A9DDC27-A4F2-4FE6-B749-A529B6F7B816");
    this.categoryId = new Lazy<int>((Func<int>) (() => documentsService.InitializeCategoryId(navigatorMapper, navigatorFactory)));
  }

  private int InitializeCategoryId(IGuidMapper navigatorMapper, IFactory navigatorFactory)
  {
    int categoryID = navigatorMapper.Register(this.categoryGuid);
    navigatorFactory.AddNodeType(categoryID, typeof (ObjectsListNode));
    navigatorFactory.AddViewsProvider(categoryID, (IViewsProvider) new LastViewedDocumentsViewsProvider());
    return categoryID;
  }

  public int CategoryId
  {
    [DebuggerStepThrough] get => this.categoryId.Value;
  }
}
