using log4net;
using SnagFree.TrayApp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTrigger
{
	public partial class Form1 : Form
	{
		private static readonly ILog Logger = LogManager.GetLogger(typeof(Form1));

		private StringBuilder _buffer;

		public Form1()
		{
			log4net.Config.XmlConfigurator.Configure();

			Logger.Info("Startup");
			InitializeComponent();

			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;

			_buffer = new StringBuilder();

			GlobalKeyboardHook hook = new GlobalKeyboardHook();
			hook.KeyboardPressed += OnKeyboardPressed;
		}

		private void OnKeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
		{
			if (e.KeyboardData.Flags == 0) // Prevents injected keyboard events from being processed. Was receiving duplicate events for every keystroke.
			{
				const int ENTER_KEY_VIRTUAL_CODE = 13;
				if (e.KeyboardData.VirtualCode == ENTER_KEY_VIRTUAL_CODE)
				{
					string bufferContents = _buffer.ToString();
					_buffer.Clear();

					if (bufferContents.ToLower() == "quit")
					{
						Logger.Info("Exiting");
						Application.Exit();
					}
					else
					{
						TryToPlayMusicForIdentifier(bufferContents);
					}
				}
				else
				{
					string character = Encoding.ASCII.GetString(new byte[] { Convert.ToByte(e.KeyboardData.VirtualCode) });
					if (!string.IsNullOrEmpty(character))
					{
						_buffer.Append(character);
					}
				}
			}
		}

		private void TryToPlayMusicForIdentifier(string identifier)
		{
			string recordsFilePath = ConfigurationManager.AppSettings["RecordFilePath"];
			if (string.IsNullOrEmpty(recordsFilePath)) 
			{
				Logger.Warn("Unable to find the `RecordFilePath` in the .config file!");
				return; // Can't process if we don't have a valid configuration!
			}

			if (!File.Exists(recordsFilePath))
			{
				Logger.Warn("The supplied RecordFilePath `" + recordsFilePath + "` does not exist, or is not able to be read!");
				return; // Can't process if we can't read the file.
			}

			string[] records = null;
			try
			{
				records = File.ReadAllLines(recordsFilePath);
			}
			catch (Exception e)
			{
				Logger.Error("Exception occurred while trying to read the RecordFilePath `" + recordsFilePath + "`.");
				Logger.Error(e);
			} // Intentionally silent. Can't do anything to handle it.

			if (records == null)
			{
				Logger.Warn("Unable to read RecordFilePath, or file was empty.");
				return; // No records, no music.
			}

			bool foundMatchingRecord = false;
			const string DELIMITER = ",";
			string lookFor = identifier + DELIMITER;
			foreach (string record in records)
			{
				if (record.StartsWith(lookFor))
				{
					foundMatchingRecord = true;
					string[] tokens = record.Split(new string[] { DELIMITER }, StringSplitOptions.None);
					if (tokens.Length == 2)
					{
						string mediaFile = tokens[1];
						Logger.Info("Matching record found for `" + identifier + "`. Media=`" + mediaFile + "`.");
						TryToPlayMediaFile(mediaFile);
						break;
					}
				}
			}

			if (!foundMatchingRecord)
			{
				Logger.Info("No matching media record found for identifier `" + identifier + "`.");
			}
		}

		private void TryToPlayMediaFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				string prefix = ConfigurationManager.AppSettings["AssetFileRoot"];
				if (string.IsNullOrEmpty(prefix))
				{
					Logger.Warn("Relative path supplied for media asset `" + filePath +"`, but no `AssetFileRoot` was configured in the .config file!");
				}
				else
				{
					filePath = prefix + filePath;
				}
			}

			if (!File.Exists(filePath))
			{
				Logger.Warn("Tried to play `" + filePath +"`, but the file does not exist.");
				return; // Nothing we can do.
			}

			Logger.Info("Playing `" + filePath + "`.");
			_formMediaPlayer.URL = filePath;
		}
	}
}
