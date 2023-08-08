using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThuocNo : MonoBehaviour
{
    public Transform[] thuocNos;
    public List<Character> dieCharacterList = new List<Character>();


    public void OnInit()
    {
        for(int i = 0; i < thuocNos.Length; i++)
        {
            thuocNos[i].gameObject.SetActive(true);
        }
    }

    public void OnHit()
    {
        for (int i = 0; i < thuocNos.Length; i++)
        {
            thuocNos[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < dieCharacterList.Count; i++)
        {
            Character c = dieCharacterList[i];
            int idx = Player.ins.characterList.IndexOf(c);
            if (idx < Player.ins.characterList.Count - 1) // k phải thằng cuối
            {
                Player.ins.characterList[idx + 1].frontCharacter = c.frontCharacter;// bàn giao frontCharacter
            }

            Player.ins.characterList.Remove(c);
            c.transform.SetParent(LevelManager.ins.currentLevel.patrolParent);
            PoolCharacterModel.ins.ReturnToPool(c.model);
            //c.gameObject.SetActive(false);

        }
    }

    public void OnCharacterEnter(Character c)
    {
        dieCharacterList.Add(c);
    }

    public void OnCharacterExit(Character c)
    {
        dieCharacterList.Remove(c);
    }
}
