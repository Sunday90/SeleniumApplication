using NLog;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMail.WebDriver
{
    public class SendMessagePage : BasePage
    {
        private static Hashtable ConfigData = GetConfigData.ConfigValues;                                       //Получаем массив с параметрами теста

        private static WebEditBox ToField = new WebEditBox("data-original-name", "To");                         //Поле "Кому"
        private static WebEditBox SubjectField = new WebEditBox(By.Id("compose_413_ab_compose_subj"));          //Поле темы письма
        private static WebEditBox TextMessageField = new WebEditBox(By.Id("tinymce"));                          //Поле ввода текста сообщения
        private static WebControl SendButton = new WebControl("data-name", "send");                             //Кнопка "Отправить"

        

        private static string ToText = ConfigData["To"].ToString();                                             //Получатель письма
        private static string SubjectText = ConfigData["Subject"].ToString();                                   //Тема письма
        private static string MessageText = ConfigData["Message"].ToString();                                   //Текст сообщения

        private static Logger logger = LogManager.GetCurrentClassLogger();                                      //Nlog variable 

        /// <summary>
        /// Отправить письмо
        /// </summary>
        public void Send()
        {
            ToField.SendKeys(ToText);
            SubjectField.SendKeys(SubjectText);
            TextMessageField.SendKeys(MessageText);
            SendButton.Click();
            logger.Info("Message sent");
        }
    }
}
