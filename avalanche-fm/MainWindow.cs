using Ionic.Zip;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace avalanche_fm
{
    public partial class MainWindow : Form
    {
        #region Константы
        const string errorMessageCaption = "Error";
        const string standartFileName = "New file";
        const string standartFolderName = "New folder";
        const string standartArchiveName = "New archive";
        const int bufferSize = 16384;
        #endregion
        
        string SOURCE_PATH = "";
        string WATCHER_START_PATH = @"C:\";

        FileSystemWatcher bigBrother = new FileSystemWatcher();

        #region Конструктор
        public MainWindow()
        {
            InitializeComponent();
            bigBrother.Path = WATCHER_START_PATH;
            bigBrother.Filter = "*.*";
            bigBrother.Created += RefreshListView;
            bigBrother.Deleted += RefreshListView;
            bigBrother.Changed += RefreshListView;
            bigBrother.Renamed += RefreshListView;

            bigBrother.EnableRaisingEvents = true;
        }
        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GoToDrivesDirectory();

            //InitializeTreeView();
        }

        private void GoToDrivesDirectory()
        {
            bigBrother.Path = WATCHER_START_PATH;
            listView.Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                ListViewItem driveItem = new ListViewItem()
                {
                    Text = drive.Name,
                    ImageIndex = 0
                };

                listView.Items.Add(driveItem);
            }
        }

        private void GoToSubdirectory(string name)
        {
            
            string path = txtPath.Text;
            if (path == "")
            {
                path = name;
            }
            else
            {
                string tempoPath = path;
                if (tempoPath.Remove(0, tempoPath.Length - 1) != @"\") path += @"\" + name;
                else path += name;
            }

            if (File.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                txtPath.Text = path;
                GoToDirectory(txtPath.Text);
            }
        }

        private void GoToDirectory(string path)
        {
            bigBrother.Path = txtPath.Text;
            listView.Invoke(new Action(() => { listView.Items.Clear(); }));
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (var dir in dirInfo.GetDirectories())
                {
                    ListViewItem dirItem = new ListViewItem()
                    {
                        Text = dir.Name,
                        ImageIndex = 1,
                        
                    };

                    listView.Invoke(new Action(() => { listView.Items.Add(dirItem); })); 
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    listView.Invoke(new Action(() => { listViewImageList.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(path + @"\" + file.Name)); })); 
                    ListViewItem fileItem = new ListViewItem()
                    {
                        Text = file.Name,
                        ImageIndex = listViewImageList.Images.Count - 1
                    };

                    listView.Invoke(new Action(() => { listView.Items.Add(fileItem); }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }

        private void GoToRootDirectory()
        {
            if (txtPath.Text != "") txtPath.Text = Path.GetDirectoryName(txtPath.Text);
            if (txtPath.Text != "") GoToDirectory(txtPath.Text);
            else GoToDrivesDirectory();
        }

        private void RefreshListView(object sender, FileSystemEventArgs e)
        {
            if (txtPath.Text != "") GoToDirectory(txtPath.Text);
            else GoToDrivesDirectory();
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            if (listView.FocusedItem != null)
            {
                GoToSubdirectory(listView.FocusedItem.Text);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            GoToRootDirectory();
        }

        private void stripCreateTxtFile_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                while (File.Exists(txtPath.Text + @"\" + standartFileName + (i == 0 ? "" : i.ToString()) + ".txt")) i++;
                string tempFileName = standartFileName + (i == 0 ? "" : i.ToString()) + ".txt";
                using (StreamWriter createFile = File.CreateText(txtPath.Text + @"\" + tempFileName))
                {
                    
                }
                //RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }

        private void stripCreateFolder_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (Directory.Exists(txtPath.Text + @"\" + standartFolderName + (i == 0 ? "" : i.ToString()))) i++;
            string tempFolderName = standartFolderName + (i == 0 ? "" : i.ToString());
            try
            {
                Directory.CreateDirectory(txtPath.Text + @"\" + tempFolderName);
                //RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }

        private void stripRefresh_Click(object sender, EventArgs e)
        {
            RefreshListView(null, null);
        }

        private void listViewContextMenu_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItem stripCopy = new ToolStripMenuItem("Copy", null, stripCopy_Click);
            ToolStripMenuItem stripPaste = new ToolStripMenuItem("Paste", null, stripPaste_Click);
            ToolStripMenuItem stripRename = new ToolStripMenuItem("Rename", null, stripRename_Click);
            ToolStripTextBox txtRename = new ToolStripTextBox
                {
                    Text = standartFileName
                };
            ToolStripMenuItem stripCompression = new ToolStripMenuItem("Compression");
            ToolStripMenuItem stripAddToZip = new ToolStripMenuItem("Add to .zip archive", null, stripAddToZip_Click);
            ToolStripMenuItem stripDelete = new ToolStripMenuItem("Delete", null, stripDelete_Click);

            List<ToolStripItem> listViewItemStripList = new List<ToolStripItem>();
            listViewItemStripList.Add(stripCopy);
            listViewItemStripList.Add(stripRename);
            listViewItemStripList.Add(stripCompression);
            listViewItemStripList.Add(stripDelete);

            stripRename.DropDownItems.Add(txtRename);
            stripCompression.DropDownItems.Add(stripAddToZip);

            if (listView.SelectedItems.Count != 0)
            {
                if (listViewContextMenu.Items.Count <= 7)
                {
                    if (listViewContextMenu.Items.Count == 6)
                    {
                        listViewContextMenu.Items.Remove(listViewContextMenu.Items[5]);
                    }
                    foreach (var item in listViewItemStripList)
                    {
                        listViewContextMenu.Items.Add(item);
                    }
                }
            }
            else
            {
                if (listViewContextMenu.Items.Count >= 7)
                {
                    (listViewContextMenu.Items[6] as ToolStripMenuItem).DropDownItems[0].Text = standartFileName;
                    foreach (var item in listViewItemStripList)
                    {
                        listViewContextMenu.Items.Remove(listViewContextMenu.Items[5]);
                    }
                }
                if (listViewContextMenu.Items.Count == 5) listViewContextMenu.Items.Add(stripPaste);
            }
            if (txtPath.Text == "")
            {
                foreach (ToolStripItem item in listViewContextMenu.Items)
                {
                    item.Enabled = false;
                }
            }
            else
            {
                foreach (ToolStripItem item in listViewContextMenu.Items)
                {
                    item.Enabled = true;
                }
            }
        }

        private void stripCopy_Click(object sender, EventArgs e)
        {
            SOURCE_PATH = txtPath.Text + @"\" + listView.SelectedItems[0].Text;
        }

        private void stripPaste_Click(object sender, EventArgs e)
        {
            if (SOURCE_PATH != "")
            {
                try
                {
                    if (File.Exists(SOURCE_PATH))
                    {
                        string[] tempStr = SOURCE_PATH.Split(Path.DirectorySeparatorChar);
                        string[] tempStr2 = tempStr[tempStr.Length - 1].Split('.');
                        string destPath = txtPath.Text + @"\" + tempStr2[0] + " copy";
                        int i = 0;
                        while (File.Exists(destPath + (i == 0 ? "" : i.ToString()) + "." + tempStr2[tempStr2.Length - 1])) i++;
                        destPath += (i == 0 ? "" : i.ToString()) + "." + tempStr2[tempStr2.Length - 1];

                        #region Старая версия
                        /*byte[] buffer = new byte[bufferSize];
                        int bytesCopied = 0;
                        UInt64 totalBytes = 0;

                        using (FileStream inStream = File.Open(
                            SOURCE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read))
                        using (FileStream outStream = File.Open(
                            destPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            do
                            {
                                bytesCopied = inStream.Read(buffer, 0, bufferSize);
                                if (bytesCopied > 0)
                                {
                                    outStream.Write(buffer, 0, bytesCopied);
                                    totalBytes += (UInt64)bytesCopied;
                                }
                            } while (bytesCopied > 0);
                        }*/
                        #endregion

                        FileSystem.CopyFile(SOURCE_PATH, destPath);
                    }

                    if (Directory.Exists(SOURCE_PATH))
                    {
                        string[] tempStr = SOURCE_PATH.Split(Path.DirectorySeparatorChar);
                        string destPath = txtPath.Text + @"\" + tempStr[tempStr.Length - 1] + " copy";
                        int i = 0;
                        while (Directory.Exists(destPath + (i == 0 ? "" : i.ToString())));
                        destPath += (i == 0 ? "" : i.ToString());

                        FileSystem.CopyDirectory(SOURCE_PATH, destPath);
                    }

                    //RefreshListView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, errorMessageCaption);
                }
            }
        }

        private void stripRename_Click(object sender, EventArgs e)
        {
            try
            {
                string oldPath = txtPath.Text + @"\" + listView.SelectedItems[0].Text;
                string extension;
                extension = Path.GetExtension(oldPath);


                //ПЕРЕДАЛАТЬ!!!1!1!11!!! Крашится к чертям!!11!1!!!1
                int i = 0;
                while (File.Exists(txtPath.Text + @"\" +
                    (sender as ToolStripMenuItem).DropDownItems[0].Text + extension)
                    | Directory.Exists(txtPath.Text + @"\" +
                    (sender as ToolStripMenuItem).DropDownItems[0].Text))
                {
                    i++;
                }
                (sender as ToolStripMenuItem).DropDownItems[0].Text += (i == 0 ? "" : i.ToString());


                string newPath = txtPath.Text + @"\" +
                    (sender as ToolStripMenuItem).DropDownItems[0].Text + extension;

                if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                }

                if (Directory.Exists(oldPath))
                {
                    Directory.Move(oldPath, newPath);
                }


                (sender as ToolStripMenuItem).DropDownItems[0].Text = standartFileName;

                //RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }

        private void stripAddToZip_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = txtPath.Text + @"\" + listView.SelectedItems[0].Text;
                string name = listView.SelectedItems[0].Text.Split('.')[0];
                string destPath = txtPath.Text + @"\" + name;
                int i = 0;
                while (File.Exists(destPath + ".zip"))
                {
                    i++;
                    destPath = txtPath.Text + @"\"+ name + (i == 0 ? "" : i.ToString());
                }
                destPath += ".zip";

                if (Directory.Exists(sourcePath))
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(sourcePath);
                        zip.Save(destPath);
                    }
                }

                if (File.Exists(sourcePath))
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(sourcePath);
                        zip.Save(destPath);
                    }
                }

                //RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }

        private void stripDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string path = txtPath.Text + @"\" + listView.SelectedItems[0].Text;

                if (File.Exists(path))
                {
                    FileSystem.DeleteFile(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                }

                if (Directory.Exists(path))
                {
                    FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                }

                //RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }
    }
}
