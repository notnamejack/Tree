using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Tree
    {
        public int Depth { get; set; }
        public bool Size { get; set; }
        public bool Human { get; set; }

        private List<TreeObject> treeObjects = new List<TreeObject>();

        

        public void Start(string dirName, int depth)
        {

            if (Directory.Exists(dirName))
            {
                Console.WriteLine($"{new DirectoryInfo(dirName).Name}");
                var dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    TreeObject tree = new TreeObject(Name: new DirectoryInfo(s).Name, Type: "D", Depth: depth, Size: "", DateCreat: new DirectoryInfo(s).CreationTime.ToString());
                    if(depth + 4 / 4 < Depth)
                        tree.trees = SetSubDirectory(s, depth + 4);
                    treeObjects.Add(tree);

                }
                var files = Directory.GetFiles(dirName);
                var lastFile = files.LastOrDefault();
                foreach (string s in files)
                {
                    TreeObject tree = null;
                    if (lastFile == s)
                    {
                        tree = new TreeObject(Name: new FileInfo(s).Name, Type: "F", Depth: 0, Size: new FileInfo(s).Length.ToString(), DateCreat: new FileInfo(s).CreationTime.ToString());
                        tree.Human = "└──";
                    }
                    else
                        tree = new TreeObject(Name: new FileInfo(s).Name, Type: "F", Depth: 0, Size: new FileInfo(s).Length.ToString(), DateCreat: new FileInfo(s).CreationTime.ToString());
                    treeObjects.Add(tree);
                }
            }
            GetTreeObject();
        }

        public List<TreeObject> SetSubDirectory(string dirName, int depth){
            List<TreeObject> treesSub = new List<TreeObject>();
            TreeObject tree = null;
            if (Directory.Exists(dirName))
            {
                var dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    tree = new TreeObject(Name: new DirectoryInfo(s).Name, Type: "D", Depth: depth, Size: "", DateCreat: new DirectoryInfo(s).CreationTime.ToString());
                    if (((depth + 4 )/ 4) < Depth)
                        tree.trees = SetSubDirectory(s, depth + 4);
                    treesSub.Add(tree);
                }
                var files = Directory.GetFiles(dirName);
                var lastFile = files.LastOrDefault();
                foreach (string s in files)
                {                    
                    if (lastFile == s)
                    {
                        tree = new TreeObject(Name: new FileInfo(s).Name, Type: "F", Depth: depth, Size: new FileInfo(s).Length.ToString(), DateCreat: new FileInfo(s).CreationTime.ToString());
                        tree.Human = "└──";
                    }
                    else
                        tree = new TreeObject(Name: new FileInfo(s).Name, Type: "F", Depth: depth, Size: new FileInfo(s).Length.ToString(), DateCreat: new FileInfo(s).CreationTime.ToString());
                    treesSub.Add(tree);
                }
            }
            return treesSub;
        }


        public void GetTreeObject()
        {
            foreach(TreeObject item in treeObjects)
            {
                string text = "";
                if(item.Type == "D")
                    text = $"{GetTab(item.Depth, item.Human)} {item.Name}";
                else
                {
                    if(Size == true)
                        text = $"{GetTab(item.Depth, item.Human)} {item.Name} ({item.Size} Б)";
                    else
                        text = $"{GetTab(item.Depth, item.Human)} {item.Name} ";
                }                    
                Console.WriteLine(text);
                if (item.Type == "D")
                    GetTreeObjectSub(item.trees);
            }
        }

        public void GetTreeObjectSub(List<TreeObject> tree)
        {
            if (tree != null)
                foreach(TreeObject item in tree)
                {
                    string text = "";
                    if (item.Type == "D")
                    {
                        text = $"{GetTab(item.Depth, item.Human)} {item.Name}";
                    }
                    else
                    {
                        if (Size == true)
                            text = $"{GetTab(item.Depth, item.Human)} {item.Name} ({item.Size} Б)";
                        else
                            text = $"{GetTab(item.Depth, item.Human)} {item.Name} ";
                    }
                    Console.WriteLine(text);
                    if (item.Type == "D")
                        GetTreeObjectSub(item.trees);
                }
        }


        public string GetTab(int depth, string human)
        {
            string tab = "";
            if (Human)
            {
                for (int i = 0; i < depth; i++)
                {
                    if (i != 0)
                        tab += " ";
                    else
                        tab += "│";
                }
                tab += human;
            }

            return tab;
        }

    }

    class TreeObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Human { get; set; } = "├──";
        public int Depth { get; set; }
        public string Size { get; set; }
        public string DateCreat { get; set; }

        public List<TreeObject> trees { get; set; }

        public TreeObject(string Name, string Type, int Depth, string Size, string DateCreat)
        {
            this.Name = Name;
            this.Type = Type;
            this.Depth = Depth;
            this.Size = Size;
            this.DateCreat = DateCreat;
        }
    }    
}
