// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationWithCadGuidDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using System;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс содержащий пару идентификатор_связи-ГУИД_CAD_входимости </summary>
public class RelationWithCadGuidDescriptor
{
  private long? _projID;
  private long? _partID;
  private long _relationID;
  private Guid? _cadEnteranceGuid;
  private bool _foundInNewStructure;

  /// <summary> Конструктор </summary>
  /// <param name="relationID"> Идентификатор связи </param>
  /// <param name="partID"> Идентификатор связи НА которую накладывается связь </param>
  /// <param name="cadEnteranceGuid"> GUID CAD входимости. Guid.Empty в том случае, если атрибут у связи отсутствует или не заполнен </param>
  public RelationWithCadGuidDescriptor(long relationID, long partID, Guid cadEnteranceGuid)
  {
    this._relationID = relationID;
    this._partID = new long?(partID);
    this._cadEnteranceGuid = new Guid?(cadEnteranceGuid);
  }

  /// <summary> Идентификатор объекта ИЗ которого наложена связь </summary>
  public long ProjID
  {
    get
    {
      if (!this._projID.HasValue)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(this._relationID);
          if (relation != null)
            this._projID = new long?(relation.ProjID);
        }
      }
      return this._projID.HasValue ? this._projID.Value : -1L;
    }
    set => this._projID = new long?(value);
  }

  /// <summary> Идентификатор объекта НА который наложена связь </summary>
  public long PartID
  {
    get
    {
      if (!this._partID.HasValue)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(this._relationID);
          if (relation != null)
            this._partID = new long?(relation.PartID);
        }
      }
      return this._partID.HasValue ? this._partID.Value : -1L;
    }
    set => this._partID = new long?(value);
  }

  /// <summary> Идентификатор связи </summary>
  public long RelationID => this._relationID;

  /// <summary> Иденгтификатор CAD входимости. Если == Guid.Empty то данный параметр или отсутствует у изделия или не заполнен </summary>
  public Guid CadEnteranceGuid
  {
    get
    {
      if (!this._cadEnteranceGuid.HasValue)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(this._relationID);
          if (relation != null)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(AvsIDCache.Attr_CADInteranceIdentify);
            if (attributeById != null)
            {
              object g = attributeById.Value;
              switch (g)
              {
                case DBNull _:
                  this._cadEnteranceGuid = new Guid?(Guid.Empty);
                  break;
                case Guid _:
                  this._cadEnteranceGuid = (Guid?) g;
                  break;
                case string _:
                  this._cadEnteranceGuid = new Guid?(new Guid((string) g));
                  break;
              }
            }
          }
          else
            this._cadEnteranceGuid = new Guid?(Guid.Empty);
        }
      }
      return !this._cadEnteranceGuid.HasValue ? Guid.Empty : this._cadEnteranceGuid.Value;
    }
    set => this._cadEnteranceGuid = new Guid?(value);
  }

  /// <summary> Признак того, что данный дескриптор уже был обработан </summary>
  public bool FoundInNewStructure
  {
    get => this._foundInNewStructure;
    set => this._foundInNewStructure = value;
  }
}
