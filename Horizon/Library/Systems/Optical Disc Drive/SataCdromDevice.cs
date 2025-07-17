using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ODFX
{
    // black ops LBN
    public class SataCdromDevice : GdfDevice
    {
        public override void DeviceControl(ulong ControlCode, ref byte[] OutputBuffer)
        {

        }
        public override void Read(EndianIO ReadIO, long Length, long FileByteOffset)
        {
            throw new NotImplementedException();
        }
    }
}