using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TestMail.WebDriver
{
    public static class GetConfigData
    {
        private static String ConfigSrc = @"C:\Config.xml";       //Путь к файлу-источнику XML                                                                                             //Переменная для хранения адреса файла XML
        private static String[] ConfigParams = { "Browser", "MailUrl", "Login", "Domain", "Password", "Logging", "MessageText", "MailTo", "Message" };                        //Массив со значениями параметров

        public static Hashtable ConfigValues = ReadFromXML(ConfigSrc);      //Массив Hashtable c прочитанными значениями

        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable


        /// <summary>
        /// Метод читает файл XML из источника и проверяет присутствуют ли в нем все параметры из переменной ConfigParams
        /// </summary>
        /// <param name="configSrc">Источник XML</param>
        /// <returns>Возвращает массив Hashtable</returns>
        private static Hashtable ReadFromXML(String configSrc)
        {
            Hashtable ht = new Hashtable();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(ConfigSrc);
            logger.Info("XML file loaded");

            XmlNodeList xnList = xml.SelectNodes("/Config");
            XmlNode xn = xnList[0];

            logger.Info("Parsing xml...");
            for (int i = 0; i < ConfigParams.Length; i++)
            {
                ht.Add(ConfigParams[i], xn[ConfigParams[i]].InnerText);
            }

            CheckHashTable(ht);
            logger.Info("XML file parsed successfully!");

            return ht;
        }

        /// <summary>
        /// Метод проверяет наличие в массиве Hashtable всех необходимых значений
        /// </summary>
        /// <param name="ht"></param>
        private static void CheckHashTable (Hashtable ht)
        {
            bool tableChecked = false;

            logger.Info("Checking parametrs exist...");

            for (int i = 0; i < ConfigParams.Length; i++)
            {
                tableChecked = CheckParam(ht, ConfigParams[i]);
            }

            if (!tableChecked)
            {

            }
        }

        /// <summary>
        /// Вспомогательный метод проверяющий наличие одного значения в массиве Hashtable
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private static bool CheckParam (Hashtable ht, String param)
        {
            return ((ht.ContainsKey(param)) ? true : false);
        }
    }
}