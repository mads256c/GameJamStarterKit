using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Juto.Rewind
{
    public class TimeRewind : MonoBehaviour
    {
        private static List<TimeRewindHolder> target = new List<TimeRewindHolder>();

        public static int FramesSavedFor = 100;


        public static bool IsReversing {
            get
            {
                return isReversing;
            }

            set
            {
                isReversing = value;
            }

        }

        private static bool isReversing;

        void Start()
        {
            foreach(TimeRewindHolder holder in target)
            {
                holder.pos = new ArrayList();
                holder.rot = new ArrayList();
            }
        }

        void FixedUpdate()
        {
            if(!isReversing)
            {
                foreach (TimeRewindHolder holder in target)
                {
                    if (holder.pos.Count > FramesSavedFor)
                    {
                        holder.pos.RemoveAt(1);
                    }

                    if (holder.rot.Count > FramesSavedFor)
                    {
                        holder.rot.RemoveAt(1);
                    }

                    holder.pos.Add(holder.target.transform.position);
                    holder.rot.Add(holder.target.transform.localEulerAngles);
                }
            }
            else
            {
                foreach (TimeRewindHolder holder in target)
                {
                    if (holder.pos.Count > 1)
                    {
                        holder.target.transform.position = (Vector3)holder.pos[holder.pos.Count - 1];
                        holder.pos.RemoveAt(holder.pos.Count - 1);
                    }

                    if (holder.rot.Count > 1)
                    {
                        holder.target.transform.localEulerAngles = (Vector3)holder.rot[holder.rot.Count - 1];
                        holder.rot.RemoveAt(holder.rot.Count - 1);
                    }

                    if (holder.rot.Count + holder.pos.Count <= 6)
                    {
                        IsReversing = false;
                    }
                }


                
            }
        }

        private void clearList()
        {
            foreach (TimeRewindHolder holder in target)
            {
                holder.rot.Clear();
                holder.pos.Clear();
            }
        }

        /// <summary>
        /// Sets the current reversing state.
        /// </summary>
        /// <param name="reversing">true = reversing</param>
        public static void SetReversing(bool reversing)
        {
            IsReversing = reversing;
        }

        /// <summary>
        /// Toggles the reversing.
        /// </summary>
        public static void ToggleReversing()
        {
            IsReversing = !IsReversing;
        }

        /// <summary>
        /// Adds a new Gameobject to the target list.
        /// </summary>
        /// <param name="obj"></param>
        public static void AddGameObject(GameObject obj)
        {
            TimeRewindHolder t = new TimeRewindHolder(obj);
            target.Add(t);
        }

        /// <summary>
        /// Adds a bunch of GameObjects to the list.
        /// </summary>
        /// <param name="objs"></param>
        public static void AddGameObjects(GameObject[] objs)
        {
            foreach(GameObject obj in objs)
            {
                AddGameObject(obj);
            }
        }
    }

    [System.Serializable]
    public class TimeRewindHolder
    {
        public GameObject target;
        public ArrayList pos;
        public ArrayList rot;

        public TimeRewindHolder(GameObject _target)
        {
            target = _target;
            pos = new ArrayList();
            rot = new ArrayList();
        }
    }
}

