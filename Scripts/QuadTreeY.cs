using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class QuadTreeY //: MonoBehaviour
{
    public QuadTreePt TOP;
    public QuadTreePt BOTTOM;

    public QuadTreeNode node;

    public QuadTreeY topTree;
    public QuadTreeY botTree;
    public QuadTreeY parentTree;
        
    //Create an empty QuadTree (default constructor)
    public QuadTreeY()
    {
        TOP = new QuadTreePt(0);
        BOTTOM = new QuadTreePt(0);
        node = null;
        topTree = null;
        botTree = null;
    }

    //create a new quad tree based on the top and bottom boundaries
    public QuadTreeY(QuadTreePt _TOP, QuadTreePt _BOTTOM)
    {
        node = null;
        topTree = null;
        botTree = null;
        TOP = _TOP;
        BOTTOM = _BOTTOM;
    }

    //Helper function to determine if a given point is within the boundary of the layer based on the bottom and top boundary points
    //return false if the point is > top boundary or < bottom boundary (outside the layer)
    bool inBoundary(QuadTreePt depth)
    {
        return (depth.y >= BOTTOM.y
             && depth.y <= TOP.y);
    }
    
    public void insert(QuadTreeNode insertNode)
    {
        //If the given node is null, return (cannot add an empty node to the tree)
        if (insertNode == null)
        {
            Debug.Log("Null Node");
            return;
        }

        //If the given node is not within the boundary of the layer, return (cannot add a node that is outside the layer to the tree)
        if (!inBoundary(insertNode.y))
        {
            return;
        }

        //If we have reached the smallest sized subquadrant where node could be inserted and there is no other node existing there
        //Insert node into tree at this position
        if (Math.Abs(TOP.y - BOTTOM.y) <= 0.1)
        {
            if (node == null)
            {
                node = insertNode;
                //Debug.Log("iiiiiiiinsert succesfull");
            }
            return;
        }

        /* DEPTH (Y) */
        //If node is on the TOP side of the current quadrant
        if ((TOP.y + BOTTOM.y) / 2 <= insertNode.y.y)
        {
            /* INSERT TO TOP SUBQUADRANT */
            //If the top subquadrant is empty
            if (topTree == null)
            {
                //Create a new top tree based on the boundaries of the top subquadrant of the original quadrant
                topTree = new QuadTreeY(
                    new QuadTreePt(TOP.y), 
                    new QuadTreePt((TOP.y + BOTTOM.y) / 2)
                    );

                topTree.parentTree = this;
            }
            //Recursively ass the node into the top subquadrant by passing it to the insert function as a new tree
            topTree.insert(insertNode);
        }

        //If node is on the BOTTOM side of the current quadrant
        else
        {
            /* INSERT TO BOTTOM SUBQUADRANT */
            //If the bottom subquadrant is empty
            if (botTree == null)
            {
                //Create a new bottpm tree based on the boundaries of the bottom subquadrant of the original quadrant
                botTree = new QuadTreeY(
                    new QuadTreePt((TOP.y + BOTTOM.y) / 2),
                    new QuadTreePt(BOTTOM.y)
                    );

                botTree.parentTree = this;
            }
            //Recursively ass the node into the bottom subquadrant by passing it to the insert function as a new tree
            botTree.insert(insertNode);
        }
    }

    //Function to search for a given point in the quad tree and return the closest node found to this point
    //recursively narrow down our search to subquadrants that would contain the coordinates of the desired point based on the boundaries
    public QuadTreeNode search(QuadTreePt depth)
    {
        //If we have located a node at the desired location and it is not empty (this occurs after tree has been traversed)
        //return this node as the closest point
        if (node != null)
        {
            return node;
        }

        //Node is calculated to be on the TOP side of the current quadrant based on the boundaries
        if ((TOP.y + BOTTOM.y) / 2 <= depth.y)
        {
            //if the node at this subtree is null (we have narrowed down our search to one node and this node is empty)
            //We need to check the neighbouring quadrants instead (the other subquadrant)
            //Call neighboursSearch method to check these quadrants
            if (topTree == null)
            {
                return this.neighboursSearch(depth);
            }
            //We haven't narrowed our search down to a single node yet, keep searching in the top subtree
            return topTree.search(depth);
        }

        //Node is calculated to be on the BOTTOM side of the current quadrant based on the boundaries
        else
        {
            //if the node at this subtree is null (we have narrowed down our search to one node and this node is empty)
            //We need to check the neighbouring quadrants instead (the other subquadrant)
            //Call neighboursSearch method to check these quadrants
            if (botTree == null)
            {
                return this.neighboursSearch(depth);
            }
            //We haven't narrowed our search down to a single node yet, keep searching in the bottom subtree
            return botTree.search(depth);
        }
    }

    //Function to search neighbouring subquadrants for a desired point when a regular search does not return a node at the smallest subdivided tree
    public QuadTreeNode neighboursSearch(QuadTreePt depth)
    {
        //If top subquadrant is not empty, search this tree
        if (topTree != null)
        {
            return topTree.search(depth);
        }

        //If bottom subquadrant is not empty, search this tree
        if (botTree != null)
        {
            return botTree.search(depth);
        }

        //If all neighbouring subtrees are empty, backtrack to the parent of the current tree and search the neighbouring quadrants of that tree
        //which were skipped over in the original search
        else
        {
            return parentTree.neighboursSearch(depth);
        }
    }

    //Function to build a node given a pair of coordinate values and their associated data from a single csv file
    QuadTreeNode buildNode(string line, int dNo)
    {
        //split the csv line
        string[] values = line.Split(',');
        //Debug.Log("ccccccccc" + values[0]);
        double d;
        double[] data;
        if (double.TryParse(values[0], out d))
        {
            data = new double[7];
            for (int i = 1; i < 8; i++)
            {
                data[i-1] = double.Parse(values[i]);
            }

            //convert
            //GameObject ob = GameObject.FindGameObjectWithTag("dataDisplay");
            //var read = ob.GetComponent<readcsv>();
            //double unityDepthY = read.getUnityY(dNo);
            double unityDepthY = readcsv.instance.getUnityY(dNo);
            //Debug.Log("aaaaaaaa" + unityDepthY);

            //create new points based on the coordinates
            QuadTreePt dep = new QuadTreePt(unityDepthY);
            //Debug.Log("fffffff" + dep.y);

            //create a new node from the points and the other data, then return this node
            QuadTreeNode node = new QuadTreeNode(dep, d, data);
            return node;
        }
        else
        {
            return null;
        }
    }

    //insert node to the tree
    public void buildTree(string[] lines)
    {
        //string[] lines = filePath.text.Split('\n');        
        string line;
        for (int i = 2; i < lines.Length; i++)
        {
            line = lines[i];
            if (line != null)
            {
                int dNo = i - 2;
                string[] values = line.Split(',');
                //Debug.Log("ddddddd" + values[0]);
                if(values[0]!=null)
                {
                    QuadTreeNode currNode = buildNode(line, dNo);
                    if(currNode!=null)
                    {
                        insert(currNode);
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
