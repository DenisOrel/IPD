// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.DocumentType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.ComponentModel;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

internal class DocumentType
{
  public DocumentType Parent;
  internal AVSDocumentTypesTemplateForm form;
  private Guid guid;
  internal Template Template;
  private List<DocumentType> childs;
  private List<Guid> dBObjectTypeList;
  private bool changed;
  private AVSDocumentType? type;
  private AVSDocumentForm? specForm;
  private string name;

  public DocumentType(
    Guid guid,
    AVSDocumentType? type,
    AVSDocumentForm? form,
    string name,
    AVSDocumentTypesTemplateForm aVSDocumentTypesTemplateForm)
  {
    this.Type = type;
    this.SpecForm = form;
    this.Name = name;
    this.Guid = guid;
    this.form = aVSDocumentTypesTemplateForm;
    this.Childs = new List<DocumentType>();
    this.DBObjectTypeList = new List<Guid>();
  }

  public Guid TypeGuid => this.DBObjectTypeList.Count > 0 ? this.DBObjectTypeList[0] : Guid.Empty;

  public Guid Guid
  {
    get => this.guid;
    set => this.guid = value;
  }

  internal List<DocumentType> Childs
  {
    get => this.childs;
    set => this.childs = value;
  }

  public List<Guid> DBObjectTypeList
  {
    get => this.Parent != null ? this.Parent.DBObjectTypeList : this.dBObjectTypeList;
    set
    {
      if (this.Parent != null)
        this.Parent.DBObjectTypeList = value;
      else
        this.dBObjectTypeList = value;
    }
  }

  public bool Changed
  {
    get
    {
      if (this.changed)
        return true;
      return this.Template != null && this.Template.Changed;
    }
    set
    {
      this.changed = value;
      if (this.Parent != null)
      {
        this.Parent.Changed = value;
      }
      else
      {
        if (this.form == null)
          return;
        this.form.UpdateCaptions(true);
      }
    }
  }

  public AVSDocumentType? Type
  {
    get => this.type;
    set => this.type = value;
  }

  public AVSDocumentForm? SpecForm
  {
    get => this.specForm;
    set => this.specForm = value;
  }

  public string Name
  {
    get
    {
      if (this.name != null && this.name != string.Empty)
        return this.name;
      AVSDocumentForm? specForm = this.SpecForm;
      if (specForm.HasValue)
      {
        specForm = this.SpecForm;
        return EnumCustomConverter.GetEnumDescription((Enum) specForm.Value);
      }
      AVSDocumentType? type = this.Type;
      if (!type.HasValue)
        return this.name;
      type = this.Type;
      return EnumCustomConverter.GetEnumDescription((Enum) type.Value);
    }
    set => this.name = value;
  }

  public bool CanChangeName
  {
    get
    {
      if (this.SpecForm.HasValue)
        return false;
      AVSDocumentType? type = this.Type;
      AVSDocumentType avsDocumentType1 = AVSDocumentType.UserAVSDocument;
      if (type.GetValueOrDefault() == avsDocumentType1 & type.HasValue)
        return true;
      type = this.Type;
      AVSDocumentType avsDocumentType2 = AVSDocumentType.UserSpecification;
      return type.GetValueOrDefault() == avsDocumentType2 & type.HasValue;
    }
  }
}
