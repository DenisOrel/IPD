// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportItem
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal abstract class ImportItem : LoggedImport
{
  protected UserSession session;
  protected DataRow briefRow;
  protected DataSet metaData;
  protected ImportItemOptions options;
  public Exception ErrorException;
  public string UniIdentifiler = string.Empty;

  public ImportItem(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    ImportItemOptions options)
  {
    this.session = userSession;
    this.briefRow = briefRow;
    this.metaData = metaData;
    this.options = options;
  }

  protected bool LangEquals
  {
    get => (this.options & ImportItemOptions.LangEquals) == ImportItemOptions.LangEquals;
  }

  protected bool CreateOnly
  {
    get => (this.options & ImportItemOptions.CreateOnly) == ImportItemOptions.CreateOnly;
  }

  public virtual bool Import() => false;
}
