// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportRulesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportRulesService : 
  RulesService,
  IImportRulesService,
  ITransferSettingsService
{
  private readonly long _defaultImbaseFolder;

  public ImportRulesService(IUserSession session)
    : base(session, "IMPORT_SETTINGS")
  {
    QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cadd9675-306c-11d8-b4e9-00304f19f545"));
    this._defaultImbaseFolder = objectInfo.Empty ? 0L : objectInfo.ObjectID;
  }

  public long DefaultObjectOwner
  {
    get
    {
      return this.Config.ReadInteger(this.moduleName, this.sectionName, "OWNER_ID", 0L, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteInteger(this.moduleName, this.sectionName, "OWNER_ID", value, 0L);
  }

  public long BaseVersionTemplate
  {
    get
    {
      return this.Config.ReadInteger(this.moduleName, this.sectionName, "BASE_VERSION_TEMPLATE", 0L, DBConfigMode.GlobalOnly);
    }
    set
    {
      this.Config.WriteInteger(this.moduleName, this.sectionName, "BASE_VERSION_TEMPLATE", value, 0L);
    }
  }

  public long DefaultImbaseFolder
  {
    get
    {
      return this.Config.ReadInteger(this.moduleName, this.sectionName, "IMPORT_FOLDER", this._defaultImbaseFolder, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteInteger(this.moduleName, this.sectionName, "IMPORT_FOLDER", value, 0L);
  }

  public long ImportCompleteTemplate
  {
    get
    {
      return this.Config.ReadInteger(this.moduleName, this.sectionName, "COMPLETE_TEMPLATE", 0L, DBConfigMode.GlobalOnly);
    }
    set
    {
      this.Config.WriteInteger(this.moduleName, this.sectionName, "COMPLETE_TEMPLATE", value, 0L);
    }
  }

  public long ImportErrorTemplate
  {
    get
    {
      return this.Config.ReadInteger(this.moduleName, this.sectionName, "ERROR_TEMPLATE", 0L, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteInteger(this.moduleName, this.sectionName, "ERROR_TEMPLATE", value, 0L);
  }

  public bool CreateDetailTaskLog
  {
    get
    {
      return this.Config.ReadBool(this.moduleName, this.sectionName, "CREATE_DETAIL_LOG", false, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteBool(this.moduleName, this.sectionName, "CREATE_DETAIL_LOG", value, 0L);
  }

  public bool CentralizedNSI
  {
    get
    {
      return this.Config.ReadBool(this.moduleName, this.sectionName, "CENTRALIZED_NSI", true, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteBool(this.moduleName, this.sectionName, "CENTRALIZED_NSI", value, 0L);
  }

  public bool RewriteArchive
  {
    get
    {
      return this.Config.ReadBool(this.moduleName, this.sectionName, "REWRITE_ARCHIVE", true, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteBool(this.moduleName, this.sectionName, "REWRITE_ARCHIVE", value, 0L);
  }

  public bool RenameCoincidenceFileNames
  {
    get
    {
      return this.Config.ReadBool(this.moduleName, this.sectionName, "RENAME_FILENAME", false, DBConfigMode.GlobalOnly);
    }
    set => this.Config.WriteBool(this.moduleName, this.sectionName, "RENAME_FILENAME", value, 0L);
  }
}
