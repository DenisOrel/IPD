
// Type: Intermech.Redline.RedliningCommonSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Configuration;
using Intermech.Interfaces;
using Intermech.Settings;
using System.Collections.Generic;


namespace Intermech.Redline;

public class RedliningCommonSettings : DBPersistentSettingsObject
{
  protected readonly string LaunchScreenShooterParameter = nameof (LaunchScreenShooter);
  private SettingsCell<bool> launchScreenShooter;

  public RedliningCommonSettings()
    : base("Redlining", "GeneralSettings")
  {
  }

  protected override void CreateCells(ICollection<ISettingsCell> cells)
  {
    base.CreateCells(cells);
    this.launchScreenShooter = new SettingsCell<bool>((object) this, "По команде 'Смотреть' запускать приложение для снятия скриншотов", false);
    cells.Add((ISettingsCell) this.launchScreenShooter);
  }

  protected override void DoAssign(SettingsObject source)
  {
    base.DoAssign(source);
    if (!(source is RedliningCommonSettings redliningCommonSettings))
      return;
    this.launchScreenShooter.RawValue = redliningCommonSettings.launchScreenShooter.RawValue;
  }

  /// <summary>
  /// Включает и выключает режим, при котором по команде "Смотреть" автоматически запускается приложение для снятия скриншотов.
  /// </summary>
  public SettingsCell<bool> LaunchScreenShooter => this.launchScreenShooter;

  public RedliningCommonSettings Clone() => (RedliningCommonSettings) this.DoClone();

  protected override void DoSave(IUserSession session)
  {
    base.DoSave(session);
    this.WriteUserString(session, this.LaunchScreenShooterParameter, this.launchScreenShooter.RawValue ? "true" : "false");
  }

  protected override void DoLoad(IUserSession session)
  {
    base.DoLoad(session);
    string str = this.ReadUserString(session, this.LaunchScreenShooterParameter);
    if (str.Length <= 0)
      return;
    this.launchScreenShooter.RawValue = AppSettingsHelper.ParseBoolean(str, false);
  }
}
