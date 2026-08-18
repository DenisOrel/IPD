// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MSOffice.Word.DocsComparison.DocsComparisonPlugin
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Client.MSOffice.Word.DocsComparison;

internal class DocsComparisonPlugin : ICanCompareObjectsFiles
{
  private DocsComparisonCommandsProvider docsComparisonCommandsProvider;
  private List<int> typeIdsList;
  private ReadOnlyCollection<int> typeIdsListWrapper;

  public DocsComparisonPlugin(
    DocsComparisonCommandsProvider docsComparisonCommandsProvider)
  {
    this.docsComparisonCommandsProvider = docsComparisonCommandsProvider != null ? docsComparisonCommandsProvider : throw new ArgumentNullException(nameof (docsComparisonCommandsProvider));
    this.typeIdsList = new List<int>();
    this.typeIdsListWrapper = new ReadOnlyCollection<int>((IList<int>) this.typeIdsList);
  }

  public void SetTypeIds(List<int> typeIds)
  {
    if (typeIds == null)
      throw new ArgumentNullException(nameof (typeIds));
    this.typeIdsList.Clear();
    this.typeIdsList.AddRange((IEnumerable<int>) typeIds);
  }

  public string UniqueName => DocsComparisonConsts.PluginUniqueName;

  public string NameInMessages => DocsComparisonConsts.PluginNameInMessages;

  public ReadOnlyCollection<int> TypeIds => this.typeIdsListWrapper;

  public void RemoveTypeId(int typeId)
  {
    if (this.typeIdsList == null)
      throw new ArgumentNullException("typeIdsList");
    this.typeIdsList.Remove(typeId);
  }

  public void CompareFilesFor(
    DBObjectToCompare object1,
    DBObjectToCompare object2,
    FileTypes fileType)
  {
    if (object1 == null)
      throw new ArgumentNullException(nameof (object1));
    if (object2 == null)
      throw new ArgumentNullException(nameof (object2));
    if (this.docsComparisonCommandsProvider == null)
      throw new ArgumentNullException("docsComparisonCommandsProvider");
    if (fileType == FileTypes.ftAuthentical)
      ServiceUtils.GetService<ICompareFilesService>((object) ApplicationServices.Container, false).CompareFilesWithCommonRules(object1, object2, fileType);
    else
      this.docsComparisonCommandsProvider.CompareObjects(new DocsComparisonCommandsProvider.ObjectInfo(object1.Caption, object1.ObjectID), new DocsComparisonCommandsProvider.ObjectInfo(object2.Caption, object2.ObjectID));
  }
}
