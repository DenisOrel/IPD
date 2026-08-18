// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.RemovableObjects
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

internal class RemovableObjects
{
  private List<long> _ObjectsIDList = new List<long>();

  public void AddObjects(IEnumerable<long> objectIDs)
  {
    foreach (long objectId in objectIDs)
    {
      long num = Math.Abs(objectId);
      if (!this._ObjectsIDList.Contains(num))
        this._ObjectsIDList.Add(num);
    }
  }

  public void AddObjects(List<DeletingObject> objects)
  {
    List<long> objectIDs = new List<long>(objects.Count);
    foreach (DeletingObject deletingObject in objects)
    {
      if (deletingObject.RemoveObject)
        objectIDs.Add(deletingObject.ObjectID);
    }
    this.AddObjects((IEnumerable<long>) objectIDs);
  }

  public void DeleteObject(long objectID) => this._ObjectsIDList.Remove(Math.Abs(objectID));

  public void Clear() => this._ObjectsIDList.Clear();

  public void StartRemoveObjects(IEnumerable<long> objectIDs)
  {
    this.Clear();
    this.AddObjects(objectIDs);
  }

  public bool Exists(long objectID) => this._ObjectsIDList.Contains(Math.Abs(objectID));
}
