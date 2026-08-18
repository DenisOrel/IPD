
// Type: Intermech.Bars.ICommandManager
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll


namespace Intermech.Bars
{
    public interface ICommandManager
    {
      ICommandState Add(params ButtonItemBase[] controls);

      ICommandState Add(string commandName, params ButtonItemBase[] controls);

      ICommandState FindCommand(string commandName);

      bool Execute(ICommandState commandState);

      void AddTarget(ICommandTarget target);

      void RemoveTarget(ICommandTarget target);

      void QueryStatus();

      void QueryStatus(ICommandState commandState);

      ICommandTarget ActiveTarget { get; set; }
    }
}
