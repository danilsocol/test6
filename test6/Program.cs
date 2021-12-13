using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace test6
{
	class Program
	{
		static int V;

		public static void Main()
		{
			string[] a = File.ReadAllLines("1.txt");
			V = a.Length;

			int[,] graph = new int[V, V];


			FillGraph(a, graph);

			int max = fordFulkerson(graph, 0, 5);

			Console.WriteLine(max);
		}

		static void FillGraph(string[] a, int[,] graph)
        {
			for (int i = 0; i < a.Length; i++)
			{
				string[] row = a[i].Split(";");
				var rowInt = row.Select(s => int.Parse(s)).ToArray();
				for (int j = 0; j < rowInt.Length; j++)
				{
					graph[i, j] = rowInt[j];
				}
			}
		}
		static int fordFulkerson(int[,] graph, int s, int t)
		{
			int u, v;

			int[,] rGraph = new int[V, V];

			Array.Copy(graph, rGraph , graph.Length);

			int[] parent = new int[V];

			int max_flow = 0;

			while (bfs(rGraph, s, t, parent))
			{

				int path_flow = int.MaxValue;
				for (v = t; v != s; v = parent[v])
				{
					u = parent[v];
					path_flow
						= Math.Min(path_flow, rGraph[u, v]);
				}

				for (v = t; v != s; v = parent[v])
				{
					u = parent[v];
					rGraph[u, v] -= path_flow;
					rGraph[v, u] += path_flow;
				}

				max_flow += path_flow;
			}

			return max_flow;
		}

		static bool bfs(int[,] rGraph, int s, int t, int[] parent)
		{

			bool[] visited = new bool[V];
			for (int i = 0; i < V; ++i)
				visited[i] = false;

			List<int> queue = new List<int>();
			queue.Add(s);
			visited[s] = true;
			parent[s] = -1;

			while (queue.Count != 0)
			{
				int u = queue[0];
				queue.RemoveAt(0);

				for (int v = 0; v < V; v++)
				{
					if (visited[v] == false
						&& rGraph[u, v] > 0)
					{
						if (v == t)
						{
							parent[v] = u;
							return true;
						}
						queue.Add(v);
						parent[v] = u;
						visited[v] = true;
					}
				}
			}

			return false;
		}
	}
}
