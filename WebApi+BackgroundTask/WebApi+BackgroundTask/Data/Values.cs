using System.Collections.Generic;

namespace WebApi_BackgroundTask
{
    public class Values
    {
        private List<int> _data = new List<int> { 1 };

        public IList<int> GetData()
        {
            return _data;
        }
        public void AddData(int value)
        {
            _data.Add(value);
        }
    }
}