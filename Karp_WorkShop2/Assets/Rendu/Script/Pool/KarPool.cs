using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Gameplay
{
    public class ChunkEvent : UnityEvent<Chunk>
    { }

    public class KarPool
    {
        public List<Chunk> members = new List<Chunk>();
        public List<Chunk> availableMembers = new List<Chunk>();
        public List<Chunk> unavailableMembers = new List<Chunk>();

        GameObject prefab;

        public ChunkEvent onPoolInitialise;
        public ChunkEvent onPoolReset;
        private Chunk CreateNewMember()
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;

            Chunk member = go.GetComponent<Chunk>();
            if (!member)
            {
                go.AddComponent<Chunk>(); ;
                member = go.GetComponent<Chunk>();
            }

            members.Add(member);
            return member;
        }

        public Chunk GetFreeMember()
        {
            for (int i = 0; i < members.Count; i++)
            {
                if (availableMembers.Contains(members[i]))
                {
                    availableMembers.Remove(members[i]);
                    unavailableMembers.Add(members[i]);

                    onPoolInitialise?.Invoke(members[i]);

                    return members[i];
                }
            }
            Chunk newMembers = CreateNewMember();

            availableMembers.Remove(newMembers);
            unavailableMembers.Add(newMembers);

            onPoolInitialise?.Invoke(newMembers);

            return newMembers;
        }

        public void FreeMember(Chunk member)
        {
            availableMembers.Add(member);
            unavailableMembers.Remove(member);

            onPoolReset?.Invoke(member);
        }

    }
}
