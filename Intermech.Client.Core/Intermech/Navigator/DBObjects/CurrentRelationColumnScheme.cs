
// Type: Intermech.Navigator.DBObjects.CurrentRelationColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.Navigator.DBObjects;

/// <summary>Схема колонок для текущего типа связи</summary>
public class CurrentRelationColumnScheme : RelationColumnScheme
{
  /// <summary>Название схемы колонок</summary>
  private static readonly string SchemeName = LocalizationHolder.rm.GetString("Client.Core_321");

  /// <summary>Название схемы</summary>
  public override string Name => CurrentRelationColumnScheme.SchemeName;
}
