using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeerHeal : MonoBehaviour, StateBase
{
    // Deer Heal
    PetsManager m_PetsManager;
    PetDeerManager m_PetDeerManager;
    GameObject m_Player;
    bool m_bHasHeal;
    float m_fHealTimer = 5f;
    float m_fAddPlayerHP = 20f;

    // StateBase
    string m_StateID;
    GameObject m_go;

    public StateDeerHeal(string _stateID, GameObject _go)
    {
        m_StateID = _stateID;
        m_go = _go;
        m_PetsManager = _go.GetComponent<PetsManager>();
        m_PetDeerManager = _go.GetComponent<PetDeerManager>();
    }

    public void EnterState()
    {
        // Player
        m_PetsManager.GetPlayer();
        // Setting Pet Heal Range
        m_PetsManager.SetHealRange(1f);
    }

    public void UpdateState()
    {
        // Get Distance Apart between Player and Pet //
        m_PetsManager.SetDistanceApart((m_PetsManager.GetPlayer().GetComponent<Transform>().position - m_go.GetComponent<Transform>().position).magnitude);

        if (m_PetsManager.GetPlayer() != null)
        {
            // Pet will walk to Player
            m_go.GetComponent<Transform>().position = Vector2.MoveTowards(m_go.GetComponent<Transform>().position, m_PetsManager.GetPlayer().GetComponent<Transform>().position, Time.deltaTime * (m_PetsManager.MoveSpeed * 0.4f));

            if(m_PetsManager.GetDistanceApart() <= m_PetsManager.GetHealRange())
            {
                m_bHasHeal = false;
            }

            if (!m_bHasHeal)
            {
                // Count Down
                m_fHealTimer -= Time.deltaTime;
                // Render Healing Sprite
                m_PetDeerManager.m_PlayerHealingSprite.SetActive(true);

                if (m_fHealTimer <= 0f)
                {
                    m_fHealTimer = 5f;
                    m_PetDeerManager.m_PlayerHealingSprite.SetActive(false);
             
                    // Heal Player 
                    m_bHasHeal = true;
                }
            }

            // Change State to GUARD when it has Healed Finish.
            if(m_bHasHeal)
            {
                // Heal Player
                if (m_PetsManager.GetPlayerStats().Health != m_PetsManager.GetPlayerStats().MaxHealth)
                {
                    // Increase Player HP
                    m_PetsManager.GetPlayerStats().Health += m_fAddPlayerHP;
                    // Decrease from Pet HP
                    m_PetsManager.Health -= m_fAddPlayerHP;
                }

                m_PetsManager.GetStateMachine().SetNextState("StateDeerGuard");
            }
        }
        else
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_PetsManager.SetPlayer(m_Player);
        }
    }

    public void ExitState()
    {

    }

    public string StateID
    {
        get { return m_StateID; }
    }
}
