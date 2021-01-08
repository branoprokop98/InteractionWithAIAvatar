using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace AIML
{
    public class LoadTopics
    {
        private XDocument xmlDocument;
        private Topics topics;
        private List<Topics> tempListOfTopics;
        private List<List<Topics>> listOfTopics;

        public LoadTopics()
        {
            loadXmlFile();
            topics = new Topics();
            tempListOfTopics = new List<Topics>();
            listOfTopics = new List<List<Topics>>();
            readTopic();
            //instantiateListOfTopics();
        }

        private void readTopic()
        {
            IEnumerable<XElement> topics = from topic in xmlDocument.Descendants("content") select topic;
            foreach (XElement element in topics)
            {
                IEnumerable<XElement> topicChildNodes = element.Elements();
                foreach (XElement nodes in topicChildNodes)
                {
                    switch (nodes.Name.ToString())
                    {
                        case "title":
                            this.topics.TopicName = nodes.Value;
                            break;
                        case "link":
                            this.topics.PathToTopic = nodes.Value;
                            break;
                        default:
                            throw new XmlException();
                    }
                }

                tempListOfTopics.Add(new Topics()
                    {TopicName = this.topics.TopicName, PathToTopic = this.topics.PathToTopic});
            }
        }

        private void loadXmlFile()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Topics.xml");
            this.xmlDocument = XDocument.Load(path);
            if (xmlDocument == null)
            {
                throw new FileLoadException();
            }
        }

        // private void instantiateListOfTopics()
        // {
        //     int k = 0;
        //     double rows = this.tempListOfTopics.Count / 9f;
        //     Double numOfRows = Math.Ceiling(rows);
        //     List<Topics> temp = new List<Topics>();
        //     for (int j = 0; j < numOfRows; j++)
        //     {
        //         for (int i = 0; i < 9; i++)
        //         {
        //             try
        //             {
        //                 temp.Add(tempListOfTopics[k]);
        //                 k++;
        //             }
        //             catch
        //             {
        //                 break;
        //             }
        //         }
        //
        //         this.listOfTopics.Add(new Topics(){Topic = temp});
        //         temp.Clear();
        //     }
        // }
    }
}
