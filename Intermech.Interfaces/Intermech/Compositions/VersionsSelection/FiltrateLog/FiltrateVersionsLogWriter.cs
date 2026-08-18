
// Type: Intermech.Compositions.VersionsSelection.FiltrateLog.FiltrateVersionsLogWriter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Compositions.VersionsSelection.FiltrateLog
{
    public class FiltrateVersionsLogWriter
    {
      private FiltrateVersionsLog _log;

      public FiltrateVersionsLogWriter(FiltrateVersionsLog log)
      {
        this.CheckArgumentNotNull((object) log, nameof (log));
        this._log = log;
      }

      public void Write(MyVersionElement version)
      {
        this.WriteToLog(this._log, version.RelTypeID, version.PrjLinkID, version.ID, version.State, version.Weigth);
      }

      public void Write(int attributeTypeID, MyVersionElement version)
      {
        this.WriteToLog(this._log, version.RelTypeID, version.PrjLinkID, version.ID, version.State, version.Weigth, attributeTypeID, -1);
      }

      private void CheckArgumentNotNull(object argument, string argumentName)
      {
        if (argument == null)
          throw new ArgumentNullException(argumentName);
      }

      /// <summary>Записать в протокол запись</summary>
      /// <param name="log">Протокол</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="state">Статус подобранной версии</param>
      /// <returns>Запись из протокола или null</returns>
      private FiltrateVersionsLogEntry WriteToLog(
        FiltrateVersionsLog log,
        int relTypeID,
        long prjLinkID,
        long objectID,
        ObjectFiltrationState state)
      {
        if (log == null || relTypeID == -1 || prjLinkID == 0L || objectID == 0L)
          return (FiltrateVersionsLogEntry) null;
        FiltrateVersionsLogEntry entry = log[relTypeID, prjLinkID, objectID] ?? new FiltrateVersionsLogEntry();
        entry.PrjLinkID = prjLinkID;
        entry.ObjectID = objectID;
        entry.State = state;
        log.Add(relTypeID, entry);
        return entry;
      }

      /// <summary>Записать в протокол запись</summary>
      /// <param name="log">Протокол</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="state">Статус подобранной версии</param>
      /// <param name="weight">"Вес", с которым подобралась или была отбракована указанная версия</param>
      /// <returns>Запись из протокола или null</returns>
      private FiltrateVersionsLogEntry WriteToLog(
        FiltrateVersionsLog log,
        int relTypeID,
        long prjLinkID,
        long objectID,
        ObjectFiltrationState state,
        int weight)
      {
        if (log == null || relTypeID == -1 || prjLinkID == 0L || objectID == 0L)
          return (FiltrateVersionsLogEntry) null;
        FiltrateVersionsLogEntry entry = log[relTypeID, prjLinkID, objectID] ?? new FiltrateVersionsLogEntry();
        entry.PrjLinkID = prjLinkID;
        entry.ObjectID = objectID;
        entry.State = state;
        entry.Weight = weight;
        log.Add(relTypeID, entry);
        return entry;
      }

      /// <summary>Записать в протокол запись</summary>
      /// <param name="log">Протокол</param>
      /// <param name="relTypeID">Идентификатор типа связи</param>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="state">Статус подобранной версии</param>
      /// <param name="weight">"Вес", с которым подобралась или была отбракована указанная версия</param>
      /// <param name="mainAttribute">Идентификатор атрибута, по значению которого была подобрана данная версия по
      /// основным критериям подбора версий.
      /// Значение Intermech.Consts.UnknownAttributeId означает, что версия не была
      /// подобрана по основным критериям подбора версий</param>
      /// <param name="criterion">Номер основного критерия, по которому была подобрана данная версия.
      /// Значение -1 означает, что версия не была подобрана по основным критериям
      /// подбора версий</param>
      /// <returns>Запись из протокола или null</returns>
      private FiltrateVersionsLogEntry WriteToLog(
        FiltrateVersionsLog log,
        int relTypeID,
        long prjLinkID,
        long objectID,
        ObjectFiltrationState state,
        int weight,
        int mainAttribute,
        int criterion)
      {
        if (log == null || relTypeID == -1 || prjLinkID == 0L || objectID == 0L)
          return (FiltrateVersionsLogEntry) null;
        FiltrateVersionsLogEntry entry = log[relTypeID, prjLinkID, objectID] ?? new FiltrateVersionsLogEntry();
        entry.PrjLinkID = prjLinkID;
        entry.ObjectID = objectID;
        entry.State = state;
        entry.Weight = weight;
        entry.MainAttribute = mainAttribute;
        entry.Criterion = criterion;
        log.Add(relTypeID, entry);
        return entry;
      }
    }
}
