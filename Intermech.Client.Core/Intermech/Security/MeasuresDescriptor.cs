
// Type: Intermech.Security.MeasuresDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Persistence;


namespace Intermech.Security;

public class MeasuresDescriptor : TopObjectsDescriptor
{
  /// <summary>Создает дескриптор.</summary>
  public MeasuresDescriptor()
    : base(ClientConsts.MeasuresCategoryID, 0, LocalizationHolder.rm.GetString("Client.Core_1123"), MeasuresDescriptor.GetPhysicValueTypeID())
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state">Сериализованное представление дескриптора</param>
  protected MeasuresDescriptor(PersistentState state)
    : this()
  {
  }

  /// <summary>Выполняет сериализацию дескриптора.</summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
  }

  private static int GetPhysicValueTypeID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.IdentHelper.PhysicValueTypeID;
  }
}
