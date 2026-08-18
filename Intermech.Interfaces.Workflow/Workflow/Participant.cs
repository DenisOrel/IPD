// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Participant
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System;

#nullable disable
namespace Intermech.Workflow;

[Serializable]
public class Participant : IValidatedItem
{
  public long ID;
  public ParticipantKind Kind;
  /// <summary>
  /// Заполнен только в случае получения объекта по Гуиду, из портфеля!
  /// </summary>
  protected internal int _objectType;
  protected internal Guid _guid;
  protected internal string _caption;

  public Participant(ParticipantKind kind, long id)
  {
    this.Kind = kind;
    this.ID = id;
  }

  public string DisplayName
  {
    get
    {
      if (this.Caption != null)
        return this.Caption;
      if (this.ID == 0L && this.Guid != Guid.Empty)
        return $"({this.Guid})";
      return ParticipantList.OnGetParticipantName != null ? ParticipantList.OnGetParticipantName(this) : "?";
    }
  }

  public override bool Equals(object obj)
  {
    if (!(obj is Participant))
      return base.Equals(obj);
    Participant participant = (Participant) obj;
    return participant.Kind == this.Kind && participant.ID == this.ID && participant.Guid == this.Guid;
  }

  public override int GetHashCode() => base.GetHashCode();

  /// <summary>Используется для экспорта/импорта</summary>
  public Guid Guid => this._guid;

  /// <summary>Используется для экспорта/импорта</summary>
  internal string Caption => this._caption;

  public bool Invalid => this.Caption != null || this._objectType == wfConsts.IncompleteObjectType;
}
