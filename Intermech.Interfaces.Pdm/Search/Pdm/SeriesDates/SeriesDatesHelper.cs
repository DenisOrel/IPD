// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.SeriesDatesHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

public static class SeriesDatesHelper
{
  private const string RangeSeparator = ", ";
  private static readonly Regex RangeSeparatorRegex = new Regex(",\\s*", RegexOptions.Compiled);

  public static string ConvertSeriesRangeArrayToString(SeriesRange[] seriesRanges)
  {
    return seriesRanges != null ? string.Join<SeriesRange>(", ", (IEnumerable<SeriesRange>) seriesRanges) : throw new ArgumentNullException(nameof (seriesRanges));
  }

  public static SeriesRange[] ConvertStringToSeriesRangeArray(string @string)
  {
    if (string.IsNullOrEmpty(@string))
      throw new ArgumentException();
    return ((IEnumerable<string>) SeriesDatesHelper.RangeSeparatorRegex.Split(@string)).Select<string, SeriesRange>((Func<string, SeriesRange>) (o => SeriesRange.FromString(o))).ToArray<SeriesRange>();
  }

  public static string ConvertDateRangeArrayToString(DateRange[] dateRange)
  {
    return dateRange != null ? string.Join<DateRange>(", ", (IEnumerable<DateRange>) dateRange) : throw new ArgumentNullException(nameof (dateRange));
  }

  public static DateRange[] ConvertStringToDateRangeArray(string @string)
  {
    if (string.IsNullOrEmpty(@string))
      throw new ArgumentException();
    return ((IEnumerable<string>) SeriesDatesHelper.RangeSeparatorRegex.Split(@string)).Select<string, DateRange>((Func<string, DateRange>) (o => DateRange.FromString(o))).ToArray<DateRange>();
  }

  public static Dictionary<long, Dictionary<long, SeriesDatesPack>> CalculateSeriesDatesIntersectionDictionaryDictionary(
    SeriesDatesPack seriesDatesPack,
    Dictionary<long, Dictionary<long, SeriesDatesPack>> otherVersionsSeriesDatesPackDictionaryDictionary)
  {
    Dictionary<long, Dictionary<long, SeriesDatesPack>> dictionaryDictionary = new Dictionary<long, Dictionary<long, SeriesDatesPack>>();
    foreach (KeyValuePair<long, Dictionary<long, SeriesDatesPack>> datesPackDictionary in otherVersionsSeriesDatesPackDictionaryDictionary)
    {
      Dictionary<long, SeriesDatesPack> dictionary = (Dictionary<long, SeriesDatesPack>) null;
      foreach (KeyValuePair<long, SeriesDatesPack> keyValuePair in datesPackDictionary.Value)
      {
        SeriesDatesPack seriesDatesPack1 = seriesDatesPack.Intersect(keyValuePair.Value);
        if (seriesDatesPack1.Groups.Count > 0)
        {
          if (dictionary == null)
            dictionary = new Dictionary<long, SeriesDatesPack>();
          dictionary[keyValuePair.Key] = seriesDatesPack1;
        }
      }
      if (dictionary != null)
        dictionaryDictionary[datesPackDictionary.Key] = dictionary;
    }
    return dictionaryDictionary;
  }

  public static bool CheckObjectsForFindSeriesDates(long[] objectVersionIds)
  {
    if (objectVersionIds == null)
      throw new ArgumentException();
    if (((IEnumerable<long>) objectVersionIds).Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() > 0)
      throw new ArgumentException();
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectVersionId in objectVersionIds)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, false);
        if (dbObject == null || longList.Contains(dbObject.ID))
          return false;
        longList.Add(dbObject.ID);
      }
    }
    return true;
  }

  public static bool CheckObjectsForSaveSeriesDates(long[] objectVersionIds)
  {
    if (objectVersionIds == null)
      throw new ArgumentException();
    if (((IEnumerable<long>) objectVersionIds).Where<long>((Func<long, bool>) (o => ObjectHelper.IsUnknownObjectVersionID(o))).Count<long>() > 0)
      throw new ArgumentException();
    return ((IEnumerable<long>) objectVersionIds).Where<long>((Func<long, bool>) (o => !ObjectHelper.CheckObjectForModification(o, SeriesDatesConstants.SeriesDatesApplicabilityAttributeTypeID))).Count<long>() == 0;
  }

  public static bool CheckSeriesDatesIntersectionsWithOtherVersions(
    SeriesDatesPack seriesDatesPack,
    Dictionary<long, Dictionary<long, SeriesDatesPack>> otherVersionsSeriesDatesPackDictionaryDictionary,
    out Dictionary<IRange, string> errorDictionary)
  {
    errorDictionary = new Dictionary<IRange, string>();
    foreach (SeriesDatesGroup group in (Collection<SeriesDatesGroup>) seriesDatesPack.Groups)
    {
      SeriesDatesGroup seriesDatesGroup = group;
      foreach (SeriesRange seriesRange1 in (Collection<SeriesRange>) seriesDatesGroup.Series)
      {
        SeriesRange seriesRange = seriesRange1;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<long, Dictionary<long, SeriesDatesPack>> datesPackDictionary in otherVersionsSeriesDatesPackDictionaryDictionary)
        {
          foreach (KeyValuePair<long, SeriesDatesPack> keyValuePair in datesPackDictionary.Value)
          {
            SeriesDatesGroup seriesDatesGroup1 = keyValuePair.Value.Groups.FirstOrDefault<SeriesDatesGroup>((Func<SeriesDatesGroup, bool>) (o => o.HeadProductVersionID == seriesDatesGroup.HeadProductVersionID));
            if (seriesDatesGroup1 != null)
            {
              SeriesRange seriesRange2 = seriesDatesGroup1.Series.FirstOrDefault<SeriesRange>((Func<SeriesRange, bool>) (o =>
              {
                if (seriesRange.IsEmpty)
                  return false;
                if (o.Start <= seriesRange.Start && seriesRange.Start <= o.End || o.Start <= seriesRange.End && seriesRange.End <= o.End)
                  return true;
                return seriesRange.Start <= o.Start && o.End <= seriesRange.End;
              }));
              if (seriesRange2 != null)
                stringBuilder.Append($"Пересечение с диапазоном серий {seriesRange2} для объекта #{keyValuePair.Key}{Environment.NewLine}");
            }
          }
        }
        string str = stringBuilder.ToString();
        if (!string.IsNullOrEmpty(str))
          errorDictionary[(IRange) seriesRange] = str;
      }
      foreach (DateRange date in (Collection<DateRange>) seriesDatesGroup.Dates)
      {
        DateRange dateRange = date;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<long, Dictionary<long, SeriesDatesPack>> datesPackDictionary in otherVersionsSeriesDatesPackDictionaryDictionary)
        {
          foreach (KeyValuePair<long, SeriesDatesPack> keyValuePair in datesPackDictionary.Value)
          {
            SeriesDatesGroup seriesDatesGroup2 = keyValuePair.Value.Groups.FirstOrDefault<SeriesDatesGroup>((Func<SeriesDatesGroup, bool>) (o => o.HeadProductVersionID == seriesDatesGroup.HeadProductVersionID));
            if (seriesDatesGroup2 != null)
            {
              DateRange dateRange1 = seriesDatesGroup2.Dates.FirstOrDefault<DateRange>((Func<DateRange, bool>) (o =>
              {
                if (dateRange.IsEmpty)
                  return false;
                if (o.Start <= dateRange.Start && dateRange.Start <= o.End || o.Start <= dateRange.End && dateRange.End <= o.End)
                  return true;
                return dateRange.Start <= o.Start && o.End <= dateRange.End;
              }));
              if (dateRange1 != null)
                stringBuilder.Append($"Пересечение с диапазоном дат {dateRange1} для объекта #{keyValuePair.Key}{Environment.NewLine}");
            }
          }
        }
        string str = stringBuilder.ToString();
        if (!string.IsNullOrEmpty(str))
          errorDictionary[(IRange) dateRange] = str;
      }
    }
    return errorDictionary.Count == 0;
  }

  public static bool CheckSeriesDatesIntersectionsWithOtherVersions(
    SeriesDatesPack seriesDatesPack,
    Dictionary<long, Dictionary<long, SeriesDatesPack>> otherVersionsSeriesDatesPackDictionaryDictionary,
    out string error)
  {
    Dictionary<IRange, string> errorDictionary = (Dictionary<IRange, string>) null;
    error = (string) null;
    if (!SeriesDatesHelper.CheckSeriesDatesIntersectionsWithOtherVersions(seriesDatesPack, otherVersionsSeriesDatesPackDictionaryDictionary, out errorDictionary))
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<IRange, string> keyValuePair in errorDictionary)
        stringBuilder.Append($"Для диапазона {keyValuePair.Key} {keyValuePair.Value}");
      error = stringBuilder.ToString();
    }
    return errorDictionary == null || errorDictionary.Count == 0;
  }
}
