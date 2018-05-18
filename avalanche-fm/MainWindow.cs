using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        #endregion

        //???
        List<string> specialNodes = new List<string>();

        #region Конструктор
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GoToDrivesDirectory();

            //InitializeTreeView();
        }

        private void GoToDrivesDirectory()
        {
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
            listView.Items.Clear();
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

                    listView.Items.Add(dirItem);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    listViewImageList.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(path + @"\" + file.Name));
                    ListViewItem fileItem = new ListViewItem()
                    {
                        Text = file.Name,
                        ImageIndex = listViewImageList.Images.Count - 1
                    };

                    listView.Items.Add(fileItem);
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

        private void RefreshListView()
        {
            if (txtPath.Text != "") GoToDirectory(txtPath.Text);
            else GoToDrivesDirectory();
        }

        private void InitializeTreeView()
        {
            specialNodes.Add("Desktop");

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                TreeNode driveNode = new TreeNode()
                {
                    Text = drive.Name,
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                treeView.Nodes.Add(driveNode);
            }

            try
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    if (!specialNodes.Contains(node.Text))
                    {
                        DirectoryInfo di = new DirectoryInfo(node.Text);
                        DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.AllDirectories);

                        foreach (var dir in directories)
                        {
                            TreeNode dirNode = new TreeNode()
                            {
                                Text = dir.Name,
                                ImageIndex = 1,
                                SelectedImageIndex = 1
                            };

                            treeView.Nodes[0].Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
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
                RefreshListView();
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
                RefreshListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorMessageCaption);
            }
        }

        private void stripRefresh_Click(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void listViewContextMenu_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItem stripDelete = new ToolStripMenuItem("Delete", null, stripDelete_Click);
            ToolStripMenuItem stripRename = new ToolStripMenuItem("Rename", null, stripRename_Click);

            ToolStripTextBox txtRename = new ToolStripTextBox
            {
                Text = standartFileName
            };

            stripRename.DropDownItems.Add(txtRename);

            if (listView.SelectedItems.Count != 0)
            {
                if (listViewContextMenu.Items.Count < 5)
                {
                    listViewContextMenu.Items.Add(stripRename);
                    listViewContextMenu.Items.Add(stripDelete);
                }

                if (txtPath.Text == "")
                {
                    listViewContextMenu.Items[5].Enabled = false;
                    listViewContextMenu.Items[4].Enabled = false;
                }
                else
                {
                    listViewContextMenu.Items[5].Enabled = true;
                    listViewContextMenu.Items[4].Enabled = true;
                }
            }
            else
            {
                if (listViewContextMenu.Items.Count >= 5)
                {
                    listViewContextMenu.Items.Remove(listViewContextMenu.Items[5]);
                    listViewContextMenu.Items.Remove(listViewContextMenu.Items[4]);
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

                RefreshListView();
            }
            catch
            {

            }
        }

        private void stripDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string path = txtPath.Text + @"\" + listView.SelectedItems[0].Text;

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }

                RefreshListView();
            }
            catch
            {

            }
        }
    }
}
