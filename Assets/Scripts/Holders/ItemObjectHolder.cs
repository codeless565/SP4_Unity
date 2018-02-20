using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectHolder : MonoBehaviour
{
    public Sprite Helmet;

    public Sprite Sword;
    public Sprite Axe;

    public Sprite HPpotion;
    public Sprite MPpotion;

    List<Sprite> spritelist = new List<Sprite>();
    void Start()
    {
        spritelist.Add(Sword);
        spritelist.Add(Axe);
        spritelist.Add(MPpotion);
        spritelist.Add(HPpotion);
        spritelist.Add(Helmet);
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
