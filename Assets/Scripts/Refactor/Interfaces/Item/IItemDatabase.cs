using System.Collections.Generic;

namespace RC
{
    public interface IItemDatabase
    {
        /*
        * Items dictionary <ID, ScriptableObject>
        */
        public Dictionary<string, IBaseItem> Items { get; set; }

        public abstract IBaseItem GetItemById(string id);

    }
}
