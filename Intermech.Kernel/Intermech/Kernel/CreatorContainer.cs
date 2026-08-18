// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.CreatorContainer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces.Server;
using System;
using System.Collections;


namespace Intermech.Kernel;

public class CreatorContainer : ICreatorContainer
{
  private Hashtable _creators = new Hashtable();

  public void AddCreator(object creatorType, object creatorInstance)
  {
    this.AddCreator(creatorType, creatorInstance, false);
  }

  public void AddCreator(object creatorType, object creatorInstance, bool replace)
  {
    if (creatorType == null)
      throw new ArgumentNullException(sc_12731.ssp_appserver_12732(), "Cannot be null");
    if (!replace && this._creators.Contains(creatorType))
      throw new ArgumentException(string.Format(sc_12731.ssp_appserver_12733(), (object) creatorType.ToString()));
    this._creators[creatorType] = creatorInstance;
  }

  public void RemoveCreator(object creatorType)
  {
    if (creatorType == null)
      throw new ArgumentNullException(sc_12731.ssp_appserver_12734(), "Cannot be null");
    this._creators.Remove(creatorType);
  }

  public object GetCreator(object creatorType) => this._creators[creatorType];
}
