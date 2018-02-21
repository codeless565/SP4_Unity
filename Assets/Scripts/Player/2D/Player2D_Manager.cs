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

    /*Equipment Animation Manager*/
    SpriteManager p_spriteManager;

    /* Getting Player Stats */
    private Player2D_StatsHolder statsHolder;

    /* Show Level Up */
    [SerializeField]
    private TextMesh m_levelup_mesh;
    private TextMesh cloneMesh;
    private float m_fLevelUpTimer = 0.0F;
    private float m_fLevelUpMaxTimer = 2.0F;
    private bool m_bCheckLevelUp;


    /* Direction Player will face */
    enum Direction
    {
        Down = 0,
        Up,
        Left,
        Right,
    };
    Direction toMove = 0;

    // Use this for initialization
    void Start()
    {
        /* Stats Things */
        statsHolder = GetComponent<Player2D_StatsHolder>();
        m_bCheckLevelUp = false;
        statsHolder.DebugPlayerStats();

        anim = GetComponent<Animator>();
        p_spriteManager = GetComponent<SpriteManager>();
        p_spriteManager.SetEquipments(SpriteManager.S_Wardrobe.DEFAULT_HEADP);
    }

    // Update is called once per frame
    void Update()
    {
        /* When EXP is maxed */
        if (statsHolder.EXP >= statsHolder.MaxEXP)
        {
            m_bCheckLevelUp = true;
            LevelUp();
        }

        // Check Timer to despawn level up
        if (m_bCheckLevelUp)
        {
            m_fLevelUpTimer += Time.deltaTime;
            if (m_fLevelUpTimer > m_fLevelUpMaxTimer)
            {
                m_fLevelUpTimer -= m_fLevelUpMaxTimer;
                Destroy(cloneMesh);
                m_bCheckLevelUp = false;
            }

        }


        Movement2D();
    }

    void KeyMove()
    {
        PlayerMoving = false;

        // Move Left / Right
        if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * statsHolder.MoveSpeed * Time.deltaTime, 0f, 0f));
            PlayerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                p_spriteManager.direction = SpriteManager.S_Dir.RIGHT;
            }
            else
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
            else
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
        /* Create Text to show u level up */
        cloneMesh = Instantiate(m_levelup_mesh, gameObject.transform);
    }
}
