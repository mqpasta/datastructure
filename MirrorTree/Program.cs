using System;

namespace MirrorTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BNode root = new BNode();
            root.Value = 1;
            root.AddLeft(2);
            root.AddRight(2);
            root.Left.AddLeft(4);
            root.Left.AddRight(5);
            root.Right.AddLeft(5);
            root.Right.AddRight(4);

            

            //Console.WriteLine(MirrorTree.IsMirror(root.Left, root.Right));
            Console.WriteLine(MirrorTree.IsMirror(root));

            Console.WriteLine("EXample from GeeksForGeeks");
            BNode exmp = GetExampleTree1();
            BNode exmpMirror = GetExampleTree1Mirror();
            Console.WriteLine(MirrorTree.IsMirror(exmp,exmpMirror));
        }

        static BNode GetExampleTree1 ()
        {
            BNode root = new BNode();
            root.Value = 10;
            root.AddLeft(20);
            root.AddRight(30);
            root.Left.AddLeft(40);
            root.Left.AddRight(60);

            return root;
        }

        static BNode GetExampleTree1Mirror()
        {
            BNode root = new BNode();
            root.Value = 10;
            root.AddLeft(30);
            root.AddRight(20);
            root.Right.AddLeft(60);
            root.Right.AddRight(40);

            return root;
        }
    }

    public class MirrorTree
    {

        public static bool IsMirror(BNode tree)
        {
            if (tree == null)
                return true;

            return IsMirror(tree.Left, tree.Right);
        }
        public static bool IsMirror(BNode lTree, BNode rTree)
        {
            if (lTree == null && rTree == null)
                return true;

            if (lTree == null || rTree == null)
                return false;

            if (lTree.Value != rTree.Value)
                return false;

            if (IsMirror(lTree.Left, rTree.Right) &&
                IsMirror(lTree.Right, rTree.Left))
                return true;

            return false;
        }

    }

    public class BNode
    {
        private BNode _root;

        public BNode Left { get; set; }
        public BNode Right { get; set; }
        public int Value { get; set; }

        public void AddLeft(int val)
        {
            if (Left == null)
                Left = new BNode();
            Left.Value = val;
        }

        public void AddRight(int val)
        {
            if (Right == null)
                Right = new BNode();
            Right.Value = val;
        }

    }
}
