using System.Collections.Generic;
using System;
using System.Linq;

namespace Dijkstra
{

    public class Dijkstra
    {
        private List<Node> _nodes;
        private List<Edge> _edges;

        public Dijkstra()
        {
            _nodes = new List<Node>();
            _edges = new List<Edge>();
        }

        public void InitWeights(List<Tuple<string, string, double>> weights)
        {
            _edges = ConvertToEdge(weights);

            InitNodes();
        }

        public List<string> Find(string start, string end)
        {
            var startNode = GetNodeByName(start);
            var endNode = GetNodeByName(end);

            if (startNode == null || endNode == null)
            {
                return new List<string>();
            }

            var s = _nodes.Where(x => x.Name == start).ToList();
            var u = _nodes.Where(x => x.Name != start).ToList();

            s.ForEach(x =>
            {
                x.Weight = 0;
            });

            u.ForEach(x =>
            {
                x.Weight = double.PositiveInfinity;
            });

            while (u.Any())
            {
                var node = s.Last();

                //更新node到x的距离
                u.ForEach(x =>
                {
                    var edge = GetEdgeByTwoNode(node, x);

                    if (edge != null)
                    {
                        var weights = node.GetAllWeight() + edge.Weight;

                        if (weights < x.Weight)
                        {
                            x.Weight = weights;
                            x.Parent = node;
                        }
                    }
                });

                //找出距离最小的
                var minNode = u.OrderBy(x => x.Weight).FirstOrDefault();

                s.Add(minNode);
                u.Remove(minNode);
            }

            var paths = new Stack<string>();

            while (endNode != null)
            {
                paths.Push(endNode.Name);
                endNode = endNode.Parent;
            }

            //如果路劲中包含起点
            if (paths.ToList().Contains(start))
            {
                return paths.ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        private Edge GetEdgeByTwoNode(Node start, Node end)
        {
            var edge = _edges.FirstOrDefault(x => x.Start == start.Name && x.End == end.Name);

            if (edge == null)
            {
                edge = _edges.FirstOrDefault(x => x.Start == end.Name && x.End == start.Name);
            }

            return edge;
        }

        /// <summary>
        /// 将权值转换成边
        /// </summary>
        /// <returns></returns>
        private List<Edge> ConvertToEdge(List<Tuple<string, string, double>> weights)
        {
            var edges = new List<Edge>();

            weights.ForEach(weight =>
            {
                edges.Add(new Edge()
                {
                    Start = weight.Item1,
                    End = weight.Item2,
                    Weight = weight.Item3
                });
            });

            return edges;
        }


        /// <summary>
        /// 根据名字获取节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private Node GetNodeByName(string nodeName)
        {
            return _nodes.FirstOrDefault(x => x.Name == nodeName);
        }

        /// <summary>
        /// 初始化点
        /// </summary>
        private void InitNodes()
        {
            _edges.ForEach(weight =>
            {
                if (_nodes.All(x => x.Name != weight.Start))
                {
                    _nodes.Add(new Node()
                    {
                        Name = weight.Start
                    });
                }

                if (_nodes.All(x => x.Name != weight.End))
                {
                    _nodes.Add(new Node()
                    {
                        Name = weight.End
                    });
                }
            });
        }
    }
    //表示一条边，从Start到End的权值为多少
    internal class Edge
    {
        public string Start
        {
            get; set;
        }

        public string End
        {
            get; set;
        }

        public double Weight
        {
            get; set;
        }
    }
    internal class Node
    {
        public string Name
        {
            get; set;
        }

        public Node Parent
        {
            get; set;
        }

        /// <summary>
        /// 该节点到起点的最短距离
        /// </summary>
        public double Weight
        {
            get; set;
        }

        public double GetAllWeight()
        {
            var allWeight = 0d;

            var node = this;

            do
            {
                allWeight += node.Weight;

                node = node.Parent;

            } while (node != null);

            return allWeight;
        }
    }
}
