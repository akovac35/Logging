// Author: Aleksander Kovač

using System;

namespace com.github.akovac35.Logging.Correlation
{
    public class Correlation
    {
        public Correlation()
        {
            GenerateNewId();
        }

        public Correlation(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            _id = id;
        }

        protected volatile string _id;

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Id));
                _id = value;
            }
        }

        public void GenerateNewId()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}
