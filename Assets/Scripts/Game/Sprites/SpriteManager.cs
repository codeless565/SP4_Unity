using UnityEngine;

public class SpriteManager : MonoBehaviour
{

    public enum S_Weapon
    {
        DAGGER,
        RAPIER,
        SPEAR,
        LONGSPEAR,
        LONGSWORD,
        ARROW,
        BOW,
    };

    public enum S_Wardrobe
    {
        TOP_NULL,
        BOTTOM_NULL,
        SHOES_NULL,
        HEADP_NULL,
        TOP_DEFAULT,
        BOTTOM_DEFAULT,
        SHOES_DEFAULT,
        HEADP_DEFAULT,
        HEADP_HAT,
        HEADP_HOOD,
        HEADP_CHAIN,
        HEADP_PLATE,

        TOP_CHAIN,
        TOP_LEATHER,
        TOP_PLATE,
        TOP_PURPLE,

        BOTTOM_PLATE,

        SHOES_PLATE,
        TOTAL
    };

    enum AttackStyle
    {
        Slash,
        Thrust,
        Bow
    };

    public GameObject Body, Head, otherHeads, Top, otherTops, Bottom, otherBottoms, Shoes, otherShoes, Weapon, otherWeapons;
    public Animator BodyAnim, HeadAnim, TopAnim, BottomAnim, ShoesAnim, WeaponAnim;
    S_Wardrobe headEquipped, topEquipped, bottomEquipped, shoesEquipped;
    S_Weapon weaponEquipped;

    //Attack style
    AttackStyle attackStyle;
    bool attackPlaceHolder;
    public float animTimer = 0.0f; // countdown timer
    private float m_fAniTime = 1.0f; // value to countdown from

    //Player actions
    bool Slash = false;
    bool Thrust = false;
    bool Bow = false;
    bool Spell = false;

    bool Die = false;
    //Player movement
    bool Moving = false;
    Vector2 MoveXY, lastMove;

    /******equipment setters******/
    public void SetHeadEquip(S_Wardrobe headp)
    {
        headEquipped = headp;

        //set other head styles inactive
        if (otherHeads != null)
        {
            foreach (object obj in otherHeads.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        //set selected head style active
        switch (headEquipped)
        {
            case S_Wardrobe.HEADP_DEFAULT:
                Head = transform.Find("Head/Hair").gameObject;
                break;

            case S_Wardrobe.HEADP_HOOD:
                Head = transform.Find("Head/Hood").gameObject;
                break;

            case S_Wardrobe.HEADP_HAT:
                Head = transform.Find("Head/Hat").gameObject;
                break;
            //case S_Wardrobe.HEADP_CHAIN:
            //    Head = transform.Find("Head/HeadChain").gameObject;
                //break;

        }
        if (headEquipped != S_Wardrobe.HEADP_NULL)
        {
            Head.SetActive(true);
            HeadAnim = Head.GetComponent<Animator>();
            HeadAnim.SetBool(attackStyle.ToString(), attackPlaceHolder);
        }
    }

    public void SetTopEquip(S_Wardrobe top)
    {
        topEquipped = top;

        //set other tops inactive
        if (otherTops != null)
        {
            foreach (object obj in otherTops.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        //set selected tops active
        switch (topEquipped)
        {
            case S_Wardrobe.TOP_DEFAULT:
                Top = transform.Find("Top/Default").gameObject;
                break;

            case S_Wardrobe.TOP_LEATHER:
                Top = transform.Find("Top/Armour1").gameObject;
                break;
            case S_Wardrobe.TOP_PURPLE:
                Top = transform.Find("Top/Armour2").gameObject;
                break;
            case S_Wardrobe.TOP_CHAIN:
                Top = transform.Find("Top/Armour3").gameObject;
                break;
            case S_Wardrobe.TOP_PLATE:
                Top = transform.Find("Top/Armour4").gameObject;
                break;
        }
        if (topEquipped != S_Wardrobe.TOP_NULL)
        {
            Top.SetActive(true);
            TopAnim = Top.GetComponent<Animator>();
            TopAnim.SetBool(attackStyle.ToString(), attackPlaceHolder);

        }
    }

    public void SetBottomEquip(S_Wardrobe bottom)
    {
        bottomEquipped = bottom;
        //set other bottoms inactive
        if (otherBottoms != null)
        {
            foreach (object obj in otherBottoms.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        //set selected bottoms active
        switch (bottomEquipped)
        {
            case S_Wardrobe.BOTTOM_DEFAULT:
                Bottom = transform.Find("Bottom/Default").gameObject;
                break;
            case S_Wardrobe.BOTTOM_PLATE:
                Bottom = transform.Find("Bottom/Armour1").gameObject;
                break;
        }
        if (bottomEquipped != S_Wardrobe.BOTTOM_NULL)
        {
            Bottom.SetActive(true);
            BottomAnim = Bottom.GetComponent<Animator>();
            BottomAnim.SetBool(attackStyle.ToString(), attackPlaceHolder);
        }

    }

    public void SetShoesEquip(S_Wardrobe shoes)
    {
        shoesEquipped = shoes;
        //set other shoes inactive
        if (otherShoes != null)
        {
            foreach (object obj in otherShoes.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        //set selected shoes active
        switch (shoesEquipped)
        {
            case S_Wardrobe.SHOES_DEFAULT:
                Shoes = transform.Find("Shoes/Default").gameObject;
                break;
            case S_Wardrobe.SHOES_PLATE:
                Shoes = transform.Find("Shoes/Armour1").gameObject;
                break;
        }
        if (shoesEquipped != S_Wardrobe.SHOES_NULL)
        {
            Shoes.SetActive(true);
            ShoesAnim = Shoes.GetComponent<Animator>();
            ShoesAnim.SetBool(attackStyle.ToString(), attackPlaceHolder);
        }

    }

    public void SetWeaponEquip(S_Weapon weapon)
    {
        weaponEquipped = weapon;

        //set other weapons inactive
        if (otherWeapons != null)
        {
            foreach (object obj in otherWeapons.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        //set selected weapons active
        switch (weaponEquipped)
        {
            case S_Weapon.DAGGER:
                Weapon = transform.Find("Weapon/Dagger").gameObject;
                attackStyle = AttackStyle.Slash;
                attackPlaceHolder = Slash;
                break;
            case S_Weapon.RAPIER:
                Weapon = transform.Find("Weapon/Rapier").gameObject;
                attackStyle = AttackStyle.Slash;
                attackPlaceHolder = Slash;
                break;
            case S_Weapon.SPEAR:
                Weapon = transform.Find("Weapon/Spear").gameObject;
                attackStyle = AttackStyle.Thrust;
                attackPlaceHolder = Thrust;
                break;
            case S_Weapon.LONGSPEAR:
                Weapon = transform.Find("Weapon/LongSpear").gameObject;
                attackStyle = AttackStyle.Thrust;
                attackPlaceHolder = Thrust;
                break;
            case S_Weapon.LONGSWORD:
                Weapon = transform.Find("Weapon/LongSword").gameObject;
                attackStyle = AttackStyle.Slash;
                attackPlaceHolder = Slash;
                break;
            case S_Weapon.ARROW:
                Weapon = transform.Find("Weapon/Arrow").gameObject;
                attackStyle = AttackStyle.Bow;
                attackPlaceHolder = Slash;
                break;
            case S_Weapon.BOW:
                Weapon = transform.Find("Weapon/Bow").gameObject;
                attackStyle = AttackStyle.Bow;
                attackPlaceHolder = Slash;
                break;
        }
    
        Weapon.SetActive(true);
        WeaponAnim = Weapon.GetComponent<Animator>();
        WeaponAnim.SetBool(attackStyle.ToString(), attackPlaceHolder);
    }

    /**************************/

    /********bool setters******/
    public void SetAttack(bool attack)
    {
        attackPlaceHolder = attack;
    
    }

    public void SetDie(bool die)
    {
        Die = die;
    }

    public void SetMoving(bool moving)
    {
        Moving = moving;
    }
    /**************************/

    /*movement setters*/
    public void SetMove(float x, float y)
    {
        MoveXY.x = x;
        MoveXY.y = y;
    }

    public void SetLastMove(float x, float y)
    {
        lastMove.x = x;
        lastMove.y = y;
    }
    /**************************/

    // Use this for initialization
    void Start()
    {
        Body = transform.Find("Body").gameObject;
        BodyAnim = Body.GetComponent<Animator>();
    }

    void UpdateAnim(Animator animator)
    {
        animator.SetBool(attackStyle.ToString(), attackPlaceHolder);
        animator.SetFloat("MoveX", MoveXY.x);
        animator.SetFloat("MoveY", MoveXY.y);
        animator.SetBool("Moving", Moving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    public void PlayAnimation()
    {
        if (attackPlaceHolder)
        {
            animTimer += Time.deltaTime;
            if (animTimer >= m_fAniTime)
            {
                attackPlaceHolder = false;
                animTimer -= m_fAniTime;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //movement
        UpdateAnim(BodyAnim);

        if (headEquipped != S_Wardrobe.HEADP_NULL)
            UpdateAnim(HeadAnim);

        if (topEquipped != S_Wardrobe.TOP_NULL)
            UpdateAnim(TopAnim);

        if (bottomEquipped != S_Wardrobe.BOTTOM_NULL)
            UpdateAnim(BottomAnim);

        if (shoesEquipped != S_Wardrobe.SHOES_NULL)
            UpdateAnim(ShoesAnim);

        UpdateAnim(WeaponAnim);

        PlayAnimation();
    }

}