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
        List<string> specialNodes = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

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
                Process.Start(txtPath.Text);
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
                        ImageIndex = 1
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
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void GoToRootDirectory()
        {
            if (txtPath.Text != "") txtPath.Text = Path.GetDirectoryName(txtPath.Text);
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
                MessageBox.Show(ex.Message, "Error");
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
    }
}
