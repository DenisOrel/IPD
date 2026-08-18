// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationWithCadGuidDescriptorList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс-список дескрипторов содержащих пару идентификатор_связи-ГУИД_CAD_входимости </summary>
public class RelationWithCadGuidDescriptorList
{
  private Dictionary<long, RelationWithCadGuidDescriptor> _relIDToDesctiptorHash;
  private Dictionary<Guid, RelationWithCadGuidDescriptor> _cadGuidToDesctiptorHash;
  private ObjectLinksList<RelationWithCadGuidDescriptor> _objectIDToLinkedDescriptorsList;

  /// <summary> Конструктор </summary>
  public RelationWithCadGuidDescriptorList()
  {
    this._relIDToDesctiptorHash = new Dictionary<long, RelationWithCadGuidDescriptor>();
    this._cadGuidToDesctiptorHash = new Dictionary<Guid, RelationWithCadGuidDescriptor>();
    this._objectIDToLinkedDescriptorsList = new ObjectLinksList<RelationWithCadGuidDescriptor>();
  }

  /// <summary> Конструктор </summary>
  public RelationWithCadGuidDescriptorList(int capacity)
  {
    this._relIDToDesctiptorHash = new Dictionary<long, RelationWithCadGuidDescriptor>(capacity);
    this._cadGuidToDesctiptorHash = new Dictionary<Guid, RelationWithCadGuidDescriptor>(capacity);
    this._objectIDToLinkedDescriptorsList = new ObjectLinksList<RelationWithCadGuidDescriptor>(capacity);
  }

  /// <summary> Хэш-таблица, где ключём выступает идентификатор связи, значением - дескриптор пары идентификатор_связи-ГУИД_CAD_входимости </summary>
  protected Dictionary<long, RelationWithCadGuidDescriptor> RelIDToDesctiptorHash
  {
    get => this._relIDToDesctiptorHash;
  }

  /// <summary> Хэш-таблица, где ключём выступает ГУИД CAD системы, значением - дескриптор пары идентификатор_связи-ГУИД_CAD_входимости </summary>
  protected Dictionary<Guid, RelationWithCadGuidDescriptor> CadGuidToDesctiptorHash
  {
    get => this._cadGuidToDesctiptorHash;
  }

  /// <summary>
  /// Хитрая хэш-таблица где ключём выступает идентификатор объекта НА который ссылается связь,
  ///   значением - дескриптор пары идентификатор_связи-ГУИД_CAD_входимости
  ///   содержит только дескрипторы с незаполненным GUID-ом CAD входимости
  /// </summary>
  protected ObjectLinksList<RelationWithCadGuidDescriptor> ObjectIDToLinkedDescriptorsList
  {
    get => this._objectIDToLinkedDescriptorsList;
  }

  public RelationWithCadGuidDescriptor AddDescriptor(long relationID, long partID, Guid cadGuid)
  {
    if (relationID == -1L || relationID == 0L || partID == -1L || partID == 0L)
      return (RelationWithCadGuidDescriptor) null;
    RelationWithCadGuidDescriptor descriptorByRelationId = this.GetDescriptorByRelationID(relationID);
    if (descriptorByRelationId != null)
      return descriptorByRelationId;
    RelationWithCadGuidDescriptor descriptorByCadGuid = this.GetDescriptorByCadGuid(cadGuid);
    if (descriptorByCadGuid != null)
      return descriptorByCadGuid;
    RelationWithCadGuidDescriptor objectLink = new RelationWithCadGuidDescriptor(relationID, partID, cadGuid);
    this._relIDToDesctiptorHash[relationID] = objectLink;
    this._cadGuidToDesctiptorHash[cadGuid] = objectLink;
    this._objectIDToLinkedDescriptorsList.RegisterObjectAndLink(partID, objectLink);
    return objectLink;
  }

  /// <summary> Получить дексриптор по идентификатору связи </summary>
  public RelationWithCadGuidDescriptor GetDescriptorByRelationID(long realtionID)
  {
    if (realtionID == 0L || realtionID == -1L)
      return (RelationWithCadGuidDescriptor) null;
    RelationWithCadGuidDescriptor cadGuidDescriptor;
    return this._relIDToDesctiptorHash.TryGetValue(realtionID, out cadGuidDescriptor) ? cadGuidDescriptor : (RelationWithCadGuidDescriptor) null;
  }

  /// <summary> Получить дексриптор по GUID-у CAD входимости </summary>
  public RelationWithCadGuidDescriptor GetDescriptorByCadGuid(Guid cadGuid)
  {
    if (cadGuid == Guid.Empty)
      return (RelationWithCadGuidDescriptor) null;
    RelationWithCadGuidDescriptor cadGuidDescriptor;
    return this._cadGuidToDesctiptorHash.TryGetValue(cadGuid, out cadGuidDescriptor) ? cadGuidDescriptor : (RelationWithCadGuidDescriptor) null;
  }

  /// <summary> Список дескрипторов, для которых ещё не обнаружено соответствие в переданом составе </summary>
  public ReadOnlyCollection<RelationWithCadGuidDescriptor> GetDescriptorsLinkedToObjectWithID(
    long objectID)
  {
    return this._objectIDToLinkedDescriptorsList[objectID];
  }

  /// <summary> Пометить дексрипторы как найденные </summary>
  public void MarkDescriptorsWithCadGuidsAsFound(IList<Guid> cadGuids)
  {
    if (cadGuids == null)
      return;
    foreach (Guid cadGuid in (IEnumerable<Guid>) cadGuids)
    {
      RelationWithCadGuidDescriptor descriptorByCadGuid = this.GetDescriptorByCadGuid(cadGuid);
      if (descriptorByCadGuid != null)
        this.MarkDescriptorAsFound(descriptorByCadGuid);
    }
  }

  /// <summary> Пометить дексрипторы как найденные </summary>
  public void MarkDescriptorsWithCadGuidsAsFound(IList<RelationWithCadGuidDescriptor> descriptors)
  {
    if (descriptors == null)
      return;
    foreach (RelationWithCadGuidDescriptor descriptor in (IEnumerable<RelationWithCadGuidDescriptor>) descriptors)
    {
      if (descriptor != null)
        this.MarkDescriptorAsFound(descriptor);
    }
  }

  /// <summary> Пометить дексриптор как найденные </summary>
  public void MarkDescriptorAsFound(
    RelationWithCadGuidDescriptor relationWithCadGuidDescriptor)
  {
    this.MarkDescriptorAsFound(relationWithCadGuidDescriptor, false);
  }

  /// <summary> Пометить дексриптор как найденные </summary>
  public void MarkDescriptorAsFound(
    RelationWithCadGuidDescriptor relationWithCadGuidDescriptor,
    bool deleteFromHashTables)
  {
    if (relationWithCadGuidDescriptor == null)
      return;
    this._objectIDToLinkedDescriptorsList.UnregisterObjectLink(relationWithCadGuidDescriptor.PartID, relationWithCadGuidDescriptor);
    if (deleteFromHashTables)
    {
      this._relIDToDesctiptorHash.Remove(relationWithCadGuidDescriptor.RelationID);
      if (relationWithCadGuidDescriptor.CadEnteranceGuid != Guid.Empty)
        this._cadGuidToDesctiptorHash.Remove(relationWithCadGuidDescriptor.CadEnteranceGuid);
    }
    relationWithCadGuidDescriptor.FoundInNewStructure = true;
  }

  /// <summary> Получить GUID CAD входимости по идентификатору связи </summary>
  public Guid? GetCADGuidByRelationID(long relationID)
  {
    return this.GetDescriptorByRelationID(relationID)?.CadEnteranceGuid;
  }

  /// <summary> Получить идентификатор связи по GUID-у CAD входимости </summary>
  public long? GetRelationByCadGuid(Guid cadGuid)
  {
    return this.GetDescriptorByCadGuid(cadGuid)?.RelationID;
  }
}
