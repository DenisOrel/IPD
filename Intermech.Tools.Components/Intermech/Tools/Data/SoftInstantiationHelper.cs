// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.SoftInstantiationHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Tools.Data;

public class SoftInstantiationHelper
{
  /// <summary>
  /// Возвращает режим работы автоматической мягкой конкретизации создаваемых связей.
  /// </summary>
  /// <returns>true - автоматическая мягкая конкретизация включена</returns>
  public bool IsAutomaticInstantiationEnabled()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.EnabledAutoSoftInstantiation;
  }

  public bool IsAllowed(int projectTypeId, int partTypeId, int relationTypeId)
  {
    IMSApplicability applicability = MetaDataHelper.GetApplicability(projectTypeId, partTypeId, relationTypeId);
    return applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (applicability.Options & ApplicabilityOptions.SoftInstantiation) != ApplicabilityOptions.None;
  }

  public bool IsAllowedForSomePart(int projectTypeId, int relationTypeId)
  {
    foreach (IMSObjectType applicabilityChildObjectType in MetaDataHelper.GetApplicabilityChildObjectTypes(projectTypeId, relationTypeId))
    {
      if (this.IsAllowed(projectTypeId, applicabilityChildObjectType.ObjectTypeID, relationTypeId))
        return true;
    }
    return false;
  }
}
