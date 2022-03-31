using System;
using System.IO;
using System.Reflection;
using Mono.Options;
using System.Collections.Generic;

#nullable enable

namespace nnyeah {
	class Program {
		static void Main (string [] args)
		{
			var doHelp = false;
			string? infile = null, outfile = null;
			var verbose = false;
			var forceOverwrite = false;
			var suppressWarnings = false;
			var warnings = new List<string> ();
			var transforms = new List<string> ();

			var options = new OptionSet () {
				{ "h|?|help", o => doHelp = true },
				{ "i=|input=", f => infile = f },
				{ "o=|output=", f => outfile = f },
				{ "v|verbose", o => verbose = true },
				{ "f|force-overwrite", o => forceOverwrite = true },
				{ "s|suppress-warnings", o => suppressWarnings = true },
			};

			try {
				var extra = options.Parse (args);
			} catch {
				doHelp = true;
			}


			if (doHelp || infile is null || outfile is null) {
				PrintOptions (options, Console.Out);
				Environment.Exit (0);
			}

			if (!File.Exists (infile)) {
				Console.Error.WriteLine ($"input file '{infile}' doesn't exist.");
				Environment.Exit (1);
			}

			if (File.Exists (outfile) && !forceOverwrite) {
				Console.Error.WriteLine ($"output file '{outfile}' already exists. Use '--force-overwrite' if you want to overwrite it.");
				Environment.Exit (1);
			}


			using var stm = new FileStream (infile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			var reworker = new Reworker (stm);

			try {
				reworker.Load ();
			} catch (Exception e) {
				Console.Error.WriteLine ($"Unable to read module from file {infile}: {e.Message}.");
				Environment.Exit (1);
			}

			reworker.WarningIssued += (s, e) => warnings.Add (e.HelpfulMessage ());
			reworker.Transformed += (s, e) => warnings.Add (e.HelpfulMessage ());

			if (reworker.NeedsReworking ()) {
				try {
					using var ostm = new FileStream (outfile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
					reworker.Rework (ostm);
					if (verbose) {
						transforms.ForEach (Console.WriteLine);
					}
					if (!suppressWarnings) {
						warnings.ForEach (Console.WriteLine);
					}
				} catch (Exception e) {
					Console.Error.Write ($"Unable to generate output file, unexpected exception: {e.Message}");
					Environment.Exit (1);
				}
			} else {
				if (verbose) {
					Console.WriteLine ("Package does not need changes - no output generated.");
				}
			}
		}

		static void PrintOptions (OptionSet options, TextWriter writer)
		{
			options.WriteOptionDescriptions (writer);
		}
	}
}