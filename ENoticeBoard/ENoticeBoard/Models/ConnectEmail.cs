//using OpenPop.Pop3;
//using System;
//using System.Collections;
//using System.IO;

//namespace ENoticeBoard.Models
//{
//    public class ConnectEmail
//    {
//        public string Connect_Email()
//        {
//            string Res = "";
//            try
//            {
//                Pop3Client email = new Pop3Client("login", "password", "server");
//                email.OpenInbox();

//                while (email.NextEmail())
//                {
//                    if (email.IsMultipart)
//                    {
//                        IEnumerator enumerator = email.MultipartEnumerator;
//                        while (enumerator.MoveNext())
//                        {
//                            Pop3Component multipart = (Pop3Component)
//                                enumerator.Current;
//                            if (multipart.IsBody)
//                            {
//                                //Console.WriteLine("Multipart body:" + multipart.Name);
//                            }
//                            else
//                            {
//                                //Console.WriteLine("Attachment name=" +    multipart.Name); // ... etc
//                                byte[] filebytes = Convert.FromBase64String(multipart.Data);

//                                //Search FileName
//                                int Begin = multipart.ContentType.IndexOf("name=");
//                                string leFileNale = multipart.ContentType.Substring(Begin + 5, 12);

//                                FileStream LeFS = new FileStream(filePath + "\\" + leFileNale, FileMode.Create, FileAccess.Write, FileShare.None);
//                                LeFS.Write(filebytes, 0, filebytes.Length);
//                                LeFS.Close();
//                            }
//                        }
//                    }
//                }
//                email.CloseConnection();
//            }
//            catch (Pop3LoginException)
//            {
//                Res = "Vous semblez avoir un problème de connexion!";
//            }
//            return Res;
//        }

//    }
//}