using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : StateController<PlayerState, PlayerStateFactory>
{
    public Terminal CurrentTerminal { get; set; }
    [SerializeField] GameEvent _playerDeadEvent;
    [SerializeField] GameEvent _playerDamagedEvent;
    [SerializeField] GameEvent _pauseEvent;



    private bool _damageable { get; set; } // This and below are not great but make the code trivial b/c no state integration soo....
    private bool _dead { get; set; }
    [SerializeField] private PlayerStats _stats;
    private Color _startingColor { get; set; }
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        _damageable = true;
        _stats.CurrentHealth = _stats.MaxHealth; // If we are loading new scenes this will need to get moved somewhere else...
    }

    private void Start()
    {
        _startingColor = this._spriteRenderer.color;
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate(this);
    }

    private void Update()
    {
        this.SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        currentState.StateUpdate(this);

        if (Input.GetKeyDown(KeyCode.E) && CurrentTerminal != null)
        {
            CurrentTerminal.ActivateTerminal();
            CurrentTerminal = null;
        }

        //if (Input.GetKeyDown(KeyCode.O))
            //ApplyDamage(1);

        if (Input.GetKeyDown(KeyCode.Escape))
            _pauseEvent.Raise();
    }


    public override void TransitionToState(PlayerState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public bool ApplyDamage(float damage)
    {
        // Ideally we'd call something on a state here but this works for now since there isn't any state level granularity

        if (_damageable)
        {
            _stats.CurrentHealth = Mathf.Max(0f, _stats.CurrentHealth - damage); // Just in case :)
            //Debug.Log("Player took: " + damage + " | HP left: " + _stats.CurrentHealth);
            if (_stats.CurrentHealth <= 0f && !_dead) 
            {
                _dead = true;
                StopAllCoroutines();
                TransitionToState(this.StateFactory.GetState(States.State.dead));
                _playerDeadEvent.Raise();
                //Debug.Log("Player DEAD!");
            }
            else if(!_dead)
            {
                _damageable = false;
                _playerDamagedEvent.Raise();
                StartCoroutine(DamageCooldown());
            }
            return true;
        }
        else
        {
            // Don't do shit
            return false;
        }
    }

    private IEnumerator DamageCooldown()
    {
        Coroutine flashRoutine = StartCoroutine(DamageFlash());
        yield return new WaitForSeconds(_stats.DamageCooldown);
        StopCoroutine(flashRoutine);
        _damageable = true;
        _spriteRenderer.color = _startingColor;
    }

    private IEnumerator DamageFlash()
    {
        while(!_damageable)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(_stats.DamageCooldown/7f);
            _spriteRenderer.color = _startingColor;
            yield return new WaitForSeconds(_stats.DamageCooldown/7f);
        } 
    }
}
