using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Scheme> schemes = new List<Scheme>();

            int Attempts = 15000;

            Random random = new Random();

            Node node1 = new Node(0.9f, "p1");
            Node node2 = new Node(0.8f, "p2");
            Node node3 = new Node(0.7f, "p3");
            Node node4 = new Node(0.6f, "p4");
            Node node5 = new Node(0.5f, "p5");
            Node node6 = new Node(0.9f, "p6");
            Node node7 = new Node(0.8f, "p7");
            Node node8 = new Node(0.7f, "p8");
            Node node9 = new Node(0.6f, "p9");
            Node node10 = new Node(0.5f, "p10");
            Node node11 = new Node(0.5f, "p11");

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

            schemes.AddRange(new Scheme[] { scheme1, scheme2, scheme3, scheme4, scheme5 });

            int positive = 0;
            float max = float.MinValue;
            Node additable = new Node(0, string.Empty);

            foreach (Scheme scheme in schemes)
            {
                foreach (Line line in scheme.Lines)
                {
                    foreach (Part part in line.Parts)
                    {
                        if (part.Nodes.Count == 1)
                        {
                            part.Nodes.Add(node11);

                            for (int i = 0; i < Attempts; i++)
                            {
                                if (scheme1.TrySolve(random) && ((scheme2.TrySolve(random) && (scheme3.TrySolve(random) || scheme4.TrySolve(random))) || scheme5.TrySolve(random)))
                                    positive++;
                            }

                            float probability = (float)((float)positive / Attempts);

                            if (max < probability)
                            {
                                max = probability;
                                additable = part.Nodes.First();
                            }

                            positive = 0;

                            part.Nodes.Remove(node11);
                        }
                    }
                }
            }

            Console.WriteLine($"Max: {max}\nAdding: {additable.Label}");
        }

        public class Node
        {
            public float Probability { get; protected set; }
            public string Label { get; private set; }

            public Node(float probability, string label)
            {
                Probability = probability;
                Label = label;
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
