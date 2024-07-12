
using System.Collections.Generic;
using UnityEngine;

namespace LLM.Visitors.Base
{
    public class MonoHall : MonoBehaviour, IHall
    {
        protected HashSet<IVisitor> visitors = new HashSet<IVisitor>();

        public virtual void Update()
        {
            foreach (var visitor in visitors)
            {
                visitor.OnTick();
            }
        }

        public void Enter(IVisitor visitor)
        {
            visitors.Add(visitor);
            OnVisitorCame(visitor);
        }

        public void Remove(IVisitor visitor)
        {
            if(!visitors.Contains(visitor)) return;
            visitors.Remove(visitor);
            OnVisitorLeft(visitor);
        }

        public void Remove<TVisitor>(bool removeAll = true)
            where TVisitor : IVisitor
        {
            foreach (var visitor in visitors)
            {
                if(visitor.GetType() != typeof(TVisitor)) continue;
                visitors.Remove(visitor);
                OnVisitorLeft(visitor);
                if(!removeAll) return;
            }
        }

        public virtual void OnVisitorLeft(IVisitor visitor) => visitor?.OnVisitorEnter();
        public virtual void OnVisitorCame(IVisitor visitor) => visitor?.OnVisitorLeave();

        private void OnDestroy()
        {
            foreach (var visitor in visitors)
            {
                OnVisitorLeft(visitor);
            }
            visitors.Clear();
        }
    }
}