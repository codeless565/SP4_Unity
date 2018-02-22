using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectHolder : MonoBehaviour
{
    public Sprite HelmetChain;
    public Sprite HelmetLeather;
    public Sprite HelmetPlate;

    public Sprite ChestpieceChain;
    public Sprite ChestpieceLeather;
    public Sprite ChestpieceLeather2;
    public Sprite ChestpiecePlate;
    public Sprite ChestpiecePurple;
    public Sprite ChestpieceRobe;

    public Sprite LeggingsGreen;
    public Sprite LeggingsPlate;
    public Sprite LeggingsRobe;

    public Sprite ShoesLeather;
    public Sprite ShoesPlate;

    public Sprite HPpotion;
    public Sprite MPpotion;
    public Sprite Arrow;

    public Sprite WeaponsBow;
    public Sprite WeaponsDagger;
    public Sprite WeaponsLongSpear;
    public Sprite WeaponsLongSword;
    public Sprite WeaponsRapier;
    public Sprite WeaponsSpear;


    List<Sprite> spritelist = new List<Sprite>();
    void Start()
    {
        spritelist.Add(HelmetChain);
        spritelist.Add(HelmetLeather);
        spritelist.Add(HelmetPlate);

        spritelist.Add(ChestpieceChain);
        spritelist.Add(ChestpieceLeather);
        spritelist.Add(ChestpieceLeather2);
        spritelist.Add(ChestpiecePlate);
        spritelist.Add(ChestpiecePurple);
        spritelist.Add(ChestpieceRobe);

        spritelist.Add(LeggingsGreen);
        spritelist.Add(LeggingsPlate);
        spritelist.Add(LeggingsRobe);

        spritelist.Add(ShoesLeather);
        spritelist.Add(ShoesPlate);

        spritelist.Add(HPpotion);
        spritelist.Add(MPpotion);
        spritelist.Add(Arrow);

        spritelist.Add(WeaponsBow);
        spritelist.Add(WeaponsDagger);
        spritelist.Add(WeaponsLongSpear);
        spritelist.Add(WeaponsLongSword);
        spritelist.Add(WeaponsRapier);
        spritelist.Add(WeaponsSpear);
    }

    public Sprite getSprite(string _spritename)
    {
        foreach (Sprite _sprite in spritelist)
        {
            if (_sprite.name == _spritename.Trim())
                return _sprite;
        }

        return null;
    }
}
