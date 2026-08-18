
// Type: Intermech.Search.CompositionByObjectTypesFilters.ICompositionByObjectTypesFiltersClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.CompositionByObjectTypesFilters;

public interface ICompositionByObjectTypesFiltersClientService
{
  CompositionByObjectTypesFilter[] GetFiltersForCurrentUser();

  CompositionByObjectTypesFilter[] GetFiltersForCurrentRole();

  void RefreshFiltersCache();

  void AddFiltersToObjectComposition(long objectVersionID);

  void RemoveFilterFromObjectComposition(long filterVersionID, long objectVersionID);

  void CreateFiltersFromFileAndAddToObjectComposition(long objectVersionID);

  void SaveFiltersToFileFromObjectComposition(long objectVersionID);

  void ConvertFiltersFromUserConfigurationFileToObjects();
}
