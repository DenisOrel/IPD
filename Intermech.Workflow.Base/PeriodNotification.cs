// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.PeriodNotification
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using System.Xml;


namespace Intermech.Workflow
{
    public class PeriodNotification : Notification
    {
      private PeriodInformation _pi;

      public PeriodInformation Period => this._pi;

      public override bool Modified
      {
        get => base.Modified || this._pi.Modified;
        set
        {
          base.Modified = value;
          this._pi.Modified = value;
        }
      }

      public PeriodNotification(Notifications owner, string name, char symbol, IUserSession session)
        : base(owner, name, symbol, session)
      {
        this._pi = new PeriodInformation(session);
      }

      protected override void BaseSave(XmlTextWriter writer)
      {
        base.BaseSave(writer);
        this.Period.WriteGuids = this._owner.WriteGuids;
        this.Period.BaseSave(writer);
      }

      protected override void BaseLoad(XmlTextReader reader)
      {
        base.BaseLoad(reader);
        this.Period.BaseLoad(reader);
      }

      public override bool Invalid
      {
        get
        {
          if (base.Invalid)
            return true;
          return this.Period != null && this.Period.Invalid;
        }
      }
    }
}
