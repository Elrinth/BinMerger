using BinMerger.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinMerger
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog dlg = new FolderBrowserDialog())
      {
        if (txtFolderBinCues.Text != "" && Directory.Exists(txtFolderBinCues.Text))
        {
          dlg.SelectedPath = txtFolderBinCues.Text;
        }
        else
        {
          dlg.SelectedPath = Directory.GetCurrentDirectory();
        }
        if (dlg.ShowDialog() != DialogResult.OK)
        {
          return;
        }
        txtFolderBinCues.Text = dlg.SelectedPath;
        btnStart.Enabled = (txtFolderBinCues.Text != "");
      }
    }


    private void btnStart_Click(object sender, EventArgs e)
    {
      btnClearLog.Enabled = false;
      txtLog.Invoke((MethodInvoker)delegate ()
      {
        txtLog.AppendText("Start initiated... Please wait while scanning folder for .cue files.\r\n");
      });
      DateTime startTime = DateTime.Now;


      // find all bin/cue files:
      string[] allCueFiles = null;
      string[] allBinFiles = null;
      try
      {
        allCueFiles = Directory.GetFiles(txtFolderBinCues.Text, "*.cue", SearchOption.AllDirectories);
        allBinFiles = Directory.GetFiles(txtFolderBinCues.Text, "*.bin", SearchOption.AllDirectories);
      }
      catch (Exception ex)
      {
        txtLog.Invoke((MethodInvoker)delegate ()
        {
          txtLog.AppendText("Could not access folder: " + txtFolderBinCues.Text + " exception: " + ex.ToString() + "\r\n");
        });
        btnClearLog.Enabled = true;
        return;
      }
      if (allCueFiles == null || allCueFiles.Length == 0)
      {
        txtLog.Invoke((MethodInvoker)delegate ()
        {
          txtLog.AppendText("No .cue files were found in the folder: " + txtFolderBinCues.Text + "! Are you sure you have .cue files there?\r\n");
        });
        btnClearLog.Enabled = true;
        return;
      }
      if (allBinFiles == null || allBinFiles.Length == 0)
      {
        txtLog.Invoke((MethodInvoker)delegate ()
        {
          txtLog.AppendText("No .bin files were found in the folder: " + txtFolderBinCues.Text + "! Are you sure you have .bin files there?\r\n");
          if (allCueFiles != null)
          {
            txtLog.AppendText("You do however have " + allCueFiles.Length + ".cue files in the folder! Are they already converted to .cdm?\r\n");
          }
        });
        btnClearLog.Enabled = true;
        return;
      }
      var approvedCueFiles = new List<string>();
      long totalFileSize = 0;
      var approvedBinFiles = new List<string>();

      Dictionary<string, List<string>> binFilesPerCue = new Dictionary<string, List<string>>();
      foreach (string cueFile in allCueFiles)
      {
        // read cue file to figure out which binfile we are looking for:
        // try here?
        string[] lines = null;
        try
        {
          lines = File.ReadAllLines(cueFile);
        }
        catch (Exception ex)
        {
          txtLog.Invoke((MethodInvoker)delegate ()
          {
            txtLog.AppendText("Could not read: " + cueFile + "! " + ex.ToString() + "\r\n");
          });
        }
        if (lines == null)
          continue;
        var binFileName = "";
        var hadBinFile = false;
        // read all lines to figure out which bin files we should actually remove afterwards if we selected remove...
        foreach (var line in lines)
        {
          if (line.StartsWith("FILE \"") && line.EndsWith("\" BINARY"))
          {
            binFileName = line.Substring("FILE \"".Length, line.LastIndexOf("\" BINARY") - "FILE \"".Length);
            if (binFileName != "" && binFileName.EndsWith(".bin"))
            {
              foreach (string binFile in allBinFiles)
              {
                if (binFile.Contains(binFileName))
                {
                  if (approvedBinFiles.Contains(binFile) == false)
                  {
                    if (!binFilesPerCue.ContainsKey(cueFile))
                    {
                      binFilesPerCue.Add(cueFile, new List<string>());
                    }
                    binFilesPerCue[cueFile].Add(binFile);
                    approvedBinFiles.Add(binFile);
                    // get file size:
                    totalFileSize += new FileInfo(binFile).Length;
                  }
                  hadBinFile = true;
                }
              }
            }
          }
        }

        if (hadBinFile)
        {
          approvedCueFiles.Add(cueFile);
        }
      }

      txtLog.Invoke((MethodInvoker)delegate ()
      {
        txtLog.AppendText("Found: " + approvedCueFiles.Count + " .cue files with .bin files. Conversion starting in 1 seconds...\r\n");
      });
      Task.Delay(1000).Wait();
      txtLog.Invoke((MethodInvoker)delegate ()
      {
        txtLog.AppendText("1 second...\r\n");
      });
      Task.Delay(1000).Wait();

      bool canceled = false;
      var binFilesRemoved = 0;
      var cueFilesRemoved = 0;
      var countCueFilesProcessed = 0;
      // CONVERT!
      foreach (string filePath in approvedCueFiles)
      {
        if (!File.Exists(filePath))
        {
          txtLog.Invoke((MethodInvoker)delegate ()
          {
            txtLog.AppendText("Could not find cue any longer @" + filePath + "!\r\n");
          });
          continue;
        }
        var tempPath = Path.GetDirectoryName(filePath) + "\\temp";
        var outpath = Path.GetDirectoryName(filePath);
        var fName = Path.GetFileNameWithoutExtension(filePath);

        // must create temp folder first!
        if (!Directory.Exists(tempPath))
        {
          Directory.CreateDirectory(tempPath);
        }

        var proc = new Process
        {
          StartInfo = new ProcessStartInfo
          {
            FileName = "binmerge/binmerge.exe",
            Arguments = "-o \"" + tempPath + "\" \"" + filePath + "\" \"" + fName + "\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
          }
        };
        proc.Start();
        string outPut = proc.StandardOutput.ReadToEnd();
        if (outPut.Contains("ERROR"))
        {
          txtLog.Invoke((MethodInvoker)delegate ()
          {
            txtLog.AppendText("Error occured when merging: " + outPut + "\r\n");
          });
          proc.WaitForExit();
          proc.Close();
          return;
        }
        txtLog.Invoke((MethodInvoker)delegate ()
        {
          txtLog.AppendText("Combined .bin/.cue cd image: " + (countCueFilesProcessed + 1) + " / " + approvedCueFiles.Count + " with file: " + filePath + ". " + outPut + "\r\n");
        });
        proc.WaitForExit();
        var exitCode = proc.ExitCode;
        proc.Close();

        // delete old bin files
        foreach (string binFile in binFilesPerCue[filePath])
        {
          if (File.Exists(binFile))
          {
            File.Delete(binFile);
            binFilesRemoved++;
          }
        }
        // delete old cue file
        if (File.Exists(filePath))
        {
          File.Delete(filePath);
          cueFilesRemoved++;
        }
        // move the temp files
        File.Move(tempPath + "\\" + fName + ".bin", outpath + "\\" + fName + ".bin");
        File.Move(tempPath + "\\" + fName + ".cue", outpath + "\\" + fName + ".cue");
        // delete the temp folder
        if (Directory.Exists(tempPath))
        {
          Directory.Delete(tempPath);
        }


        countCueFilesProcessed++;
      }
      if (binFilesRemoved != 0)
      {
        txtLog.Invoke((MethodInvoker)delegate ()
        {
          txtLog.AppendText("Deleted " + binFilesRemoved + " old .bin files!\r\n");
        });
      }

      TimeSpan diff = DateTime.Now - startTime;

      txtLog.Invoke((MethodInvoker)delegate ()
      {
        txtLog.AppendText("Finished combining " + countCueFilesProcessed + " .bin/.cue files after " + Math.Round(diff.TotalSeconds, 2) + " seconds!\r\n");
      });
      

      btnClearLog.Enabled = true;
    }

    private void txtFolderBinCues_TextChanged(object sender, EventArgs e)
    {
      if (txtFolderBinCues.Text != "")
      {
        btnStart.Enabled = true;
      } else
      {
        btnStart.Enabled = false;
      }
    }

    private void btnClearLog_Click(object sender, EventArgs e)
    {
      txtLog.Invoke((MethodInvoker)delegate ()
      {
        txtLog.ResetText();
      });
    }
  }
}
