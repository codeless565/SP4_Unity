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

    //textbox
    public bool canMove = true;
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

    /* List storing Player equipment */
    public List<Item> Inventory = new List<Item>();
    public List<Item> getPlayerInventory() { return Inventory; }
    enum EQTYPE
    {
        HELMET,
        WEAPON,
        CHESTPIECE,
        LEGGING,
        SHOE,
        TOTAL
    }
    Item[] EquipmentList = new Item[(int)EQTYPE.TOTAL];

    // Use this for initialization
    void Start()
    {
        /* Stats Things */
        statsHolder = GetComponent<Player2D_StatsHolder>();
        m_bCheckLevelUp = false;
        //statsHolder.DebugPlayerStats();

        anim = GetComponent<Animator>();
        p_spriteManager = GetComponent<SpriteManager>();

        // set default equipments
        p_spriteManager.SetEquipments(SpriteManager.S_Wardrobe.DEFAULT_HEADP);

        // initialising the equipments
        for (int i = 0; i < EquipmentList.Length; ++i)
            EquipmentList[i] = null;

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

        // Check Timer to despawn level up
        if (m_bCheckLevelUp)
        {
            m_fLevelUpTimer += Time.deltaTime;
            if (m_fLevelUpTimer > m_fLevelUpMaxTimer)
            {
                m_fLevelUpTimer -= m_fLevelUpMaxTimer;
                DestroyImmediate(cloneMesh);
                m_bCheckLevelUp = false;
            }
        }
        Movement2D();

        // Hot bar key press
        bool bA1State = false;
        if (!bA1State && Input.GetKeyDown(KeyCode.Alpha1))
        {
            bA1State = true;
            HotKeyResponse(1);
        }
        else if (bA1State && !Input.GetKeyDown(KeyCode.Alpha1))
            bA1State = false;

        bool bA2State = false;
        if (!bA2State && Input.GetKeyDown(KeyCode.Alpha2))
        {
            bA2State = true;
            HotKeyResponse(2);
        }
        else if (bA2State && !Input.GetKeyDown(KeyCode.Alpha2))
            bA2State = false;

        bool bA3State = false;
        if (!bA3State && Input.GetKeyDown(KeyCode.Alpha3))
        {
            bA3State = true;
            HotKeyResponse(3);
        }
        else if (bA3State && !Input.GetKeyDown(KeyCode.Alpha3))
            bA3State = false;

        bool bA4State = false;
        if (!bA4State && Input.GetKeyDown(KeyCode.Alpha4))
        {
            bA4State = true;
            HotKeyResponse(4);
        }
        else if (bA4State && !Input.GetKeyDown(KeyCode.Alpha4))
            bA4State = false;

        bool bA5State = false;
        if (!bA5State && Input.GetKeyDown(KeyCode.Alpha5))
        {
            bA5State = true;
            HotKeyResponse(5);
        }
        else if (bA5State && !Input.GetKeyDown(KeyCode.Alpha5))
            bA5State = false;

        bool bA6State = false;
        if (!bA6State && Input.GetKeyDown(KeyCode.Alpha6))
        {
            bA6State = true;
            HotKeyResponse(6);
        }
        else if (bA6State && !Input.GetKeyDown(KeyCode.Alpha6))
            bA6State = false;
    }

    void KeyMove()
    {
        PlayerMoving = false;

        if(canMove)
        {
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

    public void HotKeyResponse(int keynum)
    {
        
        if (GetComponent<InventoryBar>().getPlayerHotBar()[keynum-1] != null && Inventory.Contains(GetComponent<InventoryBar>().getPlayerHotBar()[keynum-1]))
        {
            AddStats(GetComponent<InventoryBar>().getPlayerHotBar()[keynum-1]);
            

            List<int> ListOfItemIndex = new List<int>();
            for (int i = 0; i <Inventory.Count;++i)
            {
                if (Inventory[i] == GetComponent<InventoryBar>().getPlayerHotBar()[keynum-1])
                {
                    ListOfItemIndex.Add(i);
                }
            }
            if (ListOfItemIndex.Count == 1)
            {
                GetComponent<InventoryBar>().RemovePlayerHotBar(Inventory[ListOfItemIndex[0]], false);
                Inventory.RemoveAt(ListOfItemIndex[0]);
            }
            else
            {
                Inventory[ListOfItemIndex[0]].Quantity--;
                GetComponent<InventoryBar>().RemovePlayerHotBar(Inventory[ListOfItemIndex[0]], true);
                Inventory.RemoveAt(ListOfItemIndex.Count - 1);
            }

        }
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
    }

    /* Adding Items to Inventory */
    public void AddItem(Item newitem)
    {
        // If Player Inventory already has new item
        if (Inventory.Contains(newitem))
        {
            foreach (Item item in Inventory)
            {
                if (item == newitem)
                {
                    // Add to item quality
                    item.Quantity++;
                    break;
                }
            }
            Inventory.Add(newitem);
        }
        else
            Inventory.Add(newitem);
        // Still Adds towards inventory
    }

    /* Equipping EQ to the Player */
    public void EquipEQ(Item _equipment)
    {
        // TODO display message
        // If Player level is under equipment Level
        if (statsHolder.Level < _equipment.Level)
            return;

        // If new equipment type is weapons
        if (_equipment.ItemType == "Weapons")
        {
            // if Player has not equipped any weapon
            if (EquipmentList[(int)EQTYPE.WEAPON] == null)
            {
                EquipmentList[(int)EQTYPE.WEAPON] = _equipment;
            }
            // If play has an equipped weapon
            // Remove stats of equipped weapon
            // Equip and add new weapon
            else
            {
                AddStats(-EquipmentList[(int)EQTYPE.WEAPON].Health,
                    -EquipmentList[(int)EQTYPE.WEAPON].MaxHealth,
                    -EquipmentList[(int)EQTYPE.WEAPON].Stamina,
                    -EquipmentList[(int)EQTYPE.WEAPON].MaxStamina,
                    -EquipmentList[(int)EQTYPE.WEAPON].Attack,
                    -EquipmentList[(int)EQTYPE.WEAPON].Defense,
                    -EquipmentList[(int)EQTYPE.WEAPON].MoveSpeed);
                EquipmentList[(int)EQTYPE.WEAPON] = _equipment;
            }
            AddStats(_equipment);
        }
        else if (_equipment.ItemType == "Helmets")
        {
            if (EquipmentList[(int)EQTYPE.HELMET] == null)
            {
                EquipmentList[(int)EQTYPE.HELMET] = _equipment;
            }
            else
            {
                AddStats(-EquipmentList[(int)EQTYPE.HELMET].Health,
                    -EquipmentList[(int)EQTYPE.HELMET].MaxHealth,
                    -EquipmentList[(int)EQTYPE.HELMET].Stamina,
                    -EquipmentList[(int)EQTYPE.HELMET].MaxStamina,
                    -EquipmentList[(int)EQTYPE.HELMET].Attack,
                    -EquipmentList[(int)EQTYPE.HELMET].Defense,
                    -EquipmentList[(int)EQTYPE.HELMET].MoveSpeed);
                EquipmentList[(int)EQTYPE.HELMET] = _equipment;
            }
            AddStats(_equipment);
        }
        else if (_equipment.ItemType == "Chestpieces")
        {
            if (EquipmentList[(int)EQTYPE.CHESTPIECE] == null)
            {
                EquipmentList[(int)EQTYPE.CHESTPIECE] = _equipment;
            }
            else
            {
                AddStats(-EquipmentList[(int)EQTYPE.CHESTPIECE].Health,
                    -EquipmentList[(int)EQTYPE.CHESTPIECE].MaxHealth,
                    -EquipmentList[(int)EQTYPE.CHESTPIECE].Stamina,
                    -EquipmentList[(int)EQTYPE.CHESTPIECE].MaxStamina,
                    -EquipmentList[(int)EQTYPE.CHESTPIECE].Attack,
                    -EquipmentList[(int)EQTYPE.CHESTPIECE].Defense,
                    -EquipmentList[(int)EQTYPE.CHESTPIECE].MoveSpeed);
                EquipmentList[(int)EQTYPE.CHESTPIECE] = _equipment;
            }
            AddStats(_equipment);
        }
        else if (_equipment.ItemType == "Leggings")
        {
            if (EquipmentList[(int)EQTYPE.LEGGING] == null)
            {
                EquipmentList[(int)EQTYPE.LEGGING] = _equipment;
            }
            else
            {
                AddStats(-EquipmentList[(int)EQTYPE.LEGGING].Health,
                    -EquipmentList[(int)EQTYPE.LEGGING].MaxHealth,
                    -EquipmentList[(int)EQTYPE.LEGGING].Stamina,
                    -EquipmentList[(int)EQTYPE.LEGGING].MaxStamina,
                    -EquipmentList[(int)EQTYPE.LEGGING].Attack,
                    -EquipmentList[(int)EQTYPE.LEGGING].Defense,
                    -EquipmentList[(int)EQTYPE.LEGGING].MoveSpeed);
                EquipmentList[(int)EQTYPE.LEGGING] = _equipment;
            }
            AddStats(_equipment);
        }
        else if (_equipment.ItemType == "Shoes")
        {
            if (EquipmentList[(int)EQTYPE.SHOE] == null)
            {
                EquipmentList[(int)EQTYPE.SHOE] = _equipment;
            }
            else
            {
                AddStats(-EquipmentList[(int)EQTYPE.SHOE].Health,
                    -EquipmentList[(int)EQTYPE.SHOE].MaxHealth,
                    -EquipmentList[(int)EQTYPE.SHOE].Stamina,
                    -EquipmentList[(int)EQTYPE.SHOE].MaxStamina,
                    -EquipmentList[(int)EQTYPE.SHOE].Attack,
                    -EquipmentList[(int)EQTYPE.SHOE].Defense,
                    -EquipmentList[(int)EQTYPE.SHOE].MoveSpeed);
                EquipmentList[(int)EQTYPE.SHOE] = _equipment;
            }
            AddStats(_equipment);
        }
    }

    /* Stats will be added when Equipped */
    public void AddStats(Item item)
    {
        statsHolder.Health += item.Health;
        statsHolder.MaxHealth += item.MaxHealth;
        statsHolder.Stamina += item.Stamina;
        statsHolder.MaxStamina += item.MaxStamina;
        statsHolder.Attack += item.Attack;
        statsHolder.Defense += item.Defense;
        statsHolder.MoveSpeed += item.MoveSpeed;
    }
    public void AddStats(float _health, float _maxHealth, float _stamina,float _maxStamina, float _attack, float _defence, float _movespeed)
    {
        statsHolder.Health += _health;
        statsHolder.MaxHealth += _maxHealth;
        statsHolder.Stamina += _stamina;
        statsHolder.MaxStamina += _maxStamina;
        statsHolder.Attack += _attack;
        statsHolder.Defense+= _defence;
        statsHolder.MoveSpeed += _movespeed;
    }

    public Player2D_StatsHolder getPlayerStats() { return statsHolder; }
    public void AddGold(int _gold)
    {
        statsHolder.gold += _gold;
    }
}
