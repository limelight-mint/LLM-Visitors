
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace LLM.Visitors.Base
{
    public abstract class Hall : IHall
    {
        protected HashSet<IVisitor> visitors;

        protected int tickrate;

        public Hall(int tickrateMiliseconds) => tickrate = tickrateMiliseconds;

        public async virtual UniTask CheckVisitors()
        {
            foreach (var visitor in visitors)
            {
                await UniTask.Delay(tickrate);
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

        public virtual void OnVisitorLeft(IVisitor visitor) => visitor.OnVisitorEnter();
        public virtual void OnVisitorCame(IVisitor visitor) => visitor.OnVisitorLeave();
    }
}