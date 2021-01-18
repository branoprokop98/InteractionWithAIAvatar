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
            initListOfTopics();
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

        private void initListOfTopics()
        {
            int k = 0;
            double numOfItemsInLayer = Math.Ceiling(tempListOfTopics.Count / 9d);
            for (int i = 0; i < numOfItemsInLayer; i++)
            {
                listOfTopics.Add(new List<Topics>());
                for (int j = 0; j < 10; j++)
                {
                    try
                    {
                        listOfTopics[i].Add(new Topics() {TopicName = tempListOfTopics[k].TopicName, PathToTopic = tempListOfTopics[k].PathToTopic});
                        k++;
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }
            }
        }
        public List<List<Topics>> ListOfTopics => listOfTopics;
    }
}
