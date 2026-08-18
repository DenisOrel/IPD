// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadStartupConfiguration
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Реализует контейнер для параметров подключения к AutoCAD.
/// </summary>
public sealed class AcadStartupConfiguration
{
  private UserRoleMarker userRole;
  private bool useSpecificProfile;
  private string profileName;

  /// <summary>
  /// Возвращает или задает роль пользователя, на которую распространяются данные настройки.
  /// Если значение этого свойства равно null, то настройки действуют на всех пользователей.
  /// </summary>
  public UserRoleMarker UserRole
  {
    get => this.userRole;
    set => this.userRole = value;
  }

  /// <summary>
  /// Возвращает или задает флаг использования специфического профиля AutoCAD.
  /// </summary>
  public bool UseSpecificProfile
  {
    get => this.useSpecificProfile;
    set => this.useSpecificProfile = value;
  }

  /// <summary>
  /// Возвращает или задает имя используемого профиля AutoCAD.
  /// </summary>
  public string ProfileName
  {
    get => this.profileName;
    set => this.profileName = value;
  }
}
