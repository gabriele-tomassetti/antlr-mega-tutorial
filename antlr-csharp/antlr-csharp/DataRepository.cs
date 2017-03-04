using System.Collections.Generic;

namespace AntlrTutorial
{
    public class DataRepository
    {
        Dictionary<string, int> data = new Dictionary<string, int>();

        public DataRepository()
        {
            data.Add("A1", 10);
            data.Add("B2", 33);
        }

        public int this[string id]
        {
            get
            {
                if (data.ContainsKey(id))
                    return data[id];
                else
                    return 0;
            }            
        }
    }
}
