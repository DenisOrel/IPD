
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersClientService : 
  ICompositionByObjectTypesFiltersClientService
{
  private ICompositionByObjectTypesFilterXmlConverter _xmlConverter;
  private CompositionByObjectTypesFilter[] _filtersForCurrentUser;
  private CompositionByObjectTypesFilter[] _filtersForCurrentRole;

  public CompositionByObjectTypesFiltersClientService(
    ICompositionByObjectTypesFilterXmlConverter xmlConverter)
  {
    this._xmlConverter = xmlConverter != null ? xmlConverter : throw new ArgumentNullException(nameof (xmlConverter));
  }

  public CompositionByObjectTypesFilter[] GetFiltersForCurrentUser()
  {
    if (this._filtersForCurrentUser == null)
      this.RefreshFiltersCache();
    return this._filtersForCurrentUser;
  }

  public CompositionByObjectTypesFilter[] GetFiltersForCurrentRole()
  {
    if (this._filtersForCurrentRole == null)
      this.RefreshFiltersCache();
    return this._filtersForCurrentRole;
  }

  public void RefreshFiltersCache()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICompositionByObjectTypesFiltersServerService customService = (ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService));
      this._filtersForCurrentUser = customService.GetFiltersForCurrentUser(sessionKeeper.Session.SessionGUID);
      this._filtersForCurrentRole = customService.GetFiltersForCurrentRole(sessionKeeper.Session.SessionGUID);
    }
  }

  public void AddFiltersToObjectComposition(long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    object[] source = SelectionWindow.Select("Выберите фильтры.", (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFilterObjectTypeID), typeof (IDBTypedObjectID), SelectionOptions.Default);
    if (source == null || source.Length == 0)
      return;
    long[] array = source.Cast<IDBTypedObjectID>().Select<IDBTypedObjectID, long>((Func<IDBTypedObjectID, long>) (o => o.ObjectID)).ToArray<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session))
        ((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).AddFiltersToObjectComposition(sessionKeeper.Session.SessionGUID, array, objectVersionID);
    }
  }

  public void RemoveFilterFromObjectComposition(long filterVersionID, long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session))
        ((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).RemoveFilterFromObjectComposition(sessionKeeper.Session.SessionGUID, filterVersionID, objectVersionID);
    }
  }

  public void CreateFiltersFromFileAndAddToObjectComposition(long objectVersionID)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string xml = string.Empty;
      using (Stream stream = openFileDialog.OpenFile())
      {
        using (StreamReader streamReader = new StreamReader(stream))
          xml = streamReader.ReadToEnd();
      }
      List<CompositionByObjectTypesFilter> objectTypesFilterList = new List<CompositionByObjectTypesFilter>();
      foreach (CompositionByObjectTypesFilter objectTypesFilter in this._xmlConverter.ConvertFromXml(xml))
      {
        if (this.IsFilterWithNameExistsInObjectComposition(objectTypesFilter.Name, objectVersionID))
        {
          if (MessageBox.Show($"Фильтр с именем '{objectTypesFilter.Name}' существует, заменить?", "Замена фильтра", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            objectTypesFilterList.Add(objectTypesFilter);
        }
        else
          objectTypesFilterList.Add(objectTypesFilter);
      }
      if (objectTypesFilterList.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (NotificationContext.Create(sessionKeeper.Session))
          ((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).CreateFiltersAndAddToObjectComposition(sessionKeeper.Session.SessionGUID, objectTypesFilterList.ToArray(), objectVersionID);
      }
      this.RefreshFiltersCache();
    }
  }

  private bool IsFilterWithNameExistsInObjectComposition(string filterName, long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).IsFilterWithNameExistsInObjectComposition(sessionKeeper.Session.SessionGUID, filterName, objectVersionID);
  }

  public void SaveFiltersToFileFromObjectComposition(long objectVersionID)
  {
    string str = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      str = ((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).CreateTextFromFiltersInObjectComposition(sessionKeeper.Session.SessionGUID, objectVersionID);
    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
    {
      saveFileDialog.RestoreDirectory = true;
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      using (Stream stream = saveFileDialog.OpenFile())
      {
        using (StreamWriter streamWriter = new StreamWriter(stream))
          streamWriter.Write(str);
      }
    }
  }

  public void ConvertFiltersFromUserConfigurationFileToObjects()
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        Intermech.Navigator.CompositionByObjectTypesFilters oldFormatFilters = new Intermech.Navigator.CompositionByObjectTypesFilters();
        oldFormatFilters.Load(sessionKeeper.Session.UserID);
        if (oldFormatFilters.Count <= 0)
          return;
        CompositionByObjectTypesFilter[] newFormat = this.ConvertFiltersFromOldToNewFormat(oldFormatFilters);
        ((ICompositionByObjectTypesFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (ICompositionByObjectTypesFiltersServerService))).CreateFiltersAndAddToCurrentUserConfigurationComposition(sessionKeeper.Session.SessionGUID, newFormat);
        oldFormatFilters.Clear();
        oldFormatFilters.Save(sessionKeeper.Session.UserID);
      }
    }
    catch (Exception ex)
    {
    }
  }

  private CompositionByObjectTypesFilter[] ConvertFiltersFromOldToNewFormat(
    Intermech.Navigator.CompositionByObjectTypesFilters oldFormatFilters)
  {
    List<CompositionByObjectTypesFilter> objectTypesFilterList = new List<CompositionByObjectTypesFilter>();
    foreach (Intermech.Navigator.CompositionByObjectTypesFilter oldFormatFilter in (List<Intermech.Navigator.CompositionByObjectTypesFilter>) oldFormatFilters)
    {
      CompositionByObjectTypesFilter objectTypesFilter = new CompositionByObjectTypesFilter();
      objectTypesFilter.Name = oldFormatFilter.Name;
      foreach (KeyValuePair<Guid, List<Guid>> childObjectType in oldFormatFilter.ChildObjectTypes)
      {
        CompositionByObjectTypesFilterProjectType projectType = CompositionByObjectTypesFiltersHelper.CreateProjectType(MetaDataHelper.GetObjectTypeID(childObjectType.Key));
        List<int> intList = new List<int>();
        foreach (Guid objTypeGuid in childObjectType.Value)
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(objTypeGuid);
          intList.Add(objectTypeId);
        }
        projectType.CheckPartTypesAndDescendants(intList.ToArray());
        objectTypesFilter.ProjectTypes.Add(projectType);
      }
      objectTypesFilterList.Add(objectTypesFilter);
    }
    return objectTypesFilterList.ToArray();
  }
}
