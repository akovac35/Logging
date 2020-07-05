// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using System;

namespace com.github.akovac35.Logging.Correlation
{
    public class Correlation
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public Correlation()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            GenerateNewId();
        }

        public Correlation(string id)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id));
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
                _id = value ?? throw new ArgumentNullException(nameof(Id));
            }
        }

        public void GenerateNewId()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}
