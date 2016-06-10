using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace SEC2_eindopdracht_encryptie
{
    public partial class Default : System.Web.UI.Page
    {
        MySqlDataAdapter sda;
        MySqlConnection con;
        string connString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connString = "Data Source=databases.aii.avans.nl;port=3306;Initial Catalog=wkranenb_db;User Id=wkranenb;password=mb8i6ywa";
            con = new MySqlConnection(connString);
            if (!IsPostBack)
            {
                using (con)
                {
                    string sql = "select * from sec2_tbl_encryptie";
                    sda = new MySqlDataAdapter(sql, con);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        drp_EncryptedMessages.DataSource = dt;
                        drp_EncryptedMessages.DataTextField = "text_encrypted";
                        drp_EncryptedMessages.DataValueField = "id";
                        drp_EncryptedMessages.DataBind();
                    }
                    
                }
            }

        }

        protected void btn_Verstuur_Click(object sender, EventArgs e)
        {
            string naam, wachtwoord, geheimetekst, encryptedtekst;
            naam = txt_Naam.Text;
            wachtwoord = txt_Wachtwoord.Text;
            geheimetekst = txt_Tekst.Text;

            if (!naam.Equals("") && !wachtwoord.Equals("") && !geheimetekst.Equals(""))
            {
                encryptedtekst = Encrypt(geheimetekst, wachtwoord);
                lbl_Encrypted.Text = encryptedtekst;
                drp_EncryptedMessages.Items.Add(encryptedtekst);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO sec2_tbl_encryptie(`naam`, `text_encrypted`) values ('" + naam + "','" + encryptedtekst + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {

            }
        }

        protected void btn_Lees_Click(object sender, EventArgs e)
        {
            string wachtwoord = txt_Wachtwoord.Text;
            if (!wachtwoord.Equals(""))
            {
                lbl_Decrypted.Text = Decrypt(drp_EncryptedMessages.SelectedItem.Text, wachtwoord);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "wachtwoord!", "alert('" + "Geen Wachtwoord in gevuld" + "');", true);
            }
            
        }

        private string Encrypt(string text, string wachtwoord)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            using (Aes encryptor = Aes.Create())
            {
                // New byte[] { 0x49.....} = de slat
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(wachtwoord, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                        cs.Close();
                    }
                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return text;
        }

        private string Decrypt(string text, string wachtwoord)
        {
            try
            {
                byte[] textBytes = Convert.FromBase64String(text);
                using (Aes encryptor = Aes.Create())
                {
                    // New byte[] { 0x49.....} = de slat
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(wachtwoord, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(textBytes, 0, textBytes.Length);
                            cs.Close();
                        }
                        text = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return text;
            }
            catch (Exception e)
            {
                return "Het wachtwoord is incorrect";
            }
        }
    }
}