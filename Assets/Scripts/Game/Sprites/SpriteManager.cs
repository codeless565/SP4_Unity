using UnityEngine;

public class SpriteManager : MonoBehaviour {
    enum S_Weapon
    {
        DAGGER,
        SWORD,
        ARROW,
        BOW,
    };

    enum S_Wardrobe
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
    };

    public enum S_Dir
    {
        FRONT,
        BACK,
        LEFT,
        RIGHT
    };

    public GameObject Head/*, Top, Bottom, Gloves, Shoes, Weapon*/;
    public Animator HairAnim;
    S_Wardrobe topSprite, bottomSprite, headSprite, glovesSprite, shoesSprite, weaponSprite;
    public S_Dir direction = 0;
    public float hori, verti;
    

    // Use this for initialization
    void Start ()
    {
        HairAnim = Head.GetComponent<Animator>();
	}

    void ChangeHead()
    {
        switch(headSprite)
        {
            case S_Wardrobe.DEFAULT_HEADP:
                
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //if(direction == S_Dir.LEFT || direction == S_Dir.RIGHT)
        {
            Debug.Log(hori);
            Debug.Log(direction);
            HairAnim.SetFloat("MoveX", hori);
        }
    }
}
