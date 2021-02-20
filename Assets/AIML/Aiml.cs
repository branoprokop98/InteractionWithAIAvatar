﻿using System;
using System.Diagnostics;
using System.Globalization;
using AIMLbot;
using SpeechLib;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using Menu;
using Menu.NewGame;
using Menu.SettingsGame;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIML
{
    public class Aiml
    {
        private Bot AI;
        private User myuser;
        private string text;
        private MenuInteraction menuInteraction;
        private int mood;
        private Process process;
        private TaskCompletionSource<bool> eventHandled;
        private Animator animator;
        private float myTime;
        private Settings settings;
        //private SpeechOut _speechOut;

        public Aiml()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            string pathToSettings = Path.Combine(Application.streamingAssetsPath, "Settings.xml");
            menuInteraction = XMLWorker.deserialize<MenuInteraction>(path);
            AI = new Bot();
            myuser = new User("Username Here", AI);
            //_speechOut = new SpeechOut();
            AI.loadSettings(); //It will Load Settings from its Config Folder with this code
            AI.loadAIMLFromFiles(); //With this Code It Will Load AIML Files from its AIML Folder
            mood = 50;
            myTime = 0f;
            settings = XMLWorker.deserialize<Settings>(pathToSettings);
        }

        public void botInput(string text, Text outText, Text errorText, Text moodText, Animator animator)
        {
            myuser.setMood();
            Request r = new Request(text, myuser, AI); //With This Code it will Request The Response From AIML Folders
            Result res = AI.Chat(r); //With This Code It Will Get Result
            string output = res.Output; //With this Code It Will Write the Result of Textbox1 Response to Textbox2 text
            outText.text = output;
            this.animator = animator;
            calculateMood(moodText);
            setMoodAnimation();
            run_cmd(output, errorText);
        }

        private void setMoodAnimation()
        {
            AnimatorStateInfo animationState = this.animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = this.animator.GetCurrentAnimatorClipInfo(0);
            this.myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
            Debug.LogWarning("Mood: " + myTime.ToString(CultureInfo.InvariantCulture));
            if (this.mood <= 30)
            {
                animator.PlayInFixedTime("SadIdle", 0, this.myTime);
                // animator.SetBool(Idle, false);
                // animator.SetBool(IdleTalk, false);
                // animator.SetBool(Happy, false);
                // animator.SetBool(HappyTalk, false);
                // animator.SetBool(SadTalk, false);
                // animator.SetBool(Sad, true);
            }
            else if (this.mood > 30 && this.mood <= 70)
            {
                animator.PlayInFixedTime("Idle", 0, this.myTime);
                // animator.SetBool(Happy, false);
                // animator.SetBool(HappyTalk, false);
                // animator.SetBool(SadTalk, false);
                // animator.SetBool(Sad, false);
                // animator.SetBool(IdleTalk, false);
                // animator.SetBool(Idle, true);
            }

            else if (this.mood > 70)
            {
                animator.PlayInFixedTime("HappyIdle", 0, this.myTime);
                // animator.SetBool(SadTalk, false);
                // animator.SetBool(Sad, false);
                // animator.SetBool(IdleTalk, false);
                // animator.SetBool(Idle, false);
                // animator.SetBool(HappyTalk, false);
                // animator.SetBool(Happy, true);
            }
            // AnimatorStateInfo animationState = this.animator.GetCurrentAnimatorStateInfo(0);
            // AnimatorClipInfo[] myAnimatorClip = this.animator.GetCurrentAnimatorClipInfo(0);
            // this.myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
            // Debug.LogWarning("Mood: " + myTime.ToString(CultureInfo.InvariantCulture));
        }

        private void setTalkAnimation()
        {
            AnimatorStateInfo animationState = this.animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = this.animator.GetCurrentAnimatorClipInfo(0);
            this.myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
            Debug.LogWarning("Talk: " + myTime.ToString(CultureInfo.InvariantCulture) + "Name of clip: " + myAnimatorClip[0].clip.name);
            if (this.mood <= 30)
            {
                animator.PlayInFixedTime("SadTalk", 0, this.myTime);
                // animator.SetBool(Idle, false);
                // animator.SetBool(IdleTalk, false);
                // animator.SetBool(Happy, false);
                // animator.SetBool(HappyTalk, false);
                // animator.SetBool(Sad, false);
                // animator.SetBool(SadTalk, true);
            }
            else if (this.mood > 30 && this.mood <= 70)
            {
                animator.PlayInFixedTime("IdleTalk", 0, this.myTime);
                // animator.SetBool(Happy, false);
                // animator.SetBool(HappyTalk, false);
                // animator.SetBool(SadTalk, false);
                // animator.SetBool(Sad, false);
                // animator.SetBool(Idle, false);
                // animator.SetBool(IdleTalk, true);
            }

            else if (this.mood > 70)
            {
                animator.PlayInFixedTime("HappyTalk", 0, this.myTime);
                // animator.SetBool(SadTalk, false);
                // animator.SetBool(Sad, false);
                // animator.SetBool(IdleTalk, false);
                // animator.SetBool(Idle, false);
                // animator.SetBool(Happy, false);
                // animator.SetBool(HappyTalk, true);
            }
            // AnimatorStateInfo animationState = this.animator.GetCurrentAnimatorStateInfo(0);
            // AnimatorClipInfo[] myAnimatorClip = this.animator.GetCurrentAnimatorClipInfo(0);
            // this.myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
            // Debug.LogWarning("Talk: " + myTime.ToString(CultureInfo.InvariantCulture) + "Name of clip: " + myAnimatorClip[0].clip.name);
        }

        private void run_cmd(string output, Text errorText)
        {
            eventHandled = new TaskCompletionSource<bool>();
            process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            //process.StartInfo.FileName = "C:/Users/Branislav/PycharmProjects/exe/dist/TextToSpeech.exe";
            //process.StartInfo.FileName = Environment.CurrentDirectory + @"\Assets\StreamingAssets" + @"\TextToSpeech.exe";
            process.StartInfo.FileName = getGender();
            //process.StartInfo.FileName = Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeechMale.exe");
            output = output.Insert(0, settings.volume + " ");
            process.StartInfo.Arguments = output;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(myProcess_Exited);
            //Debug.LogWarning(Environment.CurrentDirectory);
            try
            {
                setTalkAnimation();
                process.Start();
                //process = Process.Start(process.StartInfo);
                //Process.Start(process.StartInfo);
            }
            catch (Exception e)
            {
                errorText.enabled = true;
                errorText.text = e.Message;
                throw;
            }
        }

        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(setMoodAnimation);
            Debug.LogWarningFormat(
                $"Exit time    : {process.ExitTime}\n" +
                $"Exit code    : {process.ExitCode}\n" +
                $"Elapsed time : {Math.Round((process.ExitTime - process.StartTime).TotalMilliseconds)}");
            eventHandled.TrySetResult(true);
        }

        private string getGender()
        {
            switch (menuInteraction.newGame.gender)
            {
                case 0:
                    return Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeechMale.exe");
                case 1:
                    return Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeechFemale.exe");
                default:
                    return null;
            }
        }


        private void calculateMood(Text moodText)
        {
            string moodOfSentence = myuser.getMood();
            if (moodOfSentence == "")
            {
                moodOfSentence = "0";
            }

            int moodTemp = this.mood;
            moodTemp += int.Parse(moodOfSentence);
            if (moodTemp > 0 && moodTemp <= 100)
            {
                this.mood = moodTemp;
            }
            moodText.text = this.mood.ToString();
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

