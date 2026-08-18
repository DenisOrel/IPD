
// Type: Intermech.Holders.DataHolders
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Holders;

/// <summary>Кэш для зачитанной информации</summary>
public class DataHolders
{
  public static SubjectAreasHolder SubjectAreasHolder = new SubjectAreasHolder();
  public static LanguagesHolder LanguagesHolder = new LanguagesHolder();
  public static AttributeGroupsHolder AttributeGroupsHolder = new AttributeGroupsHolder();
  public static AttributesHolder AttributesHolder = new AttributesHolder();
  public static LevelsHolder LevelsHolder = new LevelsHolder();
  public static ObjectTypesHolder ObjectTypesHolder = new ObjectTypesHolder();
  public static RelationTypesHolder RelationTypesHolder = new RelationTypesHolder();
  public static PhysicalValuesHolder PhysicalValuesHolder = new PhysicalValuesHolder();
  public static LCSchemasHolder LCSchemasHolder = new LCSchemasHolder();
  public static StoragesHolder StoragesHolder = new StoragesHolder();

  public static void Clear()
  {
    DataHolders.SubjectAreasHolder.ClearInfo();
    DataHolders.LanguagesHolder.ClearInfo();
    DataHolders.AttributeGroupsHolder.ClearInfo();
    DataHolders.AttributesHolder.ClearInfo();
    DataHolders.LevelsHolder.ClearInfo();
    DataHolders.ObjectTypesHolder.ClearInfo();
    DataHolders.RelationTypesHolder.ClearInfo();
    DataHolders.PhysicalValuesHolder.ClearInfo();
    DataHolders.LCSchemasHolder.ClearInfo();
    DataHolders.StoragesHolder.ClearInfo();
  }
}
