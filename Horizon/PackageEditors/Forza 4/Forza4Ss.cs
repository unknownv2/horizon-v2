using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Forza4;

namespace Horizon.PackageEditors.Forza_4
{
    public partial class Forza4Ss : EditorControl
    {
        //public static readonly string FID = "4D53091P";

        private Forza4Screenshot Screenshot;
        private Forza4KeyMarshal KeyMarshal;

        private List<List<string>> ImageFolders;
        private ulong Creator;
        private string ImageFolderName, ScreenshotPath;

        public Forza4Ss()
        {
            InitializeComponent();
            TitleID = FormID.Forza4Profile;
        }
        public override bool Entry()
        {
            KeyMarshal = new Forza4KeyMarshal();

            KeyMarshal.AddKey("ScreenshotSHA", SettingAsByteArray(123));
            KeyMarshal.AddKey("ScreenshotAES", SettingAsByteArray(162));

            this.cmbBoxScreenshotIndex.Items.Clear();

            this.Creator = this.Package.Header.Metadata.Creator;
            ImageFolders = new List<List<string>>();
            int imagecount = 0;
            for (var x = 0; x < this.Package.StfsContentPackage.DirectoryEntries.Count; x++)
            {
                var DirectoryEntry = this.Package.StfsContentPackage.DirectoryEntries[x];
                if (DirectoryEntry.IsDirectory && DirectoryEntry.IsEntryBound)
                {
                    ImageFolders.Add(new List<string>());
                    ImageFolders[imagecount].Add(DirectoryEntry.FileName);
                    ImageFolders[imagecount].Add(this.Package.StfsContentPackage.StfsFindNextDirectoryName(this.Package.StfsContentPackage.GetFileStream(DirectoryEntry.FileName).Fcb, 0x00));
                    this.cmbBoxScreenshotIndex.Items.Add(imagecount++.ToString());
                }
            }

            if (ImageFolders.Count > 0)
            {
                this.pictureBox1.BackgroundImage = null;
                this.cmbBoxScreenshotIndex.SelectedIndex = 0x00;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ScreenshotIndexChanged(object sender, EventArgs e)
        {
            this.ImageFolderName = ImageFolders[this.cmbBoxScreenshotIndex.SelectedIndex][0];

            ScreenshotPath = ImageFolderName + "\\" + ImageFolders[this.cmbBoxScreenshotIndex.SelectedIndex][1];

            this.OpenScreenshot();

            this.pictureBox1.Image = Image.FromStream(Package.StfsContentPackage.GetFileStream(ImageFolderName + "\\thumb"));
        }
        private void BtnClickExtract(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.FileName = "Forza 4 Screenshot";
            sfd.Filter = "JPEG Files (*.jpg)|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Screenshot.Read().Save(sfd.FileName);
                Horizon.Functions.UI.messageBox("Successfully extracted a screenshot!", "Done!", MessageBoxIcon.Information);
            }
        }
        private void BtnClickInject(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var RealImage = resizeImage(Image.FromStream(new MemoryStream(File.ReadAllBytes(ofd.FileName))), new Size() { Width = 1280, Height = 720 });

                Package.StfsContentPackage.InjectFileFromArray(ScreenshotPath, this.Screenshot.Write(RealImage.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg)));

                this.OpenScreenshot();

                var Tumbnail = RealImage.GetThumbnailImage(416, 234, null, System.IntPtr.Zero);

                Package.StfsContentPackage.InjectFileFromArray(ImageFolderName + "\\thumb", Tumbnail.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg));

                this.pictureBox1.Image = Tumbnail;

                Horizon.Functions.UI.messageBox("Successfully injected screenshot!", "Done!", MessageBoxIcon.Information);
            }
        }
        private void OpenScreenshot()
        {
            // re-open the screenshot's EndianIO
            this.OpenStfsFile(ScreenshotPath);

            this.Screenshot = new Forza4Screenshot(this.IO, this.Creator, KeyMarshal);
        }
        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }
    }
}