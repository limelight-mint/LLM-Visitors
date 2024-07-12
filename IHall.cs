
namespace LLM.Visitors.Base
{
    public interface IHall
    {
        /// <summary>
        /// Adds particular visitor to the hall registration system
        /// </summary>
        public void Enter(IVisitor visitor);

        /// <summary>
        /// Remove particular visitor from the hall registration system
        /// </summary>
        public void Remove(IVisitor visitor);

        /// <summary>
        /// Removes all visitors by Type in one go
        /// </summary>
        /// <param name="removeAll">In case you wanna remove only the first one in order</param>
        public void Remove<TVisitor>(bool removeAll = true) where TVisitor : IVisitor;
    }
}