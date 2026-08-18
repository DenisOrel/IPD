// Decompiled with JetBrains decompiler
// Type: Intermech.Settings.DBPersistentSettingsObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Settings;

/// <summary>
/// Реализует базовый класс для объектов клиентских настроек, сохраняемых в базе данных IPS.
/// </summary>
public abstract class DBPersistentSettingsObject : PersistentSettingsObject, ICloneable
{
  protected readonly string moduleName;
  protected readonly string sectionName;

  protected DBPersistentSettingsObject(string moduleName, string sectionName)
  {
    if (moduleName == null)
      throw new ArgumentNullException(nameof (moduleName));
    if (sectionName == null)
      throw new ArgumentNullException(nameof (sectionName));
    this.moduleName = moduleName;
    this.sectionName = sectionName;
  }

  public void Assign(SettingsObject source)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    lock (this)
      this.DoAssign(source);
  }

  protected virtual void DoAssign(SettingsObject source)
  {
  }

  object ICloneable.Clone() => this.DoClone();

  protected object DoClone()
  {
    lock (this)
    {
      object emptyObject = this.CreateEmptyObject();
      if (emptyObject == null)
        throw new InvalidOperationException("Method CreateEmptyObject() must return a new object.");
      if (emptyObject.GetType() != this.GetType())
        throw new InvalidOperationException($"Method CreateEmptyObject() must return a new object of type '{this.GetType()}'.");
      ((DBPersistentSettingsObject) emptyObject).Assign((SettingsObject) this);
      return emptyObject;
    }
  }

  protected virtual object CreateEmptyObject() => Activator.CreateInstance(this.GetType());

  public sealed override void Save()
  {
    lock (this)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.DoSave(sessionKeeper.Session);
    }
  }

  protected virtual void DoSave(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
  }

  public sealed override void Load()
  {
    lock (this)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.DoLoad(sessionKeeper.Session);
    }
  }

  protected virtual void DoLoad(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
  }

  protected string ReadUserString(IUserSession session, string parameterName)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (parameterName == null)
      throw new ArgumentNullException(nameof (parameterName));
    return session.Configurations.ReadStringNoCache(this.moduleName, this.sectionName, parameterName, false);
  }

  protected string ReadGlobalString(IUserSession session, string parameterName)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (parameterName == null)
      throw new ArgumentNullException(nameof (parameterName));
    return session.Configurations.ReadStringNoCache(this.moduleName, this.sectionName, parameterName, true);
  }

  protected void WriteUserString(IUserSession session, string parameterName, string parameterValue)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (parameterName == null)
      throw new ArgumentNullException(nameof (parameterName));
    session.Configurations.WriteString(this.moduleName, this.sectionName, parameterName, parameterValue, session.UserID);
  }

  protected void WriteGlobalString(
    IUserSession session,
    string parameterName,
    string parameterValue)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (parameterName == null)
      throw new ArgumentNullException(nameof (parameterName));
    session.Configurations.WriteString(this.moduleName, this.sectionName, parameterName, parameterValue, 0L);
  }
}
