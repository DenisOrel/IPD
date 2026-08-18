// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EditingContextsForAllObjectVersionsAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public sealed class EditingContextsForAllObjectVersionsAnalyzer : ISearchGroupingObjectAnalyzer
{
  private EditingContextsForObjectsAnalyzer _editingContextsForObjectsAnalyzer;

  public EditingContextsForAllObjectVersionsAnalyzer(
    EditingContextsForObjectsAnalyzer editingContextsForObjectsAnalyzer)
  {
    this._editingContextsForObjectsAnalyzer = editingContextsForObjectsAnalyzer != null ? editingContextsForObjectsAnalyzer : throw new ArgumentNullException(nameof (editingContextsForObjectsAnalyzer));
  }

  public string Name => "Поиск среди выделенных объектов";

  public int Analyze(IUserSession userSession, SearchGroupingObjects searchGroupingObjects)
  {
    if (userSession == null)
      throw new ArgumentNullException(nameof (userSession));
    if (searchGroupingObjects == null)
      throw new ArgumentNullException(nameof (searchGroupingObjects));
    List<SearchGroupingObject> searchGroupingObjectList = new List<SearchGroupingObject>();
    foreach (SearchGroupingObject searchGroupingObject1 in searchGroupingObjects.ToArray())
    {
      foreach (long objectIdVersion in userSession.GetObjectIDVersions(searchGroupingObject1.ObjectID))
      {
        SearchGroupingObject searchGroupingObject2 = new SearchGroupingObject(objectIdVersion, searchGroupingObject1.ObjectTypeID, new Dictionary<long, int>());
        searchGroupingObjects.Add(searchGroupingObject2);
      }
    }
    return this._editingContextsForObjectsAnalyzer.Analyze(userSession, searchGroupingObjects);
  }
}
