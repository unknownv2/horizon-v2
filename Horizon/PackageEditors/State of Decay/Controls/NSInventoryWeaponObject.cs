
using System.Windows.Forms;
using StateofDecay;

namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    public partial class NSInventoryWeaponObject : UserControl
    {
        public NSMeeleWeapon Object { get; set; }
        public NSInventoryWeaponObject(NSMeeleWeapon linker)
        {
            Object = linker;
            InitializeComponent();
            Read();
        }

        public void Read()
        {
            intDurabilityMax.Value = Object.DurabilityMax;
            intDurability.Value = Object.Durability;
        }
        public void Write()
        {
            Object.DurabilityMax = (ushort) intDurabilityMax.Value;
            Object.Durability = (ushort) intDurability.Value;
        }
    }
}
