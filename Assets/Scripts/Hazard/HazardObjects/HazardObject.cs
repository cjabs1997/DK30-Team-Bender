using System.Collections.Generic;
using UnityEngine;

abstract public class HazardObject : MonoBehaviour
{   
    protected Queue<CommandContext> commandContexts;
    protected CommandContext currentCommandContext;
    protected float commandStartTime;
    abstract protected void HandleExecuteCommand();

    public virtual void ExecuteCommands(Queue<CommandContext> commandContexts)
    {   
        this.commandContexts = commandContexts;
    }
    public virtual void ExecuteCommands(CommandContext[] commands)
    {   
        var queue = new Queue<CommandContext>();
        foreach(var command in commands)
        {
            queue.Enqueue(command);
        }
        this.commandContexts = queue;
    }

}
