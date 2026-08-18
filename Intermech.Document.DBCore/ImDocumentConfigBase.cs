// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.ImDocumentConfigBase
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.DBCore;

public class ImDocumentConfigBase
{
  private static ImDocumentConfigBase instance;
  private bool notIndexTemplateWords;
  private bool? newNotindexTemplateWords;

  public static ImDocumentConfigBase Instance
  {
    get
    {
      if (ImDocumentConfigBase.instance == null)
        ImDocumentConfigBase.instance = new ImDocumentConfigBase();
      return ImDocumentConfigBase.instance;
    }
  }

  [Browsable(false)]
  public bool NotIndexTemplateWords
  {
    get => this.notIndexTemplateWords;
    set => this.notIndexTemplateWords = value;
  }

  [DisplayName("Игнорировать текст из шаблона при индексации")]
  [Description("При индексации документа будет игнорироваться текст найденный в шаблонах")]
  [Category("Настройки работы сервера")]
  [DefaultValue(false)]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool NotIndexTemplateWordsVisual
  {
    [DebuggerStepThrough] get
    {
      return this.newNotindexTemplateWords.HasValue ? this.newNotindexTemplateWords.Value : this.notIndexTemplateWords;
    }
    set
    {
      bool? notindexTemplateWords = this.newNotindexTemplateWords;
      bool flag = value;
      if (notindexTemplateWords.GetValueOrDefault() == flag & notindexTemplateWords.HasValue)
        return;
      this.newNotindexTemplateWords = new bool?(value);
    }
  }

  public virtual void Apply()
  {
    if (!this.newNotindexTemplateWords.HasValue)
      return;
    this.NotIndexTemplateWords = this.newNotindexTemplateWords.Value;
  }

  public virtual void Cancel() => this.newNotindexTemplateWords = new bool?();

  private bool IsAdminSetting(string name) => name == "NotIndexTemplateWords";

  public void SaveConfiguration(IUserSession session)
  {
    string SectionID = "ImDocumentConfig";
    IDBConfigurations configurations = session.Configurations;
    bool flag = this.IsAdminSetting("NotIndexTemplateWords");
    if (flag && !session.IsAdmin)
      return;
    configurations.WriteInteger("CLIENT", SectionID, "NotIndexTemplateWords", this.NotIndexTemplateWords ? 1L : 0L, flag ? 0L : session.UserID);
  }

  public void LoadConfiguration(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    string SectionID = "ImDocumentConfig";
    string str = "NotIndexTemplateWords";
    bool flag1 = this.IsAdminSetting(str);
    DBConfigMode configMode = flag1 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal;
    if (!configurations.ParameterPresent("CLIENT", SectionID, str, configMode))
      return;
    bool flag2 = false;
    this.NotIndexTemplateWords = Convert.ToInt32(configurations.ReadInteger("CLIENT", SectionID, str, flag2 ? 1L : 0L, flag1 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal)) > 0;
  }
}
