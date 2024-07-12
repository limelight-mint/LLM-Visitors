
using Cysharp.Threading.Tasks;

namespace LLM.Visitors.Base
{
    public interface IVisitor
    {
        public UniTask OnVisitorEnter();
        public UniTask OnVisitorLeave();

        public void OnTick();
    }
}