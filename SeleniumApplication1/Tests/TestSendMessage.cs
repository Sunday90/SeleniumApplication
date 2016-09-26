using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMail.WebDriver
{
    public static class TestSendMessage
    {
        private static LoginPage lp = new LoginPage();                           //Переменная - страница авторизации
        private static SendMessagePage smp = new SendMessagePage();              //Переменная - страница отправки письма
        private static MessagePage mp = new MessagePage();                       //Переменная - страница отображения письма

        private static Logger logger = LogManager.GetCurrentClassLogger();       //Nlog variable

        /// <summary>
        /// Проверяем прием сообщения
        /// </summary>
        /// <param name="args"></param>
        public static void Main (string[] args)
        {
            lp.Open();
            lp.Logon();
            smp.Send();
            mp.CheckInboxMailMessageExist();
            logger.Warn("Test done!");
        }
    }
}
