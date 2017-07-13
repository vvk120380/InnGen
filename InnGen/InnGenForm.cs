using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InnGen
{
    public partial class InnGenForm : Form
    {

        KeyboardHook hook = new KeyboardHook();

        public InnGenForm()
        {
            InitializeComponent();


           hook.KeyPressed +=
           new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(InnGen.ModifierKeys.Control | InnGen.ModifierKeys.Alt,
                Keys.I);
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            tbResult.Text = getInn();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            tbResult.Text = getInn();
        }

        private String getInn()
        {
            UInt32[] x = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };

            UInt32 uBase = (UInt32)(new Random().Next(960000000, 969999999));
            String strRes = uBase.ToString();

            UInt32 Result = 0;
            for (int i = 8; i >= 0; i--)
            {
                Result += (uBase % 10) * x[i];
                uBase /= 10;
            }

            strRes += ((Result % 11) % 10).ToString();
            Clipboard.SetText(strRes);

            return strRes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMail("smtp.yandex.ru", "mymail@gmail.com", "5IkENZW92v5dogP0GBcrP4Uh", "invite_edo@moedelo.org", "Тема письма", "Тело письма", null);
        }

        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            using (MailMessage mm = new MailMessage("konotopsky@moedelo.org", "invite_edo@moedelo.org"))
            {
                mm.Subject = "Гномин_тестер_ЭП, 9637004553";
                mm.Body = "Добрый день.\n\nНастройка завершена.\n\nРоуминг 13\n7714698320\n123456789\n2BM-6662089308-666201001-201412180949241494095";
                mm.IsBodyHtml = false;
                using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 25))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential("konotopsky@moedelo.org", "PCNKB34520");
                    sc.Send(mm);
                }
            }
        }

        private void InnGenForm_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.ShowBalloonTip(1000, "Генератор ИНН", "При зажатии на Alt + Ctrl + I будет сгенерирован и помещен в буфер обмена ИНН для физ. лица",ToolTipIcon.Info);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void InnGenForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
