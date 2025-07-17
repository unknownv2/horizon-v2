using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StateofDecay;

namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    public partial class NSInventoryConsumableObject : UserControl
    {
        public NSConsumable Object { get; set; }
        public NSInventoryConsumableObject(NSConsumable linker)
        {
            Object = linker;
            InitializeComponent();
            Read();
        }

        public void Read()
        {
            intStack.Value = Object.Value;
        }
        public void Write()
        {
            Object.Value = (byte) intStack.Value;
        }
    }
}
