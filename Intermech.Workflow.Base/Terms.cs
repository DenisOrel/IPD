// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Terms
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Workflow
{
    public class Terms : IValidatedItem
    {
      private readonly Term _term;
      private readonly Term _readTerm;
      public readonly List<Term> AsList = new List<Term>();
      private IUserSession _userSession;
      /// <summary>
      /// Используется при экспорте в портфель, когда BriefString недоступна
      /// </summary>
      public bool XMLOnlyMode;

      public Term Term => this._term;

      public Term ReadTerm => this._readTerm;

      public IUserSession Session => this._userSession;

      public Terms(IUserSession session)
      {
        this._term = new Term(this, nameof (Term));
        this._readTerm = new Term(this, nameof (ReadTerm));
        this.AsList.Add(this._term);
        this.AsList.Add(this._readTerm);
        this._userSession = session;
      }

      public string BriefString
      {
        get
        {
          string briefString = "";
          if (this.Term.Period != null)
          {
            briefString += "T";
            if (this.Term.Enabled)
              briefString += "R";
          }
          if (this.ReadTerm.Period != null && this.ReadTerm.Enabled)
            briefString += "U";
          return briefString;
        }
        set
        {
          if (value.IndexOf('T') != -1)
          {
            if (this.Term.Period == null)
              this.Term.Period = new PeriodInformation(this.Session);
            this.Term.Enabled = value.IndexOf('R') != -1;
          }
          if (value.IndexOf('U') == -1)
            return;
          if (this.ReadTerm.Period == null)
            this.ReadTerm.Period = new PeriodInformation(this.Session);
          this.ReadTerm.Enabled = true;
        }
      }

      public void LoadFromStream(Stream stream)
      {
        if (stream.Length == 0L)
          return;
        if (stream.Position != 0L)
          stream.Position = 0L;
        XmlTextReader reader = new XmlTextReader(stream);
        reader.ReadStartElement(nameof (Terms));
        this.Term.Load(reader);
        this.ReadTerm.Load(reader);
      }

      public void SaveToStream(Stream stream)
      {
        XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartElement(nameof (Terms));
        this.Term.Save(writer);
        this.ReadTerm.Save(writer);
        writer.WriteEndElement();
        writer.Flush();
      }

      public void Load(IDBObject obj)
      {
        IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrTermsID);
        if (attributeById == null)
          return;
        this.Load(attributeById);
      }

      public void Load(IDBAttribute attr)
      {
        string asString = attr.AsString;
        if (!(asString != ""))
          return;
        this.BriefString = asString;
        StreamHelper.LoadFromBlobStream(attr as IBlobReader, new ProcessStreamDelegate(this.LoadFromStream));
      }

      public void Save(IDBObject obj)
      {
        this.Save(obj.Attributes.AddAttribute(wfConsts.AttrTermsID, false));
      }

      public void Save(IDBAttribute attr)
      {
        StreamHelper.SaveToBlobStream(attr as IBlobWriter, new ProcessStreamDelegate(this.SaveToStream), this.BriefString);
      }

      public bool Modified => this.Term.Modified || this.ReadTerm.Modified;

      /// <summary>
      /// Записывать гуиды объектов, или нет. Полный формат используется при экспорте в портфель
      /// </summary>
      public bool WriteGuids
      {
        get => this.Term.WriteGuids;
        set
        {
          this.Term.WriteGuids = value;
          this.ReadTerm.WriteGuids = value;
        }
      }

      public bool Invalid => this.Term.Invalid || this.ReadTerm.Invalid;
    }
}
