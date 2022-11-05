using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class HazardObject : MonoBehaviour
{   
    protected Queue<HazardCommand> commands;
    protected HazardCommand currentCommand;
    protected float commandStartTime;
    abstract protected void HandleExecuteCommand();

    public virtual void ExecuteCommands(Queue<HazardCommand> commands)
    {   
        this.commands = commands;
    }

}
