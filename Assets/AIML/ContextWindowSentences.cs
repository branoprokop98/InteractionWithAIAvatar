using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using AIMLbot;

namespace AIML
{
    public class ContextWindowSentences : ContextLayer
    {
        private Bot bot;
        private XDocument aimlFile;
        private AIMLStructure aimlStructure;
        private List<AIMLStructure> sentences;
        private List<List<AIMLStructure>> listOfAimlSentences;


        public ContextWindowSentences()
        {
            sentences = new List<AIMLStructure>();
            listOfAimlSentences = new List<List<AIMLStructure>>();
            aimlStructure = new AIMLStructure();
        }

        public void listSentences(string nameOfFile)
        {
            sentences.Clear();
            bot = new Bot();
            string path = bot.PathToAIML + "\\" + "aiml";
            string[] files = Directory.GetFiles(path, nameOfFile);

            aimlFile = XDocument.Load(files[0]);

            IEnumerable<XElement> aimlNodes = from aiml in aimlFile.Descendants("category") select aiml;

            foreach (XElement aimlNode in aimlNodes)
            {
                IEnumerable<XElement> childNodes = aimlNode.Elements();
                foreach (XElement childNode in childNodes)
                {
                    switch (childNode.Name.ToString())
                    {
                        case "pattern":
                            string sentence = childNode.Value.ToLower();
                            aimlStructure.Pattern = sentence; //char.ToUpper(sentence[0]) + sentence.Remove(0, 1);
                            break;
                        default:
                            continue;
                            ;
                    }
                }

                sentences.Add(new AIMLStructure() {Pattern = aimlStructure.Pattern});
            }

            addTo2DList();
        }

        public void addTo2DList()
        {
            int k = 0;
            listOfAimlSentences.Clear();
            double numOfItemsInLayer = Math.Ceiling(sentences.Count / 10d);
            for (int i = 0; i < numOfItemsInLayer; i++)
            {
                listOfAimlSentences.Add(new List<AIMLStructure>());
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        listOfAimlSentences[i].Add(new AIMLStructure() {Pattern = sentences[k].Pattern});
                        k++;
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            }
        }

        public void getNextLayer()
        {
            throw new System.NotImplementedException();
        }

        public void getPrevLayer()
        {
            throw new System.NotImplementedException();
        }

        public List<List<AIMLStructure>> ListOfAimlSentences
        {
            get => listOfAimlSentences;
            set => listOfAimlSentences = value;
        }
    }
}
