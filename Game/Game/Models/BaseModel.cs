
namespace Game.Models
{
    /// <summary>
    /// Base model for records that get saved
    /// </summary>
    public class BaseModel<T> : DefaultModel
    {
        /// <summary>
        /// Update Method is virutal and changed for each class
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public virtual bool Update(T newData)
        {
            return true;
        }
    }
}