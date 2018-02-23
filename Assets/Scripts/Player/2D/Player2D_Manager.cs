using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player in 2D */
public class Player2D_Manager : MonoBehaviour
{
    /*Base Animation(body)*/
    private bool PlayerMoving;
    private Vector2 lastMove;

    //if player can move(used when other canvas are open)
    public bool canMove = true;

    /*Equipment Animation Manager*/
    SpriteManager p_spriteManager;

    //attack animation
    public float animTimer; // countdown timer
    private float m_fAniTime; // value to countdown from
    static public bool attackClicked;

    /* Getting Player Stats */
    private Player2D_StatsHolder statsHolder;

    /* Show Level Up */
    [SerializeField]
    private TextMesh m_levelup_mesh;
    private float m_fLevelUpTimer = 0.0F;
    private float m_fLevelUpMaxTimer = 2.0F;
    private bool m_bCheckLevelUp;

    private GameObject temp; // store the created game object
    private float m_timer, testTimer; // for duration of hitbox

    /* List storing Player equipment */
    public List<Item> Inventory = new List<Item>();
    public List<Item> getPlayerInventory() { return Inventory; }
    enum EQTYPE
    {
        HELMET,
        CHESTPIECE,
        LEGGING,
        SHOE,
        WEAPON,
        TOTAL
    }
    Item[] EquipmentList = new Item[(int)EQTYPE.TOTAL-1];
    Sprite[] EQDisplayLayout = new Sprite[(int)EQTYPE.TOTAL-1];

    // Use this for initialization
    void Start()
    {
        /* Stats Things */
        statsHolder = GetComponent<Player2D_StatsHolder>();
        m_bCheckLevelUp = false;
        //statsHolder.DebugPlayerStats();

        /* Animation */
        animTimer = 0.0f;
        m_fAniTime = 1.0f;
        attackClicked = false;
        p_spriteManager = GetComponent<SpriteManager>();

        // set default equipments(will be moved to savefile)
        p_spriteManager.SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_DEFAULT);
        p_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_DEFAULT);
        p_spriteManager.SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_DEFAULT);
        p_spriteManager.SetShoesEquip(SpriteManager.S_Wardrobe.SHOES_DEFAULT);
        p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.DAGGER);

        // initialising the equipments
        for (int i = 0; i < EquipmentList.Length; ++i)
            EquipmentList[i] = null;

        //Vector3[] DisplayPosition = new Vector3[(int)EQTYPE.TOTAL-1];
        //for (int j = 0; j < DisplayPosition.Length ; ++j)
        //{
        //    DisplayPosition[j] = new Vector3();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        /* When Player Dies, Stop Updating */
        if (statsHolder.Health <= 0)
            return;

        // Check Timer to despawn level up
        if (m_bCheckLevelUp)
        {
            m_fLevelUpTimer += Time.deltaTime;
            if (m_fLevelUpTimer > m_fLevelUpMaxTimer)
            {
                m_fLevelUpTimer -= m_fLevelUpMaxTimer;
               
                m_bCheckLevelUp = false;
            }
        }

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

        /* When canMove, move */
        if (canMove)
            Movement2D();

        /* Attack Animation */
        PlayerAttack2D();
    }

    /* Key Board Movement of the Player */
    void KeyMove()
    {
        PlayerMoving = false;

        /* Getting the Direction of the Player */
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0f && inputY != 0f)
        {
            Player2D_Attack.Direction.Set(inputX, inputY);
        }
        else if (inputX != 0f)
        {
            Player2D_Attack.Direction.Set(inputX, 0);
        }
        else if (inputY != 0f)
        {
            Player2D_Attack.Direction.Set(0, inputY);
        }

        // Move Left / Right
        if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * statsHolder.MoveSpeed * Time.deltaTime, 0f, 0f));
            p_spriteManager.SetMoving(true);
            PlayerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            p_spriteManager.SetLastMove(lastMove.x, 0);
        }

        // Move Up / Down
        if (Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * statsHolder.MoveSpeed * Time.deltaTime, 0f));
            PlayerMoving = true;
            p_spriteManager.SetMoving(true);
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            p_spriteManager.SetLastMove(0, lastMove.y);
        }

        p_spriteManager.SetMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        p_spriteManager.SetMoving(PlayerMoving);
    }

    public void setAttackClicked(bool _atk) { attackClicked = _atk; }
    /* Attack Animation of Player */
    public void PlayerAttack2D()
    {
        // Change Animation
        if (attackClicked)
        {
            //p_spriteManager.Attack2D();

            canMove = false;
           // animTimer += Time.deltaTime;
            p_spriteManager.SetAttack(true);

            if (animTimer >= m_fAniTime)
            {
               // attackClicked = false;
              //  p_spriteManager.SetSlash(false);
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

    /* HotKeys */
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
    public bool EquipEQ(Item _equipment)
    {
        // If Player level is under equipment Level
        if (statsHolder.Level < _equipment.Level)
            return false;

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

        AddSprite(_equipment);
        return true;
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
    public void AddStats(float _health, float _maxHealth, float _stamina, float _maxStamina, float _attack, float _defence, float _movespeed)
    {
        statsHolder.Health += _health;
        statsHolder.MaxHealth += _maxHealth;

        if (statsHolder.Health > statsHolder.MaxHealth)
            statsHolder.Health = statsHolder.MaxHealth;

        statsHolder.Stamina += _stamina;
        statsHolder.MaxStamina += _maxStamina;
        if (statsHolder.Stamina > statsHolder.MaxStamina)
            statsHolder.Stamina = statsHolder.MaxStamina;
        statsHolder.Attack += _attack;
        statsHolder.Defense += _defence;
        statsHolder.MoveSpeed += _movespeed;
    }

    public Player2D_StatsHolder getPlayerStats() { return statsHolder; }
    public void AddGold(int _gold)
    {
        statsHolder.gold += _gold;
    }

    public void DisplayEquipments()
    {
        for (int i = 0; i < EquipmentList.Length; ++i)
        {
            if (EquipmentList[i] == null)
                EQDisplayLayout[i] = GameObject.FindGameObjectWithTag("Holder").GetComponent<MiscellaneousHolder>().Empty;
            else
                EQDisplayLayout[i] = EquipmentList[i].ItemImage;
        }
    }

    public void AddSprite(Item _equipment)
    {
        if (_equipment.ItemType == "Weapons")
        {
            if (_equipment.Name.Contains("Dagger"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.DAGGER);
            else if (_equipment.Name.Contains("Rapier"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.RAPIER);
            else if (_equipment.Name.Contains("Spear"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.SPEAR);
            else if (_equipment.Name.Contains("Long Spear"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.LONGSPEAR);
            else if (_equipment.Name.Contains("Long Sword"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.LONGSWORD);
            else if (_equipment.Name.Contains("Arrow"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.ARROW);
            else if (_equipment.Name.Contains("Bow"))
                p_spriteManager.SetWeaponEquip(SpriteManager.S_Weapon.BOW);
        }
        else if (_equipment.ItemType == "Helmets")
        {
            if (_equipment.Name.Contains("Leather"))
                p_spriteManager.SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_HAT);
            else if (_equipment.Name.Contains("Chain"))
                p_spriteManager.SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_CHAIN);
            else if (_equipment.Name.Contains("Plate"))
                p_spriteManager.SetHeadEquip(SpriteManager.S_Wardrobe.HEADP_PLATE);
        }
        else if (_equipment.ItemType == "Chestpieces")
        {
            if (_equipment.Name.Contains("Robe"))
                p_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_DEFAULT);
            else if (_equipment.Name.Contains("Leather"))
                p_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_LEATHER);
            else if (_equipment.Name.Contains("Chain"))
                p_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_CHAIN);
            else if (_equipment.Name.Contains("Plate"))
                p_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_PLATE);
            else if (_equipment.Name.Contains("Purple"))
                p_spriteManager.SetTopEquip(SpriteManager.S_Wardrobe.TOP_PURPLE);
        }
        else if (_equipment.ItemType == "Leggings")
        {
            if (_equipment.Name.Contains("Robe"))
                p_spriteManager.SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_DEFAULT);
            else if (_equipment.Name.Contains("Plate"))
                p_spriteManager.SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_PLATE);
            else if (_equipment.Name.Contains("Green"))
                p_spriteManager.SetBottomEquip(SpriteManager.S_Wardrobe.BOTTOM_GREEN);
        }
        else if (_equipment.ItemType == "Shoes")
        {
            if (_equipment.Name.Contains("Leather"))
                p_spriteManager.SetShoesEquip(SpriteManager.S_Wardrobe.SHOES_DEFAULT);
            else if (_equipment.Name.Contains("Plate"))
                p_spriteManager.SetShoesEquip(SpriteManager.S_Wardrobe.SHOES_PLATE);
        }
    }
}
