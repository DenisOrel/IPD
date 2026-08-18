// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StartupConfigurationSurrogate
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Cadmech.Integrator;

[DefaultProperty("UserRole")]
internal sealed class StartupConfigurationSurrogate : ICloneable
{
  private UserRoleMarker userRole;
  private bool useSpecificProfile;
  private string profileName;

  [Category("1. Общие настройки")]
  [DisplayName("Роль пользователя")]
  [Description("Задает роль пользователя, на которую распространяются настройки. Если значение не задано, то настройки будут действовать на пользователей всех ролей")]
  [Editor(typeof (RoleUIEditor), typeof (UITypeEditor))]
  public UserRoleMarker UserRole
  {
    get => this.userRole;
    set => this.userRole = value;
  }

  [Category("1. Общие настройки")]
  [DisplayName("Использовать профиль?")]
  [Description("Включает и выключает установку специального профиля в приложении после подключения к нему интегратора.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool UseSpecificProfile
  {
    get => this.useSpecificProfile;
    set => this.useSpecificProfile = value;
  }

  [Category("1. Общие настройки")]
  [DisplayName("Имя профиля")]
  [Description("Имя профиля, устанавливаемого в приложении после подключения к нему интегратора.")]
  public string ProfileName
  {
    get => this.profileName;
    set => this.profileName = value;
  }

  public StartupConfigurationSurrogate Clone()
  {
    return new StartupConfigurationSurrogate()
    {
      userRole = this.userRole != null ? this.userRole.Clone() : (UserRoleMarker) null,
      useSpecificProfile = this.useSpecificProfile,
      profileName = this.profileName
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override int GetHashCode()
  {
    int hashCode = 0;
    if (this.userRole != null)
      hashCode ^= this.userRole.GetHashCode();
    return hashCode;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is StartupConfigurationSurrogate configurationSurrogate))
      return base.Equals(obj);
    return object.Equals((object) configurationSurrogate.userRole, (object) this.userRole) && configurationSurrogate.useSpecificProfile == this.useSpecificProfile && !(configurationSurrogate.profileName != this.profileName);
  }

  public override string ToString() => "Параметры подключения к приложению";
}
