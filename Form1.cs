using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;
namespace VC
{
    public partial class Form1 : Form
    {
        //global vars

        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            clist.Add(new String[] { "hello", "stop", "Open Youtube", "close","sussy balls" });



        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            Grammar gr = new Grammar(new GrammarBuilder(clist));

            try
            {

                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }

        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
               
                case "stop":
                    ss.SpeakAsync("stopping voice recognition");
                    break;

                case "Open Youtube":
                    System.Diagnostics.Process.Start(@"C:\Users\rkrae\AppData\Local\Vivaldi\Application\vivaldi.exe", "https://www.youtube.com");
                    ss.SpeakAsync("Opening Youtube");
                    break;

                

            }
          textBox1.Text += e.Result.Text.ToString()+ Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            button2.Enabled = false;
            button1.Enabled = true;
        }
    }

}
