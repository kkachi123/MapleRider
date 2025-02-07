using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class BehaviorTree : ScriptableObject
{
    public Node rootNode;
    public List<Node> nodes = new List<Node>();

    GameObject target;

    public void SetTarget(GameObject target)
    {
        this.target = target;
        rootNode.SetTarget(target);
    }
    public Node.STATE Operate()
    {
        return rootNode.Evaluate();
    }
    public BehaviorTree Clone()
    {
        BehaviorTree tree= Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        return tree;
    }

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }
    public void DeleteNode(Node node)
    {
        RootNode root = node as RootNode;
        if(root != null)
        {
            rootNode = null;
        }
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }
    public void AddChild(Node parent, Node child)
    {
        RootNode root = parent as RootNode;
        if (root != null)
        {
            root.child = child;
        }

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator != null)
        {
            decorator.child = child;
        }
        
        CompositeNode composite = parent as CompositeNode;
        if (composite != null)
        {
            composite.AddChild(child);
        }


    }
    public void RemoveChild(Node parent, Node child)
    {
        RootNode root = parent as RootNode;
        if (root != null)
        {
            root.child = null;
        }
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator != null)
        {
            decorator.child = null;
        }
        CompositeNode composite = parent as CompositeNode;
        if (composite != null)
        {
            composite.RemoveChild(child);
        }
    }
    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        RootNode root = parent as RootNode;
        if(root != null && root.child != null)
        {
            children.Add(root.child);
        }
        DecoratorNode decorator = parent as DecoratorNode;
        if(decorator != null && decorator.child != null)
        {
            children.Add(decorator.child);
        }
        CompositeNode composite = parent as CompositeNode;
        if(composite != null)
        {
            return composite.children;
        }

        return children;

    }
}
