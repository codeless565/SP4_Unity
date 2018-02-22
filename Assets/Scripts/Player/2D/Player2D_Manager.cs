using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player in 2D */
public class Player2D_Manager : MonoBehaviour, CollisionBase
{
    /*Base Animation(body)*/
    private Animator anim;
    private bool PlayerMoving;
    private Vector2 lastMove;

    //attack animation
    public float animTimer; // countdown timer
    private float m_fAniTime; // value to countdown from

    public bool attackClicked;

    /*Equipment Animation Manager*/
    SpriteManager p_spriteManager;

    //textbox
    public bool canMove = true;

    /* Getting Player Stats */
    private Player2D_StatsHolder statsHolder;

    /* Show Level Up */
    [SerializeField]
    private TextMesh m_levelup_mesh;
    private TextMesh cloneMesh;
    private float m_fLevelUpTimer = 0.0F;
    private float m_fLevelUpMaxTimer = 2.0F;
    private bool m_bCheckLevelUp;

    private GameObject temp; // store the created game object
    private float m_timer, testTimer; // for duration of hitbox

    /* Direction Player will face */
    //enum Direction
    //{
    //    Down = 0,
    //    Up,
    //    Left,
    //    Right,
    //};
    //Direction toMove = 0;

    // Use this for initialization
    void Start()
    {
        /* Stats Things */
        statsHolder = GetComponent<Player2D_StatsHolder>();
        m_bCheckLevelUp = false;
        //statsHolder.DebugPlayerStats();

        animTimer = 0.0f;
        m_fAniTime = 1.0f;
        attackClicked = false;
        anim = GetComponent<Animator>();
        p_spriteManager = GetComponent<SpriteManager>();

        // set default equipments
        p_spriteManager.SetEquipments(SpriteManager.S_Wardrobe.HEADP_DEFAULT, SpriteManager.S_Weapon.DAGGER);
    }

    // Update is called once per frame
    void Update()
    {
        /* When Player Dies, Stop Updating */
        if (statsHolder.Health <= 0)
            return;

        /* When EXP is maxed */
        if (statsHolder.EXP >= statsHolder.MaxEXP)
        {
            m_bCheckLevelUp = true;
            LevelUp();
        }
        if(canMove)
        {
            Movement2D();
        }
            PlayerAttack2D();
    }

    void KeyMove()
    {
        PlayerMoving = false;

        // Move Left / Right
        if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * statsHolder.MoveSpeed * Time.deltaTime, 0f, 0f));
            //transform.Rotate
            PlayerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                p_spriteManager.direction = SpriteManager.S_Dir.RIGHT;
            }
            if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                p_spriteManager.direction = SpriteManager.S_Dir.LEFT;
            }
            p_spriteManager.SetLastMove(lastMove.x, 0);
        }

        // Move Up / Down
        if (Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * statsHolder.MoveSpeed * Time.deltaTime, 0f));
            PlayerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Vertical") > 0f)
            {
                p_spriteManager.direction = SpriteManager.S_Dir.BACK;
            }
            if (Input.GetAxisRaw("Vertical") < 0f)
            {
                p_spriteManager.direction = SpriteManager.S_Dir.FRONT;
            }
            p_spriteManager.SetLastMove(0, lastMove.y);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", PlayerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    public void PlayerAttack2D()
    {
        /* When Clicked down */
        if (Input.GetMouseButtonDown(0) && !attackClicked)
        {
            attackClicked = true;
            p_spriteManager.SetBoolSM(true);
            anim.SetBool("PlayerSlash", true);
        }

        // Change Animation
        if (attackClicked)
        {
            canMove = false;
            animTimer += Time.deltaTime;
            if (animTimer >= m_fAniTime)
            {
                attackClicked = false;
                p_spriteManager.SetBoolSM(false);
                anim.SetBool("PlayerSlash", false);
                canMove = true;
                animTimer -= m_fAniTime;
            }
        }
    }

    /* For Mobile */
    void AccMove()
    {
        //values from accelerometer;
        float x = Input.acceleration.x;
        float y = Input.acceleration.y;
    }

    /* Movement of Player - Camera is Fixed, Player will move according to its direction */
    void Movement2D()
    {
        KeyMove();
        //AccMove();
    }

    /* Interaction with Objects */
    public void CollisionResponse(string _tag)
    {
        // Player - Traps 
        // When hit by traps, change state to be "playerInjuried"
        // in state do everything when injuried before coming back to this state


        // Player - Merchant
        // When near merchant, press e to go into buying state
        // in state , dont update anything until when the player press back button , then change script back to this


        // Player- Wooden box



    }

    /* Level Up Character */
    private void LevelUp()
    {
        /* Reset all Exp */
        statsHolder.EXP = 0.0F;
        statsHolder.MaxEXP += 1;
        statsHolder.Level += 1;

        /* Create Text to show level up */
        cloneMesh = Instantiate(m_levelup_mesh, gameObject.transform);

        // Check Timer to despawn level up
        if (m_bCheckLevelUp)
        {
            m_fLevelUpTimer += Time.deltaTime;
            if (m_fLevelUpTimer > m_fLevelUpMaxTimer)
            {
                m_fLevelUpTimer -= m_fLevelUpMaxTimer;
                Destroy(cloneMesh);
                Destroy(m_levelup_mesh);
                m_bCheckLevelUp = false;
            }
        }
    }
}
