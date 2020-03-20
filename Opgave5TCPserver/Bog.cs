using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave5TCPserver
{
    class Bog
    {
        private string _titel;
        private string _forfatter;
        private int _sideTal;
        private string _isbn;

        public string Titel
        {
            get => _titel;
            set
            {
                if (value == null) throw new ArgumentNullException();
                _titel = value;
            }

        }

        public string Forfatter
        {
            get => _forfatter;
            set
            {
                if (value.Length < 2) throw new ArgumentOutOfRangeException();
                _forfatter = value;
            }
        }

        public int SideTal
        {
            get => _sideTal;
            set
            {
                if (4 >= value || value >= 1000) throw new ArgumentOutOfRangeException();
                _sideTal = value;
            }

        }

        public string Isbn
        {
            get => _isbn;
            set
            {
                if (value.Length != 13) throw new ArgumentOutOfRangeException();
                _isbn = value;
            }
        }
    }
}
