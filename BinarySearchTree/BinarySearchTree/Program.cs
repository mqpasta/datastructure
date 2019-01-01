using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree bst = new BinarySearchTree();
            bst.Add(20);
            bst.Add(10);
            bst.Add(30);
            bst.Add(5);
            bst.Add(2);
            bst.Add(15);
            bst.Add(17);

            bst.InOrder();
            bst.Delete(30);
            bst.InOrder();

        }
    }

    public class BinarySearchTree
    {
        private BNode root;

        public BinarySearchTree()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.root = null;
        }

        public void Add(int e)
        {
            root = Add(root, e);
        }

        public BNode Search(int e)
        {
            return Search(root, e);
        }

        private BNode Search(BNode node, int e)
        {
            if (node == null)
                return null;

            if (node.data == e)
                return node;

            if (e < node.data)
                return Search(node.left, e);
            else
                return Search(node.right, e);
        }

        private BNode Add(BNode node, int e)
        {
            if (node == null)
            {
                node = new BNode(e);
                return node;
            }

            if (e < node.data)
                node.left = Add(node.left, e);
            else
                node.right = Add(node.right, e);

            return node;
        }

        public void InOrder()
        {
            InOrder(root);
        }

        public void PreOrder()
        {
            PreOder(root);
        }

        public void PostOrder()
        {
            PostOder(root);
        }

        private void InOrder(BNode node)
        {
            if (node == null)
                return;
            InOrder(node.left);
            Console.WriteLine(node.data);
            InOrder(node.right);
        }

        private void PreOder(BNode node)
        {
            if (node == null)
                return;

            Console.WriteLine(node.data);
            PreOder(node.left);
            PreOder(node.right);
        }

        private void PostOder(BNode node)
        {
            if (node == null)
                return;

            PostOder(node.left);
            PostOder(node.right);
            Console.WriteLine(node.data);
        }

        public int Height()
        {
            return Height(root);
        }

        public int Height(BNode node)
        {
            if (node == null)
                return -1;

            int l = Height(node.left);
            int r = Height(node.right);

            if (l > r)
                return l + 1;
            else
                return r + 1;
        }

        public int Size()
        {
            return Size(root);
        }

        public int Size(BNode node)
        {
            if (node == null)
                return 0;

            return 1 + Size(node.left) + Size(node.right);
        }

        public BNode Successor(BNode node)
        {
            if (node == null)
                return null;

            return Minimum(node.right);
        }

        public BNode Predecessor(BNode node)
        {
            if (node == null)
                return null;

            return Maximum(node.left);
        }

        public BNode Minimum(BNode node)
        {
            if (node == null)
                return null;

            if (node.left == null)
                return node;
            else
                return Minimum(node.left);
        }

        public BNode Maximum(BNode node)
        {
            if (node == null)
                return null;

            if (node.right == null)
                return node;
            else
                return Maximum(node.right);
        }

        public void Delete(int e)
        {
            root = Delete(root, e);
        }

        public BNode Delete(BNode node, int e)
        {
            if (node == null)
                return null;

            if (node.data == e)
            {
                // case 1
                if (node.left == null && node.right == null)
                    return null;
                // case 2 - left child is not empty - connect parent with left child
                if (node.left != null && node.right == null)
                    return node.left;
                // case 2 - right child is not empty - connect parent with right child
                if (node.right != null && node.left == null)
                    return node.right;
                // nothing like above then
                // both children are not empty
                if (node.right != null)
                {
                    BNode succ = Successor(node);
                    node.data = succ.data;
                    node.right = Delete(node.right, succ.data);
                }
                else
                {
                    BNode pred = Predecessor(node);
                    node.data = pred.data;
                    node.left = Delete(node.left, pred.data);
                }

                return node;
            }

            if (e < node.data)
                node.left = Delete(node.left, e);
            else
                node.right = Delete(node.right, e);

            return node;
        }
    }

    public class BNode
    {
        public int data;
        public BNode left;
        public BNode right;

        public BNode()
        {
            Initialize();
        }

        public BNode(int e)
        {
            this.data = e;
            Initialize();
        }

        private void Initialize()
        {
            left = null;
            right = null;
        }
    }
}
