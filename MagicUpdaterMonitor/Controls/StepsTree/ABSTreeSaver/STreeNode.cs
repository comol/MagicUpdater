using StepTreeControl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ABSTreeSaver
{
    [Serializable]
    public class STreeNode
    {
        #region USER DEFINED VARIABLES
        string name=String.Empty;
        int imageIndex=-1;
        int selectedImageIndex = -1;
        StepsProperties tag = new StepsProperties();
        string text = String.Empty;
        string toolTipText = String.Empty;
        List<STreeNode> nodes=new List<STreeNode>();
        #endregion
        #region PROPERTIES
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex=value; }
        }
        public int SelectedImageIndex
        {
            get { return selectedImageIndex; }
            set { selectedImageIndex = value; }
        }
        
        public StepsProperties Tag
        {
            get { return tag; }
            set { tag=value; }
        }
        public string ToolTipText
        {
            get { return toolTipText; }
            set { toolTipText=value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public List<STreeNode> Nodes
        {
            get { return nodes; }
            set { nodes=value; }
        }
        #endregion
        public STreeNode()
		{
		}
        
    }
}
