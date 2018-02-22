using UnityEngine;

public class SpriteManager : MonoBehaviour {
    enum S_Weapon
    {
        DAGGER,
        SWORD,
        ARROW,
        BOW,
    };

    public enum S_Wardrobe
    {
        DEFAULT_TOP,
        DEFAULT_BOTTOM,
        DEFAULT_SHOES,
        DEFAULT_HEADP,
        METAL_TOP,
        METAL_BOTTOM,
        METAL_GLOVES,
        METAL_SHOES,
        METAL_HEADP,
        HAT_HEADP,
        TOTAL
    };

    public enum S_Dir
    {
        FRONT,
        BACK,
        LEFT,
        RIGHT
    };

    public GameObject Head, Top, Bottom, Gloves, Shoes, Weapon;
    public Animator HeadAnim;
    S_Wardrobe headEquipped,  topEquipped, bottomEquipped,glovesEquipped, shoesEquipped, weaponEquipped;
    public S_Dir direction = 0;
    Vector2 lastMove;

    public void SetEquipments(S_Wardrobe headp)
    {
        headEquipped = headp;
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
        switch (headEquipped)
        {
            case S_Wardrobe.DEFAULT_HEADP:
                Head = transform.Find("Head/Hood").gameObject;
                break;

            case S_Wardrobe.METAL_HEADP:
                Head = transform.Find("Head/Hood").gameObject;
                break;
        }
        HeadAnim = Head.GetComponent<Animator>();
        Head.SetActive(true);
        HeadAnim.SetFloat("MoveX", lastMove.x);
        HeadAnim.SetFloat("MoveY", lastMove.y);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("In Update");
        ChangeHead();
        HeadAnim.SetFloat("MoveX", lastMove.x);
        HeadAnim.SetFloat("MoveY", lastMove.y);
        //Debug.Log("dir" + direction.ToString());
        //Debug.Log("lastmove" + lastMove.x+ ", " + lastMove.y);
    }
}
