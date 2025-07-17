using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.Editors;
using StateofDecay;

namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    public partial class NSInventoryGunObject : UserControl
    {
        public NSFireArm Object { get; set; }
        public NSInventoryGunObject(NSFireArm gunObject)
        {
            Object = gunObject;
            InitializeComponent();
            Read();
        }

        public void Read()
        {
            intDurabilityCritical.Value = Object.DurabilityCritical;
            intDurability.Value = Object.Durability;
            intClip.Value = Object.Clip;
            intSilencerShots.Value = Object.SilencerShots;
        }

        public void Write()
        {
            Object.DurabilityCritical = (ushort) intDurabilityCritical.Value;
            Object.Durability = (ushort) intDurability.Value;
            Object.Clip = (byte) intClip.Value;
            Object.SilencerShots = (byte) intSilencerShots.Value;
        }

        /*
        private Node CreateInt32Node(ValueType key, string title, int maxValue)
        {
            var intInput = new IntegerInput
            {
                Tag = key,
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true
            };
            switch (key)
            {
                case ValueType.DurabilityCritical:
                    intInput.Value = Object.DurabilityCritical;
                    break;
                case ValueType.Durability:
                    intInput.Value = Object.Durability;
                    break;
                case ValueType.Clip:
                    intInput.Value = Object.Clip;
                    break;
                case ValueType.SilencerShots:
                    intInput.Value = Object.SilencerShots;
                    break;
            }
            intInput.ValueChanged += Int32_ValueChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }

        private void Int32_ValueChanged(object sender, EventArgs e)
        {
            var i = sender as IntegerInput;
            if (i != null)
            {
                switch ((ValueType)i.Tag)
                {
                        
                }
            }
        }

        public enum ValueType
        {
            DurabilityCritical,
            Durability,
            Clip,
            SilencerShots
        }
        */
    }
}
