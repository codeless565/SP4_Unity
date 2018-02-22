using UnityEngine;

public class SpriteManager : MonoBehaviour {
    public enum S_Weapon
    {
        DAGGER,
        SWORD,
        ARROW,
        BOW,
    };

    public enum S_Wardrobe
    {
        TOP_DEAFULT,
        BOTTOM_DEFAULT,
        SHOES_DEFAULT,
        HEADP_DEFAULT,
        HEADP_HAT,
        HEADP_HOOD,
        //METAL_TOP,
        //METAL_BOTTOM,
        //METAL_GLOVES,
        //METAL_SHOES,
        //METAL_HEADP,
        //HAT_HEADP,
        TOTAL
    };

    public enum S_Dir
    {
        FRONT,
        BACK,
        LEFT,
        RIGHT
    };

    public GameObject Head, otherHeads, Top, Bottom, Gloves, Shoes, Weapon;
    public Animator HeadAnim, WeaponAnim;
    S_Wardrobe headEquipped, topEquipped, bottomEquipped, glovesEquipped, shoesEquipped;
    S_Weapon weaponEquipped;
    public S_Dir direction = 0;
    bool PlayerSlash = false;
    Vector2 lastMove;

    public void SetEquipments(S_Wardrobe headp, S_Weapon weapon)
    {
        headEquipped = headp;
        weaponEquipped = weapon;
    }

    public void SetBoolSM(bool playerSlash)
    {
        PlayerSlash = playerSlash;
    }
    // Use this for initialization
    void Start ()
    {
	}

    public void SetLastMove(float x, float y)
    {
        lastMove.x = x;
        lastMove.y = y;
    }

    void ChangeHead()
    {
        //set other head styles inactive
        otherHeads = GameObject.Find("Head");
        if(otherHeads!=null)
        {
            foreach (object obj in otherHeads.transform)
            {
                Transform child = (Transform)obj;
                child.gameObject.SetActive(false);
            }
        }
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
        }
        HeadAnim = Head.GetComponent<Animator>();
        //set selected head style active
        Head.SetActive(true);
        HeadAnim.SetBool("PlayerSlash", PlayerSlash);
        HeadAnim.SetFloat("MoveX", lastMove.x);
        HeadAnim.SetFloat("MoveY", lastMove.y);
    }

    void ChangeWeapon()
    {
        Weapon = transform.Find("Weapon/Dagger").gameObject;
        WeaponAnim = Weapon.GetComponent<Animator>();
        //Weapon.SetActive(true);
        WeaponAnim.SetFloat("MoveX", lastMove.x);
        WeaponAnim.SetFloat("MoveY", lastMove.y);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("In Update");
        ChangeHead();
        ChangeWeapon();
    }
}
