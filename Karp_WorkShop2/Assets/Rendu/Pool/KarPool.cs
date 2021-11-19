using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarPool<T> where T : MonoBehaviour ,IPoolable
{
    public List<T> members = new List<T>();
    public List<T> unavailableMembers = new List<T>();

    GameObject prefab;

    private T CreateNewMember()
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;

        T member = go.GetComponent<T>();
        if (!member)
        {
            go.AddComponent<T>(); ;
            member = go.GetComponent<T>();
        }

        members.Add(member);
        return member;
    }

    public T GetFreeMember()
    {
        for (int i = 0; i < members.Count; i++)
        {
            if (!unavailableMembers.Contains(members[i]))
            {
                unavailableMembers.Add(members[i]);
                return members[i];
            }
        }
        T newMembers = CreateNewMember();
        unavailableMembers.Add(newMembers);
        return newMembers;
    }

    public void FreeMember(T member)
    {
        member.PoolReset();
        unavailableMembers.Remove(member);
    }

}
