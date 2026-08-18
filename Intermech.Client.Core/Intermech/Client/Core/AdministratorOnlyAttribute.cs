
// Type: Intermech.Client.Core.AdministratorOnlyAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

/// <summary>Атрибут определяет, доступно ли свойство только Администратору Системы</summary>
[AttributeUsage(AttributeTargets.Property)]
[Serializable]
public class AdministratorOnlyAttribute : Attribute
{
  private bool _adminOnly;

  public AdministratorOnlyAttribute(bool value) => this._adminOnly = value;

  public bool AdminOnly => this._adminOnly;
}
