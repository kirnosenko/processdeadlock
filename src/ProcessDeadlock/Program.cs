using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProcessDeadlock
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var collection = new int[16];
			for (int i = 1; i <= 100; i++)
			{
				Console.WriteLine($"iteration: {i}...");
				Parallel.ForEach(
					collection,
					new ParallelOptions
					{
						MaxDegreeOfParallelism = 16
					},
					item =>
					{
						var output = RunAndWaitForExit("git", "--version");
					});
			}
			Console.WriteLine("no deadlock!");
		}

		static string RunAndWaitForExit(string command, string arguments)
		{
			ProcessStartInfo psi = new ProcessStartInfo(command, arguments);
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;

			Process process = new Process();
			process.StartInfo = psi;
			process.Start();
			var output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();

			return process.ExitCode == 0 ? output : null;
		}
	}
}
