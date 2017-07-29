using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ABSTreeSaver;

namespace StepTreeControl
{
    public partial class StepsTree : UserControl
    {
        private const int IMAGE_INDEX_POSITIVE = 0;
        private const int IMAGE_INDEX_NEGATIVE = 1;
        private bool _preventExpand = false;
        private DateTime _lastMouseDown = DateTime.Now;

        public Dictionary<int, string> Operations = new Dictionary<int, string>();

		public List<int> StepsIdListToDelete { get; private set; } = new List<int>();
		public StepsTree()
        {
            InitializeComponent();
            stepTree.NodeMouseClick += (sender, args) => stepTree.SelectedNode = args.Node;
        }

        public void ResetTree()
        {
            this.stepTree.Nodes.Clear();
            var rootNode = new TreeNode
            {
                Text = "Базовая операция",
                ImageIndex = 0,
                Tag = null
            };
            this.stepTree.Nodes.Add(rootNode);
        }

        public void ClearTree()
        {
            this.stepTree?.Nodes?.Clear();
        }

        public void ExpandAll()
        {
            this.stepTree.ExpandAll();
        }

        public bool IsEmpty()
        {
            if (this.stepTree.Nodes.Count == 0)
            {
                return true;
            }
            return (this.stepTree.Nodes[0].Tag == null);
        }


        // Даёт возможность сохранять структуру делева в файл
        public void SaveTree(string fileName)
        {
            try
            {
                BinaryFormatter bin = new BinaryFormatter();
                STreeNode strToBeGone = STROperation.fnPrepareToWrite(this.stepTree);
                FileStream fTree = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                bin.Serialize(fTree, strToBeGone);
                fTree.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Save StepTree Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Загружает структуру дерева из файла (здесь не используется, но может пригодиться)
        public void LoadTree(string fileName)
        {
            this.stepTree.Nodes[0]?.Remove();
            try
            {
                BinaryFormatter bin = new BinaryFormatter();
                this.stepTree.Nodes.Clear();
                FileStream fTree = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                STreeNode str = (STreeNode)bin.Deserialize(fTree);
                fTree.Close();
                TreeNode trParent = STROperation.fnPrepareToRead(str);
                foreach (TreeNode trn in trParent.Nodes)
                {
                    this.stepTree.Nodes.Add(trn);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "ABS Treereader",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.stepTree.ExpandAll();
        }

        private List<TreeNode> _allNodes;

        private void CollectNodesRecursive(TreeNode oParentNode)
        {
            this._allNodes.Add(oParentNode);
            foreach (TreeNode oSubNode in oParentNode.Nodes)
            {
                CollectNodesRecursive(oSubNode);
            }
        }

        public List<TreeNode> GetAllNodes()
        {
            this._allNodes = new List<TreeNode>();
            TreeNode oMainNode = this.stepTree.Nodes[0];
            this.CollectNodesRecursive(oMainNode);

            return this._allNodes;
        }

        private void CorrectImagesNodesRecursive(TreeNode oParentNode)
        {
            if ((StepsProperties)oParentNode.Tag == null)
            {
                return;
            }

            if (((StepsProperties)oParentNode.Tag).IsPositive)
            {
                oParentNode.ImageIndex = 1;
            }
            else
            {
                oParentNode.ImageIndex = 0;
            }

            foreach (TreeNode oSubNode in oParentNode.Nodes)
            {
                CorrectImagesNodesRecursive(oSubNode);
            }
        }

        private void showPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPropertiesForm(StepFormMode.Edit);
        }

        private void stepTree_DoubleClick(object sender, EventArgs e)
        {
            OpenPropertiesForm(StepFormMode.Edit);
        }

        private void OpenPropertiesForm(StepFormMode mode)
        {
            if (stepTree.SelectedNode == null)
            {
                return;
            }
            if (stepTree.SelectedNode.Parent == null &&
                stepTree.SelectedNode.Tag == null &&
                (mode == StepFormMode.AddNegative || mode == StepFormMode.AddPositive))
            {
                MessageBox.Show("Сначала необходимо задать свойства корневого элемента (базовой операции)", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.OpenPropertiesForm(StepFormMode.Edit);
                return;
            }

            var selectNode = this.stepTree.SelectedNode;
            var propertiesForm = new StepPropertiesForm(mode, (StepsProperties)selectNode.Tag, this.Operations);
            propertiesForm.ShowDialog();

            if (propertiesForm?.SelectedOperationItem == null)
            {
                return;
            }

            if (mode == StepFormMode.Edit)
            {
                var newNode = this.NodeToAdd(true, propertiesForm.StepsProperties);
                selectNode.Text = newNode.Text;
                selectNode.Tag = newNode.Tag;
                if (selectNode.Parent == null)
                {
                    ((StepsProperties)selectNode.Tag).IsPositive = true;
                }
            }
            else
            if (mode == StepFormMode.AddPositive)
            {
                selectNode.Nodes.Add(this.NodeToAdd(true, propertiesForm.StepsProperties));
                selectNode.Expand();
            }
            else
            if (mode == StepFormMode.AddNegative)
            {
                selectNode.Nodes.Add(this.NodeToAdd(false, propertiesForm.StepsProperties));
                selectNode.Expand();
            }
        }

        private TreeNode NodeToAdd(bool isPositive, StepsProperties stepsProperties)
        {
            var imageIndex = (isPositive) ? IMAGE_INDEX_POSITIVE : IMAGE_INDEX_NEGATIVE;
            var stepNode = new TreeNode();
            stepNode.ImageIndex = imageIndex;
            stepNode.SelectedImageIndex = imageIndex;
            stepNode.Tag = stepsProperties;
            stepNode.Text = stepsProperties.OperationName;
            return stepNode;
        }

        private void addPositiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenPropertiesForm(StepFormMode.AddPositive);
        }

        private void addNegativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenPropertiesForm(StepFormMode.AddNegative);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.stepTree.SelectedNode == null)
            {
                addPositiveToolStripMenuItem.Enabled = false;
                addNegativeToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                showPropertiesToolStripMenuItem.Enabled = false;

                return;
            }
            addPositiveToolStripMenuItem.Enabled = true;
            addNegativeToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
            foreach (TreeNode node in this.stepTree.SelectedNode.Nodes)
            {
                if (node.Tag == null)
                {
                    continue;
                }
                if (((StepsProperties)node.Tag).IsPositive)
                {
                    addPositiveToolStripMenuItem.Enabled = false;
                    continue;
                }

                if (!((StepsProperties)node.Tag).IsPositive)
                {
                    addNegativeToolStripMenuItem.Enabled = false;
                    continue;
                }
            }
            if (stepTree.SelectedNode.Parent == null)
            {
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void DeleteItem()
        {
            if (stepTree.SelectedNode.Parent == null)
            {
                MessageBox.Show("Выделенные узел является корневым элементом", "Невозможно удалить шаг",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            {
                if (MessageBox.Show("Удалить текущий элемент и все дочерние?", "Подтвердите действие",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
					List<TreeNode> nodes = new List<TreeNode>();
					AddChildren(nodes, stepTree.SelectedNode);
					nodes.Add(stepTree.SelectedNode);
					StepsIdListToDelete.Clear();
					foreach (TreeNode node in nodes)
					{
						StepsProperties stepsProperties = node.Tag as StepsProperties;
						if (stepsProperties != null)
						{
							StepsIdListToDelete.Add(stepsProperties.Id);
						}
					}
					stepTree.SelectedNode.Remove();
				}
            }
        }

		public void AddChildren(List<TreeNode> nodes, TreeNode node)
		{
			foreach (TreeNode thisNode in node.Nodes)
			{
				nodes.Add(thisNode);
				AddChildren(nodes, thisNode);
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteItem();
        }

        private void stepTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = this._preventExpand;
            this._preventExpand = false;
        }

        private void stepTree_MouseDown(object sender, MouseEventArgs e)
        {
            int delta = (int)DateTime.Now.Subtract(this._lastMouseDown).TotalMilliseconds;
            this._preventExpand = (delta < SystemInformation.DoubleClickTime);
            this._lastMouseDown = DateTime.Now;
        }

        private void toolStripButtonProperty_Click(object sender, EventArgs e)
        {
            this.OpenPropertiesForm(StepFormMode.Edit);
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            this.DeleteItem();
        }

        private void toolStripButtonAddPositive_Click(object sender, EventArgs e)
        {
            this.OpenPropertiesForm(StepFormMode.AddPositive);
        }

        private void toolStripButtonAddNegative_Click(object sender, EventArgs e)
        {
            this.OpenPropertiesForm(StepFormMode.AddNegative);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (this.stepTree.SelectedNode == null)
            {
                toolStripButtonAddPositive.Enabled = false;
                toolStripButtonAddNegative.Enabled = false;
                toolStripButtonRemove.Enabled = false;               
                toolStripButtonProperty.Enabled = false;
                timer1.Enabled = true;
                return;
            }
            toolStripButtonProperty.Enabled = true;
            toolStripButtonProperty.Enabled = true;
            toolStripButtonAddPositive.Enabled = true;
            toolStripButtonAddNegative.Enabled = true;
            toolStripButtonRemove.Enabled = true;
            foreach (TreeNode node in this.stepTree.SelectedNode.Nodes)
            {
                if (node.Tag == null)
                {
                    continue;
                }
                if (((StepsProperties)node.Tag).IsPositive)
                {
                    toolStripButtonAddPositive.Enabled = false;
                    continue;
                }

                if (!((StepsProperties)node.Tag).IsPositive)
                {
                    toolStripButtonAddNegative.Enabled = false;
                    continue;
                }
            }
            if (stepTree.SelectedNode.Parent == null)
            {
                toolStripButtonRemove.Enabled = false;
            }
            timer1.Enabled = true;
        }
    }
    public enum StepFormMode
    {
        Edit = 0,
        AddPositive = 1,
        AddNegative = 2,
    }
}
