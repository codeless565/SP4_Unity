using UnityEngine;

public class SpriteManager : MonoBehaviour {

    public enum S_Weapon
    {
        DAGGER,
        RAPIER,
        SPEAR,
        LONGSPEAR,
        LONGSWORD,
        ARROW,
        BOW       
    };

    // HEADP = HELMET
    public enum S_Wardrobe
    {
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
        BOTTOM_GREEN,

        SHOES_PLATE,
        //METAL_TOP,
        //METAL_BOTTOM,
        //METAL_GLOVES,
        //METAL_SHOES,
        //METAL_HEADP,
        //HAT_HEADP,
        TOTAL
    };

    enum AttackStyle
    {
        PlayerSlash,
        PlayerThrust,
        PlayerBow
    };
    
    public GameObject Body, Head, otherHeads, Top, otherTops, Bottom, otherBottoms, Shoes, otherShoes, Weapon, otherWeapons;
    public Animator BodyAnim, HeadAnim, TopAnim, BottomAnim, ShoesAnim, WeaponAnim;
    S_Wardrobe headEquipped, topEquipped, bottomEquipped, shoesEquipped;
    S_Weapon weaponEquipped;

    //Attack style
    AttackStyle attackStyle;
    bool attackPlaceHolder;

    //Player actions
    bool PlayerSlash = false;
    bool PlayerThrust = false;
    bool PlayerBow = false;
    bool PlayerSpell = false;
    bool PlayerDie = false;
    //Player movement
    bool PlayerMoving = false;
    Vector2 MoveXY, lastMove;

    /******equipment setters******/
    public void SetHeadEquip(S_Wardrobe headp)
    {
        headEquipped = headp;

        //set other head styles inactive
		if(otherHeads!=null)
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
			    Head = transform.Find("Head/HeadLeather").gameObject;
			    break;
            case S_Wardrobe.HEADP_CHAIN:
                Head = transform.Find("Head/HeadChain").gameObject;
                break;
            case S_Wardrobe.HEADP_PLATE:
                Head = transform.Find("Head/HeadPlate").gameObject;
                break;
        }
		Head.SetActive(true);
		HeadAnim = Head.GetComponent<Animator>();
		HeadAnim.SetBool ("PlayerSlash", PlayerSlash);
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
        //set selected weapons active
        switch (topEquipped)
        {
            case S_Wardrobe.TOP_DEFAULT:
                Top = transform.Find("Top/Default").gameObject;
                break;
            case S_Wardrobe.TOP_LEATHER:
                Top = transform.Find("Top/Armour1").gameObject;
                break;
            case S_Wardrobe.TOP_CHAIN:
                Top = transform.Find("Top/Armour3").gameObject;
                break;
            case S_Wardrobe.TOP_PLATE:
                Top = transform.Find("Top/TopPlate").gameObject;
                break;
            case S_Wardrobe.TOP_PURPLE:
                Top = transform.Find("Top/Armour2").gameObject;
                break;
        }

        Top.SetActive(true);
        TopAnim = Top.GetComponent<Animator>();
        TopAnim.SetBool("PlayerSlash", PlayerSlash);
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
        //set selected weapons active
        switch (bottomEquipped)
        {
            case S_Wardrobe.BOTTOM_DEFAULT:
				Bottom = transform.Find("Bottom/Default").gameObject;
                break;
            case S_Wardrobe.BOTTOM_PLATE:
                Bottom = transform.Find("Bottom/BottomPlate").gameObject;
                break;
            case S_Wardrobe.BOTTOM_GREEN:
                Bottom = transform.Find("Bottom/BottomGreen").gameObject;
                break;
        }

        Bottom.SetActive(true);
        BottomAnim = Bottom.GetComponent<Animator>();
        BottomAnim.SetBool("PlayerSlash", PlayerSlash);
    }

    public void SetShoesEquip(S_Wardrobe shoes)
    {
        shoesEquipped = shoes;
        //set other tops inactive
        if (otherShoes != null)
        {
            foreach (object obj in otherShoes.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
        //set selected weapons active
        switch (shoesEquipped)
        {
            case S_Wardrobe.SHOES_DEFAULT:
                Shoes = transform.Find("Shoes/Default").gameObject;
                break;
            case S_Wardrobe.SHOES_PLATE:
                Shoes = transform.Find("Shoes/Default").gameObject;
                break;
        }

        Shoes.SetActive(true);
        ShoesAnim = Shoes.GetComponent<Animator>();
        ShoesAnim.SetBool("PlayerSlash", PlayerSlash);
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
				Weapon = transform.Find ("Weapon/Dagger").gameObject;
                attackStyle = AttackStyle.PlayerSlash;
                attackPlaceHolder = PlayerSlash;
                break;
            case S_Weapon.RAPIER:
                Weapon = transform.Find("Weapon/Rapier").gameObject;
                attackStyle = AttackStyle.PlayerSlash;
                attackPlaceHolder = PlayerSlash;
                break;
            case S_Weapon.SPEAR:
                Weapon = transform.Find("Weapon/Spear").gameObject;
                attackStyle = AttackStyle.PlayerThrust;
                attackPlaceHolder = PlayerSlash;
                break;
            case S_Weapon.LONGSPEAR:
                Weapon = transform.Find("Weapon/LongSpear").gameObject;
                attackStyle = AttackStyle.PlayerThrust;
                attackPlaceHolder = PlayerThrust;
                break;
            case S_Weapon.LONGSWORD:
                Weapon = transform.Find("Weapon/LongSword").gameObject;
                attackStyle = AttackStyle.PlayerSlash;
                attackPlaceHolder = PlayerSlash;
                break;
            case S_Weapon.ARROW:
                Weapon = transform.Find("Weapon/Arrow").gameObject;
                attackStyle = AttackStyle.PlayerBow;
                attackPlaceHolder = PlayerSlash;
                break;
            case S_Weapon.BOW:
                Weapon = transform.Find("Weapon/Bow").gameObject;
                attackStyle = AttackStyle.PlayerBow;
                attackPlaceHolder = PlayerSlash;
                break;
        }
		Weapon.SetActive (true);
		WeaponAnim = Weapon.GetComponent<Animator> ();
		WeaponAnim.SetBool (attackStyle.ToString (), PlayerSlash);
    }

    /**************************/

    /********bool setters******/
    public void SetSlash(bool playerSlash)
    {
        PlayerSlash = playerSlash;
    }

    public void SetSpell(bool playerSpell)
    {
        PlayerSpell = playerSpell;
    }

    public void SetThrust(bool playerThrust)
    {
        PlayerThrust = playerThrust;
    }

    public void SetBow(bool playerBow)
    {
        PlayerBow = playerBow;
    }

    public void SetDie(bool playerDie)
    {
        PlayerDie = playerDie;
    }

    public void SetPlayerMoving(bool playerMoving)
    {
        PlayerMoving = playerMoving;
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
    void Start ()
    {
        Body = transform.Find("Body").gameObject;
        BodyAnim = Body.GetComponent<Animator>();
    }

    void UpdateAnim(Animator animator)
    {
        //animator.SetBool("PlayerSlash", PlayerSlash);
        animator.SetFloat("MoveX", MoveXY.x);
        animator.SetFloat("MoveY", MoveXY.y);
        animator.SetBool("PlayerMoving", PlayerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    /*Animation cases*/
    void RenderBody()
    {
        //movement
        UpdateAnim(BodyAnim);

        //slash
        BodyAnim.SetBool("PlayerSlash", PlayerSlash);
        //bow
        BodyAnim.SetBool("PlayerBow", PlayerBow);
        //thrust
        BodyAnim.SetBool("PlayerThrust", PlayerThrust);
        //spell
        BodyAnim.SetBool("PlayerSpell", PlayerSpell);
        //die
        BodyAnim.SetBool("PlayerDie", PlayerDie);
    }

    void RenderHead()
    {
        HeadAnim.SetBool("PlayerSlash", PlayerSlash);
        UpdateAnim(HeadAnim);
    }
    void RenderTop()
    {
        TopAnim.SetBool("PlayerSlash", PlayerSlash);
        UpdateAnim(TopAnim);
    }
    void RenderBottom()
    {
        BottomAnim.SetBool("PlayerSlash", PlayerSlash);
        UpdateAnim(BottomAnim);
    }
    void RenderShoes()
    {
        ShoesAnim.SetBool("PlayerSlash", PlayerSlash);
        UpdateAnim(ShoesAnim);
    }
    void RenderWeapon()
    {
		WeaponAnim.SetBool (attackStyle.ToString (), PlayerSlash);
        UpdateAnim(WeaponAnim);
    }
    /**************************/

    // Update is called once per frame
    void Update () {
        RenderBody();
        RenderHead();
        RenderTop();
        RenderBottom();
        RenderShoes();
        RenderWeapon();
    }
}
