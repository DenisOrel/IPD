
// Type: Intermech.Navigator.DBObjects.CurrentObjectColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.Navigator.DBObjects;

/// <summary>Схема колонок для текущего типа объекта</summary>
public class CurrentObjectColumnScheme : ObjectColumnScheme
{
  /// <summary>Название схемы колонок</summary>
  private static readonly string SchemeName = LocalizationHolder.rm.GetString("Client.Core_299");

  /// <summary>Название схемы</summary>
  public override string Name => CurrentObjectColumnScheme.SchemeName;
}
