using System;
using System.Diagnostics;
using AIMLbot;
using SpeechLib;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIML
{
    public class Aiml
    {
        private Bot AI;
        private User myuser;

        private string text;
        //private SpeechOut _speechOut;

        public Aiml()
        {
            AI = new Bot();
            myuser = new User("Username Here", AI);
            //_speechOut = new SpeechOut();
            AI.loadSettings(); //It will Load Settings from its Config Folder with this code
            AI.loadAIMLFromFiles(); //With this Code It Will Load AIML Files from its AIML Folder
        }

        public void botInput(string text, Text outText, Text errorText)
        {
            Request r = new Request(text, myuser, AI); //With This Code it will Request The Response From AIML Folders
            Result res = AI.Chat(r); //With This Code It Will Get Result
            string output = res.Output; //With this Code It Will Write the Result of Textbox1 Response to Textbox2 text
            outText.text = output;
            run_cmd(output, errorText);
        }

        private void run_cmd(string output, Text errorText)
        {
            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            //process.StartInfo.FileName = "C:/Users/Branislav/PycharmProjects/exe/dist/TextToSpeech.exe";
            process.StartInfo.FileName = Environment.CurrentDirectory + @"\Assets\StreamingAssets" + @"\TextToSpeech.exe";
            process.StartInfo.FileName = Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeech.exe");
            process.StartInfo.Arguments = output;
            process.EnableRaisingEvents = true;
            Debug.LogWarning(Environment.CurrentDirectory);
            try
            {
                Process.Start(process.StartInfo);
                //Process.Start(process.StartInfo);
            }
            catch (Exception e)
            {
                errorText.enabled = true;
                errorText.text = e.Message;
                throw;
            }
        }
        public void speechOutput(string output)
        {
            SpVoice voice = new SpVoice();
            voice.Speak(output, SpeechVoiceSpeakFlags.SVSFNLPSpeakPunc);
            voice.SynchronousSpeakTimeout = 500;
            voice.Rate = 0;
        }
    }
}

// speechOutput(output);
// _speechOut.speechOutput(output);


// Process process = new Process
// {
//     StartInfo = new ProcessStartInfo
//     {
//         FileName = Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeech.exe"),
//         CreateNoWindow = true,
//         Arguments = output,
//         UseShellExecute = false,
//         RedirectStandardOutput = true,
//         RedirectStandardInput = true,
//         RedirectStandardError = true,
//         WindowStyle = ProcessWindowStyle.Hidden
//     }
// };

// Process process = new Process();
// process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
// process.StartInfo.CreateNoWindow = true;
// process.StartInfo.UseShellExecute = false;
// //process.StartInfo.FileName = "C:/Users/Branislav/PycharmProjects/exe/dist/TextToSpeech.exe";
// process.StartInfo.FileName = Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeech.exe");
// process.StartInfo.Arguments = output;
// process.EnableRaisingEvents = true;
// process.Start();
// if (Input.GetKeyDown(KeyCode.F))
// {
//     process.Kill();
// }
//process.WaitForExit();
//Process process = Process.Start("C:/Users/Branislav/PycharmProjects/exe/dist/TextToSpeech.exe", output);

// private void run_cmd(string cmd, string args)
// {
//   ProcessStartInfo start = new ProcessStartInfo();
//   start.FileName = "D:/Programy/Python/python.exe";
//   start.Arguments = string.Format("{0} {1}", cmd, args);
//   start.UseShellExecute = false;
//   start.RedirectStandardOutput = true;
//   using (Process process = Process.Start(start))
//   {
//     using (StreamReader reader = process.StandardOutput)
//     {
//       string result = reader.ReadToEnd();
//       Debug.LogWarning(result);
//     }
//   }
// }

