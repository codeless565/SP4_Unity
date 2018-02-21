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

    // Use this for initialization
    void Start ()
    {
        HeadAnim = Head.GetComponent<Animator>();
	}

    public void SetLastMove(float x, float y)
    {
        lastMove.x = x;
        lastMove.y = y;
    }

    public void SetEquipments(S_Wardrobe headp)
    {
        headEquipped = headp;

    }

    void ChangeHead()
    {
        switch(headEquipped)
        {
            case S_Wardrobe.DEFAULT_HEADP:
                //Head.GetComponentInChildren
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        HeadAnim.SetFloat("MoveX", lastMove.x);
        HeadAnim.SetFloat("MoveY", lastMove.y);
    }
}
