// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CDBRelationsApplicability
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Объект, описывающий контекст связи типа RelationType между объектами типа ObjectType (дочерний) и
/// InObjectType (родительский).
/// Прокси-класс для реализации IDBRelationsApplicability на стороне клиента.
/// </summary>
internal class CDBRelationsApplicability : CacheObject, IDBRelationsApplicability, IDeletable
{
  /// <summary>Имя идентификационного поля объекта данной категории</summary>
  protected string _DBKeyField = "";
  /// <summary>Если true, то фильтровать по предметным областям</summary>
  protected bool _AreaSupport;
  /// <summary>Если true, то фильтровать по языковым вариантам</summary>
  protected bool _LanguageSupport;

  public override object GetServerObject() => (object) this.ServerSideIntf;

  /// <summary>Создать экземпляр контекста связи</summary>
  /// <param name="uSession">Клиентская сессия</param>
  /// <param name="applicabilityID">Идентификатор контекста связи</param>
  public CDBRelationsApplicability(ClientSession uSession, int applicabilityID)
    : base(uSession, applicabilityID)
  {
    this._DBKeyField = "F_APPLICABILITY_ID";
    this._AreaSupport = false;
    this._LanguageSupport = false;
    DataRow[] dataRowArray = uSession.ClientCache.GetTable("IMS_TYPES_APPLICABILITY").Select("F_APPLICABILITY_ID = " + applicabilityID.ToString());
    if (dataRowArray.Length == 0)
      throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_6"), (object) applicabilityID));
    this.paramsTable.Create(dataRowArray[0]);
    this.InitOptions(4, 0L, "IMS_TYPES_APPLICABILITY", LocalizationHolder.rm.GetString("Interfaces.Client_117"));
  }

  /// <summary>
  /// Интерфейс IDBRelationsApplicability с серверной стороны
  /// </summary>
  internal IDBRelationsApplicability ServerSideIntf
  {
    [DebuggerStepThrough] get
    {
      IUserSession userSession = (IUserSession) this._clientSession;
      IClientSession clientSession = (IClientSession) this._clientSession;
      if (clientSession != null)
        userSession = clientSession.Session;
      return userSession.GetRelationsApplicabilityCollection().GetApplicability(this._id);
    }
  }

  /// <summary>
  /// Удалить информацию о текущей применяемости из вспомогательного кэша
  /// </summary>
  private void InvalidateInCache()
  {
    CDBRelationsApplicabilityCache.Remove(new MyCompositeKey(new object[3]
    {
      (object) this.RelationType,
      (object) this.ObjectType,
      (object) this.InObjectType
    }));
    CDBRelationsApplicabilityCache.Remove(new MyCompositeKey(new object[3]
    {
      (object) -1,
      (object) this.ObjectType,
      (object) -1
    }));
    CDBRelationsApplicabilityCache.Remove(new MyCompositeKey(new object[3]
    {
      (object) -1,
      (object) -1,
      (object) this.InObjectType
    }));
    CDBRelationsApplicabilityCache.Remove(new MyCompositeKey(new object[3]
    {
      (object) this.RelationType,
      (object) -1,
      (object) this.InObjectType
    }));
    CDBRelationsApplicabilityCache.Remove(new MyCompositeKey(new object[3]
    {
      (object) this.RelationType,
      (object) this.ObjectType,
      (object) -1
    }));
  }

  /// <summary>Идентификатор контекста связи</summary>
  public int ApplicabilityID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._id;
    }
  }

  /// <summary>Тип дочернего объекта</summary>
  public int ObjectType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_OBJECT_TYPE"]);
    }
  }

  /// <summary>Тип родительского объекта</summary>
  public int InObjectType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_INOBJECT_TYPE"]);
    }
  }

  /// <summary>Тип связи</summary>
  public int RelationType
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_RELATION_TYPE"]);
    }
  }

  /// <summary>
  /// Если true, то при создании версии родительского объекта нужно создавать версии дочерних объектов
  /// </summary>
  public bool CloneChildRelations
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_CLONE_RELATIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.CloneChildRelations == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.CloneChildRelations = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Максимально допустимое количество таких связей. Если = Int32.MaximumValue, то бесконечное
  /// </summary>
  public int MaximumLinks
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_MAX_LINKS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.MaximumLinks == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.MaximumLinks = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>Свойство обязательности связи</summary>
  public ApplicabilityModes ApplicabilityMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (ApplicabilityModes) Convert.ToInt32(this.paramsTable[0]["F_MIN_LINKS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.ApplicabilityMode == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.ApplicabilityMode = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Способ обрабатки удаление объектов, связанных этой связью
  /// </summary>
  public RelationConstraintModes RelationConstraintMode
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (RelationConstraintModes) Convert.ToInt32(this.paramsTable[0]["F_CONSTRAINT_MODE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.RelationConstraintMode == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.RelationConstraintMode = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>Удалить контекст связи</summary>
  /// <returns>0, если удаление успешно</returns>
  public int Delete()
  {
    this._clientSession.Guard.ValidateCall();
    this.InvalidateInCache();
    int num = this.ServerSideIntf.Delete();
    this.ReloadClientCache();
    return num;
  }

  /// <summary>Суммарные свойства контекста связи</summary>
  public RelationsApplicabilityProperties PropertiesStructure
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return new RelationsApplicabilityProperties(this.ApplicabilityID, this.ObjectType, this.InObjectType, this.RelationType, this.CloneChildRelations, this.MaximumLinks, this.ApplicabilityMode, this.RelationConstraintMode, this.CheckoutFiles, this.IsContent, this.Options);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      this.ServerSideIntf.PropertiesStructure = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>
  /// Влияет ли данная связь на содержимое родительского объекта. Если да, то при модификации
  /// атрибутов этой связи меняется дата модификации родительского объекта. Также при создании
  /// объекта по прототипу родительского объекта копируются все связи, у которых IsContent == true
  /// </summary>
  public bool IsContent
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToInt32(this.paramsTable[0]["F_CONTENT"]) == 1;
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.IsContent == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.IsContent = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>Опции связи</summary>
  public ApplicabilityOptions Options
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return (ApplicabilityOptions) Convert.ToInt32(this.paramsTable[0]["F_OPTIONS"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.Options == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.Options = value;
      this.ReloadClientCache();
    }
  }

  /// <summary>Извлекать ли на диск файлы по таким связям</summary>
  public bool CheckoutFiles
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return Convert.ToBoolean(this.paramsTable[0]["F_CHKOUTFILE"]);
    }
    [DebuggerStepThrough] set
    {
      this._clientSession.Guard.ValidateCall();
      if (this.CheckoutFiles == value)
        return;
      this.InvalidateInCache();
      this.ServerSideIntf.CheckoutFiles = value;
      this.ReloadClientCache();
    }
  }

  public int RelationsCount
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this.ServerSideIntf.RelationsCount;
    }
  }

  /// <summary>Удалить контекст связи</summary>
  /// <param name="DeleteMode">Параметр для указания доп. информации по удалению.
  /// Если не нужен в конкретной реализации, то туда будут передавать 0.</param>
  /// <returns>Результаты удаления</returns>
  public int Delete(long DeleteMode)
  {
    this._clientSession.Guard.ValidateCall();
    int num = this.ServerSideIntf.Delete();
    this.InvalidateInCache();
    this.ReloadClientCache();
    return num;
  }
}
