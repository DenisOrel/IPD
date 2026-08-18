// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.UserMessageUserComparer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Forums;

public class UserMessageUserComparer : IComparer<UserMessage>
{
  private SortOrder order = SortOrder.Descending;

  public UserMessageUserComparer(SortOrder order) => this.order = order;

  public int Compare(UserMessage x, UserMessage y)
  {
    Guid objectGUID1 = new Guid(x.UserGuid);
    Guid objectGUID2 = new Guid(y.UserGuid);
    string strB1;
    string strB2;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(objectGUID1, false);
      IDBObject dbObject2 = sessionKeeper.Session.GetObject(objectGUID2, false);
      strB1 = dbObject1 != null ? dbObject1.Caption : string.Format(LocalizationHolder.rm.GetString("Workflow.Design_190"), (object) objectGUID1);
      strB2 = dbObject2 != null ? dbObject2.Caption : string.Format(LocalizationHolder.rm.GetString("Workflow.Design_190"), (object) objectGUID2);
    }
    return this.order != SortOrder.Ascending ? strB2.CompareTo(strB1) : strB1.CompareTo(strB2);
  }
}
