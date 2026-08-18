// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Term
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces.Workflow;
using System.Xml;


namespace Intermech.Workflow
{
    public class Term : IValidatedItem
    {
      private PeriodInformation _period;
      private bool _enabled;
      public readonly string Name;
      private bool _modified;
      private Terms _parent;

      public PeriodInformation Period
      {
        get => this._period;
        set
        {
          if (value == this._period)
            return;
          this._period = value;
          this._modified = true;
        }
      }

      public bool Enabled
      {
        get => this._enabled;
        set
        {
          if (this._enabled == value)
            return;
          this._enabled = value;
          this._modified = true;
        }
      }

      public bool Modified
      {
        get
        {
          if (this._modified)
            return true;
          return this.Period != null && this.Period.Modified;
        }
        set
        {
          this._modified = value;
          if (this.Period == null)
            return;
          this.Period.Modified = value;
        }
      }

      public Term(Terms parent, string name)
      {
        this._parent = parent;
        this.Name = name;
      }

      internal void Save(XmlTextWriter writer)
      {
        if (this.Period == null)
          return;
        writer.WriteStartElement(this.Name);
        this.Period.BaseSave(writer);
        writer.WriteEndElement();
      }

      internal void Load(XmlTextReader reader)
      {
        if (this._parent.XMLOnlyMode && this.Period == null)
          this.Period = new PeriodInformation(this._parent.Session);
        if (this.Period == null)
          return;
        try
        {
          reader.ReadStartElement(this.Name);
          this.Period.BaseLoad(reader);
          reader.ReadEndElement();
          this._modified = false;
        }
        catch
        {
          if (this._parent.XMLOnlyMode)
            this.Period = (PeriodInformation) null;
        }
        this.Modified = false;
      }

      /// <summary>
      /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
      /// </summary>
      public bool WriteGuids
      {
        get
        {
          if (this.Period == null)
            return false;
          return this.Period.WriteGuids || this.Invalid;
        }
        set
        {
          if (this.Period == null)
            return;
          this.Period.WriteGuids = value;
        }
      }

      public bool Invalid => this.Period != null && this.Period.Invalid;
    }
}
