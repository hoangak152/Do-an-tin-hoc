using System;
using System.Collections.Generic;

public class AVLNode
{
    public double Key;           
    public string[] RowData;     
    public int Height;
    public AVLNode Left, Right;

    public AVLNode(double key, string[] row)
    {
        Key = key;
        RowData = row;
        Height = 1;
    }
}

public class AVLTree
{
    public AVLNode Root;

    int Height(AVLNode N) => N?.Height ?? 0;

    int GetBalance(AVLNode N) => N == null ? 0 : Height(N.Left) - Height(N.Right);

    AVLNode RightRotate(AVLNode y)
    {
        AVLNode x = y.Left;
        AVLNode T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    AVLNode LeftRotate(AVLNode x)
    {
        AVLNode y = x.Right;
        AVLNode T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }

    public AVLNode Insert(AVLNode node, double key, string[] row)
    {
        if (node == null)
            return new AVLNode(key, row);

        if (key <= node.Key)
            node.Left = Insert(node.Left, key, row);
        else if (key > node.Key)
            node.Right = Insert(node.Right, key, row);
        

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        int balance = GetBalance(node);

        // 4 trường hợp mất cân bằng
        if (balance > 1 && key <= node.Left.Key)
            return RightRotate(node);

        if (balance < -1 && key > node.Right.Key)
            return LeftRotate(node);

        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }
    public List<AVLNode> InOrder(AVLNode node)
    {
        List<AVLNode> list = new List<AVLNode>();
        if (node == null) return list;

        list.AddRange(InOrder(node.Left));
        list.Add(node);
        list.AddRange(InOrder(node.Right));

        return list;
    }
}
