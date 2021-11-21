using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3.P
{
    class Program
    {
        static void Main(string[] args)
        {
            int Attempts = 15000;

            Random random = new Random();

            Node node1 = new Node(0.9f);
            Node node2 = new Node(0.8f);
            Node node3 = new Node(0.7f);
            Node node4 = new Node(0.6f);
            Node node5 = new Node(0.5f);
            Node node6 = new Node(0.9f);
            Node node7 = new Node(0.8f);
            Node node8 = new Node(0.7f);
            Node node9 = new Node(0.6f);
            Node node10 = new Node(0.5f);

            Part part1 = new Part();
            part1.Nodes.Add(node1);

            Part part2 = new Part();
            part2.Nodes.Add(node10);
            part2.Nodes.Add(node2);

            Part part3 = new Part();
            part3.Nodes.Add(node3);

            Part part4 = new Part();
            part4.Nodes.Add(node4);

            Part part5 = new Part();
            part5.Nodes.Add(node5);

            Part part6 = new Part();
            part6.Nodes.Add(node6);

            Part part7 = new Part();
            part7.Nodes.Add(node7);

            Part part8 = new Part();
            part8.Nodes.Add(node8);
            part8.Nodes.Add(node9);

            Line line5 = new Line();
            line5.Parts.Add(part1);

            Scheme scheme1 = new Scheme();
            scheme1.Lines.Add(line5);

            Line line6 = new Line();
            line6.Parts.Add(part2);

            Scheme scheme2 = new Scheme();
            scheme2.Lines.Add(line6);

            Line line7 = new Line();
            line7.Parts.Add(part3);
            line7.Parts.Add(part4);

            Scheme scheme3 = new Scheme();
            scheme3.Lines.Add(line7);

            Line line8 = new Line();
            line8.Parts.Add(part5);
            line8.Parts.Add(part6);
            Scheme scheme4 = new Scheme();
            scheme4.Lines.Add(line8);

            Line line9 = new Line();
            line9.Parts.Add(part7);
            line9.Parts.Add(part8);

            Scheme scheme5 = new Scheme();
            scheme5.Lines.Add(line9);

            int positive = 0;

            for (int i = 0; i < Attempts; i++)
            {
                if (scheme1.TrySolve(random) && ((scheme2.TrySolve(random) && (scheme3.TrySolve(random) || scheme4.TrySolve(random))) || scheme5.TrySolve(random)))
                    positive++;
            }

            float p = (float)((float)positive / Attempts);
            Console.WriteLine($"Probability: {p}");
        }

        public class Node
        {
            public float Probability { get; protected set; }

            public Node(float probability)
            {
                Probability = probability;
            }
        }

        public class Part
        {
            private List<Node> _nodes = new List<Node>();

            public List<Node> Nodes => _nodes;

            public bool TrySolve(Random random)
            {
                foreach (Node node in _nodes)
                {
                    if (random.NextDouble() < node.Probability)
                        return true;

                    System.Threading.Thread.Sleep(1);
                }

                return false;
            }
        }

        public class Line
        {
            private List<Part> _parts = new List<Part>();

            public List<Part> Parts => _parts;

            public bool TrySolve(Random random)
            {
                foreach (Part part in _parts)
                {
                    if (!part.TrySolve(random))
                        return false;
                }

                return true;
            }
        }

        public class Scheme
        {
            private List<Line> _lines = new List<Line>();

            public List<Line> Lines => _lines;

            public bool TrySolve(Random random) 
            {
                foreach (Line line in _lines)
                {
                    if (line.TrySolve(random))
                        return true;
                }

                return false;
            }
        }
    }
}
