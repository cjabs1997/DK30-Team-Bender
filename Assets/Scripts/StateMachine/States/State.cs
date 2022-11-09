// Enum used as keys for the StateFactory dict

namespace States
{
    public enum State
    {
        idle,
        move,
        jump,
        fall,
        attack,
        inTerminal,
        delayedJump,
        playerInRange,
        terminalComplete,
        playerOutOfRange
    }
}