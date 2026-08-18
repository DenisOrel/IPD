// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ExtendedSaveOnCheckinSettings
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Settings;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует контейнер для режимов работы расширенного сохранения при выполнении команды "Завершить редактирование".
/// </summary>
public sealed class ExtendedSaveOnCheckinSettings : PersistentSettingsObject
{
  private const string ModuleName = "ToolService";
  private const string SectionName = "ExtendedSaveOnCheckin";
  private const string CreateArticlesParam = "CreateArticles";
  private const string UpdateArticlesParam = "UpdateArticles";
  private const string RecalculateMassParam = "RecalculateMassParam";
  private static ExtendedSaveOnCheckinSettings instance;
  private SettingsCell<bool> createArticles;
  private SettingsCell<bool> updateArticles;
  private SettingsCell<bool> recalculateMass;

  protected override void CreateCells(ICollection<ISettingsCell> cells)
  {
    base.CreateCells(cells);
    this.createArticles = new SettingsCell<bool>((object) this, LocalizationHolder.rm.GetString("SR_161"), true);
    cells.Add((ISettingsCell) this.createArticles);
    this.updateArticles = new SettingsCell<bool>((object) this, LocalizationHolder.rm.GetString("SR_162"), true);
    cells.Add((ISettingsCell) this.updateArticles);
    this.recalculateMass = new SettingsCell<bool>((object) this, LocalizationHolder.rm.GetString("SR_163"), false);
    cells.Add((ISettingsCell) this.recalculateMass);
  }

  public override void Load()
  {
    lock (this)
    {
      IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      this.createArticles.RawValue = service.ReadBool("ToolService", "ExtendedSaveOnCheckin", "CreateArticles", true, DBConfigMode.UserOnly);
      this.updateArticles.RawValue = service.ReadBool("ToolService", "ExtendedSaveOnCheckin", "UpdateArticles", true, DBConfigMode.UserOnly);
      this.recalculateMass.RawValue = service.ReadBool("ToolService", "ExtendedSaveOnCheckin", "RecalculateMassParam", false, DBConfigMode.UserOnly);
    }
  }

  public override void Save()
  {
    lock (this)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBConfigurations configurations = sessionKeeper.Session.Configurations;
        configurations.WriteBool("ToolService", "ExtendedSaveOnCheckin", "CreateArticles", this.createArticles.RawValue);
        configurations.WriteBool("ToolService", "ExtendedSaveOnCheckin", "UpdateArticles", this.updateArticles.RawValue);
        configurations.WriteBool("ToolService", "ExtendedSaveOnCheckin", "RecalculateMassParam", this.recalculateMass.RawValue);
      }
    }
  }

  public SettingsCell<bool> CreateArticles => this.createArticles;

  public SettingsCell<bool> UpdateArticles => this.updateArticles;

  public SettingsCell<bool> RecalculateMass => this.recalculateMass;

  /// <summary>
  /// Возвращает глобальный объект с настройками. Свойства этого объекта автоматически загружаются и сохраняются в базе IPS.
  /// Инициализация выполняется при первом обращении к этому свойству.
  /// </summary>
  public static ExtendedSaveOnCheckinSettings Instance
  {
    [MethodImpl(MethodImplOptions.Synchronized)] get
    {
      if (ExtendedSaveOnCheckinSettings.instance == null)
      {
        ExtendedSaveOnCheckinSettings.instance = new ExtendedSaveOnCheckinSettings();
        ExtendedSaveOnCheckinSettings.instance.Load();
        ExtendedSaveOnCheckinSettings.instance.Changed += (EventHandler) ((sender, e) => ExtendedSaveOnCheckinSettings.instance.SaveInBackground());
      }
      return ExtendedSaveOnCheckinSettings.instance;
    }
  }
}
