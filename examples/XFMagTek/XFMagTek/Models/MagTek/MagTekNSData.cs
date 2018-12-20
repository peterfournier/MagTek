using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFMagTek.Interfaces.MagTek;

namespace XFMagTek.Models.MagTek
{
    public class MagTekNSData : INSData
    {
        private readonly long _length;
        private readonly byte[] _bytes;

        public long Length => _length;
        public byte[] Bytes => _bytes;

        public MagTekNSData(byte[] bytes, long length)
        {
            _bytes = bytes;
            _length = length;
        }
    }
}
