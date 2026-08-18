
// Type: Intermech.Bars.CommandManager
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Bars
{
    public class CommandManager : ICommandManager
    {
      private Hashtable _itemTable = new Hashtable();
      private ArrayList _targets = new ArrayList();
      private Dictionary<string, CommandState> _states = new Dictionary<string, CommandState>();
      private ICommandTarget _activeTarget;

      public ICommandState Add(params ButtonItemBase[] controls)
      {
        return controls.Length != 0 ? this.Add(controls[0].CommandName, controls) : (ICommandState) null;
      }

      public ICommandState Add(string commandName, params ButtonItemBase[] controls)
      {
        if (commandName == null || commandName.Length == 0)
          throw new ArgumentException("commandName can't be empty.");
        int length = controls.Length;
        if (length == 0)
          return (ICommandState) null;
        ButtonItemBase control1 = controls[0];
        CommandState commandState = this.GetCommandState(commandName, control1);
        for (int index = 0; index < length; ++index)
        {
          ButtonItemBase control2 = controls[index];
          if (!commandState.CountainsItem(control2))
          {
            control2.Click += new EventHandler(this.Control_Click);
            control2.Disposed += new EventHandler(this.control_Disposed);
            commandState.AddItem(control2);
            this._itemTable[(object) control2] = (object) commandName;
          }
        }
        if (commandState.ActiveItem == null)
          commandState.ActiveItem = controls[0];
        return (ICommandState) commandState;
      }

      private void control_Disposed(object sender, EventArgs e)
      {
        if (!(sender is ButtonItemBase buttonItemBase))
          return;
        buttonItemBase.Click -= new EventHandler(this.Control_Click);
        buttonItemBase.Disposed -= new EventHandler(this.control_Disposed);
        if (this._itemTable.ContainsKey((object) buttonItemBase))
          this._itemTable.Remove((object) buttonItemBase);
        CommandState commandState = this.GetCommandState(buttonItemBase.CommandName, buttonItemBase);
        if (commandState == null || !(commandState.Items is ArrayList) || !(commandState.Items as ArrayList).Contains((object) buttonItemBase))
          return;
        commandState.RemoveItem(buttonItemBase);
      }

      public void AddTarget(ICommandTarget target)
      {
        this._targets.Insert(0, (object) target);
        this.QueryStatus();
      }

      public void RemoveTarget(ICommandTarget target)
      {
        this._targets.Remove((object) target);
        this.QueryStatus();
      }

      public void QueryStatus()
      {
        try
        {
          this.BeginQuery();
          foreach (ICommandState commandState in this._states.Values)
            this.QueryStatus(commandState);
        }
        finally
        {
          this.EndQuery();
        }
      }

      private void BeginQuery()
      {
        if (this._activeTarget is ICommandTarget2 activeTarget)
          activeTarget.BeginQuery();
        foreach (ICommandTarget target in this._targets)
        {
          if (target != this._activeTarget && target is ICommandTarget2 commandTarget2)
            commandTarget2.BeginQuery();
        }
      }

      private void EndQuery()
      {
        if (this._activeTarget is ICommandTarget2 activeTarget)
          activeTarget.EndQuery();
        foreach (ICommandTarget target in this._targets)
        {
          if (target != this._activeTarget && target is ICommandTarget2 commandTarget2)
            commandTarget2.EndQuery();
        }
      }

      public void QueryStatus(ICommandState commandState)
      {
        if (this._activeTarget != null && this._activeTarget.QueryStatus(commandState))
          return;
        foreach (ICommandTarget target in this._targets)
        {
          if (target != this._activeTarget && target.QueryStatus(commandState))
            return;
        }
        commandState.Enabled = false;
      }

      internal CommandState _Find(string commandName)
      {
        CommandState commandState = (CommandState) null;
        this._states.TryGetValue(commandName, out commandState);
        return commandState;
      }

      public ICommandState FindCommand(string commandName) => (ICommandState) this._Find(commandName);

      private CommandState GetCommandState(string commandName, ButtonItemBase control)
      {
        CommandState commandState1 = this._Find(commandName);
        if (commandState1 != null)
          return commandState1;
        CommandState commandState2 = new CommandState(commandName);
        commandState2.ActiveItem = control;
        this._states.Add(commandName, commandState2);
        return commandState2;
      }

      public bool Execute(ICommandState commandState)
      {
        if (commandState != null && commandState.Enabled)
        {
          if (this._activeTarget != null && this._activeTarget.Execute(commandState))
          {
            this.QueryStatus();
            return true;
          }
          foreach (ICommandTarget target in this._targets)
          {
            if (target.Execute(commandState))
            {
              this.QueryStatus();
              return true;
            }
          }
        }
        return false;
      }

      private void Control_Click(object sender, EventArgs e)
      {
        if (!(sender is ButtonItemBase buttonItemBase))
          return;
        if (!this._itemTable.Contains((object) buttonItemBase) && buttonItemBase.Tag is MenuButtonItem)
          buttonItemBase = (ButtonItemBase) (buttonItemBase.Tag as MenuButtonItem);
        string commandName = (string) this._itemTable[(object) buttonItemBase];
        if (commandName == null)
          return;
        CommandState commandState = this.GetCommandState(commandName, buttonItemBase);
        commandState.ActiveItem = buttonItemBase;
        this.Execute((ICommandState) commandState);
      }

      public ICommandTarget ActiveTarget
      {
        get => this._activeTarget;
        set
        {
          if (this._activeTarget == value)
            return;
          this._activeTarget = value;
          this.QueryStatus();
        }
      }
    }
}
